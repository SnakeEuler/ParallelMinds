// GameManager.cs
using System;
using ParallelMinds.StateMachine;
using Tocsin;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tocsin.Tests {
public class GameManager: MonoBehaviour {
  public static GameManager instance;
  public HFSM fsm;
  public InputChannel inputChannel;
  public Tocsin<int> onPlayerHealthChanged;

  [SerializeField]
  private PlayerController playerController;

  private void Awake () {
    instance = this;
    onPlayerHealthChanged = new Tocsin<int> ();
    Debug.Log ("GameManager awake");
    DontDestroyOnLoad (gameObject);

    // Initialize the FSM and input channel
    fsm = new HFSM ();
    InitializeStateMachine ();
  }

  private void OnEnable () {
    // Subscribe to input channel events
    inputChannel.AddPauseListener (HandlePausePressed);
  }

  private void OnDisable () {
    // Unsubscribe from input channel events
    inputChannel.RemovePauseListener (HandlePausePressed);
  }

  private void InitializeStateMachine () {
    // Initialize states and transitions
    fsm.AddState ("Play").
        UponEnteringDo (() => Debug.Log ("Game Playing")).
        UponExitingDo (() => Debug.Log ("Game Exiting Play State"));

    fsm.AddState ("Pause").
        UponEnteringDo (() => Debug.Log ("Game Paused")).
        UponExitingDo (() => Debug.Log ("Game Unpaused"));

    // Start in the Play state
    fsm.ChangeState ("Play");
  }

  private void HandlePausePressed (bool isPaused) {
    // Toggle between Play and Pause states
    string newState = fsm.currentState.Name == "Pause"? "Play" : "Pause";
    fsm.ChangeState (newState);
  }

  public void PlayerTakesDamage (int damage) {
    int newHealth = CalculateHealthAfterDamage (damage);
    onPlayerHealthChanged.Dispatch (newHealth);
  }

  private int CalculateHealthAfterDamage (int damage) {
    return Mathf.Max (0, playerController.health - damage);
  }

  // Example methods for PlayerController to update health
  public void UpdatePlayerHealth (int newHealth) {
    playerController.health = newHealth;
    // Handle health change logic...
  }
}
}
