using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeGameUIController : MonoBehaviour 
{
    public Text txtHP;
    public Text txtTime;
    public Text txtScore;
    public Text txtLevel;
    public GameObject txtNotice;
    public float SurviveTime;
    public float Score;
    private DodgeGame dodgeGame;
    public Animator txtScoreAnimator;
    public Animator txtTimeAnimation;
    public bool TimeNewRecordPlayed = false;
    public bool ScoreNewRecordPlayed = false;
    private void Start()
    {
        dodgeGame = GameObject.FindObjectOfType<DodgeGame>();
    }
    private void Update()
    {
        if (!dodgeGame.GameOver && dodgeGame.Gaming)
        {
            SurviveTime += Time.deltaTime;
            Score += Time.deltaTime;
            if(Score > dodgeGame.HighestScore)
            {
                txtScoreAnimator.SetBool("NewRecord", true);
                if (!ScoreNewRecordPlayed)
                {
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().NewRecordSoundEffect();
                    ScoreNewRecordPlayed = true;
                }
            }else
            {
                txtScoreAnimator.SetBool("NewRecord", false);
            }
            if (SurviveTime > dodgeGame.LongestTime)
            {
                txtTimeAnimation.SetBool("NewRecord", true);
                if (!TimeNewRecordPlayed)
                {
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().NewRecordSoundEffect();
                    TimeNewRecordPlayed = true;
                }
            }
            else
            {
                txtTimeAnimation.SetBool("NewRecord", false);
            }
        }
        txtTime.text = ((int)SurviveTime / 60).ToString() + ":" + (SurviveTime % 60).ToString("f2");
        txtScore.text = Score.ToString("f0");
        if (dodgeGame.player.HP > 0)
        {
            txtHP.text = "HP :" + dodgeGame.player.HP;
        }
        else
        {
            txtHP.text = "HP:0";
        }
        
    }
}
