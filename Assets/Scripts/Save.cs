using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Save : MonoBehaviour
{
  private void Start()
  {
    int isFirstRun = PlayerPrefs.GetInt("isFirstRun");
    if (isFirstRun == 0)
    {
      PlayerPrefs.SetInt("isFirstRun", 1);

      PlayerPrefs.SetInt("level", 1);
      PlayerPrefs.SetInt("lastLevel", 78);

      // Sistem diline göre dili ayarlıyor.
      switch (Application.systemLanguage)
      {
        case SystemLanguage.English:
          LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
          SetLanguage(0);
          break;
        case SystemLanguage.Turkish:
          LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
          SetLanguage(1);
          break;

        default:   // dil yoksa direkt olarak ingilizce yapıyor.
          LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
          SetLanguage(0);
          break;
      }
    }
  }

  public static int GetLevel()
  {
    return PlayerPrefs.GetInt("level");
  }

  public static void SetLevel(int level)
  {
    PlayerPrefs.SetInt("level", level);
  }

  public static int GetLastLevel()
  {
    return PlayerPrefs.GetInt("lastLevel");
  }

  public static void SetLanguage(int language)
  {
    PlayerPrefs.SetInt("language", language);
  }

  public static int GetLanguage()
  {
    return PlayerPrefs.GetInt("language");
  }
}
