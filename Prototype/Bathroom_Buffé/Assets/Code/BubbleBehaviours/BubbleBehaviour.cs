using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BubbleBehaviour : ScriptableObject
{
    public abstract void OnShot(GameObject thisGO, GameObject shotByGO);
    public abstract void OnCollided(GameObject thisGO, GameObject shotByGO);
}
