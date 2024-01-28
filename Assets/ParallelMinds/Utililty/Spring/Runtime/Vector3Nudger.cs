using UnityEngine;

namespace ParallelMinds.Utility {
public class Vector3Nudger: MonoBehaviour {
  public BaseSpringBehaviour nudgeable;
  public Vector3 nudgeAmount = new Vector3 (0, 500, 0);
  public Vector2 nudgeFrequency = new Vector2 (2, 10);
  public bool autoNudge;

  private float lastNudgeTime;
  private float nextNudgeFrequency;
  public Vector3Nudger () { autoNudge = true; }

  private void Awake () { nextNudgeFrequency = Random.Range (nudgeFrequency.x, nudgeFrequency.y); }

  private void Update () {
    if (autoNudge && lastNudgeTime + nextNudgeFrequency < Time.time) {
      Nudge ();
    }
  }

  public void Nudge () {
    (nudgeable as INudger<Vector3>)?.Nudge (nudgeAmount);
    lastNudgeTime = Time.time;
    nextNudgeFrequency = Random.Range (nudgeFrequency.x, nudgeFrequency.y);
  }
}
}
