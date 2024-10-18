using UnityEngine;

public class BoosterController : MonoBehaviour
{
    [SerializeField] private string _ballTag;
    [SerializeField] private ColliderEventsProvider _boosterTrigger;
    [SerializeField] private Transform _directionTransform;
    [SerializeField] private float _boostPower;

    private void Awake()
    {
        _boosterTrigger.OnTriggerEnterEvent += OnBoosterTriggerEnter;
    }

    private void OnBoosterTriggerEnter(Collider other)
    {
        if ( other.tag == _ballTag )
        {
            var ball = other.GetComponentInParent<BallController>();
            ball.SetBallDirection(_directionTransform.forward.normalized, _boostPower);
        }
    }
}
