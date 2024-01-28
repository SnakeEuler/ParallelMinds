using UnityEngine;
namespace Q.UI {
[CreateAssetMenu(fileName = "ViewSriptable", menuName = "UI/View", order = 1)]
public class ViewSriptable: ScriptableObject {

  public ThemeScriptable theme;
  public RectOffset padding;
  public float spacing;
  
}
}
