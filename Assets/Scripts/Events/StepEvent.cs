using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "More Events/StepEvent", order = 52)]
public class StepEvent : GameEvent
{
    [SerializeField] public HeroManager.Direction Direction;
}
