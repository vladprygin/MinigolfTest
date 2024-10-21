using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _raycastMask;
    [SerializeField] private float _maxPower;
    [SerializeField] private float _sensitivityDelta;
    [SerializeField] private string _ballTag;
    [SerializeField] private float _powerMultiplier;
    [SerializeField] private PowerIndicatorController _powerIndicator;
    
    private bool _isPreparingShot;
    private Vector3 _originPoint;
    private BallController _currentBall;

    private void Update()
    {
        var currentState = Hub.GameStateManager.GetCurrentGameState();
        if ( currentState!= GameStateEnum.Game)
        {
            return;
        }
        
        CheckPlayerInput();
        UpdatePowerIndicator();
    }

    private void CheckPlayerInput()
    {
        if ( Input.GetMouseButtonDown(0) )
        {
            CheckBallClick();
        }
        
        if ( Input.GetMouseButtonUp(0) )
        {
            if ( _isPreparingShot && _currentBall != null )
            {
                MakeShot();
            }
        }
    }

    private void UpdatePowerIndicator()
    {
        if ( !_isPreparingShot )
        {
            return;
        }
        
        RaycastFromMousePosition(out RaycastHit hit);
        var updatedHitPoint = new Vector3(hit.point.x, _originPoint.y, hit.point.z);
        var powerVectors = new Vector3[2] { _currentBall.transform.position, updatedHitPoint};
        _powerIndicator.UpdateLineRendererPoints(powerVectors);
    }

    private void MakeShot()
    {
        if ( RaycastFromMousePosition(out RaycastHit hit) )
        {
            if ( hit.transform.tag != _ballTag && Vector3.Distance(_originPoint, hit.point) > _sensitivityDelta )
            {
                var updatedHitPoint = new Vector3(hit.point.x, _originPoint.y, hit.point.z);
                var dir = _originPoint - updatedHitPoint;
                
                var power = dir.magnitude * _powerMultiplier;
                if ( power > _maxPower )
                {
                    power = _maxPower;
                }
                _currentBall.SetBallDirection(dir.normalized, power);
                _isPreparingShot = false;
                _currentBall = null;
                Hub.BallManager.BallHitConfirmed();
                _powerIndicator.HideLineRenderer();
            } 
        }
    }

    private bool RaycastFromMousePosition(out RaycastHit hit)
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        
        if ( Physics.Raycast(ray, out hit, _raycastMask) )
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 5f );
            return true;
        }

        return false;
    }
    

    private bool CheckBallClick()
    {
        if ( RaycastFromMousePosition(out RaycastHit hit) )
        {
            var ball = hit.transform.GetComponent<BallController>();
            if ( ball == null )
            {
                return false;
            }

            if ( !ball.IsIdle )
            {
                return false;
            }

            _isPreparingShot = true;
            _currentBall = ball;
            _originPoint = ball.transform.position;
            _powerIndicator.ShowLineRenderer();
            return true;
        }
        
        return false;
    }
    
}
