using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DodgeGameSoundEffect : MonoBehaviour
{
    public AudioClip Hurt;
    public AudioClip GameOver;
    public AudioClip NewRecord;
    public AudioClip Shoes;
    public AudioClip Heal;
    public AudioClip TooMany;
    public AudioClip Magic;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    public void HurtSoundEffect()
    {
        audioSource.PlayOneShot(Hurt);
    }
    public void GameOverSoundEffect()
    {
        audioSource.PlayOneShot(GameOver);
    }
    public void NewRecordSoundEffect()
    {
        audioSource.PlayOneShot(NewRecord);
    }
    public void ShoesSoundEffect()
    {
        audioSource.PlayOneShot(Shoes);
    }
    public void HealSoundEffect()
    {
        audioSource.PlayOneShot(Heal);
    }
    public void TooManySoundEffect()
    {
        audioSource.PlayOneShot(TooMany);
    }
    public void MagicSoundEffect()
    {
        audioSource.PlayOneShot(Magic);
    }
}
