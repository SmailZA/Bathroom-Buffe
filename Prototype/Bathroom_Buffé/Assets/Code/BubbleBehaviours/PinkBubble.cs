using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BubbleBehaviour/Pink")]
public class PinkBubble : BubbleBehaviour
{
    public override void OnShot(GameObject thisGO, GameObject shotByGO)
    {
        Destroy(thisGO);
    }
    
    public override void OnCollided(GameObject thisGO, GameObject shotByGO)
    {

    }
}
