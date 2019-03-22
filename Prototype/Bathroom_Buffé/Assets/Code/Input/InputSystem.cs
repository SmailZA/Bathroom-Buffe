using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class InputSystem : MonoBehaviour
{
    bool[] playerIndexSet;
    bool[] playerJoinedGame;
    PlayerIndex playerIndex;
    GamePadState[] state;
    GamePadState[] prevState;

    public PlayerInput gamepadInput;
    public PlayerInput keyboardOneInput;
    public PlayerInput keyboardTwoInput;

    public PlayerInput[] gamepadInputs;

    public delegate void OnPlayerJoinInputDelegate(int index, PlayerInput input);
    public OnPlayerJoinInputDelegate OnPlayerJoinInput;

    PlayerInput[] activeControllers;

    // Use this for initialization
    void Start()
    {
        gamepadInputs = new PlayerInput[4];

        playerIndexSet = new bool[4] { false, false, false, false };
        playerJoinedGame = new bool[4] { false, false, false, false };

        state = new GamePadState[4];
        prevState = new GamePadState[4];

        activeControllers = new PlayerInput[4];
    }

    void Update()
    {
        for (int i = 0; i < 4; ++i)
        {
            if (!playerIndexSet[i] && !prevState[i].IsConnected)
            {
                state[i] = GamePad.GetState((PlayerIndex)i);
                if (state[i].IsConnected)
                {
                    prevState[i] = state[i];
                    AddNewPlayerInput(i, InputType.Gamepad);
                    playerIndexSet[i] = true;
                }
            }
        }

        for (int i = 0; i < 4; ++i)
        {
            if (playerIndexSet[i])
            {
                PlayerIndex index = (PlayerIndex)i;
                prevState[i] = state[i];
                state[i] = GamePad.GetState(index);

                bool aDown = prevState[i].Buttons.A == ButtonState.Released && state[i].Buttons.A == ButtonState.Pressed;
                if (aDown)
                {
                    gamepadInputs[i].shootInput = true;
                }
            }
        }

        for (int i = 0; i < 4; ++i)
        {
            if (playerJoinedGame[i])
            {
                activeControllers[i].flightInput = state[i].Buttons.B == ButtonState.Pressed;
                activeControllers[i].shootInput = prevState[i].Buttons.A == ButtonState.Released && state[i].Buttons.A == ButtonState.Pressed;

                activeControllers[i].rotateLeft = state[i].DPad.Left == ButtonState.Pressed;
                activeControllers[i].rotateRight = state[i].DPad.Right == ButtonState.Pressed;
            }
        }

        for (int i = 0; i < 4; ++i)
        {
            if (gamepadInputs[i])
            {
                if (gamepadInputs[i].shootInput)
                {
                    if (playerJoinedGame[i])
                    {
                        //gamepadInputs[i].shootInput = false;
                        return;
                    }

                    OnPlayerJoinInput(i, gamepadInputs[i]);
                    gamepadInputs[i].shootInput = false;
                    playerJoinedGame[i] = true;
                    activeControllers[i] = gamepadInputs[i];
                }
            }
        }
    }

    public void AddNewPlayerInput(int index, InputType inputType)
    {
        PlayerInput newInput = ScriptableObject.CreateInstance<PlayerInput>();
        
        if (inputType == InputType.Gamepad)
        {
            newInput = Instantiate(gamepadInput);
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
}
