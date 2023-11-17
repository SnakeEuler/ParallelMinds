using UnityEngine;
using ParallelMinds.StateMachine;

public class PauseState : State
{
  public PauseState(string name, HFSM fsm) : base(name, fsm) {}

  public override void EnterState()
  {
    base.EnterState();
    Time.timeScale = 0f;// Pause the game
    Debug.Log("Game Paused");
  }

  public override void ExitState()
  {
    base.ExitState();
    Time.timeScale = 1f;// Unpause the game
    Debug.Log("Game Unpaused");
  }
}
