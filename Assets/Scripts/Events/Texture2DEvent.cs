using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Game Event", menuName = "More Events/Texture2DEvent", order = 52)]
public class Texture2DEvent : GameEvent
{
    [NonSerialized]
    private Texture2D _Texture;

    public Texture2D Texture { get { return _Texture; } set { _Texture = value; } }
}
