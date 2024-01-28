using UnityEngine;
namespace Q.UI {
[CreateAssetMenu (fileName = "Theme", menuName = "UI/Theme", order = 1)]
public class ThemeScriptable: ScriptableObject {
  [Header ("Primary")]
  public Color primaryBackground;
  public Color primaryText;

  [Header ("Secondary")]
  public Color secondaryBackground;
  public Color secondaryText;

  [Header ("Tertiary")]
  public Color tertiaryBackground;
  public Color tertiaryText;

  [Header ("Misc")]
  public Color accent;
  public Color disable;

  public Color GetBackgroundColor (ThemeStyle style) {
    return style switch {
      ThemeStyle.Primary   => primaryBackground,
      ThemeStyle.Secondary => secondaryBackground,
      ThemeStyle.Tertiary  => tertiaryBackground,
      _                    => primaryBackground
    };
  }

  public Color GetTextColor (ThemeStyle style) {
    return style switch {
      ThemeStyle.Primary   => primaryText,
      ThemeStyle.Secondary => secondaryText,
      ThemeStyle.Tertiary  => tertiaryText,
      _                    => primaryText
    };
  }
}
}
