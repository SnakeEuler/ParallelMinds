using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Q.UI {
public class SceneTransition: MonoBehaviour {
  public CanvasGroup fadePanel;
  public float fadeDuration = 1f;

  private void Start () { StartCoroutine (FadeIn ()); }

  public void LoadScene (int sceneIndex) { StartCoroutine (FadeAndLoadScene (sceneIndex)); }

  private IEnumerator FadeIn () {
    float timer = 0;
    while (timer <= fadeDuration) {
      timer += Time.deltaTime;
      fadePanel.alpha = 1 - timer / fadeDuration;
      yield return null;
    }
  }

  private IEnumerator FadeAndLoadScene (int sceneIndex) {
    // Fade out
    float timer = 0;
    while (timer <= fadeDuration) {
      timer += Time.deltaTime;
      fadePanel.alpha = timer / fadeDuration;
      yield return null;
    }

    // Load the scene
    SceneManager.LoadScene (sceneIndex);
  }
}
}
