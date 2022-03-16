using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllegoricalGame : MonoBehaviour
{
    public List<AllegoricalGameQuestion> QuestionList;
    public List<AllegoricalGameQuestion> CurrentQuestionList;
    public AllegoricalGameQuestion CurrentQuestion;

    [SerializeField] private GameObject SquarePrefab;
    [SerializeField] private GameObject SquareGenerator;
    [SerializeField] private int QuestionCount;

    [SerializeField] private Text txtQuestion;
    [SerializeField] public GameObject AnswerSquareContent;
    [SerializeField] public GameObject SquareGeneratorContent;
    [SerializeField] public GameObject StartWindow;
    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip CorrectSoundEffect;


    private void Start()
    {
        LoadQuestion();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            btnQuit();
        }
    }
    private void LoadQuestion()
    {
        QuestionList = new List<AllegoricalGameQuestion>();
        CurrentQuestionList = new List<AllegoricalGameQuestion>();
        TextAsset AllegoricalGameQuestionTextAsset = Resources.Load<TextAsset>("TextAssets/AllegoricalGame/AllegoricalText");
        string[] QuestionArray = AllegoricalGameQuestionTextAsset.text.Split(new char[] { '\n' });
        for (int i = 0; i < QuestionArray.Length; i++)
        {
            string[] QuestionAndNotice = QuestionArray[i].Split(new char[] { ',' });
            AllegoricalGameQuestion NewQuestion = new AllegoricalGameQuestion();
            NewQuestion.Answer = QuestionAndNotice[0];
            NewQuestion.Question = QuestionAndNotice[1];
            QuestionList.Add(NewQuestion);
        }
    }
    private void SetUpGame()
    {
        for (int i = 0; i < QuestionCount; i++)
        {
            GenerateQuestionSquare(QuestionList[Random.Range(0, QuestionList.Count - 1)]);
        }
        UpdateQuestion();
        
    }
    private void GenerateQuestionSquare(AllegoricalGameQuestion question)
    {
        CurrentQuestionList.Add(question);
        QuestionList.Remove(question);
        foreach (char text in question.Answer)
        {
            GameObject NewSquare = GameObject.Instantiate(SquarePrefab);
            NewSquare.transform.SetParent(SquareGenerator.transform);
            NewSquare.transform.position = new Vector2(Random.Range(SquareGenerator.transform.position.x - 1000, SquareGenerator.transform.position.x + 1000), Random.Range(SquareGenerator.transform.position.y, SquareGenerator.transform.position.y+500));
            NewSquare.GetComponent<AllegoricalSquare>().txtWord.text = text.ToString();
        }
    }
    private void UpdateQuestion()
    {
        CurrentQuestion = CurrentQuestionList[Random.Range(0, CurrentQuestionList.Count - 1)];
        CurrentQuestionList.Remove(CurrentQuestion);
        txtQuestion.text = " , "  + CurrentQuestion.Question;
    }
    public void btnClear()
    {
        foreach(Transform child in AnswerSquareContent.transform)
        {
            GenerateSquare(child.GetComponent<AllegoricalSquare>().txtWord.text);
        }
        foreach (Transform child in AnswerSquareContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void CheckAnswer()
    {
        string playerAnswer = "";
        foreach(Transform child in AnswerSquareContent.transform)
        {
            playerAnswer += child.GetComponent<AllegoricalSquare>().txtWord.text;
        }
        if(playerAnswer == CurrentQuestion.Answer)
        {
            foreach (Transform child in AnswerSquareContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            audioSource.PlayOneShot(CorrectSoundEffect);
            QuestionCount--;
            if (QuestionCount > 0)
            {
                UpdateQuestion();
            }
            else
            {
                StartCoroutine(FinishGame());
            }
        }
    }
    private void GenerateSquare(string GetWord)
    {
        GameObject NewSquare = GameObject.Instantiate(SquarePrefab);
        NewSquare.transform.SetParent(SquareGenerator.transform);
        NewSquare.transform.position = new Vector2(Random.Range(SquareGenerator.transform.position.x - 1075, SquareGenerator.transform.position.x + 1075), Random.Range(SquareGenerator.transform.position.y, SquareGenerator.transform.position.y + 500));
        NewSquare.GetComponent<AllegoricalSquare>().txtWord.text = GetWord;
    }
    public void btnSelectDifficulty(int GetQuestionCount)
    {
        QuestionCount = GetQuestionCount;
        SetUpGame();
        StartWindow.SetActive(false);
    }
    public void btnQuit()
    {
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(1));
    }
    IEnumerator FinishGame()
    {
        //some animation.
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(8));
    }
}
