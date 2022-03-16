using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuTimer : MonoBehaviour
{
    public Text txtTimer;
    public float SpendTime;
    public Sudoku game;
    private void Start()
    {
        game = GameObject.FindObjectOfType<Sudoku>();
    }
    private void Update()
    {
        if (!game.Pause)
        {
            SpendTime += Time.deltaTime;
            txtTimer.text = ((int)SpendTime / 60).ToString() + ":" + (SpendTime % 60).ToString("f2");
        }
    }
}
