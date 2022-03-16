using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizGame : MonoBehaviour
{
    private List<QuizGameQuestion> QuestionList;
    private List<QuizGameQuestion> AskedList;
    [Header("Option Text")]
    [SerializeField] private Text txtOptionOne;
    [SerializeField] private Text txtOptionTwo;
    [SerializeField] private Text txtOptionThree;
    [SerializeField] private Text txtOptionFour;
    [SerializeField] private Text txtQuestion;
    private QuizGameQuestion CurrentQuestion;
    [Header("Animator")]
    [SerializeField] private Animator animator;
    private int QuestionLeft;
    [Header("Result")]
    [SerializeField] private GameObject ResultWindow;
    [SerializeField] private Text txtScore;
    private int CorrectCount;
    [Header("Sound Effect")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip CorrectSoundEffect;
    [SerializeField] private AudioClip WrongSoundEffect;
    [Header("Answer")]
    [SerializeField] private GameObject AnswerWindow;
    [SerializeField] private GameObject AnswerSlotPrefab;
    [SerializeField] private GameObject AnswerContent;
    [SerializeField] private GameObject StartWindow;
    private void Start()
    {
        LoadQuestion();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            btnSelectMenu();
        }
    }
    private void LoadQuestion()
    {
        QuestionList = new List<QuizGameQuestion>();
        TextAsset QuestionTextAsset = Resources.Load<TextAsset>("TextAssets/QuizGame/QuizGame");
        string[] quizGameQuestionsArray = QuestionTextAsset.text.Split(new char[] { '\n' });
        for (int i = 0; i < quizGameQuestionsArray.Length; i++)
        {
            string[] QuestionDetails = quizGameQuestionsArray[i].Split(new char[] { ',' });
            QuizGameQuestion NewQuestion = new QuizGameQuestion();
            NewQuestion.Question = QuestionDetails[0];
            NewQuestion.Options = new List<string>();
            for (int j = 1; j <= 4; j++)
            {
                NewQuestion.Options.Add(QuestionDetails[j]);
            }
            NewQuestion.Answer = QuestionDetails[5];
            QuestionList.Add(NewQuestion);
        }
    }
    private void NewQuestion()
    {
        if (QuestionList.Count > 0 && QuestionLeft > 0)
        {
            QuestionLeft--;
            CurrentQuestion = QuestionList[Random.Range(0, QuestionList.Count - 1)];
            QuestionList.Remove(CurrentQuestion);
            AskedList.Add(CurrentQuestion);
            txtOptionOne.text = CurrentQuestion.Options[Random.Range(0, CurrentQuestion.Options.Count - 1)];
            CurrentQuestion.Options.Remove(txtOptionOne.text);
            txtOptionTwo.text = CurrentQuestion.Options[Random.Range(0, CurrentQuestion.Options.Count - 1)];
            CurrentQuestion.Options.Remove(txtOptionTwo.text);
            txtOptionThree.text = CurrentQuestion.Options[Random.Range(0, CurrentQuestion.Options.Count - 1)];
            CurrentQuestion.Options.Remove(txtOptionThree.text);
            txtOptionFour.text = CurrentQuestion.Options[Random.Range(0, CurrentQuestion.Options.Count - 1)];
            CurrentQuestion.Options.Remove(txtOptionFour.text);
            txtQuestion.text = CurrentQuestion.Question;
        } else
        {
            Debug.Log("All Question Asked!");
            ShowResult();
        }
    }
    public void CheckAnswer(string PlayerAnswer)
    {
        if (PlayerAnswer.Trim().Equals(CurrentQuestion.Answer.Trim()))//trim :remove all space or spacific things in text 
        {
            StartCoroutine(CorrectAnswer());
        }
        else
        {
            StartCoroutine(WrongAnswer());
            Debug.Log("Wrong");
        }
    }
    IEnumerator CorrectAnswer()
    {
        animator.SetTrigger("Correct");
        CorrectCount++;
        audioSource.PlayOneShot(CorrectSoundEffect);
        yield return new WaitForSeconds(0.5f);
        NewQuestion();
    }
    IEnumerator WrongAnswer()
    {
        animator.SetTrigger("Wrong");
        audioSource.PlayOneShot(WrongSoundEffect);
        yield return new WaitForSeconds(0.5f);
        NewQuestion();
    }
    private void ShowResult()
    {
        ResultWindow.SetActive(true);
        txtScore.text = "Score: " + CorrectCount + "/" + AskedList.Count;
    }
    public void btnAnswer()
    {
        AnswerWindow.SetActive(true);
        foreach(Transform child in AnswerContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach(QuizGameQuestion question in AskedList)
        {
            GameObject SpawnAnswerSlot = GameObject.Instantiate(AnswerSlotPrefab);
            SpawnAnswerSlot.transform.SetParent(AnswerContent.transform);
            SpawnAnswerSlot.transform.Find("txtQuestion").GetComponent<Text>().text = question.Question;
            SpawnAnswerSlot.transform.Find("txtAnswer").GetComponent<Text>().text = question.Answer;
        }
    }
    public void btnQuitAnswer()
    {
        AnswerWindow.SetActive(false);
    }
    public void btnPlayAgain()
    {
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(9));
    }
    public void btnQuestionNumber(int Number)
    {
        AskedList = new List<QuizGameQuestion>();
        QuestionLeft = Number;
        CorrectCount = 0;
        NewQuestion();
        StartCoroutine(StartWindowClose());
    }
    IEnumerator StartWindowClose()
    {
        StartWindow.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        StartWindow.SetActive(false);
    }
    public void btnSelectMenu()
    {
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(1));
    }
}
