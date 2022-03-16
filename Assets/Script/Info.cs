using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(GameObject.FindObjectOfType<LoadingSystem>().LoadScene(0));
        }
    }
}
