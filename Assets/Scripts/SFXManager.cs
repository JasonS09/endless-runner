using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private AudioClip coinSFX;
    [SerializeField] private AudioClip doubleJumpSFX;
    [SerializeField] private AudioClip gameOverHitSFX;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip landSFX;
    [SerializeField] private AudioClip powerUpDoubleJumpSFX;
    [SerializeField] private AudioClip powerUpShieldSFX;
    [SerializeField] private AudioClip shieldBreakSFX;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(string clipToPlay)
    {
        switch (clipToPlay)
        {
            case "Coin":
                audioSource.clip = coinSFX;
                break;

            case "Double Jump":
                audioSource.clip = doubleJumpSFX;
                break;

            case "Game Over Hit":
                audioSource.clip = gameOverHitSFX;
                break;

            case "Jump":
                audioSource.clip = jumpSFX;
                break;

            case "Land":
                audioSource.clip = landSFX;
                break;

            case "Power Up Double Jump":
                audioSource.clip = powerUpDoubleJumpSFX;
                break;

            case "Power Up Shield":
                audioSource.clip = powerUpShieldSFX;
                break;

            case "Shield Break":
                audioSource.clip = shieldBreakSFX;
                break;

            default:
                break;
        }

        audioSource.Play();
    }
}
