using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllegoricalSquare : MonoBehaviour
{
    public Text txtWord;
    private void Start()
    {
        this.GetComponent<Rigidbody2D>().freezeRotation = true;
    }
    public void Clicked()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic =true;
        this.gameObject.transform.SetParent(GameObject.FindObjectOfType<AllegoricalGame>().AnswerSquareContent.transform);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.FindObjectOfType<AllegoricalGame>().CheckAnswer();
    }
    public void RemoveFromAnswer()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        this.gameObject.transform.SetParent(GameObject.FindObjectOfType<AllegoricalGame>().SquareGeneratorContent.transform);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
