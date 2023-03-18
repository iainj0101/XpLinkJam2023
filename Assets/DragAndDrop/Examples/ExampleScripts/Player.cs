using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;

    public CharmsArrayUI LevelBugs;
    public CharmsArrayUI UseBugs;

    public Charm[] Level1 = new Charm[1];
    public Charm[] Level2 = new Charm[3];
    public Charm[] Level3 = new Charm[3];
    public Charm[] Level4 = new Charm[6];

    public List<Power> powers = new List<Power>(4);

    [Header("Equipped Items")]
    public Charm helmet;
    public Charm amulet;
    public Charm ring1;
    public Charm ring2;
    public Charm gloves;
    public Charm boots;
    public Charm armour;

    // accessor stuff for UI to use
    public enum CharmList
    {
        Level1,
        Level2,
        Level3,
        Level4
    };

    public Charm[] GetCharms(CharmList list)
    {
        switch (list)
        {
            case CharmList.Level1: return Level1;
            case CharmList.Level2: return Level2;
            case CharmList.Level3: return Level3;
            case CharmList.Level4: return Level4;
            default: return null;
        }
    }
}
