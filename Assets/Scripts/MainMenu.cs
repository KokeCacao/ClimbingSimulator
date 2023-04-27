using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  GameObject menu;
  GameObject minimap;

  public void Awake()
  {
    menu = GameObject.Find("Menu");
    minimap = GameObject.Find("MinimapObject");
    
    menu.SetActive(true);
    minimap.SetActive(false);
  }

  public void PlayGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void DisableMenu()
  {
    // does nothing when player in game
    // go to game when player in menu
    menu.SetActive(false);
    minimap.SetActive(true);
  }

  public void EnableMenu()
  {
    // go to menu when player in game
    // quit game when player in menu
    if (menu.activeSelf) {
      QuitGame();
      return;
    }
    menu.SetActive(true);
    minimap.SetActive(false);
    Scene scene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(scene.name);
  }

  public void QuitGame()
  {
    Debug.Log("Quit");
    Application.Quit();
  }
}
