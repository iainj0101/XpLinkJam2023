using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class EndTile : Tile
{
    public MMF_Player effect;
    public LevelManager lm;

    public void Win()
    {
        lm.StartNextLevel();
    }
}
