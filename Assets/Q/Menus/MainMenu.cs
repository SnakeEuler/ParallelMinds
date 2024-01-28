using System.Collections;
using System.Collections.Generic;
using Q.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Q.Menus {
public class MainMenu: MonoBehaviour {
  // This method is linked to the Play Game button
  public void PlayGame () {
    // Load the scene with the specified build index
    // Make sure this scene is added in the Build Settings
    int sceneIndexToLoad = 1;// You can change this as needed

    FindObjectOfType<SceneTransition> ().
        LoadScene (sceneIndexToLoad);// Assuming 1 is your game scene index    } else {

  }

// This method is linked to the Quit Game button
  public void QuitGame () {
    // For testing in the Unity Editor
    Debug.Log ("Quit!");

    // This will work in a built application
    Application.Quit ();
  }

// Placeholder for the Settings button functionality
  public void OpenSettings () {
    // TODO: Implement settings functionality
    Debug.Log ("Settings Opened!");
  }

// Optional: Confirm Quit functionality
  public void ConfirmQuit () {
    // Display a confirmation dialogue here
    // For simplicity, calling QuitGame directly
    QuitGame ();
  }
}
}
