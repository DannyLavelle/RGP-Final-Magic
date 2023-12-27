using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayersInput inputActions;
    public event Action<InputActionMap> actionMapChange;

    private void Awake()
    {
        inputActions = new PlayersInput();
    }

    void Start()
    {
        ToggleActionMap(inputActions.Player);//.Player
    }

    public void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
        {
            return;
        }

        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
