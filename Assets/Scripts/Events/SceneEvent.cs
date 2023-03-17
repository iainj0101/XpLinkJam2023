using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "More Events/SceneEvent", order = 52)]
public class SceneEvent : ActionEvent
{
    public string ScenePath;
    public string SceneName;
    public bool Additive = true;
}
