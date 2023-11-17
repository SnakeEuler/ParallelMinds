using System.Collections.Generic;
using UnityEngine;
namespace ParallelMinds.StateMachine {
public class HFSM : MonoBehaviour
{
  public State currentState;
  private Dictionary<string, State> states = new Dictionary<string, State>();

  void Update()
  {
    currentState?.UpdateState();
  }

  public StateBuilder AddState(string stateName)
  {
    var newState = new State(stateName, this);
    states[stateName] = newState;
    return new StateBuilder(newState, this);
  }

  public void ChangeState(string newStateName)
  {
    if (states.TryGetValue(newStateName, out var newState))
    {
      currentState?.ExitState();
      currentState = newState;
      currentState.EnterState();
    }
    else
    {
      Debug.LogError($"State '{newStateName}' not found.");
    }
  }

  public void FireEvent(string eventName)
  {
    currentState?.HandleEvent(eventName);
  }

  // Additional methods for saving/loading states, initializing, etc., go here.
}
}
