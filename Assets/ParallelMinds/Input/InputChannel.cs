using UnityEngine;
using UnityEngine.InputSystem;
using Tocsin;

[CreateAssetMenu(fileName = "InputChannel", menuName = "Input/Input Channel")]
public class InputChannel : ScriptableObject, InputActions.IInGameActions {
    private InputActions inputActions;
    
    // Define Tocsin events for different input actions
    private Tocsin<Vector2> moveStarted = new Tocsin<Vector2>();
    private Tocsin<Vector2> movePerformed = new Tocsin<Vector2>();
    private Tocsin<Vector2> moveCanceled = new Tocsin<Vector2>();
    private Tocsin<bool> pausePressed = new Tocsin<bool>();
    private void OnEnable() {
        if (inputActions == null) {
            inputActions = new InputActions();
            inputActions.InGame.SetCallbacks(this);
        }
        inputActions.InGame.Enable();
    }

    private void OnDisable() {
        inputActions.InGame.Disable();
    }

    // Implement the input system's movement callbacks
    public void OnMove(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
            moveStarted.Dispatch(context.ReadValue<Vector2>());
        } else if (context.phase == InputActionPhase.Performed) {
            movePerformed.Dispatch(context.ReadValue<Vector2>());
        } else if (context.phase == InputActionPhase.Canceled) {
            moveCanceled.Dispatch(context.ReadValue<Vector2>());
        }
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            pausePressed.Dispatch(true);// Dispatch true when paused
        }
    }

    // Public methods to allow other scripts to subscribe to the Tocsin events
    public void AddMoveStartedListener(System.Action<Vector2> listener) {
        moveStarted.AddListener(listener);
    }

    public void RemoveMoveStartedListener(System.Action<Vector2> listener) {
        moveStarted.RemoveListener(listener);
    }

    public void AddMovePerformedListener(System.Action<Vector2> listener) {
        movePerformed.AddListener(listener);
    }

    public void RemoveMovePerformedListener(System.Action<Vector2> listener) {
        movePerformed.RemoveListener(listener);
    }

    public void AddMoveCanceledListener(System.Action<Vector2> listener) {
        moveCanceled.AddListener(listener);
    }

    public void RemoveMoveCanceledListener(System.Action<Vector2> listener) {
        moveCanceled.RemoveListener(listener);
    }
    
    public void AddPauseListener(System.Action<bool> listener)
    {
        pausePressed.AddListener(listener);
    }

    public void RemovePauseListener(System.Action<bool> listener)
    {
        pausePressed.RemoveListener(listener);
    }

}
