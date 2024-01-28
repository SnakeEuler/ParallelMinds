using System;
using UnityEngine;
using ParallelMinds.StateMachine;

namespace Q {
public class WalkState: State, ITransition {
  private PlayerMovement playerMovement;
  private PlayerData playerData;
  private Rigidbody rb;

  private Phase phase;
  private float curveTime;
  private float localXVelocity;

  // Constructor for WalkState
  public WalkState (PlayerMovement controller): base ("Walk", controller.csm) {
    playerMovement = controller;
    playerData = controller.playerData;
    rb = controller.rb;
  }

  // Called when entering the walk state
  public override void EnterState () {
    curveTime = 0f;
    localXVelocity = rb.velocity.magnitude;
    phase = Phase.Accelerating;
  }

  // Called every frame when in the walk state
  public override void UpdateState () {
    if (playerMovement.grounded) {
      playerMovement.HandleSlope (playerMovement.LastHit);
      ApplyWalkingMovement ();
    }
    HandleStateChange ();
  }

  // Called every fixed frame-rate frame when in the walk state
  public override void FixedUpdateState () {
    curveTime += Time.fixedDeltaTime;
    UpdatePhaseAndVelocity ();
  }

  // Apply walking movement based on player input and current phase
  private void ApplyWalkingMovement () {
    bool isMoving = playerMovement.MoveDirection != Vector2.zero;

    // Convert the 2D movement direction to a 3D vector
    Vector3 inputDirection
        = new Vector3 (playerMovement.MoveDirection.x, 0, playerMovement.MoveDirection.y);

    // Transform the input direction by the player's orientation
    Vector3 orientedDirection
        = playerMovement.orientation.TransformDirection (inputDirection).normalized;

    float speedFromCurve = GetCurrentSpeedFromCurve ();
    Vector3 desiredVelocity = isMoving? orientedDirection * speedFromCurve : Vector3.zero;

    // Apply the desired velocity, preserving the existing y-axis velocity
    rb.velocity = new Vector3 (desiredVelocity.x, rb.velocity.y, desiredVelocity.z);
  }


  // Update the phase of movement and adjust velocity accordingly
  private void UpdatePhaseAndVelocity () {
    if (IsChangingDirection ()) {
      phase = Phase.Reversing;
      curveTime = 0f;
    }

    switch (phase) {
      case Phase.Accelerating:
        if (curveTime > playerData.accelerationTime) {
          phase = Phase.Decelerating;
          curveTime = 0f;
        }
        break;
      case Phase.Decelerating:
        // Deceleration logic if needed
        break;
      case Phase.Reversing:
        if (curveTime > playerData.reverseTime) {
          phase = Phase.Accelerating;
          curveTime = 0f;
        }
        break;
      default:
        throw new ArgumentOutOfRangeException ();
    }
  }

  // Check if the player is changing direction
  private bool IsChangingDirection () {
    float currentDirection = Mathf.Sign (rb.velocity.x);
    float inputDirection = Mathf.Sign (playerMovement.MoveDirection.x);
    return Math.Abs (currentDirection - inputDirection) > 0.1
        && rb.velocity.magnitude > playerData.directionChangeThreshold;
  }

  // Calculate the current speed from the appropriate curve based on the current phase
  private float GetCurrentSpeedFromCurve () {
    return phase switch {
      Phase.Accelerating => playerData.accelerationCurve.Evaluate (curveTime
      / playerData.accelerationTime) * playerData.maxSpeed,
      Phase.Decelerating => playerData.decelerationCurve.Evaluate (curveTime
      / playerData.decelerationTime) * playerData.maxSpeed,
      Phase.Reversing => playerData.reverseCurve.Evaluate (curveTime / playerData.reverseTime)
          * playerData.maxSpeed,
      _ => 0f,
    };
  }

  public void HandleStateChange () {
    if (playerMovement.MoveDirection == Vector2.zero && rb.velocity.magnitude < .05) {
      playerMovement.csm.ChangeState ("Idle");
    } else if (playerMovement.isJumping && playerMovement.grounded && playerMovement.readyToJump) {
      playerMovement.csm.ChangeState ("Jump");
    }
  }
}
}
