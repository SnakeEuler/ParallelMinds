using System.Collections;
using UnityEngine;

namespace ParallelMinds.Utility {
public class SpringToTarget2D: BaseSpringBehaviour, ISpringTo<Vector2>, INudger<Vector2> {
  private SpringVector2 spring;

  private void Awake () {
    spring = new SpringVector2 () {
      StartValue = transform.position,
      EndValue = transform.position,
      Damping = Damping,
      Stiffness = Stiffness
    };
  }

  private IEnumerator DoSpringToTarget (Vector2 targetPosition) {
    if (Mathf.Approximately (spring.CurrentVelocity.sqrMagnitude, 0)) {
      spring.Reset ();
      spring.StartValue = transform.position;
      spring.EndValue = targetPosition;
    } else {
      spring.UpdateEndValue (targetPosition, spring.CurrentVelocity);
    }

    while (!Mathf.Approximately (
    Vector2.SqrMagnitude (new Vector2 (transform.position.x, transform.position.y)
    - targetPosition), 0)) {
      transform.position = spring.Evaluate (Time.deltaTime);

      yield return null;
    }

    spring.Reset ();
  }

  public void SpringTo (Vector2 targetPosition) {
    StopAllCoroutines ();

    CheckInspectorChanges ();

    StartCoroutine (DoSpringToTarget (targetPosition));
  }

  private void CheckInspectorChanges () {
    spring.Damping = Damping;
    spring.Stiffness = Stiffness;
  }

  public void Nudge (Vector2 amount) {
    CheckInspectorChanges ();
    if (Mathf.Approximately (spring.CurrentVelocity.sqrMagnitude, 0)) {
      StartCoroutine (HandleNudge (amount));
    } else {
      spring.UpdateEndValue (spring.EndValue, spring.CurrentVelocity + amount);
    }
  }

  private IEnumerator HandleNudge (Vector2 amount) {
    spring.Reset ();
    spring.StartValue = transform.position;
    spring.EndValue = transform.position;
    spring.InitialVelocity = amount;
    Vector3 targetPosition = transform.position;
    transform.position = spring.Evaluate (Time.deltaTime);

    while (!Mathf.Approximately (0, Vector2.SqrMagnitude (targetPosition - transform.position))) {
      transform.position = spring.Evaluate (Time.deltaTime);

      yield return null;
    }

    spring.Reset ();
  }
}
}
