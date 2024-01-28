using System;
using UnityEngine;
namespace Q {
public class CameraConstraint: MonoBehaviour {
  [SerializeField]
  private Transform cameraPosition;

  private void Update () { transform.position = cameraPosition.position; }
}
}
