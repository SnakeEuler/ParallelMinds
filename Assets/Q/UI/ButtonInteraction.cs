using System.Collections;
using UnityEngine;
using UnityEngine.UI;       // Required for interacting with UI elements
using ParallelMinds.Utility;// Assuming SpringToScale is in this namespace
namespace Q.UI {
[RequireComponent (typeof (Button))]// Ensure there is a Button component
public class ButtonInteraction: MonoBehaviour {
  private Button button;
  private SpringToScale springToScale;

  public Vector3 targetScale = new Vector3 (1.1f, 1.1f, 1.1f);// Scale to spring to
  public float duration = 0.1f;                               // Duration of the scale effect

  void Awake () {
    button = GetComponent<Button> ();
    springToScale = GetComponent<SpringToScale> ();

    // Add listener to the button's onClick event
    button.onClick.AddListener (OnButtonClick);
  }

  private void OnButtonClick () {
    // Call the SpringTo method on button click
    if (springToScale != null) {
      springToScale.SpringTo (targetScale);
      StartCoroutine (ResetScaleAfterTime (duration));
    }
  }

  private IEnumerator ResetScaleAfterTime (float time) {
    yield return new WaitForSeconds (time);
    // Resetting scale to original after specified duration
    springToScale.SpringTo (Vector3.one);// Assuming Vector3.one is the original scale
  }
}
}
