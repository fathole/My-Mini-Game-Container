using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameCard : MonoBehaviour
{
    private Animator animator;
    public Image CardImage;
    public int ID;
    public bool Clicked;
    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }
    public void ClickCard()
    {
        if (!Clicked && GameObject.FindObjectOfType<MemoryGame>().CanClick)
        {
            Clicked = true;
            animator.ResetTrigger("Hide");
            animator.SetTrigger("Show");
            GameObject.FindObjectOfType<MemoryGame>().CheckCard(this);
        }
    }
    public void HideImage()
    {
        Clicked = false;
        animator.ResetTrigger("Show");
        animator.SetTrigger("Hide");
    }
    public void SetCard(int getID, Sprite getImage)
    {
        CardImage.sprite = getImage;
        ID = getID;
    }
}
