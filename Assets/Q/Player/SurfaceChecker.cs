using UnityEngine;
using Tocsin;// Ensure Tocsin namespace is included

namespace Q {
public class SurfaceChecker: MonoBehaviour {
  // Tocsin events for surface detection
  private Tocsin<(Vector3 point, Vector3 normal, bool isContact)> onSurfaceDetected
      = new Tocsin<(Vector3, Vector3, bool)> ();

  // Public method to access the Tocsin event link
  public ILink<(Vector3, Vector3, bool)> OnSurfaceDetected => onSurfaceDetected.plink;

  // Method to perform a surface check
  public void PerformSurfaceCheck (
  Vector3 origin, Vector3 direction, float distance, LayerMask layerMask) {
    RaycastHit hit;
    bool isHit = Physics.Raycast (origin, direction, out hit, distance, layerMask);

    // Dispatch the event with the check results
    onSurfaceDetected.Dispatch ((hit.point, hit.normal, isHit));
  }
  
 
}
}
