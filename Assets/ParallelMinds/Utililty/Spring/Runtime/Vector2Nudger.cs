using UnityEngine;

namespace ParallelMinds.Utility {
public class Vector2Nudger: MonoBehaviour {
  public BaseSpringBehaviour nudgeable;
  public Vector3 nudgeAmount = new Vector2 (0, 500);
  public Vector2 nudgeFrequency = new Vector2 (2, 10);
  public bool autoNudge = true;

  private float lastNudgeTime;
  private float nextNudgeFrequency;

  private void Awake () { nextNudgeFrequency = Random.Range (nudgeFrequency.x, nudgeFrequency.y); }

  private void Update () {
    if (autoNudge && lastNudgeTime + nextNudgeFrequency < Time.time) {
      Nudge ();
    }
  }

  public void Nudge () {
    (nudgeable as INudger<Vector2>)?.Nudge (nudgeAmount);
    lastNudgeTime = Time.time;
    nextNudgeFrequency = Random.Range (nudgeFrequency.x, nudgeFrequency.y);
  }
}
}
