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
            case CharmList.Level1: return (Charm[])Level1.Clone();
            case CharmList.Level2: return (Charm[])Level2.Clone();
            case CharmList.Level3: return (Charm[])Level3.Clone();
            case CharmList.Level4: return (Charm[])Level4.Clone();
            case CharmList.Level5: return (Charm[])Level5.Clone();
            case CharmList.Level6: return (Charm[])Level6.Clone();
            case CharmList.Level7: return (Charm[])Level7.Clone();
            case CharmList.Level8: return (Charm[])Level8.Clone();
            case CharmList.Level9: return (Charm[])Level9.Clone();
            case CharmList.Level10: return (Charm[])Level10.Clone();
            case CharmList.Level11: return (Charm[])Level11.Clone();
            case CharmList.Level12: return (Charm[])Level12.Clone();
            case CharmList.Level13: return (Charm[])Level13.Clone();
            case CharmList.Level14: return (Charm[])Level14.Clone();
            case CharmList.Level15: return (Charm[])Level15.Clone();
            case CharmList.Level16: return (Charm[])Level16.Clone();
            default: return null;
        }
    }
}
