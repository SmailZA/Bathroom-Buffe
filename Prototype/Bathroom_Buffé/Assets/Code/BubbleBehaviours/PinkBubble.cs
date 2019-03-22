using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BubbleBehaviour/Pink")]
public class PinkBubble : BubbleBehaviour
{
    public float slowDownDuration;
    public FloatVariable defaultFlySpeed;
    public FloatVariable slowDownVariable;
    public int deductPoints = 20; 

    public override void OnShot(GameObject thisGO, GameObject shotByGO)
    {
        FlyController controller = shotByGO.GetComponent<Projectile>().controller;
        Bubble bubble = thisGO.GetComponent<Bubble>();
        controller?.OnIncreaseScore(controller.playerIndex, bubble.type.shootScore);
        controller?.OnHitBubble(thisGO.transform.position, bubble.type.shootScore);

        // Drop power up
        //Destroy(thisGO);
        bubble.BeginDestroy();
    }
    
    public override void OnCollided(GameObject thisGO, GameObject collidedWithGO)
    {
        FlyController controller = collidedWithGO.GetComponent<FlyController>();
        if (controller)
        {
            controller?.OnDecreaseScore(controller.playerIndex, deductPoints);
            controller.flightSpeedVariable = slowDownVariable;
            controller.StartCoroutine(ResetSpeed(controller));
            controller?.OnHitBubble(thisGO.transform.position, -deductPoints);
        }
    }

    IEnumerator ResetSpeed(FlyController controller)
    {
        yield return new WaitForSeconds(slowDownDuration);
        controller.flightSpeedVariable = defaultFlySpeed;
    }
}
