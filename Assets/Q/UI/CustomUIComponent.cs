using System;
using UnityEngine;
using EasyButtons;

namespace Q.UI {
public abstract class CustomUIComponent: MonoBehaviour {
  private void Awake () { Initialize (); }

  public abstract void Setup ();
  public abstract void Configure ();

  [Button("Reconfigure")]
  public void Initialize () {
    Setup ();
    Configure ();
  }

  private void OnValidate () { Initialize (); }
}
}
