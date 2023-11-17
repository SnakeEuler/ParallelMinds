using System.Collections;
using System.Collections.Generic;
using Tocsin;
using UnityEngine;
namespace Tocsin {
public class TocsinBinding<TDelegate>: ITocsinBinding where TDelegate : class {
  protected ILinkBase<TDelegate> Tocsin { get; private set; }
  protected TDelegate Listener { get; private set; }

  //Constructors
  private TocsinBinding () { }
  public TocsinBinding (
  ILinkBase<TDelegate> tocsin, TDelegate listener, bool allowDuplicates,
  bool isListening): this () {
    Tocsin = tocsin;
    Listener = listener;
    this.AllowDuplicates = allowDuplicates;
    Enabled = isListening;
  }

  public bool Enabled { get; private set; }
  public bool AllowDuplicates { get; set; }

  public uint PersistentListenerCount => Tocsin.PersistentListenerCount;

  

public bool Enable (bool enable) {
  if (enable) {
    if (!Enabled) {
      if (Tocsin.AddListener (Listener, AllowDuplicates)) {
        Enabled = true;
        return true;
      }
    }
  } else {
    if (Enabled) {
      Tocsin.RemoveListener (Listener);
      Enabled = false;
      return true;
    }
  }
  return false;
}
}
}
