using System;
using TMPro;
using UnityEngine;
using EasyButtons;
using UnityEngine.UI;

namespace Q.UI {
public class Text: CustomUIComponent {
  public TextScriptable textScriptable;
  public ThemeStyle themeStyle;

  private TextMeshProUGUI textMeshProUGUI;

  public override void Setup () { textMeshProUGUI = GetComponent<TextMeshProUGUI> (); }

  public override void Configure () {
    textMeshProUGUI.color = textScriptable.theme.GetTextColor (themeStyle);
    textMeshProUGUI.font = textScriptable.font;
    textMeshProUGUI.fontSize = textScriptable.size;
  }

}
}
