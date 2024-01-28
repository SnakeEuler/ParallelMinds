using UnityEngine;

namespace Q.UI {
public class ResponsiveButtonSize: MonoBehaviour {
  public Vector2 minSize = new Vector2 (100, 50);// Set your minimum size

  void Update () {
    RectTransform rectTransform = GetComponent<RectTransform> ();
    rectTransform.sizeDelta = new Vector2 (Mathf.Max (rectTransform.sizeDelta.x, minSize.x),
    Mathf.Max (rectTransform.sizeDelta.y, minSize.y));
  }
}
}
