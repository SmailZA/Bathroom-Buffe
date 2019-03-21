using UnityEngine;

[RequireComponent(typeof(FlyController))]
public class Player : MonoBehaviour
{
    [HideInInspector] public FlyController controller;

    [SerializeField] PlayerInput input;

    public void Initialize(PlayerInput input)
    {
        Debug.Log(input);
        this.input = input;
        controller = GetComponent<FlyController>();
    }

    public void Tick()
    {
        controller.SetInputVariables(input);
        controller.SteerFly();
        controller.AnimateFly();
    }
}
