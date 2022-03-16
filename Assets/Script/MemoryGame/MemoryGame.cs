using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryGame : MonoBehaviour
{
    private int CardCount;//to create card
    public bool CanClick; //wait for the checkmatch finish
    [SerializeField]private Sprite[] ImageArray;
    [SerializeField] private GameObject CardPrefab;
    [SerializeField] private GameObject CardContent;
    [SerializeField] private GameObject ClearWindow;
    private MemoryGameCard FirstCard;
    private MemoryGameCard SecondCard;
    private int CorrectCount;
    public AudioSource AudioSource;
    public AudioClip ClearSoundEffect;
    public GameObject StartWindow;

    private void Start() 
    {
        StartWindow.SetActive(true);
    }//setup
    public void CheckCard(MemoryGameCard card)
    {
        if (FirstCard == null)
        {
            FirstCard = card;
        }
        else
        {
            SecondCard = card;
            CanClick = false;
            StartCoroutine(CheckMatch());
        }
    }
    IEnumerator CheckMatch()
    {
        if (FirstCard.ID == SecondCard.ID)
        {
            //add mark here, check all clear
            CorrectCount+=2;
            if(CorrectCount == CardCount)
            {
                AudioSource.PlayOneShot(ClearSoundEffect);
                ClearWindow.SetActive(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            FirstCard.HideImage();
            SecondCard.HideImage();
        }
        CanClick = true;
        FirstCard = null;
        SecondCard = null;
    }
    public void btnRestart()//later modify(may make menu scene)
    {
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(3));
    }
    public void btnSelectMenu()
    {
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(1));
    }
    public void SetUpGame()
    {
        StartWindow.SetActive(false);
        CanClick = true;
        ClearWindow.SetActive(false);
        List<int> CardList = new List<int>();//create a id list to setup the card id and image
        for (int i = 0; i < CardCount / 2; i++)
        {
            CardList.Add(i);
            CardList.Add(i);//two same card so run two times
        }
        for (int i = 0; i < CardCount; i++)
        {
            //create card
            GameObject card = GameObject.Instantiate(CardPrefab);//generate card
            int randomID = CardList[Random.Range(0, CardList.Count - 1)];//get random id
            card.transform.SetParent(CardContent.transform);
            card.GetComponent<MemoryGameCard>().SetCard(randomID, ImageArray[randomID]);//set up the card
            CardList.Remove(randomID);//avoid same card show too much
        }
    }
    public void btnCardCount(int GetCardCount)
    {
        CardCount = GetCardCount;
        SetUpGame();
    }

}