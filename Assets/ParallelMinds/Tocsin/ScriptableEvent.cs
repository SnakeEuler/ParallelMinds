using System;
using UnityEngine;
namespace Tocsin {
[CreateAssetMenu(fileName = "New Event", menuName = "Events/Tocsin")]
public class ScriptableEvent<T>: ScriptableObject where T : new () {
  private Tocsin<T> tocsin = new Tocsin<T> ();

  public void Invoke (T value) { tocsin.Dispatch (value); }

  public void Register (Action<T> eventListener) { tocsin.AddListener (eventListener); }

  public void Unregister (Action<T> eventListener) { tocsin.RemoveListener (eventListener); }
}
}
