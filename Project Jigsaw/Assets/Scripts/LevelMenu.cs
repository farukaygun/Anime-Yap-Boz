using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
  public List<Button> levelButtons;
  int count;
  GameManager gameManager;

  private void Start()
  {
    CurrentLevelProgress();
    count = transform.childCount;
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  public void CurrentLevelProgress()
  {

    int i = 1;
    foreach (var item in gameObject.GetComponentsInChildren<Button>())
    {
      item.name = i.ToString();
      item.GetComponentInChildren<TextMeshProUGUI>().GetComponent<TextMeshProUGUI>().text = item.name;
      string level = "Level " + item.name;
      item.onClick.AddListener(() => gameManager.LevelSelect(level));

      levelButtons.Add(item);
      i++;
    }

    foreach (var item in levelButtons)
    {
      if (int.Parse(item.name) > Save.GetLevel())
      {
        item.interactable = false;
      }
      else item.interactable = true;
    }
  }
}
