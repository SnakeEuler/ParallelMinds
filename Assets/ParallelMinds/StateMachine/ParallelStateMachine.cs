using System;
using System.Collections.Generic;
using UnityEngine;
namespace ParallelMinds.StateMachine {
public class ParallelStateMachine: MonoBehaviour {
  public State currentState;
  private Dictionary<string, State> states = new Dictionary<string, State> ();

  public void Update () { currentState?.UpdateState (); }

  public void FixedUpdate () { currentState?.FixedUpdateState (); }

  public StateBuilder AddState (string stateName) {
    var newState = new State (stateName, this);
    states[stateName] = newState;
    return new StateBuilder (newState, this);
  }

  public void ChangeState (string newStateName) {
    if (states.TryGetValue (newStateName, out var newState)) {
      currentState?.ExitState ();
      currentState = newState;
      currentState.EnterState ();
    } else {
      Debug.LogError ($"State '{newStateName}' not found.");
    }
  }

  public void FireEvent (string eventName) { currentState?.HandleEvent (eventName); }

  public State GetCurrentState () { return currentState; }
}
}
