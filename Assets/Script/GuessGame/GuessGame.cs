using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessGame : MonoBehaviour
{
    [SerializeField] private int GuessNumber;
    private int BiggestNumber;
    private int SmallestNumber;
    private int PlayerInputNumber;
    private int playerInputNumber
    {
        get { return PlayerInputNumber; }
        set
        {
            if (value >= BiggestNumber || value <= SmallestNumber)
            {
                StartCoroutine(NoobInput());
            }
            else
            {
                PlayerInputNumber = value;
                UpdateGuessGame();
            }
        }
    }
    [SerializeField] private InputField inputField;
    [SerializeField] private Text txtNumber;
    [SerializeField] private Text txtCoverText;
    [SerializeField] private Text txtNotice;
    [SerializeField] private GameObject CoverImage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip ClearSoundEffect;
    [SerializeField] private AudioClip WrongSoundEffect;
    [SerializeField] private GameObject ClearScene;

    private void Start()
    {
        ClearScene.SetActive(false);
        SetUpGuessGame();
    }
    private void SetUpGuessGame()
    {
        SmallestNumber = 0;
        BiggestNumber = 100;
        GuessNumber = Random.Range(1, 100);
        txtNumber.text = GuessNumber.ToString();
        txtNotice.text = SmallestNumber + " - " + BiggestNumber;
    }
    private void UpdateGuessGame()
    {
        inputField.text = "";
        txtNotice.text = SmallestNumber + " - " + BiggestNumber;
        if (playerInputNumber == GuessNumber)
        {
            StartCoroutine(WinCall());
        }
        else
        {
            StartCoroutine(WrongCall());
            if (playerInputNumber > GuessNumber)
            {
                BiggestNumber = playerInputNumber;
            }
            else
            {
                SmallestNumber = playerInputNumber;
            }
            txtNotice.text = SmallestNumber + " - " + BiggestNumber;
        }
    }
    public void btnConfirm()
    {
        if (inputField.text != "")
        {
            playerInputNumber = int.Parse(inputField.text);
        }else
        {
            Debug.Log("You Should Input Something In The Input Field!");
        }
    }
    public void btnNewGame()
    {
        StartCoroutine(StartNewGame());
        ClearScene.SetActive(false);
    }
    public void btnSelecrMenu()
    {
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(1));
    }
    IEnumerator WrongCall()
    {
        txtCoverText.text = "X";
        audioSource.PlayOneShot(WrongSoundEffect);
        yield return new WaitForSeconds(1);
        txtCoverText.text = "?";
    }
    IEnumerator WinCall()
    {
        CoverImage.GetComponent<Animator>().SetTrigger("RemoveCoverImage");
        audioSource.PlayOneShot(ClearSoundEffect);
        yield return new WaitForSeconds(1f);
        ClearScene.SetActive(true);
        ClearScene.GetComponent<Animator>().SetTrigger("Start");
        //ask next game 
    }
    IEnumerator StartNewGame()
    {
        ClearScene.GetComponent<Animator>().SetTrigger("End");
        CoverImage.GetComponent<Animator>().SetTrigger("Reset");
        yield return new WaitForSeconds(1f);
        SetUpGuessGame();
    }
    IEnumerator NoobInput()
    {
        txtCoverText.text = "Hello?";
        yield return new WaitForSeconds(1f);
        txtCoverText.text = "?";
    }
}
