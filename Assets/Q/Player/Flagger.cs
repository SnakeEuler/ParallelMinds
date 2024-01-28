using UnityEngine;
namespace Q {
public class Flagger: MonoBehaviour {
  public PlayerMovement playerMovement;
  public string stateName;
  public bool jumping;

  public void Update () {
    stateName = playerMovement.csm.currentState.Name;
    jumping = playerMovement.isJumping;
  }
}
}
