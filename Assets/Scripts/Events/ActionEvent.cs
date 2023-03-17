using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action Event", menuName = "Action Event", order = 52)]
public class ActionEvent : GameEvent
{
   public virtual void Undo()
    {    
        foreach (ActionEventListener listener in listeners)
        {
            listener.OnEventUndone(this);
        }
    }
}
