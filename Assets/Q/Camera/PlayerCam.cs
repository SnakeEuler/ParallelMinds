using UnityEngine;

namespace Q {
public class PlayerCam: MonoBehaviour {
  public float sensitivityX = 5f;
  public float sensitivityY = 5f;
  public bool invertY = false;

  [SerializeField]
  private Transform orientation;

  private float targetRotationX;
  private float targetRotationY;
  private float rotationX;
  private float rotationY;
  public float smoothTime = 0.1f;// Time to smooth the camera movement

  private void Start () {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  private void Update () {
    float mouseX = Input.GetAxis ("Mouse X") * sensitivityX;
    float mouseY = Input.GetAxis ("Mouse Y") * sensitivityY;

    targetRotationY += mouseX;
    targetRotationX += invertY? mouseY : -mouseY;
    targetRotationX = Mathf.Clamp (targetRotationX, -90f, 90f);

    // Smoothly interpolate towards the target rotation
    rotationY = Mathf.Lerp (rotationY, targetRotationY, smoothTime / Time.deltaTime);
    rotationX = Mathf.Lerp (rotationX, targetRotationX, smoothTime / Time.deltaTime);

    // Apply the rotations
    transform.rotation = Quaternion.Euler (rotationX, rotationY, 0);
    orientation.rotation = Quaternion.Euler (0, rotationY, 0);
  }
}
}
