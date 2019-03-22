using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="BubbleBehaviour/Gas")]
public class GasBubbleBehaviour : BubbleBehaviour
{
    public override void OnCollided(GameObject thisGO, GameObject shotByGO)
    {
        // Increase speed 
        FlyController controller = shotByGO.GetComponent<FlyController>();
        Bubble bubble = thisGO.GetComponent<Bubble>();
        controller?.OnIncreaseScore(controller.playerIndex, bubble.type.collideScore);
        controller?.OnHitBubble(thisGO.transform.position, bubble.type.collideScore);

        Debug.Log("collided with gas bub");

        bubble.BeginDestroy();
    }

    public override void OnShot(GameObject thisGO, GameObject shotByGO)
    {
        FlyController controller = shotByGO.GetComponent<Projectile>().controller;
        Bubble bubble = thisGO.GetComponent<Bubble>();
        controller?.OnIncreaseScore(controller.playerIndex, bubble.type.shootScore);
        controller?.OnHitBubble(thisGO.transform.position, bubble.type.shootScore);

        bubble.BeginDestroy();
    }
}
