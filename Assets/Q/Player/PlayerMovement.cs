using System;
using ParallelMinds.StateMachine;
using UnityEngine;

namespace Q {
public class PlayerMovement: MonoBehaviour {
  // Public Fields
  public string state;

  [Header ("References")]
  public Rigidbody rb;
  public InputChannel inputChannel;
  public ParallelStateMachine csm;
  public PlayerData playerData;
  public SurfaceChecker surfaceChecker;

  // Private Fields
  private RaycastHit lastHit;
  [SerializeField]
  private float horizontalInput;
  [SerializeField]
  private float verticalInput;
  [SerializeField]
  private Vector2 moveDirection;
  [SerializeField]
  private float currentSpeed;

  // Jumping related fields
  [Header ("Jumping")]
  public bool readyToJump;
  public int jumpCount;
  public bool isFalling;

  // Slope related fields
  [Header ("Slopes")]
  public float slopeAngle;
  public Vector3 slopeNormal;
  public bool isOnWalkableSlope;

  // Other flags
  [Header ("Flags")]
  public bool grounded;
  public bool isJumping;

  //Properties
  public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
  public float VerticalInput { get => verticalInput; set => verticalInput = value; }
  public Vector2 MoveDirection { get => moveDirection; set => moveDirection = value; }
  public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
  public RaycastHit LastHit { get => lastHit; set => lastHit = value; }

  public Transform orientation;

  private void Awake () { InitializeStateMachine (); }

  private void OnEnable () { SubscribeToInputEvents (); }

  private void OnDisable () { UnsubscribeFromInputEvents (); }
  private void Update () { state = csm.currentState.Name; }
  private void FixedUpdate () {

    CheckGround ();
    HandleDrag ();
  }

  // Initialize state machine and add states
  private void InitializeStateMachine () {
    csm = gameObject.AddComponent<ParallelStateMachine> ();

    var idleState = new IdleState (this);
    var walkState = new WalkState (this);
    var jumpState = new JumpState (this);

    csm.AddState (idleState.Name).
        UponEnteringDo (idleState.EnterState).
        WhileUpdatingDo (idleState.UpdateState).
        WhileFixedUpdatingDo (idleState.FixedUpdateState).
        UponExitingDo (idleState.ExitState);

    csm.AddState (walkState.Name).
        UponEnteringDo (walkState.EnterState).
        WhileUpdatingDo (walkState.UpdateState).
        WhileFixedUpdatingDo (idleState.FixedUpdateState).
        UponExitingDo (walkState.ExitState);

    csm.AddState (jumpState.Name).
        UponEnteringDo (jumpState.EnterState).
        WhileUpdatingDo (jumpState.UpdateState).
        WhileFixedUpdatingDo (idleState.FixedUpdateState).
        UponExitingDo (jumpState.ExitState);

    csm.ChangeState ("Idle");
  }

  // Subscribe to input events
  private void SubscribeToInputEvents () {
    inputChannel.AddMoveStartedListener (OnMoveInput);
    inputChannel.AddMovePerformedListener (OnMoveInput);
    inputChannel.AddMoveCanceledListener (OnMoveInput);
    inputChannel.AddJumpListener (OnJumpInput);
    surfaceChecker.OnSurfaceDetected.AddListener (HandleSurfaceCheck);
  }

  // Unsubscribe from input events
  private void UnsubscribeFromInputEvents () {
    inputChannel.RemoveMoveStartedListener (OnMoveInput);
    inputChannel.RemoveMovePerformedListener (OnMoveInput);
    inputChannel.RemoveMoveCanceledListener (OnMoveInput);
    inputChannel.RemoveJumpListener (OnJumpInput);
    surfaceChecker.OnSurfaceDetected.RemoveListener (HandleSurfaceCheck);

  }

  // Handle movement input
  private void OnMoveInput (Vector2 input) {
    // Convert 2D input direction to a 3D vector assuming Y is up
    var inputDirection = new Vector3 (input.x, 0.0f, input.y);

    horizontalInput = inputDirection.x;
    verticalInput = inputDirection.z;
    moveDirection = new Vector2 (horizontalInput, verticalInput).normalized;
  }

  public void OnJumpInput (bool input) {
    if (input && grounded && readyToJump && jumpCount < playerData.maxJumps) {
      csm.ChangeState ("Jump");
    }
  }


  // Check if the player is on the ground
  private void CheckGround () {
    var origin = transform.position + Vector3.up * playerData.playerHeight * 0.5f;
    surfaceChecker.PerformSurfaceCheck (origin, Vector3.down, playerData.checkDepth,
    playerData.groundLayer);
  }

  // Handle result from surface check
  private void HandleSurfaceCheck ((Vector3 point, Vector3 normal, bool isContact) result) {
    grounded = result.isContact;
    if (grounded) {
      slopeAngle = Vector3.Angle (result.normal, Vector3.up);
      isOnWalkableSlope = slopeAngle <= playerData.maxSlopeAngle;
      LastHit = new RaycastHit {
        normal = result.normal,
        point = result.point,
        distance = Vector3.Distance (transform.position, result.point)
      };
    }
  }

  // Handle physics drag based on whether the player is on the ground
  private void HandleDrag () {
    float targetDrag = grounded? playerData.groundDrag : playerData.airDrag;
    rb.drag = Mathf.Lerp (rb.drag, targetDrag, Time.fixedDeltaTime * playerData.dragAdjustmentRate);
  }

  // Gizmo drawing for debugging
  private void OnDrawGizmos () {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere (transform.position, playerData.playerHeight);
  }

  // Reset the jump flag
  public void ResetJumpFlag () { readyToJump = true; }

  // Handle slope related physics
  public void HandleSlope (RaycastHit hit) {
    CalculateSlopeAngle (hit.normal);
    AdjustMovementBasedOnSlope ();
    ApplySlidingIfNecessary (hit);
    ApplyStabilityOnSlope ();
    AdjustGravityBasedOnSlope ();
  }

  // Calculate the angle of the slope
  private void CalculateSlopeAngle (Vector3 normal) {
    slopeAngle = Vector3.Angle (normal, Vector3.up);
    isOnWalkableSlope = slopeAngle <= playerData.maxSlopeAngle;
  }

  // Adjust player movement based on the slope
  private void AdjustMovementBasedOnSlope () {
    if (!isOnWalkableSlope) return;

    Vector3 slopeDirection = Vector3.Cross (slopeNormal, Vector3.down);
    float directionAngle = Vector3.Angle (moveDirection, slopeDirection);
    float slopeFactor = Mathf.Clamp (1 - (slopeAngle / 90), 0.5f, 1);

    if (directionAngle < 90) {
      rb.velocity = new Vector3 (rb.velocity.x * slopeFactor, rb.velocity.y,
      rb.velocity.z * slopeFactor);
    } else {
      // Logic for moving down the slope or sideways
    }
  }

  // Apply sliding if necessary based on slope angle
  private void ApplySlidingIfNecessary (RaycastHit hit) {
    if (slopeAngle > playerData.steepSlopeAngleThreshold) {
      Vector3 slideDirection = new Vector3 (hit.normal.x, -hit.normal.y, hit.normal.z).normalized;
      rb.AddForce (slideDirection * playerData.slideForce);
    }
  }

  // Apply stability drag based on slope angle
  private void ApplyStabilityOnSlope () {
    if (slopeAngle > playerData.moderateSlopeAngleThreshold
      && slopeAngle <= playerData.steepSlopeAngleThreshold) {
      rb.drag = playerData.slopeStabilityDrag;
    } else {
      rb.drag = playerData.groundDrag;
    }
  }

  // Adjust gravity based on slope angle
  private void AdjustGravityBasedOnSlope () {
    if (slopeAngle > playerData.steepSlopeAngleThreshold && rb.velocity.y < 0) {
      Physics.gravity = new Vector3 (0,
      -Mathf.Abs (Physics.gravity.y) * playerData.steepSlopeGravityMultiplier, 0);
    } else {
      Physics.gravity = new Vector3 (0, -Mathf.Abs (Physics.gravity.y), 0);
    }
  }
}
}
