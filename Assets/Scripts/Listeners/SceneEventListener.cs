using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEventListener : ActionEventListener
{
    public override void OnEventRaised(GameEvent source)
    {
        if (((SceneEvent)source).Additive)
        {
            SceneManager.LoadScene(((SceneEvent)source).ScenePath, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(((SceneEvent)source).ScenePath, LoadSceneMode.Single);
        }

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Debug.Log(SceneManager.GetSceneAt(i).name);
        }

        base.OnEventRaised(source);
    }

    public override void OnEventUndone(GameEvent source)
    {
        if (((SceneEvent)source).Additive)
        {
            SceneManager.UnloadSceneAsync(((SceneEvent)source).ScenePath);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        base.OnEventUndone(source);
    }
}
