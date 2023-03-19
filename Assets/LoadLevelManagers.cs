using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelManagers : MonoBehaviour
{
    public Transform Hero;
    public Player player;
    public GameObject CurrentLevel;

    [Header("Levels")]
    public GameObject Level1;

    private void Start()
    {
        DestroyAndGoNext(Level1);
    }
    public void DestroyAndGoNext(GameObject nextLevel)
    {
        Camera.main.GetComponent<MoreMountains.Tools.MMFollowTarget>().ChangeFollowTarget(this.transform);
        Camera.main.GetComponent<MoreMountains.Tools.MMFollowTarget>().FollowPositionY = true;
        Camera.main.GetComponent<MoreMountains.Tools.MMFollowTarget>().Offset = new Vector3(0, 10, 0);
        Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);

        Hero.GetComponent<HeroManager>().ResetButton.SetActive(false);
        Hero.GetComponent<HeroManager>().canrestart = false; ;
        Destroy(Hero.GetComponent<HeroManager>().BugInstance);
        Destroy(CurrentLevel);
        CurrentLevel = Instantiate(nextLevel, this.transform);
        CurrentLevel.GetComponent<LevelManager>().LoadData(Hero, player, this);
    }
}
