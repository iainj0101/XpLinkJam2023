using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform Hero;
    public Transform StartTile;
    public Player player;
    public Player.CharmList Level;
    public GameObject NextLevel;
    LoadLevelManagers llm;
    public Direction StartDirection;

    public void LoadData(Transform hero, Player _player, LoadLevelManagers _llm)
    {
        Hero = hero;
        player = _player;
        llm = _llm;
        StartLevel();
    }
    public void StartLevel()
    {
        Hero.position = StartTile.position;
        Hero.GetComponent<HeroManager>().CurrentTile = StartTile.GetComponent<Tile>();
        Hero.GetComponent<HeroManager>().CurrentDirection = StartDirection;
        player.gameObject.SetActive(true);
        player.LoadPlayer(Level);
    }

    public void StartNextLevel()
    {
        llm.DestroyAndGoNext(NextLevel);
    }
}
