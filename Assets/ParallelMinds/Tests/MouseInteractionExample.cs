using ParallelMinds.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInteractionExample: MonoBehaviour {
  private HFSM fsm;

  void Start () {
    fsm = GetComponent<HFSM> ();

    fsm.AddState ("Idle").
        UponEnteringDo (() => SetMaterialColor (Color.white)).
        WhileUpdatingDo (CheckForMouseOver).
        UponExitingDo (() => { });

    fsm.AddState ("Highlighted").
        UponEnteringDo (() => SetMaterialColor (Color.yellow)).
        WhileUpdatingDo (CheckForSelection).
        UponExitingDo (() => { });

    fsm.AddState ("Selected").
        UponEnteringDo (() => SetMaterialColor (Color.green)).
        WhileUpdatingDo (CheckForDeselection).
        UponExitingDo (() => { });

    fsm.ChangeState ("Idle");
  }

  void CheckForMouseOver () {
    if (IsMouseOver ()) {
      fsm.ChangeState ("Highlighted");
    }
  }

  void CheckForSelection () {
    if (IsMouseOver () && Mouse.current.leftButton.wasPressedThisFrame) {
      fsm.ChangeState ("Selected");
    } else if (!IsMouseOver ()) {
      fsm.ChangeState ("Idle");
    }
  }

  void CheckForDeselection () {
    if (!IsMouseOver () || Mouse.current.leftButton.wasPressedThisFrame) {
      fsm.ChangeState ("Idle");
    }
  }

  bool IsMouseOver () {
    // Raycast from mouse position to see if it hits this GameObject
    var ray = Camera.main!.ScreenPointToRay (Mouse.current.position.ReadValue ());
    return Physics.Raycast (ray, out var hit) && hit.transform == transform;
  }

  void SetMaterialColor (Color color) { GetComponent<Renderer> ().material.color = color; }
}
