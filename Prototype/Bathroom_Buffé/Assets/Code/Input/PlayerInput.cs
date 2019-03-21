using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInput")]
[System.Serializable]
public class PlayerInput : ScriptableObject
{
    public bool flightInput = false;
    public bool shootInput = false;
    public bool rotateLeft = false;
    public bool rotateRight = false;

    public float horizontalValue = 0f;
    public float verticalValue = 0f;

    public KeyCode flightInputButton;
    public KeyCode shootInputbutton;
    public KeyCode rotateLeftButton;
    public KeyCode rotateRightButton;

    public string horizontalAxis;
    public string verticalAxis;
}
