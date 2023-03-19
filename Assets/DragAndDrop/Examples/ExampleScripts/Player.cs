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
    public Charm[] Level3 = new Charm[4];
    public Charm[] Level4 = new Charm[5];
    public Charm[] Level5 = new Charm[3];
    public Charm[] Level6 = new Charm[1];
    public Charm[] Level7 = new Charm[3];
    public Charm[] Level8 = new Charm[4];
    public Charm[] Level9 = new Charm[6];
    public Charm[] Level10 = new Charm[6];
    public Charm[] Level11 = new Charm[6];
    public Charm[] Level12 = new Charm[6];
    public Charm[] Level13 = new Charm[6];
    public Charm[] Level14 = new Charm[6];
    public Charm[] Level15 = new Charm[6];
    public Charm[] Level16 = new Charm[6];

    public List<Power> powers = new List<Power>(4);

    [Header("Equipped Items")]
    public Charm helmet;
    public Charm amulet;
    public Charm ring1;
    public Charm ring2;
    public Charm gloves;
    public Charm boots;
    public Charm armour;

    public void LoadPlayer(CharmList level)
    {
        LevelBugs.charmList =  level;
        UseBugs.charmList = level;
        LevelBugs.Load();
        UseBugs.Load();
    }

    // accessor stuff for UI to use
    public enum CharmList
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        Level11,
        Level12,
        Level13,
        Level14,
        Level15,
        Level16
    };

    public Charm[] GetCharms(CharmList list)
    {
        switch (list)
        {
            case CharmList.Level1: return Level1;
            case CharmList.Level2: return Level2;
            case CharmList.Level3: return Level3;
            case CharmList.Level4: return Level4;
            case CharmList.Level5: return Level5;
            case CharmList.Level6: return Level6;
            case CharmList.Level7: return Level7;
            case CharmList.Level8: return Level8;
            case CharmList.Level9: return Level9;
            case CharmList.Level10: return Level10;
            case CharmList.Level11: return Level11;
            case CharmList.Level12: return Level12;
            case CharmList.Level13: return Level13;
            case CharmList.Level14: return Level14;
            case CharmList.Level15: return Level15;
            case CharmList.Level16: return Level16;
            default: return null;
        }
    }
}
