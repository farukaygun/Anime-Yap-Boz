using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

public class MainMenu : MonoBehaviour
{
  public Button buttonPlay;
  public Button buttonExit;

  public Loading loading;

  void Play()
  {
      loading.LoadLevel(1);
  }

  void Exit()
  {
      Application.Quit();
  }

  private void OnEnable()
  {
      buttonPlay.onClick.AddListener(() => Play());
      buttonExit.onClick.AddListener(() => Exit());
  }
}
