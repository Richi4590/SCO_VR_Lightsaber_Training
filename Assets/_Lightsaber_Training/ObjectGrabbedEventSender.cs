using Oculus.Interaction;
using System;
using UnityEngine;

public class ObjectGrabbedEventSender : MonoBehaviour
{
    public event Action<GameObject> OnObjectGrabbed;
    public event Action<GameObject> OnObjectMoved;
    public event Action<GameObject> OnObjectReleased;

    private Grabbable _grabbable;

    private void Awake()
    {
        _grabbable = GetComponent<Grabbable>();

        if (_grabbable == null)
        {
            Debug.LogError("Grabbable is missing! This script requires a Grabbable component.");
        }
    }

    private void OnEnable()
    {
        // Subscribe to Grabbable events
        _grabbable.WhenPointerEventRaised += HandlePointerEvent;
    }

    private void OnDisable()
    {
        // Unsubscribe from Grabbable events
        _grabbable.WhenPointerEventRaised -= HandlePointerEvent;
    }

    private void HandlePointerEvent(PointerEvent pointerEvent)
    {
        switch (pointerEvent.Type)
        {
            case PointerEventType.Select:
                OnObjectGrabbed?.Invoke(gameObject);
                break;
            case PointerEventType.Move:
                OnObjectMoved?.Invoke(gameObject);
                break;
            case PointerEventType.Unselect:
                OnObjectReleased?.Invoke(gameObject);
                break;
        }
    }
}