using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
  Vector2 goodOffset;
  Vector2 offset;
  Vector2 scale;
  GameManager gameBoard;

  void Start()
  {
    gameBoard = (GameManager)GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<GameManager>();
  }

  public void CreatePuzzlePiece(int size)
  {
    transform.localScale = new Vector3(transform.localScale.x / size, transform.localScale.z / size, 1);
  }

  public void AssignImage(Vector2 scale, Vector2 offset)
  {
    goodOffset = offset;
    this.scale = scale;
    AssignImage(offset);
  }

  public void AssignImage(Vector2 offset)
  {
    this.offset = offset;
    GetComponent<RawImage>().uvRect = new Rect(offset.x, offset.y, scale.x, scale.y);
  }

  public void OnClick()
  {
    Puzzle previousSelection = gameBoard.GetSelection();
    if (previousSelection != null)
    {
      previousSelection.GetComponent<RawImage>().color = Color.white;
      Vector2 tempOffset = previousSelection.GetImageOffset();
      previousSelection.AssignImage(offset);
      AssignImage(tempOffset);
      gameBoard.SetSelection(null);
      if (gameBoard.CheckBoard() == true)
      {
        gameBoard.Win();
      }
    }
    else
    {
      GetComponent<RawImage>().color = Color.gray;
      gameBoard.SetSelection(this);
    }
  }

  public Vector2 GetImageOffset()
  {
    return offset;
  }

  public bool CheckGoodPlacement()
  {
    return (goodOffset == offset);
  }

}
