using UnityEngine;
using ParallelMinds.StateMachine;
namespace Q {
public class IdleState: State, ITransition {
  private PlayerMovement playerMovement;
  private PlayerData playerData;
  private Rigidbody rb;
  public IdleState (PlayerMovement controller): base ("Idle", controller.csm) {
    playerMovement = controller;
    playerData = controller.playerData;
    rb = controller.rb;
  }

  //simple transition logic



  public override void EnterState () { base.EnterState (); }

  public override void UpdateState () {
    HandleStateChange ();
    base.UpdateState ();
  }

  public override void ExitState () { base.ExitState (); }


  public void HandleStateChange() {
    if (playerMovement.MoveDirection != Vector2.zero) {
      playerMovement.csm.ChangeState("Walk");
    } else if (playerMovement.isJumping && playerMovement.grounded && playerMovement.readyToJump) {
      playerMovement.csm.ChangeState("Jump");
    }
  }

}
}
