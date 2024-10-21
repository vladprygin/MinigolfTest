using System;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    private Action _onBallInsideEvent;
    [SerializeField] private string _ballTag;
    [SerializeField] private ColliderEventsProvider _ballColliderEventProvider;
    
    public event Action OnBallInsideEvent
    {
        add => _onBallInsideEvent+=value;
        remove => _onBallInsideEvent -= value;
    }
    
    private void Awake()
    {
        _ballColliderEventProvider.OnTriggerEnterEvent += OnBallHitTriggerEventHandler;
    }
    
    private void OnDestroy()
    {
        _ballColliderEventProvider.OnTriggerEnterEvent -= OnBallHitTriggerEventHandler;
    }

    private void OnBallHitTriggerEventHandler(Collider other)
    {
        _onBallInsideEvent?.Invoke();
    }
}
