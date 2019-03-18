using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInput")]
[System.Serializable]
public class PlayerInput : ScriptableObject
{
    public KeyCode flightInputButton;
    public KeyCode shootInputbutton;
    public KeyCode rotateLeftButton;
    public KeyCode rotateRightButton;

    public string horizontalAxis;
    public string verticalAxis;
}
