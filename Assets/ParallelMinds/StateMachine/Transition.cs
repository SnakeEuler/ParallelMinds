using System;
namespace ParallelMinds.StateMachine {
public class Transition
{
  public string TargetStateName { get; }
  public Func<bool> Condition { get; }

  public Transition(string targetStateName, Func<bool> condition)
  {
    TargetStateName = targetStateName;
    Condition = condition;
  }
}
}
