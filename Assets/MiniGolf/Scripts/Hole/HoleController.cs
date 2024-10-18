using System;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public Action OnBallInsideEvent;
    [SerializeField] private string _ballTag;
    [SerializeField] private ColliderEventsProvider _ballColliderEventProvider;

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
        OnBallInsideEvent?.Invoke();
    }
}
