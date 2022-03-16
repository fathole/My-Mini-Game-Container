using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = this.transform.Find("MainMenu").GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator LoadScene(int index)
    {
        animator.SetTrigger("LoadOtherScene");
        yield return new WaitForSeconds(2);//animation length
        SceneManager.LoadScene(index);
    }


    public void btnLoadScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }
}
