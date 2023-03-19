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
        Hero.GetComponent<HeroManager>().ResetButton.SetActive(false);
        Destroy(Hero.GetComponent<HeroManager>().BugInstance);
        Destroy(CurrentLevel);
        CurrentLevel = Instantiate(nextLevel, this.transform);
        CurrentLevel.GetComponent<LevelManager>().LoadData(Hero, player, this);
    }
}
