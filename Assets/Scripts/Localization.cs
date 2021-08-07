using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class Localization : MonoBehaviour
{
  public Button buttonTR;
  public Button buttonENG;

  private void Start()
  {
    ChangeLanguage(Save.GetLanguage()); // oyun çalıştığında dili değiştir.
  }

  void ChangeLanguage(int index)
  {
    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    Save.SetLanguage(index);
  }

  private void OnEnable()
  {
    buttonENG.onClick.AddListener(() => ChangeLanguage(0));
    buttonTR.onClick.AddListener(() => ChangeLanguage(1));
  }
}