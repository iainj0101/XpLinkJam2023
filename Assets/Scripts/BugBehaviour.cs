using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class BugBehaviour : MonoBehaviour
{
    public MMF_Player MoveFoward;
    public MMF_Player TurnRight;
    public MMF_Player TurnLeft;
    public MMF_Player CatiNorth;
    public MMF_Player CatiEast;
    public MMF_Player CatiSouth;
    public MMF_Player CatiWest;
    public MMF_Player Land;
    public int CatiPrevMove = -1;//if 0 = right, if 1 = left
    public int CatiIndex = 0;
    public int CatiFirstMove = -1;
    public bool FirstMove = true;
    public Direction PreviousDirection = Direction.North;
}
