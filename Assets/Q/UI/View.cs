using System;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
namespace Q.UI {
public class View: CustomUIComponent {
  public ViewSriptable viewSriptable;

  public GameObject containerTop;
  public GameObject containerBottom;
  public GameObject containerCenter;

  private Image imageTop;
  private Image imageBottom;
  private Image imageCenter;

  private VerticalLayoutGroup verticalLayoutGroup;

  public override void Setup () {
    verticalLayoutGroup = GetComponent<VerticalLayoutGroup> ();
    imageTop = containerTop.GetComponent<Image> ();
    imageBottom = containerBottom.GetComponent<Image> ();
    imageCenter = containerCenter.GetComponent<Image> ();
  }

  public override void Configure () {
    verticalLayoutGroup.spacing = viewSriptable.spacing;
    verticalLayoutGroup.padding = viewSriptable.padding;

    imageTop.color = viewSriptable.theme.primaryBackground;
    imageBottom.color = viewSriptable.theme.secondaryBackground;
    imageCenter.color = viewSriptable.theme.tertiaryBackground;
  }
}
}
