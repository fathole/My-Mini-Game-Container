using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuSquare : MonoBehaviour
{
    public Text txtNumber;
    public bool CanClick;
    void Start()
    {

    }
    public void SetUpSquare(string number)
    {
        if (number == "0")
        {
            txtNumber.color = new Color32(255, 0, 0, 255);
            txtNumber.text = "";
            CanClick = true;
            this.gameObject.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
        }
        else
        {
            txtNumber.text = number;
            CanClick = false;
        }
    }
    public void btnSquare()
    {
        foreach(Transform square in GameObject.Find("Game").transform.Find("GameBoard").transform)
        {
            square.gameObject.GetComponent<Animator>().SetBool("CurrentSquare", false);
        }
        if (CanClick)
        {
            this.gameObject.GetComponent<Animator>().SetBool("CurrentSquare", true);
            GameObject.FindObjectOfType<Sudoku>().CurrentSquare = this;
        }else
        {
            GameObject.FindObjectOfType<Sudoku>().CurrentSquare = null;
        }
    }
}
