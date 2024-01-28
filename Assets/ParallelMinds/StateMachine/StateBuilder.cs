using System;
namespace ParallelMinds.StateMachine {
public class StateBuilder {
  private State state;
  private ParallelStateMachine fsm;

  public StateBuilder (State state, ParallelStateMachine fsm) {
    this.state = state;
    this.fsm = fsm;
  }

  // "Upon entering [state], do [action]."
  public StateBuilder UponEnteringDo (Action action) {
    state.OnEnter = action;
    return this;
  }

  // "While updating [state], do [action]."
  public StateBuilder WhileUpdatingDo (Action action) {
    state.OnUpdate = action;
    return this;
  }
  // "While fixed updating [state], do [action]."
  public StateBuilder WhileFixedUpdatingDo (Action action) {
    state.OnFixedUpdate = action;
    return this;
  }
  // "Upon exiting [state], do [action]."
  public StateBuilder UponExitingDo (Action action) {
    state.OnExit = action;
    return this;
  }



  // "When [event] happens, transition to [state] if [condition]."
  public StateBuilder WhenEventHappensTransitionToIf (
  string eventName, string targetStateName, Func<bool> condition) {
    var transition = new Transition (targetStateName, condition);
    state.AddTransition (eventName, transition);
    return this;
  }
}
}
