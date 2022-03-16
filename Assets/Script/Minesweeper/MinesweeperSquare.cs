using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinesweeperSquare : MonoBehaviour
{
    public int Column;
    public int Row;
    public bool IsBoom;
    public bool IsFlagged;
    public bool DidCheck;
    public Text txtCount;
    public GameObject CoverImage;
    public GameObject FlagImage;
    public GameObject BoomImage;

    public void SetUpSquare(int index)
    {
        if (IsBoom)
        {
            BoomImage.SetActive(true); 
        }
        else
        {
            if (index == 0)
            {
                txtCount.text = "";
            }
            else
            {
                txtCount.text = index.ToString();
            }
        }
    }
    public void Clicked()
    {
        if (GameObject.FindObjectOfType<Minesweeper>().PlaceFlag)
        {
            if (!IsFlagged && CoverImage.activeSelf)
            {
                IsFlagged = true;
                FlagImage.SetActive(true);
            }
            else
            {
                IsFlagged = false;
                FlagImage.SetActive(false);
            }
            GameObject.FindObjectOfType<Minesweeper>().CheckWin();
        }
        else
        {
            if (!IsFlagged)
            {
                CoverImage.SetActive(false);
                if (txtCount.text == "" && !IsBoom)
                {
                    GameObject.FindObjectOfType<Minesweeper>().CheckNearbyMinesweeperSquare(this);
                }
                else if (IsBoom)
                {
                    StartCoroutine(GameObject.FindObjectOfType<Minesweeper>().LoseCall());
                }
            }
        }
    }
}
