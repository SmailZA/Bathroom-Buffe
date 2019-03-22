using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BubbleBehaviour/Pink")]
public class PinkBubble : BubbleBehaviour
{
    public float slowDownDuration;
    public FloatVariable defaultFlySpeed;
    public FloatVariable slowDownVariable;

    public override void OnShot(GameObject thisGO, GameObject shotByGO)
    {
        FlyController controller = shotByGO.GetComponent<Projectile>().controller;
        Bubble bubble = thisGO.GetComponent<Bubble>();
        controller?.OnIncreaseScore(controller.playerIndex, bubble.type.score);

        // Drop power up
        //Destroy(thisGO);
        bubble.BeginDestroy();
    }
    
    public override void OnCollided(GameObject thisGO, GameObject collidedWithGO)
    {
        FlyController controller = collidedWithGO.GetComponent<FlyController>();
        if (controller)
        {
            controller.flightSpeedVariable = slowDownVariable;
            collidedWithGO.GetComponent<FlyController>().StartCoroutine(ResetSpeed(controller));
        }
    }

    IEnumerator ResetSpeed(FlyController controller)
    {
        yield return new WaitForSeconds(slowDownDuration);
        controller.flightSpeedVariable = defaultFlySpeed;
    }
}
