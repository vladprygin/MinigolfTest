using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rBody;
    [SerializeField] private float _stopDelta = 0.05f;
    private Vector3 _currentShotDir;
    private bool _isIdle;

    public bool IsIdle
    {
        get => _isIdle;
    }

    public void SetBallDirection(Vector3 dir, float power)
    {
        _currentShotDir = dir * power;
        Shoot();
    }

    private void Shoot()
    {
        _isIdle = false;
        _rBody.AddForce(_currentShotDir, ForceMode.Impulse);
    }

    private void Update()
    {
        CheckSpeed(_rBody);
    }

    
    //Preventing the ball from forever rolling
    private void CheckSpeed(Rigidbody rBody)
    {
        if ( rBody.angularVelocity.magnitude <= _stopDelta )
        {
            rBody.angularVelocity = Vector3.zero;
            _isIdle = true;
        }
    }
}
