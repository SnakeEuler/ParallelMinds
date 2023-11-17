// PlayerController.cs
using Tocsin;
using UnityEngine;

namespace Tocsin.Tests {
public class PlayerController: MonoBehaviour {
  Tocsin<int> healthChangedTocsin = new Tocsin<int> ();
  public int health = 100;
  private void OnEnable () { healthChangedTocsin.AddListener (OnPlayerHealthChanged); }

  private void OnDisable () { healthChangedTocsin.RemoveListener (OnPlayerHealthChanged); }
  private void OnPlayerHealthChanged (int newHealth) {
    Debug.Log ($"Player's new health: {newHealth}");
  }
}
}
