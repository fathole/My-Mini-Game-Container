using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGameGameOverSceneScript : MonoBehaviour
{
    private DodgeGame dodgeGame;
    private Animator animator;

    void Start()
    {
        dodgeGame = GameObject.FindObjectOfType<DodgeGame>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void ActiveGameOverScene()
    {
        animator.SetTrigger("Start");
    }
    public void btnRestart()
    {
        dodgeGame.Restart();
    }
    public void btnbackToSelectMenu()
    {
        StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(1));
    }
    public void InactiveGameOverScene()
    {
        animator.SetTrigger("RestartTrigger");
        animator.ResetTrigger("Start");
    }
}
