using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class InputSystem : MonoBehaviour
{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    public PlayerInput gamepadInput;
    public PlayerInput keyboardOneInput;
    public PlayerInput keyboardTwoInput;

    PlayerInput[] gamepadInputs;

    public delegate void OnPlayerJoinInputDelegate();
    public OnPlayerJoinInputDelegate OnPlayerJoinInput;

    // Use this for initialization
    void Start()
    {
        gamepadInputs = new PlayerInput[4];

        for (int i = 0; i < 4; ++i)
        {
            gamepadInputs[i] = gamepadInput;
        }
    }

    void FixedUpdate()
    {
        // SetVibration should be sent in a slower rate.
        // Set vibration according to triggers
        GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
    }

    // Update is called once per frame
    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected and use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        for (int i = 0; i < 4; ++i)
        {
            if (gamepadInputs[i])
            {
                PlayerIndex index = (PlayerIndex)i;
                GamePadState padState = GamePad.GetState(index);

                //Debug.Log(string.Format("A button pressed: {0}", padState.Buttons.A == ButtonState.Pressed));
                gamepadInputs[i].shootInput = padState.Buttons.A == ButtonState.Pressed;
            }
        }

        for (int i = 0; i < 4; ++i)
        {
            if (gamepadInputs[i])
            {
                Debug.Log(string.Format("shootInput: {0}", gamepadInputs[i].shootInput));
                if (gamepadInputs[i].shootInput)
                {
                    Debug.Log("player index: " + i + " pressed shoot input! hecking sweet");
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            //GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        // Detect if a button was released this frame
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        // Make the current object turn
        transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
    }

    public void AddNewPlayerInput(int index, InputType inputType)
    {
        PlayerInput newInput = ScriptableObject.CreateInstance<PlayerInput>();
        
        if (inputType == InputType.Gamepad)
        {
            newInput = gamepadInput;
        } else if (inputType == InputType.KeyboardOne)
        {
            newInput = keyboardOneInput;
        } else if (inputType == InputType.KeyboardTwo)
        {
            newInput = keyboardTwoInput;
        }
        else
        {
            Debug.LogError("No InputType specified for player index: " + index + " in InputSystem, is this even possible?");
            return;
        }

        gamepadInputs[index] = newInput;
    }

    void OnGUI()
    {
        string text = "Use left stick to turn the cube, hold A to change color\n";
        text += string.Format("IsConnected {0} Packet #{1}\n", state.IsConnected, state.PacketNumber);
        text += string.Format("\tTriggers {0} {1}\n", state.Triggers.Left, state.Triggers.Right);
        text += string.Format("\tD-Pad {0} {1} {2} {3}\n", state.DPad.Up, state.DPad.Right, state.DPad.Down, state.DPad.Left);
        text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", state.Buttons.Start, state.Buttons.Back, state.Buttons.Guide);
        text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", state.Buttons.LeftStick, state.Buttons.RightStick, state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
        text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", state.Buttons.A, state.Buttons.B, state.Buttons.X, state.Buttons.Y);
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }
}
