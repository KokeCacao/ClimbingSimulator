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
    menu.SetActive(false);
    minimap.SetActive(true);
  }

  public void QuitGame()
  {
    Debug.Log("Quit");
    Application.Quit();
  }
}
