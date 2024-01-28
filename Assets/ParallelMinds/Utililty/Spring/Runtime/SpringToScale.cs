using System.Collections;
using UnityEngine;

namespace ParallelMinds.Utility {
public class SpringToScale: BaseSpringBehaviour, ISpringTo<Vector3>, INudger<Vector3> {
  private SpringVector3 spring;

  private void Awake () {
    spring = new SpringVector3 () {
      StartValue = transform.localScale,
      EndValue = transform.localScale,
      Damping = Damping,
      Stiffness = Stiffness
    };
  }

  public void SpringTo (Vector3 targetScale) {
    StopAllCoroutines ();

    CheckInspectorChanges ();

    StartCoroutine (DoSpringToTarget (targetScale));
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
    spring.StartValue = transform.localScale;
    spring.EndValue = transform.localScale;
    spring.InitialVelocity = amount;
    Vector3 targetScale = transform.localScale;
    transform.localScale = spring.Evaluate (Time.deltaTime);

    while (!Mathf.Approximately (0, Vector3.SqrMagnitude (targetScale - transform.localScale))) {
      transform.localScale = spring.Evaluate (Time.deltaTime);

      yield return null;
    }

    spring.Reset ();
  }

  private IEnumerator DoSpringToTarget (Vector3 targetScale) {
    if (Mathf.Approximately (spring.CurrentVelocity.sqrMagnitude, 0)) {
      spring.Reset ();
      spring.StartValue = transform.localScale;
      spring.EndValue = targetScale;
    } else {
      spring.UpdateEndValue (targetScale, spring.CurrentVelocity);
    }

    while (!Mathf.Approximately (Vector3.SqrMagnitude (transform.localScale - targetScale), 0)) {
      transform.localScale = spring.Evaluate (Time.deltaTime);

      yield return null;
    }

    spring.Reset ();
  }

  private void CheckInspectorChanges () {
    spring.Damping = Damping;
    spring.Stiffness = Stiffness;
  }
}
}
