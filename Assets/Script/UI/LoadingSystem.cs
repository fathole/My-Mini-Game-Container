using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSystem : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }
    public IEnumerator LoadScene(int SceneIndex)
    {
        animator.SetTrigger("LoadingStart");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneIndex);
    }
}
