using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeGame : MonoBehaviour
{
    public bool GameOver;
    public bool Gaming;
    public DodgeGamePlayer player;
    private BulletController bulletController;
    private DodgeGameUIController UIController;
    private DodgeGameGameOverSceneScript gameOverSceneScript;
    private DodgeGameSoundEffect soundEffect;
    private GameObject Shooter;
    public GameObject ShooterParent;
    private DodgeGameSpawnItems spawnItems;
    public int HighestScore;
    public int LongestTime;
    public GameObject Lv1Shooter, Lv4Shooter, LvXShooter;
    public GameObject ShowRecordWindow;
    public Text txtHighestScrore, txtLongestTime;  


    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<DodgeGamePlayer>();
        bulletController = GameObject.FindObjectOfType<BulletController>();
        UIController = GameObject.FindObjectOfType<DodgeGameUIController>();
        gameOverSceneScript = GameObject.FindObjectOfType<DodgeGameGameOverSceneScript>();
        spawnItems = GameObject.FindObjectOfType<DodgeGameSpawnItems>();
        soundEffect = GameObject.FindObjectOfType<DodgeGameSoundEffect>();
        HighestScore = PlayerPrefs.GetInt("HighestScore");
        LongestTime = PlayerPrefs.GetInt("LongestTime");
        ActiveShowRecord();
    }
    private void ActiveShowRecord()
    {
        ShowRecordWindow.SetActive(true);
        txtHighestScrore.text = "最高分數: " + HighestScore.ToString();
        txtLongestTime.text = "最長時間: " + LongestTime.ToString();
    }
    public void GameOverCall()
    {
        soundEffect.GameOverSoundEffect();
        GameOver = true;
        StopAllCoroutines();
        foreach (Transform child in ShooterParent.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach(Transform child in GameObject.Find("BulletContainer").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("ItemContainer").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if(UIController.Score > HighestScore)
        {
            HighestScore = (int)UIController.Score;
            PlayerPrefs.SetInt("HighestScore", HighestScore);
        }
        if(UIController.SurviveTime > LongestTime)
        {
            LongestTime = (int)UIController.SurviveTime;
            PlayerPrefs.SetInt("LongestTime", LongestTime);
        }
        gameOverSceneScript.ActiveGameOverScene();

    }
    public void Restart()
    {
        ActiveShowRecord();
        GameOver = false;
        Gaming = false;
        player.gameObject.transform.position = new Vector3(0, 0, -1);
        player.HP = 3;
        player.moveSpeed = 5;
        UIController.SurviveTime = 0;
        UIController.Score = 0;
        UIController.txtLevel.text = "Level 1";
        gameOverSceneScript.InactiveGameOverScene();
        UIController.txtNotice.SetActive(true);
    }
    public void Update()
    {
        if (Input.anyKeyDown && !Gaming)
        {
            Gaming = true;
            Lv1Shooter.SetActive(true);
            StartCoroutine(spawnItems.Spawn());
            StartCoroutine(Level2());
            bulletController.BulletSpeed = 3;
            UIController.txtNotice.SetActive(false);
            ShowRecordWindow.SetActive(false);
            foreach (Transform child in Lv1Shooter.transform)
            {
                int RandomIndex = Random.Range(0, 3);
                child.GetComponent<DodgeGameShooter>().BulletPrefab = Resources.Load("DodgeGame/Bullet") as GameObject;
            }
        }
    }
    IEnumerator Level2()
    {
        yield return new WaitForSeconds(10);
        UIController.txtLevel.text = "Level 2";
        foreach (Transform child in Lv1Shooter.transform)
        {
            int RandomIndex = Random.Range(0, 3);
            child.GetComponent<DodgeGameShooter>().BulletPrefab = Resources.Load("DodgeGame/BulletLarger") as GameObject;
        }
        bulletController.BulletSpeed++;
        StartCoroutine(Level3());
    }
    IEnumerator Level3()
    {
        yield return new WaitForSeconds(10);
        UIController.txtLevel.text = "Level 3";
        foreach (Transform child in Lv1Shooter.transform)
        {
            int RandomIndex = Random.Range(0, 3);
            switch (RandomIndex)
            {
                case 0:
                    child.GetComponent<DodgeGameShooter>().BulletPrefab = Resources.Load("DodgeGame/Bullet") as GameObject;
                    continue;
                case 1:
                    child.GetComponent<DodgeGameShooter>().BulletPrefab = Resources.Load("DodgeGame/BulletLarger") as GameObject;
                    continue;
                case 2:
                    child.GetComponent<DodgeGameShooter>().BulletPrefab = Resources.Load("DodgeGame/SpreadBullet") as GameObject;
                    continue;
                default:
                    child.GetComponent<DodgeGameShooter>().BulletPrefab = Resources.Load("DodgeGame/Bullet") as GameObject;
                    continue;
            }
        }
        bulletController.BulletSpeed++;
        StartCoroutine(Level4());
    }
    IEnumerator Level4()
    {
        yield return new WaitForSeconds(20);
        UIController.txtLevel.text = "Level 4";
        bulletController.BulletSpeed++;
        Lv1Shooter.SetActive(false);
        Lv4Shooter.SetActive(true);
        bulletController.BulletSpeed++;
        StartCoroutine(LevelX());
    }
    IEnumerator LevelX()
    {
        yield return new WaitForSeconds(20);
        UIController.txtLevel.text = "Highest Level";
        bulletController.BulletSpeed++;
        Lv4Shooter.SetActive(false);
        LvXShooter.SetActive(true);
        bulletController.BulletSpeed++;
        //later
    }
}
