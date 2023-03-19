using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class EndTile : Tile
{
    public MMF_Player effect;
    public LevelManager lm;

    IEnumerator Woo()
    {
        
        yield return effect.PlayFeedbacksCoroutine(this.transform.position, 1, false);

        lm.StartNextLevel();
        yield break;
    }
    public void Win()
    {
        StartCoroutine(Woo());
    }
}
