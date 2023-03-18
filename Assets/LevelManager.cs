using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform Hero;
    public Transform StartTile;
    public Player player;
    public Player.CharmList Level;
    public void StartLevel()
    {
        Hero.position = StartTile.position;
        player.gameObject.SetActive(true);
        player.LoadPlayer(Level);
    }
}
