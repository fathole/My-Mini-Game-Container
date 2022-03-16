using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sudoku : MonoBehaviour
{
    public List<SudokuQuestionData> QuestionList;
    public SudokuQuestionData CurrentQuestion;
    [SerializeField]private GameObject SettingWindow;
    [SerializeField]private GameObject ClearWindow;
    public SudokuSquare CurrentSquare;
    public AudioClip ClearSoundEffect;
    public AudioSource audioSource;
    public bool Pause;

    void Start()
    {
        QuestionList = new List<SudokuQuestionData>();
        TextAsset QuestionAsset= new TextAsset();
        //get question textasset
        PlayerPrefs.SetInt("Difficulty", Random.Range(1, 4));
        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            QuestionAsset = Resources.Load<TextAsset>("TextAssets/Sudoku/Easy");
        }
        else if (PlayerPrefs.GetInt("Difficulty") ==2)
        {
            QuestionAsset = Resources.Load<TextAsset>("TextAssets/Sudoku/Normal");
        }
        else if(PlayerPrefs.GetInt("Difficulty")==3)
        {
            QuestionAsset = Resources.Load<TextAsset>("TextAssets/Sudoku/Hard");
        }
        string[] QuestionDataArray = QuestionAsset.text.Split(new char[] { '\n' });
        for (int i = 0; i < QuestionDataArray.Length; i++)
        {
            string[] QuestionAndAnswer = QuestionDataArray[i].Split(new char[] { '|' });
            SudokuQuestionData NewQuestionData = new SudokuQuestionData();
            NewQuestionData.Question = QuestionAndAnswer[0].Split(new char[] { ',' });
            NewQuestionData.Answer = QuestionAndAnswer[1].Split(new char[] { ',' });
            QuestionList.Add(NewQuestionData);
        }
        SetUpGame();
    }
    private void SetUpGame()
    {
        if(QuestionList.Count > 0)
        {
            GameObject.FindObjectOfType<SudokuTimer>().SpendTime = 0;
            CurrentQuestion = QuestionList[Random.Range(0, QuestionList.Count - 1)];
            QuestionList.Remove(CurrentQuestion);//avoid same question show again
            for (int i = 0; i < 81; i++)
            {
                GameObject SpawnSquare = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Sudoku_Square"));
                SpawnSquare.transform.SetParent(this.gameObject.transform.Find("GameBoard").transform);
                SpawnSquare.GetComponent<SudokuSquare>().SetUpSquare(CurrentQuestion.Question[i]);
            }
        }
        else
        {
            Debug.Log("No New Game");
            btnSelectMenu();
        }
    }
    public void btnSetting()
    {
        Pause = true;
       SettingWindow.SetActive(true);
    }
    public void btnNumber(string number)
    {
        if (CurrentSquare != null)
        {
            CurrentSquare.txtNumber.text = number;
            CheckAnswer();
        }
    }
    public void CheckAnswer()
    {
        int CorrectIndex = 0;
        List<string> PlayerAnswerList = new List<string>();
        foreach (Transform child in this.gameObject.transform.Find("GameBoard").transform)
        {
            PlayerAnswerList.Add(child.GetComponent<SudokuSquare>().txtNumber.text);
        }
        for (int i = 0; i < 81; i++)
        {
            if (int.Parse(PlayerAnswerList[i]) == int.Parse(CurrentQuestion.Answer[i]))
            {
                CorrectIndex++;
            }
            else
            {
                Debug.Log("Index: " + i + ", is wrong");
            }
        }
        if (CorrectIndex == 81)
        {

            ClearSudoku();
        }
    }
    public void ClearSudoku()
    {
        audioSource.PlayOneShot(ClearSoundEffect);
        ClearWindow.SetActive(true);
        int difficulty = PlayerPrefs.GetInt("Difficulty");
        float BestTime;
        SudokuTimer timer = GameObject.FindObjectOfType<SudokuTimer>();
        switch (difficulty)
        {
            case 1:
                BestTime = PlayerPrefs.GetFloat("Difficulty1BestTime");
                if(timer.SpendTime < BestTime)
                {
                    BestTime = (int)timer.SpendTime;
                    PlayerPrefs.SetFloat("Difficulty1BestTime", BestTime);
                }
                return;
            case 2:
                BestTime = PlayerPrefs.GetFloat("Difficulty2BestTime");
                if (timer.SpendTime < BestTime)
                {
                    BestTime = (int)timer.SpendTime;
                    PlayerPrefs.SetFloat("Difficulty1BestTime", BestTime);
                }
                return;
            case 3:
                BestTime = PlayerPrefs.GetFloat("Difficulty3BestTime");
                if (timer.SpendTime < BestTime)
                {
                    BestTime = (int)timer.SpendTime;
                    PlayerPrefs.SetFloat("Difficulty1BestTime", BestTime);
                }
                return;
        }
    }
    //setting
    public void btnBack()
    {
        if (ClearWindow.activeSelf)
        {
            ClearWindow.SetActive(false);
        }
        Pause = false;
        SettingWindow.SetActive(false);
    }
    public void btnRestart()
    {
        foreach (Transform child in this.gameObject.transform.Find("GameBoard").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < 81; i++)
        {
            GameObject spawnSquare = GameObject.Instantiate(Resources.Load("Prefab/Sudoku_Square") as GameObject);
            spawnSquare.transform.SetParent(this.gameObject.transform.Find("GameBoard").transform);
            spawnSquare.GetComponent<SudokuSquare>().SetUpSquare(CurrentQuestion.Question[i]);
        }
        btnBack();
    }
    public void btnNewGame()
    {
        foreach (Transform child in this.gameObject.transform.Find("GameBoard").transform)
        {
            GameObject.Destroy(child.gameObject);
        }   
        SetUpGame();
        btnBack();
    }
    public void btnSelectMenu()
    {
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(1));
    }
    public void btnDifficulty(int index)
    {
        if(index != PlayerPrefs.GetInt("Difficulty"))
        {
            foreach(Transform child in this.gameObject.transform.Find("GameBoard").transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            QuestionList = new List<SudokuQuestionData>();
            TextAsset QuestionAsset = new TextAsset();
            PlayerPrefs.SetInt("Difficulty", index);
            if (PlayerPrefs.GetInt("Difficulty") == 1)
            {
                QuestionAsset = Resources.Load<TextAsset>("TextAssets/Sudoku/Easy");
            }
            else if (PlayerPrefs.GetInt("Difficulty") == 2)
            {
                QuestionAsset = Resources.Load<TextAsset>("TextAssets/Sudoku/Normal");
            }
            else if (PlayerPrefs.GetInt("Difficulty") == 3)
            {
                QuestionAsset = Resources.Load<TextAsset>("TextAssets/Sudoku/Hard");
            }
            string[] QuestionDataArray = QuestionAsset.text.Split(new char[] { '\n' });
            for (int i = 0; i < QuestionDataArray.Length; i++)
            {
                string[] QuestionAndAnswer = QuestionDataArray[i].Split(new char[] { '|' });
                SudokuQuestionData NewQuestionData = new SudokuQuestionData();
                NewQuestionData.Question = QuestionAndAnswer[0].Split(new char[] { ',' });
                NewQuestionData.Answer = QuestionAndAnswer[1].Split(new char[] { ',' });
                QuestionList.Add(NewQuestionData);
            }
            SetUpGame();
            btnBack();
        }
        else
        {
            btnNewGame();
        }
    }
}
