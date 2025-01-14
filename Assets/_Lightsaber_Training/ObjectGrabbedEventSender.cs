using Oculus.Interaction;
using UnityEngine;

public class ObjectGrabbedEventSender : MonoBehaviour
{
    public delegate void ObjectGrabbed(GameObject source);
    public event ObjectGrabbed OnObjectGrabbed;
    public delegate void ObjectMoved(GameObject source);
    public event ObjectMoved OnObjectMoved;
    public delegate void ObjectReleased(GameObject source);
    public event ObjectReleased OnObjectReleased;

    public Rigidbody _rigidbody;
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
                HandleGrab();
                break;
            case PointerEventType.Unselect:
                HandleRelease();
                break;
        }
    }

    private void HandleGrab()
    {
        OnObjectGrabbed?.Invoke(gameObject);
    }

    private void HandleRelease()
    {
        OnObjectReleased?.Invoke(gameObject);
    }
}