using System;
using UnityEngine;

public class ColliderEventsProvider : MonoBehaviour
{
    public Action<Collider> OnTriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }
}
