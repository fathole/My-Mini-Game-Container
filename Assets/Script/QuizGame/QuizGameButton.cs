using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizGameButton : MonoBehaviour
{
    public Text txtOption;
    public void btnClicked()
    {
        GameObject.FindObjectOfType<QuizGame>().CheckAnswer(txtOption.text);
    }
}
