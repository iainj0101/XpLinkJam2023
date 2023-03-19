using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;

public class EndTile : Tile
{
    public MMF_Player effect;
    public LevelManager lm;
    public bool LAST = false;

    IEnumerator Woo()
    {
        
        yield return effect.PlayFeedbacksCoroutine(this.transform.position, 1, false);

        if (LAST) {
            SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).name);
        } else
        {
            lm.StartNextLevel();
        }

        yield break;
    }
    public void Win()
    {
        //lm.StartNextLevel();
        StartCoroutine(Woo());
    }
}
