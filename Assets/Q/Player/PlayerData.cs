using UnityEngine;

namespace Q {
[CreateAssetMenu (menuName = "Q/PlayerData", fileName = "PlayerData")]
public class PlayerData: ScriptableObject {
  [Header ("Input")]
  public InputChannel inputChannel;

  [Header ("Movement Parameters")]
  [Tooltip ("Maximum speed of the player.")]
  public float maxSpeed;

  [Tooltip ("Acceleration curve determining how player speeds up.")]
  [SerializeField, Space (3)]
  public AnimationCurve accelerationCurve;
  public float accelerationTime;

  [Tooltip ("Deceleration curve determining how player slows down.")]
  [SerializeField, Space (5)]
  [BoundCurve (0, 0, 1, 1, 5)]
  public AnimationCurve decelerationCurve;
  public float decelerationTime;

  [Tooltip ("Curve for reverse movement.")]
  [SerializeField, Space (5)]
  [BoundCurve (0, 0, 1, 1, 5)]
  public AnimationCurve reverseCurve;
  public float reverseTime;

  [Tooltip ("Threshold to detect direction changes.")]
  public float directionChangeThreshold = .05f;

  [Header ("Jumping Parameters")]
  [Tooltip ("Force applied when jumping.")]
  public float jumpForce;

  [Tooltip ("Height of the jump.")]
  public int jumpHeight;

  [Tooltip ("Curve for upward part of the jump.")]
  [SerializeField, Space (height: 5)]
  [BoundCurve (0, 0, 1  , 1, 5)]
  public AnimationCurve jumpUpCurve;
  public float jumpTime;

  [Tooltip ("Curve for downward part of the jump.")]
  [SerializeField, Space (5)]
  [BoundCurve (0, 0, 1, 1, 5)]
  public AnimationCurve jumpDownCurve;

  [Tooltip ("Maximum number of consecutive jumps.")]
  public int maxJumps = 3;

  [Tooltip ("How much control the player has in the air by adjusting horizontal speed.")]
  [SerializeField, Space (3)]
  public float airControl;

  [Tooltip ("Multiplier applied to movement while in the air.")]
  public float airMultiplier;

  [Tooltip ("Additional gravity applied during fall.")]
  public float fallMultiplier = 2.5f;

  [Tooltip (
  "Multiplier applied to gravity when player is making a low jump. This acts opposite to fallMultiplier.")]
  public float lowJumpMultiplier = 2f;

  [Tooltip ("Multiplier applied to gravity when player is falling and holding jump button.")]
  public float jumpCutMultiplier = .5f;


  [Header ("Ground Check Parameters")]
  [Tooltip ("Height of the player for ground checks.")]
  public float playerHeight;
  public float checkDepth;
  public LayerMask groundLayer;

  [Header ("Physics Parameters")]
  [Tooltip ("Drag applied when the player is on the ground.")]
  public float groundDrag = 3;

  [Tooltip("Drag applied when the player is in the air.")]
  public float airDrag = 0;
  
  [Tooltip("Drag adjustment rate to smoothly interpolate the drag value to the target")]
  public float dragAdjustmentRate = 5;
  
  [Tooltip ("Maximum slope angle the player can walk on.")]
  public double maxSlopeAngle;

  [Tooltip ("Slope angle threshold for steep slopes.")]
  public double steepSlopeAngleThreshold;

  [Tooltip ("Slope angle threshold for moderately steep slopes.")]
  public double moderateSlopeAngleThreshold;

  [Tooltip ("Force applied when sliding down steep slopes.")]
  public float slideForce;

  [Tooltip ("Drag applied for stability on slopes.")]
  public float slopeStabilityDrag;

  [Tooltip ("Gravity multiplier when on steep slopes.")]
  public float steepSlopeGravityMultiplier;
}
}
