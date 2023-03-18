using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public enum Direction { North, East, South, West }

public class HeroManager : GameEventListener
{
    public StepEvent StepEvent;
    public Tile CurrentTile;
    public GameObject NorthButton;
    public GameObject EastButton;
    public GameObject SouthButton;
    public GameObject WestButton;
    public enum Bug { Ant, Hopper, Caterpillar }
    public Bug CurrentBug = Bug.Hopper;
    public List<Bug> Bugs = new List<Bug>();
    public List<Action> PossibleActions = new List<Action>();
    
    public Direction CurrentDirection;
    public GameObject BugInstance;
    [SerializeField] GameObject AntPrefab;
    [SerializeField] GameObject HopperPrefab;
    [SerializeField] GameObject CaterpillarPrefab;

    private void Start()
    {
        PossibleActions = GetPossibleActions();
        UpdateButtonUI();
    }
    public override void OnEventRaised(GameEvent source)
    {
        PossibleActions = GetPossibleActions();
        UpdateButtonUI();
    }

    private void UpdateButtonUI()
    {
        NorthButton.SetActive(false);
        EastButton.SetActive(false);
        SouthButton.SetActive(false);
        WestButton.SetActive(false);
        foreach (Action a in PossibleActions)
        {
            switch (a.Direction)
            {
                case (Direction.North):
                    NorthButton.SetActive(true);
                    break;
                case (Direction.East):
                    EastButton.SetActive(true);
                    break;
                case (Direction.South):
                    SouthButton.SetActive(true);
                    break;
                case (Direction.West):
                    WestButton.SetActive(true);
                    break;
            }
        }
    }

    public void TryStep(int direction)
    {
        foreach (Action a in PossibleActions)
        {
            if (a.Direction == (Direction)direction)
            {
                Step(a);
                return;
            }
        }
        //nope try again silly boy
    }
    private void Update()
    { 
        int horizontalInput = (int)Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
        {
            if (horizontalInput > 0)
            {
                TryStep((int)Direction.East);
            }
            else { TryStep((int)Direction.West); }
        }
        int VertInput = (int)Input.GetAxisRaw("Vertical");

        if (VertInput != 0)
        {
            if (VertInput > 0)
            {
                TryStep((int)Direction.North);
            }
            else { TryStep((int)Direction.South); }
        }
        
    }

    public void SwapBug(Bug bug)
    {
        switch (bug)
        {
            case Bug.Ant:
                BugInstance = Instantiate(AntPrefab, this.transform);
                break;
            case Bug.Hopper:
                break;
            case Bug.Caterpillar:
                break;
        }
    }

    private List<Action> GetPossibleActions()
    {
        List<Action> actions = new List<Action>();

        switch (CurrentBug)
        {
            case (Bug.Ant):
                foreach (KeyValuePair<Direction, Tile> key in CurrentTile.Tiles)
                {
                    if (key.Key == CurrentDirection)
                    {
                        if (key.Value.CanMoveTo)
                        {
                            Action a = new Action();
                            a.SetAction = Action.ActionType.Move;
                            a.Direction = key.Key;
                            a.InteractionTile = key.Value;
                            actions.Add(a);
                        }
                        else
                        {
                            //die
                        }
                    }
                }
                break;
            case (Bug.Hopper):
                Direction directionRight = CurrentDirection;
                Direction directionLeft = CurrentDirection;
                switch (CurrentDirection)
                {
                    case Direction.North:
                        directionRight = Direction.East;
                        directionLeft = Direction.West;
                        break;
                    case Direction.East:
                        directionRight = Direction.South;
                        directionLeft = Direction.North;
                        break;
                    case Direction.South:
                        directionRight = Direction.West;
                        directionLeft = Direction.East;
                        break;
                    case Direction.West:
                        directionRight = Direction.North;
                        directionLeft = Direction.South;
                        break;
                }
                Action right = new Action();
                right.SetAction = Action.ActionType.Turn;
                right.Direction = directionRight;
                right.InteractionTile = CurrentTile;
                right.Turn = Action.TurnDirection.Right;
                actions.Add(right);

                Action Left = new Action();
                Left.SetAction = Action.ActionType.Turn;
                Left.Direction = directionLeft;
                Left.InteractionTile = CurrentTile;
                Left.Turn = Action.TurnDirection.Left;
                actions.Add(Left);
                break;
            case (Bug.Caterpillar):
                break;
        }

        //next bug

        return actions;
    }

    bool inMotion = false;
    IEnumerator Move(Action action)
    {
        BugInstance.GetComponent<BugBehaviour>().MoveFoward.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_DestinationTransform>().Destination = action.InteractionTile?.transform;
        inMotion = true;
        yield return BugInstance.GetComponent<BugBehaviour>().MoveFoward.PlayFeedbacksCoroutine(this.transform.position, 1, false);

        inMotion = false;
        CurrentTile = action.InteractionTile;

        StepEvent.CurrentTile = CurrentTile;
        StepEvent.Raise();

        yield break;
    }

    IEnumerator Turn(Action action)
    {
        if (action.Turn == Action.TurnDirection.Right)
        {
            BugInstance.GetComponent<BugBehaviour>().TurnRight.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_DestinationTransform>().Destination = action.InteractionTile?.transform;
            inMotion = true;
            yield return BugInstance.GetComponent<BugBehaviour>().TurnRight.PlayFeedbacksCoroutine(this.transform.position, 1, false);
        }
        else
        {
            BugInstance.GetComponent<BugBehaviour>().TurnLeft.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_DestinationTransform>().Destination = action.InteractionTile?.transform;
            inMotion = true;
            yield return BugInstance.GetComponent<BugBehaviour>().TurnLeft.PlayFeedbacksCoroutine(this.transform.position, 1, false);
        }
        inMotion = false;
        CurrentDirection = action.Direction;
        CurrentTile = action.InteractionTile;

        StepEvent.CurrentTile = CurrentTile;
        StepEvent.Raise();

        yield break;
    }

    private void Step(Action action)
    {
        //get tile type in direstion and 
        switch (action.SetAction)
        {
            case Action.ActionType.Move:
                if (!inMotion)
                    StartCoroutine(Move(action));
                break;
            case Action.ActionType.Turn:
                if (!inMotion)
                    StartCoroutine(Turn(action));
                break;
            case Action.ActionType.Cut:
                break;
        }
    }
}

public class Action
{
    public enum ActionType { Move, Turn, MoveAndTurn, Cut }
    public ActionType SetAction;
    public Direction Direction;
    public Tile InteractionTile;
    public enum TurnDirection { Right, Left}
    public TurnDirection Turn;
}
