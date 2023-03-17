using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 52)]
public class GameEvent : ScriptableObject
{
    protected HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();
    public virtual void Raise()
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.OnEventRaised(this);
        }
    }

    public void Register(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void DeRegister(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
