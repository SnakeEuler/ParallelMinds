using System.Collections;
using UnityEngine;

namespace ParallelMinds.Utility {
public class SpringToTarget3D: BaseSpringBehaviour, ISpringTo<Vector3>, INudger<Vector3> {
  private SpringVector3 Spring;

  private void Awake () {
    Spring = new SpringVector3 () {
      StartValue = transform.position,
      EndValue = transform.position,
      Damping = Damping,
      Stiffness = Stiffness
    };
  }

  public void SpringTo (Vector3 targetPosition) {
    StopAllCoroutines ();

    CheckInspectorChanges ();

    StartCoroutine (DoSpringToTarget (targetPosition));
  }


  public void Nudge (Vector3 amount) {
    CheckInspectorChanges ();
    if (Mathf.Approximately (Spring.CurrentVelocity.sqrMagnitude, 0)) {
      StartCoroutine (HandleNudge (amount));
    } else {
      Spring.UpdateEndValue (Spring.EndValue, Spring.CurrentVelocity + amount);
    }
  }

  private IEnumerator HandleNudge (Vector3 amount) {
    Spring.Reset ();
    Spring.StartValue = transform.position;
    Spring.EndValue = transform.position;
    Spring.InitialVelocity = amount;
    Vector3 targetPosition = transform.position;
    transform.position = Spring.Evaluate (Time.deltaTime);

    while (!Mathf.Approximately (0, Vector3.SqrMagnitude (targetPosition - transform.position))) {
      transform.position = Spring.Evaluate (Time.deltaTime);

      yield return null;
    }

    Spring.Reset ();
  }

  private IEnumerator DoSpringToTarget (Vector3 targetPosition) {
    if (Mathf.Approximately (Spring.CurrentVelocity.sqrMagnitude, 0)) {
      Spring.Reset ();
      Spring.StartValue = transform.position;
      Spring.EndValue = targetPosition;
    } else {
      Spring.UpdateEndValue (targetPosition, Spring.CurrentVelocity);
    }

    while (!Mathf.Approximately (Vector3.SqrMagnitude (transform.position - targetPosition), 0)) {
      transform.position = Spring.Evaluate (Time.deltaTime);

      yield return null;
    }

    Spring.Reset ();
  }

  private void CheckInspectorChanges () {
    Spring.Damping = Damping;
    Spring.Stiffness = Stiffness;
  }
}
}
