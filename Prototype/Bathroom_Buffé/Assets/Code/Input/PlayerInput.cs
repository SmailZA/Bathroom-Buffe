using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInput")]
public class PlayerInput : ScriptableObject
{
    public KeyCode flightInputButton;
    public KeyCode shootInputbutton;
    public KeyCode rotateLeftButton;
    public KeyCode rotateRightButton;
}
