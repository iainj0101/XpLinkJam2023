using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Froggy : GameEventListener
{
    public int Length;
    public bool startOut = false;
    public Tile TileOn;
    public Direction MyDirection;
    public LevelManager lm;
    public enum FrogType
    {
        Green, Blue
    }
    public FrogType ThisFrogType;
    public enum FrogSteps { In, Out, TurnR, TurnL }
    public FrogSteps[] BlueFrog = new FrogSteps[4];
    public FrogSteps[] GreenFrog = new FrogSteps[2];

    public MMF_Player Outmmf;
    public MMF_Player Inmmf;
    public MMF_Player Leftmmf;
    public MMF_Player Rightmmf;

    public int index = 0;
    public int Maxindex = 0;

    private void Start()
    {
        GreenFrog[0] = FrogSteps.Out;
        GreenFrog[1] = FrogSteps.In;

        BlueFrog[0] = FrogSteps.Out;
        BlueFrog[1] = FrogSteps.TurnR;
        BlueFrog[2] = FrogSteps.Out;
        BlueFrog[2] = FrogSteps.TurnL;

        switch (ThisFrogType)
        {
            case (FrogType.Green):
                Maxindex = GreenFrog.Length;
                break;
            case (FrogType.Blue):
                Maxindex = BlueFrog.Length;
                break;
        }
        TileOn = this.transform.parent.GetComponent<Tile>();
        if (startOut)
        {
            StartCoroutine(Out());
        }
    }
    public override void OnEventRaised(GameEvent source)
    {
        switch (ThisFrogType)
        {
            case (FrogType.Green):
                switch (GreenFrog[index])
                {
                    case (FrogSteps.In):
                        StartCoroutine(In());
                        break;
                    case (FrogSteps.Out):
                        StartCoroutine(Out());
                        break;
                }
                if (index == GreenFrog.Length - 1) { index = 0; } else { index++; }
                break;
            case (FrogType.Blue):
                switch (BlueFrog[index])
                {
                    case (FrogSteps.In):
                        StartCoroutine(In());
                        break;
                    case (FrogSteps.Out):
                        StartCoroutine(Out());
                        break;
                    case (FrogSteps.TurnR):
                        break;
                    case (FrogSteps.TurnL):
                        break;
                }
                break;
        }
    }

    public void CheckKill()
    {
        Tile last = TileOn;
        for(int i = 0; i < Length; i++)
        {
            last.GetTiles();
            foreach (KeyValuePair<Direction,Tile> t in last.Tiles)
            {
                if (t.Key == MyDirection)
                {
                    if (lm.Hero.GetComponent<HeroManager>().CurrentTile == t.Value)
                    {
                        lm.Hero.GetComponent<HeroManager>().DeathTime();
                    }
                    last = t.Value;
                }
            }
        }
    }

    IEnumerator Out()
    {
        Outmmf.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_Scale>().RemapCurveOne = Length;    
        yield return Outmmf.PlayFeedbacksCoroutine(this.transform.position, 1, false);
        CheckKill();
        yield break;
    }
    IEnumerator In()
    {
        yield return Inmmf.PlayFeedbacksCoroutine(this.transform.position, 1, false);
        yield break;
    }
}

