using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGameSpawnItems : MonoBehaviour
{
    public GameObject[] Items;
    private DodgeGame dodgeGame;
    public float time;
    public float TimeToDestroyItem;
    private void Start()
    {
        dodgeGame = GameObject.FindObjectOfType<DodgeGame>();
    }
    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(5, 15));
        if (dodgeGame.Gaming)
        {
            GameObject SpawnItem = GameObject.Instantiate(Items[Random.Range(0, Items.Length)]);
            SpawnItem.transform.SetParent(GameObject.Find("ItemContainer").transform);
            float RandomY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float RandomX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            SpawnItem.transform.position = new Vector2(RandomX, RandomY);
            StartCoroutine(Spawn());
        }
    }
    private void Update()
    {
        if (dodgeGame.GameOver)
        {
            StopAllCoroutines();
        }
    }
}
