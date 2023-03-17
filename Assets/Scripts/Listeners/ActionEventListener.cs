using UnityEngine;
using UnityEngine.Events;

public class ActionEventListener : GameEventListener
{
    [SerializeField] private UnityEvent undoResponse;
    public virtual void OnEventUndone(GameEvent source)
    {
        undoResponse.Invoke();
    }
}