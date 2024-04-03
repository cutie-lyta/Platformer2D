using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    public event Action<InputAction.CallbackContext> Movement;

    public event Action<InputAction.CallbackContext> Slam;

    public event Action<InputAction.CallbackContext> Teleport;

    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _input.onActionTriggered += OnAction;
    }

    private void OnAction(InputAction.CallbackContext ctx)
    {
        switch (ctx.action.name)
        {
            case "Movement":
                Movement?.Invoke(ctx);
                break;

            case "Slam":
                Slam?.Invoke(ctx);
                break;

            case "Teleport":
                Teleport?.Invoke(ctx);
                break;
        }
    }

    private void StopInput()
    {
        _input.enabled = false;
    }

    private void StartInput()
    {
        _input.enabled = true;
    }
}
