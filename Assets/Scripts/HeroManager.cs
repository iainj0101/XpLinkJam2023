using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : Singleton<HeroManager>
{
    public enum Bug { Ant, Hopper, Caterpillar }
    public Bug CurrentBug;
    public enum Direction {  North, East, South, West}
    public Direction CurrentDirection;

    private List<Action> GetPossibleActions()
    {
        List<Action> actions = new List<Action>();
        //foreach direction is it possible for set bug to do an action?

        return actions;
    }
    private void TryStep(Direction direction)
    {

    }

    private void Step(Direction direction)
    {
        //get tile type in direstion and 
    }
}

public class Action
{
    public enum ActionType { Move, Turn, Cut}
    public ActionType SetAction;
}
