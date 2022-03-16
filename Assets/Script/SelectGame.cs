using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGame : MonoBehaviour
{
    LoadingSystem loadingSystem;

    private void Start()
    {
        loadingSystem = GameObject.FindObjectOfType<LoadingSystem>();
    }

    public void btnLoadScene(int index)
    {
        StartCoroutine(loadingSystem.LoadScene(index));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(loadingSystem.LoadScene(0));
        }
    }
}
