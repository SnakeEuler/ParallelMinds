using System.Collections;
using UnityEngine;

namespace ParallelMinds.Utility {
public class SpringToRotation
    : BaseSpringBehaviour,
        ISpringTo<Vector3>,
        ISpringTo<Quaternion>,
        INudger<Vector3>,
        INudger<Quaternion> {
  private SpringVector3 spring;

  private void Awake () {
    spring = new SpringVector3 () {
      StartValue = transform.rotation.eulerAngles,
      EndValue = transform.rotation.eulerAngles,
      Damping = Damping,
      Stiffness = Stiffness
    };
  }

  public void SpringTo (Vector3 targetRotation) { SpringTo (Quaternion.Euler (targetRotation)); }


  public void SpringTo (Quaternion targetRotation) {
    StopAllCoroutines ();

    CheckInspectorChanges ();

    StartCoroutine (DoSpringToTarget (targetRotation));
  }

  private IEnumerator DoSpringToTarget (Quaternion targetRotation) {
    if (Mathf.Approximately (spring.CurrentVelocity.sqrMagnitude, 0)) {
      spring.Reset ();
      spring.StartValue = transform.eulerAngles;
      spring.EndValue = targetRotation.eulerAngles;
    } else {
      spring.UpdateEndValue (targetRotation.eulerAngles, spring.CurrentVelocity);
    }

    while (!Mathf.Approximately (0, 1 - Quaternion.Dot (transform.rotation, targetRotation))) {
      transform.rotation = Quaternion.Euler (spring.Evaluate (Time.deltaTime));

      yield return null;
    }

    spring.Reset ();
  }

  private void CheckInspectorChanges () {
    spring.Damping = Damping;
    spring.Stiffness = Stiffness;
  }

  public void Nudge (Vector3 amount) {
    CheckInspectorChanges ();
    if (Mathf.Approximately (spring.CurrentVelocity.sqrMagnitude, 0)) {
      StartCoroutine (HandleNudge (amount));
    } else {
      spring.UpdateEndValue (spring.EndValue, spring.CurrentVelocity + amount);
    }
  }

  private IEnumerator HandleNudge (Vector3 amount) {
    spring.Reset ();
    spring.StartValue = transform.rotation.eulerAngles;
    spring.EndValue = transform.rotation.eulerAngles;
    spring.InitialVelocity = amount;
    Quaternion targetRotation = transform.rotation;
    transform.rotation = Quaternion.Euler (spring.Evaluate (Time.deltaTime));

    while (!Mathf.Approximately (0, 1 - Quaternion.Dot (targetRotation, transform.rotation))) {
      transform.rotation = Quaternion.Euler (spring.Evaluate (Time.deltaTime));

      yield return null;
    }

    spring.Reset ();
  }

  public void Nudge (Quaternion amount) { Nudge (amount.eulerAngles); }
}
}
