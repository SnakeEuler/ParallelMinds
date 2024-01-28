using TMPro;
using UnityEngine;
namespace Q.UI {
[CreateAssetMenu (menuName = "UI/TextScriptable", fileName = "Text")]
public class TextScriptable: ScriptableObject {
  public ThemeScriptable theme;

  public TMP_FontAsset font;
  public float size;


}
}
