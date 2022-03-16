using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGameItem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyItem());
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(GameObject.FindObjectOfType<DodgeGameSpawnItems>().TimeToDestroyItem);
        GameObject.Destroy(this.gameObject);
    }
}
