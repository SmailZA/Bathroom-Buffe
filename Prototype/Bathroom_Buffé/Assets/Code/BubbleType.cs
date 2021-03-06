﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BubbleType")]
public class BubbleType : ScriptableObject
{
    public Sprite sprite;
    public int shootScore;
    public int collideScore;
    public float speed;

    public BubbleBehaviour bubbleBehaviour;
}
