using System;
using System.Collections.Generic;
namespace ParallelMinds.StateMachine {
public class State
{
  public string Name { get; }
  public HFSM fsm;
  private Dictionary<string, Transition> transitions = new Dictionary<string, Transition>();

  public Action OnEnter { get; set; }
  public Action OnUpdate { get; set; }
  public Action OnExit { get; set; }

  public State(string name, HFSM fsm)
  {
    Name = name;
    this.fsm = fsm;
  }

  public virtual void EnterState()
  {
    OnEnter?.Invoke();
  }

  public void UpdateState()
  {
    OnUpdate?.Invoke();
  }

  public virtual void ExitState()
  {
    OnExit?.Invoke();
  }

  public void AddTransition(string eventName, Transition transition)
  {
    transitions[eventName] = transition;
  }

  public void HandleEvent(string eventName)
  {
    if (transitions.TryGetValue(eventName, out var transition) && transition.Condition())
    {
      fsm.ChangeState(transition.TargetStateName);
    }
  }
}
}
