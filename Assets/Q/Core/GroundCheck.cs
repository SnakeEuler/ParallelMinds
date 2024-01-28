// using UnityEngine;
// using Q;
//
// public class GroundCheck: MonoBehaviour {
//   public PlayerMovement player;
//   public PlayerData playerData;
//   private RaycastHit hit;
//
//   void Update () {
//     PerformGroundCheck ();
//     UpdateGroundedFlags ();
//     HandleGroundInteractions ();
//   }
//
//   private void PerformGroundCheck () {
//     Vector3 origin = player.transform.position + Vector3.up * (playerData.playerHeight * 0.5f);
//     playerData.groundedNextFrame = Physics.SphereCast (origin, playerData.groundCheckRadius,
//     Vector3.down, out hit, playerData.checkDepth, playerData.groundLayer);
//   }
//
//   private void UpdateGroundedFlags () {
//     //playerData.groundedLastFrame = playerData.groundedThisFrame;
//     playerData.groundedThisFrame = playerData.groundedNextFrame;
//   }
//
//   private void HandleGroundInteractions () {
//     if (playerData.groundedThisFrame) {
//       CalculateSlopeAngle ();
//       AdjustMovementBasedOnSlope ();
//       ApplySlidingIfNecessary ();
//       ApplyStabilityOnSlope ();
//       AdjustGravityBasedOnSlope ();
//     }
//   }
//
//   private void CalculateSlopeAngle () {
//     playerData.slopeAngle = Vector3.Angle (hit.normal, Vector3.up);
//     playerData.isOnWalkableSlope = playerData.slopeAngle <= playerData.maxSlopeAngle;
//   }
//
//   private void AdjustMovementBasedOnSlope () {
//     if (playerData.isOnWalkableSlope) {
//       float slopeFactor
//           = Mathf.Clamp (1 - (playerData.slopeAngle / 90), 0.5f, 1);// Example, adjust as needed
//       player.rb.velocity = new Vector3 (player.rb.velocity.x * slopeFactor, player.rb.velocity.y,
//       player.rb.velocity.z * slopeFactor);
//     }
//   }
//
//   private void ApplySlidingIfNecessary () {
//     if (playerData.slopeAngle > playerData.steepSlopeAngleThreshold) {
//       Vector3 slideDirection = new Vector3 (hit.normal.x, -hit.normal.y, hit.normal.z).normalized;
//       player.rb.AddForce (slideDirection * playerData.slideForce);
//     }
//   }
//
//   private void ApplyStabilityOnSlope () {
//     if (playerData.slopeAngle > playerData.moderateSlopeAngleThreshold
//       && playerData.slopeAngle <= playerData.steepSlopeAngleThreshold) {
//       player.rb.drag = playerData.slopeStabilityDrag;
//     } else {
//       player.rb.drag = playerData.groundDrag;// Reset drag to default
//     }
//   }
//
//   private void AdjustGravityBasedOnSlope () {
//     if (playerData.slopeAngle > playerData.steepSlopeAngleThreshold && player.rb.velocity.y < 0) {
//       Physics.gravity = new Vector3 (0,
//       -Mathf.Abs (Physics.gravity.y) * playerData.steepSlopeGravityMultiplier, 0);
//     } else {
//       Physics.gravity
//           = new Vector3 (0, -Mathf.Abs (Physics.gravity.y), 0);// Reset to default gravity
//     }
//   }
// }
