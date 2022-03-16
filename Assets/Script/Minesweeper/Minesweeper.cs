using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{
    public GameObject[,] MinesweeperSquaresArray;
    private int Columns;
    private int Rows;
    private int Booms;
    public GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GameObject MinesweeperSquarePrefab;
    public List<MinesweeperSquare> CheckMinesweeperSquareList = new List<MinesweeperSquare>();
    public bool PlaceFlag;
    [SerializeField] private GameObject PlaceFlagButton;
    [SerializeField] private GameObject PauseWindow;
    public bool IsPause;
    public bool IsCleared;
    public bool IsLosed;
    [SerializeField] private GameObject Barrier;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip LoseSoundEffect;
    [SerializeField] private AudioClip ClearSoundEffect;
    [SerializeField] private GameObject StartWindow;
    [SerializeField] private InputField IFColumns;
    [SerializeField] private InputField IFRows;
    [SerializeField] private InputField IFBooms;

    void Start()
    {
        StartWindow.SetActive(true);
    }
    private void SetUpGame(int Columns, int Rows, int Booms)
    {
        StartWindow.SetActive(false);
        Barrier.SetActive(false);
        if (Columns <= 21 && Rows <= 10 && Booms < (Columns * Rows))
        {
            MinesweeperSquaresArray = new GameObject[Columns, Rows];
            if (Columns > Rows)
            {
                gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayoutGroup.constraintCount = Columns;
            }
            else if (Rows > Columns)
            {
                gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                gridLayoutGroup.constraintCount = Rows;
            }
            else
            {
                gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayoutGroup.constraintCount = Columns;
            }
            GenerateMinesweeperSquare();
            GenerateBoom();
            SetUpMinesweeperSquare();
        }
        else
        {
            Debug.Log("Not Available");
        }
    }
    private void GenerateMinesweeperSquare()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                GameObject SpawnMinesweeperSquare = GameObject.Instantiate(MinesweeperSquarePrefab);
                SpawnMinesweeperSquare.transform.SetParent(gridLayoutGroup.gameObject.transform);
                MinesweeperSquaresArray[column, row] = SpawnMinesweeperSquare;
                SpawnMinesweeperSquare.GetComponent<MinesweeperSquare>().Column = column;
                SpawnMinesweeperSquare.GetComponent<MinesweeperSquare>().Row = row;

                ///look like this 
                ///{0,0}, {0,1}, {0,2}
                ///{1,0}, {1,1}. {1,2}
                ///{2,0}, {2,1}, {2,2}
                ///so when column + = x+
                ///column - = x-
                ///row - = y+
                ///row+ = y-
            }
        }

    }
    private void GenerateBoom()
    {
        int CurrentBoomsCount = 0;
        while (CurrentBoomsCount < Booms)
        {
            int randomX, randomY;
            randomX = Random.Range(0, Columns);
            randomY = Random.Range(0, Rows);
            if (!MinesweeperSquaresArray[randomX, randomY].GetComponent<MinesweeperSquare>().IsBoom)
            {
                MinesweeperSquaresArray[randomX, randomY].GetComponent<MinesweeperSquare>().IsBoom = true;
                CurrentBoomsCount++;
            }
        }
    }
    private void SetUpMinesweeperSquare()
    {
        for (int column = 0; column < Columns; column++)
        {
            for (int row = 0; row < Rows; row++)
            {
                int AroundBoomCount = 0;
                if (column + 1 < Columns)//east
                {
                    if (MinesweeperSquaresArray[column + 1, row].GetComponent<MinesweeperSquare>().IsBoom)
                    {
                        AroundBoomCount++;
                    }
                }
                if (column - 1 >= 0)//west
                {
                    if (MinesweeperSquaresArray[column - 1, row].GetComponent<MinesweeperSquare>().IsBoom)
                    {
                        AroundBoomCount++;
                    }
                }
                if (row + 1 < Rows)//south
                {
                    if (MinesweeperSquaresArray[column, row + 1].GetComponent<MinesweeperSquare>().IsBoom)
                    {
                        AroundBoomCount++;
                    }
                }
                if (row - 1 >= 0)//north
                {
                    if (MinesweeperSquaresArray[column, row - 1].GetComponent<MinesweeperSquare>().IsBoom)
                    {
                        AroundBoomCount++;
                    }
                }
                if (column + 1 < Columns && row - 1 >= 0) //northeast
                {
                    if (MinesweeperSquaresArray[column + 1, row - 1].GetComponent<MinesweeperSquare>().IsBoom)
                    {
                        AroundBoomCount++;
                    }
                }
                if (column + 1 < Columns && row + 1 < Rows) //southeast
                {
                    if (MinesweeperSquaresArray[column + 1, row + 1].GetComponent<MinesweeperSquare>().IsBoom)
                    {
                        AroundBoomCount++;
                    }
                }
                if (column - 1 >= 0 && row + 1 < Rows)//southwest
                {
                    if (MinesweeperSquaresArray[column - 1, row + 1].GetComponent<MinesweeperSquare>().IsBoom)
                    {
                        AroundBoomCount++;
                    }
                }
                if (column - 1 >= 0 && row - 1 >= 0)//northwest
                {
                    if (MinesweeperSquaresArray[column - 1, row - 1].GetComponent<MinesweeperSquare>().IsBoom)
                    {
                        AroundBoomCount++;
                    }
                }

                MinesweeperSquaresArray[column, row].GetComponent<MinesweeperSquare>().SetUpSquare(AroundBoomCount);
            }
        }
    }
    public void CheckNearbyMinesweeperSquare(MinesweeperSquare GetMinesweeperSquare)
    {
        int column = GetMinesweeperSquare.Column;
        int row = GetMinesweeperSquare.Row;
        if (column + 1 < Columns)//east
        {
            CheckMinesweeperSquareAt(column + 1, row);
        }
        if (column - 1 >= 0)//west
        {
            CheckMinesweeperSquareAt(column - 1, row);
        }
        if (row + 1 < Rows)//south
        {
            CheckMinesweeperSquareAt(column, row + 1);
        }
        if (row - 1 >= 0)//north
        {
            CheckMinesweeperSquareAt(column, row - 1);
        }
        if (column + 1 < Columns && row - 1 >= 0) //northeast
        {
            CheckMinesweeperSquareAt(column + 1, row - 1);
        }
        if (column + 1 < Columns && row + 1 < Rows) //southeast
        {
            CheckMinesweeperSquareAt(column + 1, row + 1);
        }
        if (column - 1 >= 0 && row + 1 < Rows)//southwest
        {
            CheckMinesweeperSquareAt(column - 1, row + 1);
        }
        if (column - 1 >= 0 && row - 1 >= 0)//northwest
        {
            CheckMinesweeperSquareAt(column - 1, row - 1);
        }
        for (int i = CheckMinesweeperSquareList.Count - 1; i >= 0; i--)
        {
            if (CheckMinesweeperSquareList[i].DidCheck)
            {
                CheckMinesweeperSquareList.RemoveAt(i);
            }
        }
        if (CheckMinesweeperSquareList.Count > 0)
        {
            CheckCheckMinesweeperSquareList();
        }
    }
    private void CheckMinesweeperSquareAt(int column, int row)
    {
        GameObject MinesweeperSquare = MinesweeperSquaresArray[column, row];
        if (MinesweeperSquare.GetComponent<MinesweeperSquare>().txtCount.text == "" && !MinesweeperSquare.GetComponent<MinesweeperSquare>().IsBoom)
        {
            CheckMinesweeperSquareList.Add(MinesweeperSquare.GetComponent<MinesweeperSquare>());
        }
        else if (MinesweeperSquare.GetComponent<MinesweeperSquare>().txtCount.text == "1" || MinesweeperSquare.GetComponent<MinesweeperSquare>().txtCount.text == "2" || MinesweeperSquare.GetComponent<MinesweeperSquare>().txtCount.text == "3" ||
            MinesweeperSquare.GetComponent<MinesweeperSquare>().txtCount.text == "4" || MinesweeperSquare.GetComponent<MinesweeperSquare>().txtCount.text == "5" || MinesweeperSquare.GetComponent<MinesweeperSquare>().txtCount.text == "6" ||
            MinesweeperSquare.GetComponent<MinesweeperSquare>().txtCount.text == "7" || MinesweeperSquare.GetComponent<MinesweeperSquare>().txtCount.text == "8")
        {
            MinesweeperSquare.GetComponent<MinesweeperSquare>().CoverImage.SetActive(false);
        }
    }
    private void CheckCheckMinesweeperSquareList()
    {
        for (int i = 0; i < CheckMinesweeperSquareList.Count; i++)
        {
            CheckMinesweeperSquareList[i].DidCheck = true;
            CheckMinesweeperSquareList[i].CoverImage.SetActive(false);
            CheckNearbyMinesweeperSquare(CheckMinesweeperSquareList[i]);
        }
    }
    public void CheckWin()
    {
        int CorrectFlagCount = 0;
        foreach (GameObject child in MinesweeperSquaresArray)
        {
            if (child.GetComponent<MinesweeperSquare>().IsBoom && child.GetComponent<MinesweeperSquare>().IsFlagged)
            {
                CorrectFlagCount++;
            }
        }
        if (CorrectFlagCount == Booms)
        {
            WinCall();
        }
    }
    public void btnFlag()
    {
        if (PlaceFlag)
        {
            PlaceFlag = false;
            PlaceFlagButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            PlaceFlag = true;
            PlaceFlagButton.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
        }
    }
    public void btnPause()
    {
        if (IsPause)
        {
            IsPause = false;
            PauseWindow.SetActive(false);
            gridLayoutGroup.gameObject.SetActive(true);
        }
        else
        {
            IsPause = true;
            PauseWindow.SetActive(true);
            gridLayoutGroup.gameObject.SetActive(false);
        }
    }
    public void btnSelectMenu()
    {
       StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(1));
    }
    public void btnNewGame()
    {
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(2));
    }
    private void WinCall()
    {
        audioSource.PlayOneShot(ClearSoundEffect);
        Barrier.SetActive(true);
        btnPause();
        IsCleared = true;
    }
    public IEnumerator LoseCall()
    {
        audioSource.PlayOneShot(LoseSoundEffect);
        Barrier.SetActive(true);
        IsLosed = true;
        //play sound effect
        yield return new WaitForSeconds(2f);
        btnPause();
    }
    ///
    public void btnStart()
    {
        try
        {
            if (int.Parse(IFColumns.text) < 21)
            {
                Columns = int.Parse(IFColumns.text.ToString());
            }
        }
        catch
        {
            Columns = Random.Range(3,21);
        }

        try
        {
            if (int.Parse(IFRows.text) < 10)
            {
                Rows = int.Parse(IFRows.text.ToString());
            }
        }
        catch
        {
            Rows = Random.Range(3,10);
        }

        try
        {
            if (int.Parse(IFBooms.text) < int.Parse(IFColumns.text) * int.Parse(IFRows.text))
            {
                Booms = int.Parse(IFBooms.text.ToString());
            }
        }
        catch
        {
            Booms = 1+ Random.Range(Columns * Rows * 1 / 10, Columns * Rows * 3 / 10);
        } 
        SetUpGame(Columns, Rows, Booms);
    }
}
