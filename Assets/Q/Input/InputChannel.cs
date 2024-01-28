using UnityEngine;
using UnityEngine.InputSystem;
using Tocsin;

[CreateAssetMenu (fileName = "InputChannel", menuName = "Input/Input Channel")]
public class InputChannel: ScriptableObject, InputActions.IInGameActions {
  private InputActions inputActions;

  // Define Tocsin events for different input actions
  private Tocsin<Vector2> moveStarted = new Tocsin<Vector2> ();
  private Tocsin<Vector2> movePerformed = new Tocsin<Vector2> ();
  private Tocsin<Vector2> moveCanceled = new Tocsin<Vector2> ();
  private Tocsin<bool> pausePressed = new Tocsin<bool> ();
  private Tocsin<float> rotateCamera = new Tocsin<float> ();
  private Tocsin<bool> jumpPressed = new Tocsin<bool> ();
  private Tocsin<bool> jumpReleased = new Tocsin<bool> ();
  private void OnEnable () {
    if (inputActions == null) {
      inputActions = new InputActions ();
      inputActions.InGame.SetCallbacks (this);
    }
    inputActions.InGame.Enable ();
  }

  private void OnDisable () { inputActions.InGame.Disable (); }

  // Implement the input system's movement callbacks
  public void OnMove (InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Started) {
      moveStarted.Dispatch (context.ReadValue<Vector2> ());
    } else if (context.phase == InputActionPhase.Performed) {
      movePerformed.Dispatch (context.ReadValue<Vector2> ());
    } else if (context.phase == InputActionPhase.Canceled) {
      moveCanceled.Dispatch (context.ReadValue<Vector2> ());
    }
  }

  public void OnPause (InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
      pausePressed.Dispatch (true);// Dispatch true when paused
    }
  }

  public void OnCameraRotate (InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
      float rotationInput = context.ReadValue<float> ();
      rotateCamera.Dispatch (rotationInput);
    }
  }

  public void OnJump (InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
      jumpPressed.Dispatch (true);
    }
    if (context.phase == InputActionPhase.Canceled) {
      jumpReleased.Dispatch (true);
    }
  }



  // Public methods to allow other scripts to subscribe to the Tocsin events
  public void AddMoveStartedListener (System.Action<Vector2> listener) {
    moveStarted.AddListener (listener);
  }

  public void RemoveMoveStartedListener (System.Action<Vector2> listener) {
    moveStarted.RemoveListener (listener);
  }

  public void AddMovePerformedListener (System.Action<Vector2> listener) {
    movePerformed.AddListener (listener);
  }

  public void RemoveMovePerformedListener (System.Action<Vector2> listener) {
    movePerformed.RemoveListener (listener);
  }

  public void AddMoveCanceledListener (System.Action<Vector2> listener) {
    moveCanceled.AddListener (listener);
  }

  public void RemoveMoveCanceledListener (System.Action<Vector2> listener) {
    moveCanceled.RemoveListener (listener);
  }

  public void AddPauseListener (System.Action<bool> listener) {
    pausePressed.AddListener (listener);
  }

  public void RemovePauseListener (System.Action<bool> listener) {
    pausePressed.RemoveListener (listener);
  }

  public void AddRotateListener (System.Action<float> listener) {
    rotateCamera.AddListener (listener);
  }

  public void RemoveRotateListener (System.Action<float> listener) {
    rotateCamera.RemoveListener (listener);
  }

  public void AddJumpListener(System.Action<bool> listener)
  {
    jumpPressed.AddListener(listener);
  }

  public void RemoveJumpListener (System.Action<bool> listener) {
    jumpPressed.RemoveListener (listener);
  }

  public void AddJumpReleasedListener(System.Action<bool> listener) {
    jumpReleased.AddListener(listener);
  }

  public void RemoveJumpReleasedListener(System.Action<bool> listener) {
    jumpReleased.RemoveListener(listener);
  }
}
