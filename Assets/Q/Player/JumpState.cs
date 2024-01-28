using UnityEngine;
using State = ParallelMinds.StateMachine.State;

namespace Q {
public class JumpState: State {
  private PlayerMovement playerMovement;
  private PlayerData playerData;
  private Rigidbody rb;

  private Phase phase;
  private float curveTime;
  private Vector3 initialHorizontalVelocity;

  public JumpState (PlayerMovement controller): base ("Jump", controller.csm) {
    playerMovement = controller;
    playerData = controller.playerData;
    rb = controller.rb;
  }
  public override void EnterState () {
    
    playerMovement.isJumping = true;
    playerMovement.inputChannel.AddJumpReleasedListener (OnJumpReleased);
  }


  private void ApplyInitialJump () {
    float jumpVelocity = Mathf.Sqrt (2 * playerData.jumpHeight * Mathf.Abs (Physics.gravity.y));
    rb.velocity = new Vector3 (rb.velocity.x, jumpVelocity, rb.velocity.z);

  }


  public override void UpdateState () {
    curveTime += Time.deltaTime;
    ApplyJumpingMovement ();
    CheckPhaseTransition ();

// Check if there is movement input to decide next state.
    if (playerMovement.grounded && playerMovement.MoveDirection != Vector2.zero) {
      playerMovement.csm.ChangeState ("Walk");
    }
    if (playerMovement.grounded && playerMovement.MoveDirection == Vector2.zero) {
      playerMovement.csm.ChangeState ("Idle");
    }
  }

  private void ApplyJumpingMovement () {
    ApplyInitialJump ();

    // Only modify the y-velocity if we are ascending.
    if (phase == Phase.Ascending) {
      float curveValue = playerData.jumpUpCurve.Evaluate (curveTime / playerData.jumpTime);
      // We multiply by -1 because Unity's gravity is negative.
      rb.velocity = new Vector3 (rb.velocity.x, -curveValue * Mathf.Abs (Physics.gravity.y),
      rb.velocity.z);
    }

  }

// Adjust the gravity scale to be called in FixedUpdate since it's physics-related.
  public override void FixedUpdateState () {
    if (phase == Phase.Descending) {
      Vector3 gravity = Physics.gravity.y * (playerData.fallMultiplier - 1) * Vector3.up;
      rb.AddForce (gravity, ForceMode.Acceleration);
    }
  }

  private void CheckPhaseTransition () {
    if (phase == Phase.Ascending && rb.velocity.y <= 0) {
      phase = Phase.Descending;
    }
    if (playerMovement.grounded) {
      if (playerMovement.MoveDirection == Vector2.zero) {
        playerMovement.csm.ChangeState ("Idle");
      } else {
        playerMovement.csm.ChangeState ("Walk");
      }
    }
  }

  public void OnJumpReleased (bool input) {
    if (input && phase == Phase.Ascending) {
      // Cut the jump short if the jump button is released during ascent.
      rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y * playerData.jumpCutMultiplier,
      rb.velocity.z);
      phase = Phase.Descending;// Switch to descending phase
      curveTime = 0f;          // Reset the curve time for descending phase
    }
  }

  public override void ExitState () {
    playerMovement.inputChannel.RemoveJumpReleasedListener (OnJumpReleased);
    playerMovement.isJumping = false;// Reset isJumping on exit
    playerMovement.readyToJump = true;
    playerMovement.jumpCount = 0;
  }
}
}
