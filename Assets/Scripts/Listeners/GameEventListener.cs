using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public List<GameEvent> gameEvents;
    [SerializeField] protected UnityEvent response;

    private void OnEnable() 
    {
        foreach (GameEvent ge in gameEvents)
        {
            ge.Register(this);
        }
    }

    private void OnDisable() 
    {
        foreach (GameEvent ge in gameEvents)
        {
            ge.DeRegister(this);
        }
    }

    /// <summary>
    /// Calls the listeners "responce" event whenever any of the subscribed events are raised, source is a refrence to the event that was raised
    /// </summary>
    /// <param name="source"></param>
    public virtual void OnEventRaised(GameEvent source) 
    {
        response.Invoke();
    }
}