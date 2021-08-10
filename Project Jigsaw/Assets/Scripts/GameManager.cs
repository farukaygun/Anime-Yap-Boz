using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

  public int size;
  public GameObject puzzlePiece;
  public GameObject winPanel;
  public GameObject levelPanel;
  public GameObject pausePanel;
  public GameObject panelCurrent;

  private Puzzle[,] puzzle;
  private Puzzle puzzleSelection;

  public int randomPasses = 12;
  int level;

  public Button buttonNext;
  public Button buttonExit;
  public Button buttonPause;
  public Button buttonResume;
  public Button buttonMainMenu;
  public Button buttonMusicController;

  public Sprite spriteVolumeOn;
  public Sprite spriteMute;

  bool isMute = false;

  void RandomizePlacement()
  {
    Vector2Int[] puzzleLocation = new Vector2Int[2];
    Vector2[] puzzleOffset = new Vector2[2];
    do
    {
      for (int i = 0; i < randomPasses; i++)
      {
        puzzleLocation[0].x = UnityEngine.Random.Range(0, size);
        puzzleLocation[0].y = UnityEngine.Random.Range(0, size);
        puzzleLocation[1].x = UnityEngine.Random.Range(0, size);
        puzzleLocation[1].y = UnityEngine.Random.Range(0, size);

        puzzleOffset[0] = puzzle[puzzleLocation[0].x, puzzleLocation[0].y].GetImageOffset();
        puzzleOffset[1] = puzzle[puzzleLocation[1].x, puzzleLocation[1].y].GetImageOffset();

        puzzle[puzzleLocation[0].x, puzzleLocation[0].y].AssignImage(puzzleOffset[1]);
        puzzle[puzzleLocation[1].x, puzzleLocation[1].y].AssignImage(puzzleOffset[0]);
      }

    } while (CheckBoard() == true);
  }

  void SetupBoard()
  {
    Vector2 offset;
    Vector2 m_scale = new Vector2(1f / size, 1f / size);
    for (int i = 0; i < size; i++)
    {
      for (int j = 0; j < size; j++)
      {
        offset = new Vector2(i * (1f / size), j * (1f / size));
        puzzle[i, j].AssignImage(m_scale, offset);
      }
    }
  }

  public Puzzle GetSelection()
  {
    return puzzleSelection;
  }

  public void SetSelection(Puzzle selection)
  {
    puzzleSelection = selection;
  }

  public bool CheckBoard()
  {
    for (int i = 0; i < size; i++)
    {
      for (int j = 0; j < size; j++)
      {
        if (puzzle[i, j].CheckGoodPlacement() == false)
          return false;
      }
    }
    return true;
  }

  public void Win()
  {
    winPanel.GetComponent<Animator>().SetBool("isWin", true);

    if (level >= Save.GetLevel())
    {
      Save.SetLevel(level + 1);
    }
  }

  public void LevelSelect(string levelName)
  {
    winPanel.GetComponent<Animator>().SetBool("isWin", false);

    levelPanel.SetActive(false);
    panelCurrent.SetActive(true);

    LoadLevel(levelName);
  }

  void DestroyPuzzle()
  {
    foreach (var item in GameObject.FindGameObjectsWithTag("Puzzle Image"))
    {
      Destroy(item);
    }
  }

  void GetCurrentLevel(string levelName)
  {
    string curLevel = "";
    for (int i = 0; i < levelName.Length; i++)
    {
      if (Char.IsDigit(levelName[i]))
        curLevel += levelName[i];
    }

    if (curLevel.Length > 0)
      this.level = int.Parse(curLevel);
  }

  void LoadLevel(string levelName)
  {
    DestroyPuzzle();
    GetCurrentLevel(levelName);

    winPanel.SetActive(true);
    winPanel.GetComponent<Animator>().SetBool("isWin", false);

    GameObject temp;
    puzzle = new Puzzle[size, size];

    for (int i = 0; i < size; i++)
    {
      for (int j = 0; j < size; j++)
      {
        if (level <= Save.GetLastLevel())
        {
          temp = Instantiate(Resources.Load(levelName) as GameObject, new Vector2(i * 1080 / size, j * 1920 / size), Quaternion.identity);
          temp.transform.SetParent(GameObject.Find("Current").transform);
          puzzle[i, j] = (Puzzle)temp.GetComponent<Puzzle>();
          puzzle[i, j].CreatePuzzlePiece(size);
        }
        else GoToLevelMenu();
      }
    }

    SetupBoard();
    RandomizePlacement();
  }

  void GoToLevelMenu()
  {
    pausePanel.SetActive(false);

    winPanel.GetComponent<Animator>().SetBool("isWin", false);
    winPanel.SetActive(false);

    DestroyPuzzle();

    levelPanel.SetActive(true);
    panelCurrent.SetActive(false);

    GameObject.Find("Content").GetComponent<LevelMenu>().CurrentLevelProgress();
  }

  void MusicController()
  {
    var soundTrack = GameObject.Find("Soundtrack").GetComponent<AudioSource>();
    if (!isMute)
    {
      soundTrack.Stop();
      buttonMusicController.GetComponent<Image>().sprite = spriteMute;
    }
    else if (isMute)
    {
      soundTrack.Play();
      buttonMusicController.GetComponent<Image>().sprite = spriteVolumeOn;
    }
    isMute = !isMute;
  }

  private void OnEnable()
  {
    buttonNext.onClick.AddListener(() => LoadLevel("level " + (level + 1).ToString()));
    buttonExit.onClick.AddListener(() => GoToLevelMenu());
    buttonPause.onClick.AddListener(() => pausePanel.SetActive(true));
    buttonResume.onClick.AddListener(() => pausePanel.SetActive(false));
    buttonMainMenu.onClick.AddListener(() => GoToLevelMenu());
    buttonMusicController.onClick.AddListener(() => MusicController());
  }
}
