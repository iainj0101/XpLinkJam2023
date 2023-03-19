using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains;
using MoreMountains.Feedbacks;

[Serializable] public enum Direction { North, East, South, West }

public class HeroManager : GameEventListener
{
    public StepEvent StepEvent;
    public Tile CurrentTile;
    public GameObject NorthButton;
    public GameObject EastButton;
    public GameObject SouthButton;
    public GameObject WestButton;
    public GameObject NextBugButton;
    public GameObject ResetButton;
    public LoadLevelManagers llm;
    public MMF_Player SwapEffect;
    public enum Bug { Ant, Hopper, Caterpillar }
    public Bug CurrentBug = Bug.Hopper;
    public List<Bug> Bugs = new List<Bug>();
    public List<Action> PossibleActions = new List<Action>();

    public Direction CurrentDirection;
    public GameObject BugInstance;
    [SerializeField] GameObject AntPrefab;
    [SerializeField] GameObject HopperPrefab;
    [SerializeField] GameObject CaterpillarPrefab;

    public void StartGame(Charm[] UsedBugs)
    {
        ResetButton.SetActive(true);
        Bugs.Clear();
        foreach (Charm b in UsedBugs)
        {
            Bugs.Add(b.BugType);
        }
        SwapBug(Bugs[0]);
        CurrentTile.GetTiles();
        PossibleActions = GetPossibleActions();
        UpdateButtonUI();
        Camera.main.GetComponent<MoreMountains.Tools.MMFollowTarget>().ChangeFollowTarget(BugInstance?.transform);
    }
    public override void OnEventRaised(GameEvent source)
    {
        CurrentTile.GetTiles();
        PossibleActions = GetPossibleActions();
        UpdateButtonUI();
        if (CurrentTile.GetComponent<EndTile>() != null)
        {
            CurrentTile.GetComponent<EndTile>().Win();
        }
    }

    public void LevelReset()
    {
        NorthButton.SetActive(false);
        EastButton.SetActive(false);
        SouthButton.SetActive(false);
        WestButton.SetActive(false);
        NextBugButton.SetActive(false);
        llm.DestroyAndGoNext(llm.CurrentLevel);
    }

    private void UpdateButtonUI()
    {
        NorthButton.SetActive(false);
        EastButton.SetActive(false);
        SouthButton.SetActive(false);
        WestButton.SetActive(false);
        NextBugButton.SetActive(false);
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
        if (Bugs.Count > 1)
        {
            NextBugButton.SetActive(true);
        }
    }

    public void TryStep(int direction)
    {
        foreach (Action a in PossibleActions)
        {
            if (a.Direction == (Direction)direction)
            {
                switch ((Direction)direction)
                {
                    case (Direction.North):
                        NorthButton.transform.GetChild(0).GetComponent<MoreMountains.Feedbacks.MMF_Player>().PlayFeedbacks();
                        break;
                    case (Direction.East):
                        EastButton.transform.GetChild(0).GetComponent<MoreMountains.Feedbacks.MMF_Player>().PlayFeedbacks();
                        break;
                    case (Direction.South):
                        SouthButton.transform.GetChild(0).GetComponent<MoreMountains.Feedbacks.MMF_Player>().PlayFeedbacks();
                        break;
                    case (Direction.West):
                        WestButton.transform.GetChild(0).GetComponent<MoreMountains.Feedbacks.MMF_Player>().PlayFeedbacks();
                        break;
                }
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

    public void NextBug(bool CallNextStep)
    {
        foreach (Bug b in Bugs)
        {
            Debug.Log(b);
        }
        if (Bugs.Count > 1)
        {
            Bugs.Remove(Bugs[0]);
            SwapBug(Bugs[0]);
            if (CallNextStep)
            {
                StepEvent.Raise();
            }
        }
        else
        {
            //die
        }
    }

    IEnumerator Swap()
    {
        //BugInstance.GetComponent<BugBehaviour>().MoveFoward.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_DestinationTransform>().Destination = action.InteractionTile?.transform;
        inMotion = true;
        yield return BugInstance.GetComponent<BugBehaviour>().MoveFoward.PlayFeedbacksCoroutine(this.transform.position, 1, false);

        inMotion = false;

        StepEvent.CurrentTile = CurrentTile;
        StepEvent.Raise();

        yield break;
    }
    public void SwapBug(Bug bug)
    {
        if (!inMotion)
            StartCoroutine(Swap());
        CurrentBug = bug;
        switch (bug)
        {
            case Bug.Ant:
                Destroy(BugInstance);
                BugInstance = Instantiate(AntPrefab, CurrentTile.transform.position, this.transform.rotation, this.transform);
                break;
            case Bug.Hopper:
                Destroy(BugInstance);
                BugInstance = Instantiate(HopperPrefab, CurrentTile.transform.position, this.transform.rotation, this.transform);
                break;
            case Bug.Caterpillar:
                lastTurn = Action.TurnDirection.Dont;
                lastLastTurn = Action.TurnDirection.Dont;
                SecondTurn = true;
                fix2 = true;
                Destroy(BugInstance);
                BugInstance = Instantiate(CaterpillarPrefab, CurrentTile.transform.position, this.transform.rotation, this.transform);
                break;
        }

        switch (CurrentDirection)
        {
            case Direction.North:
                BugInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.East:
                BugInstance.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Direction.South:
                BugInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Direction.West:
                BugInstance.transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
        }

        Camera.main.GetComponent<MoreMountains.Tools.MMFollowTarget>().ChangeFollowTarget(BugInstance.transform);
        Camera.main.GetComponent<MoreMountains.Tools.MMFollowTarget>().Offset = new Vector3(0,5,-2);
        Camera.main.GetComponent<MoreMountains.Tools.MMFollowTarget>().FollowPositionY = false;
        Camera.main.transform.rotation = Quaternion.Euler(60,0,0);
    }

    private Direction[] GetRightLeft()
    {
        Direction directionRight = Direction.North;
        Direction directionLeft = Direction.North;
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
        Direction[] rightLeft = new Direction[2];
        rightLeft[0] = directionRight;
        rightLeft[1] = directionLeft;
        return rightLeft;
    }
    bool SecondTurn = true;
    bool fix2 = true;
    Action.TurnDirection lastTurn = Action.TurnDirection.Dont;
    Action.TurnDirection lastLastTurn = Action.TurnDirection.Dont;
    private List<Action> GetPossibleActions()
    {
        List<Action> actions = new List<Action>();
        BugBehaviour bb = BugInstance.GetComponent<BugBehaviour>();
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
                Direction[] rightLeft = GetRightLeft();
                Direction directionRight = rightLeft[0];
                Direction directionLeft = rightLeft[1];

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
                foreach (KeyValuePair<Direction, Tile> key in CurrentTile.Tiles)
                {
                    if (key.Key == CurrentDirection)
                    {
                        if (bb?.CatiPrevMove == -1 && bb.FirstMove)
                        {
                            Action firstcati = new Action();
                            firstcati.SetAction = Action.ActionType.MoveAndTurn;
                            firstcati.Facing = CurrentDirection;
                            firstcati.Direction = CurrentDirection;
                            firstcati.InteractionTile = key.Value;
                            firstcati.Turn = Action.TurnDirection.Dont;
                            firstcati.LastTurn = Action.TurnDirection.Dont;
                            firstcati.LastLastTurn = Action.TurnDirection.Dont;
                            actions.Add(firstcati);
                            SecondTurn = true;
                            bb.FirstMove = false;
                        }
                        else
                        {/*
                            bool left1 = bb?.CatiIndex == 1 && bb?.CatiPrevMove == 1;
                            bool left2 = bb?.CatiIndex == 0 && bb?.CatiPrevMove == 1;

                            bool Right1 = bb?.CatiIndex == 1 && bb?.CatiPrevMove == 0;
                            bool Right2 = bb?.CatiIndex == 0 && bb?.CatiPrevMove == 0;
                            */
                            Direction[] rightLeft3 = GetRightLeft();
                            Direction directionRight3 = rightLeft3[0];
                            Direction directionLeft3 = rightLeft3[1];

                            if (lastTurn != lastLastTurn && lastLastTurn == Action.TurnDirection.Dont)
                            {
                                if (lastTurn == Action.TurnDirection.Right)
                                {
                                    Action Left3 = new Action();
                                    Left3.SetAction = Action.ActionType.MoveAndTurn;
                                    Left3.Direction = CurrentDirection;
                                    Left3.Facing = directionLeft3;
                                    Left3.InteractionTile = key.Value;
                                    Left3.Turn = Action.TurnDirection.Left;
                                    Left3.LastLastTurn = Action.TurnDirection.Left;
                                    Left3.LastTurn = Action.TurnDirection.Left;
                                    actions.Add(Left3);
                                }
                                else if (lastTurn == Action.TurnDirection.Left)
                                {
                                    Action Right3 = new Action();
                                    Right3.SetAction = Action.ActionType.MoveAndTurn;
                                    Right3.Direction = CurrentDirection;
                                    Right3.Facing = directionRight3;
                                    Right3.InteractionTile = key.Value;
                                    Right3.Turn = Action.TurnDirection.Right;
                                    Right3.LastLastTurn = Action.TurnDirection.Right;
                                    Right3.LastTurn = Action.TurnDirection.Right;
                                    actions.Add(Right3);
                                }
                            }
                            if (lastTurn == lastLastTurn)
                            {
                                if (lastLastTurn == Action.TurnDirection.Right)
                                {
                                    Action Left3 = new Action();
                                    Left3.SetAction = Action.ActionType.MoveAndTurn;
                                    Left3.Direction = CurrentDirection;
                                    Left3.Facing = directionLeft3;
                                    Left3.InteractionTile = key.Value;
                                    Left3.Turn = Action.TurnDirection.Left;
                                    Left3.LastLastTurn = lastLastTurn;
                                    Left3.LastTurn = lastTurn;
                                    actions.Add(Left3);
                                }
                                else if (lastLastTurn == Action.TurnDirection.Left)
                                {
                                    Action Right3 = new Action();
                                    Right3.SetAction = Action.ActionType.MoveAndTurn;
                                    Right3.Direction = CurrentDirection;
                                    Right3.Facing = directionRight3;
                                    Right3.InteractionTile = key.Value;
                                    Right3.Turn = Action.TurnDirection.Right;
                                    Right3.LastLastTurn = lastLastTurn;
                                    Right3.LastTurn = lastTurn;
                                    actions.Add(Right3);
                                }
                            }
                            else if (lastTurn == Action.TurnDirection.Left)
                            {
                                Action Left3 = new Action();
                                Left3.SetAction = Action.ActionType.MoveAndTurn;
                                Left3.Direction = CurrentDirection;
                                Left3.Facing = directionLeft3;
                                Left3.InteractionTile = key.Value;
                                Left3.Turn = Action.TurnDirection.Left;
                                Left3.LastLastTurn = lastLastTurn;
                                Left3.LastTurn = lastTurn;
                                actions.Add(Left3);
                            }
                            else if (lastTurn == Action.TurnDirection.Right)
                            {
                                Action Right3 = new Action();
                                Right3.SetAction = Action.ActionType.MoveAndTurn;
                                Right3.Direction = CurrentDirection;
                                Right3.Facing = directionRight3;
                                Right3.InteractionTile = key.Value;
                                Right3.Turn = Action.TurnDirection.Right;
                                Right3.LastTurn = (Action.TurnDirection)bb.CatiFirstMove;
                                Right3.LastLastTurn = lastLastTurn;
                                Right3.LastTurn = lastTurn;
                                actions.Add(Right3);
                            }



                        }
                    }
                    else if (!bb.FirstMove && SecondTurn)
                    {
                        Direction[] rightLeft2 = GetRightLeft();
                        Direction directionRight2 = rightLeft2[0];
                        Direction directionLeft2 = rightLeft2[1];

                        if (key.Key == directionRight2)
                        {
                            Action right2 = new Action();
                            right2.SetAction = Action.ActionType.MoveAndTurn;
                            right2.Facing = CurrentDirection;
                            right2.Direction = directionRight2;
                            right2.InteractionTile = key.Value;
                            right2.Turn = Action.TurnDirection.Right;
                            right2.LastTurn = Action.TurnDirection.Dont;
                            actions.Add(right2);

                        }

                        if (key.Key == directionLeft2)
                        {
                            Action Left2 = new Action();
                            Left2.SetAction = Action.ActionType.MoveAndTurn;
                            Left2.Facing = CurrentDirection;
                            Left2.Direction = directionLeft2;
                            Left2.InteractionTile = key.Value;
                            Left2.Turn = Action.TurnDirection.Left;
                            Left2.LastTurn = Action.TurnDirection.Dont;
                            actions.Add(Left2);

                        }
                    }

                }//end loop

                if (bb != null)
                {
                    bb.PreviousDirection = CurrentDirection;
                }

                if (!fix2) SecondTurn = false;
                if (fix2) fix2 = false;
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

        NextBug(false);

        StepEvent.CurrentTile = CurrentTile;
        StepEvent.Raise();


        yield break;
    }

    IEnumerator MoveAndTurn(Action action)
    {
        BugInstance.GetComponent<BugBehaviour>().MoveFoward.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_DestinationTransform>().Destination = action.InteractionTile?.transform;
        inMotion = true;
        yield return BugInstance.GetComponent<BugBehaviour>().MoveFoward.PlayFeedbacksCoroutine(this.transform.position, 1, false);

        if (lastTurn == Action.TurnDirection.Dont)
        {

        }

        if (action.Turn != Action.TurnDirection.Dont)
        {
            if (action.Facing == Direction.North)
            {
                inMotion = true;
                yield return BugInstance.GetComponent<BugBehaviour>().CatiNorth.PlayFeedbacksCoroutine(this.transform.position, 1, false);
            }
            else if (action.Facing == Direction.East)
            {
                inMotion = true;
                yield return BugInstance.GetComponent<BugBehaviour>().CatiEast.PlayFeedbacksCoroutine(this.transform.position, 1, false);
            }
            else if (action.Facing == Direction.South)
            {
                inMotion = true;
                yield return BugInstance.GetComponent<BugBehaviour>().CatiSouth.PlayFeedbacksCoroutine(this.transform.position, 1, false);
            }
            else if (action.Facing == Direction.West)
            {
                inMotion = true;
                yield return BugInstance.GetComponent<BugBehaviour>().CatiWest.PlayFeedbacksCoroutine(this.transform.position, 1, false);
            }
        }


        lastLastTurn = action.LastTurn;
        lastTurn = action.Turn;
        inMotion = false;
        CurrentDirection = action.Facing;
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
            case Action.ActionType.MoveAndTurn:
                if (!inMotion)
                    StartCoroutine(MoveAndTurn(action));
                break;
        }
    }
}

public class Action
{
    public enum ActionType { Move, Turn, MoveAndTurn, Cut }
    public ActionType SetAction;
    public Direction Direction;
    public Direction Facing;
    public Tile InteractionTile;
    public enum TurnDirection { Right, Left, Dont }
    public TurnDirection Turn;
    public TurnDirection LastTurn;
    public TurnDirection LastLastTurn;
}
