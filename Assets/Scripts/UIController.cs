using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text distanceNumber;
    [SerializeField] private Text coinsNumber;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Player player;
    [SerializeField] private GameObject gameMusic;
    [SerializeField] private GameObject sky;

    public void ShowGameOverScreen()
    {
        gameMusic.SetActive(false);
        sky.SetActive(false);
        gameOverScreen.SetActive(true);
        distanceNumber.text = Mathf.Ceil(player.DistanceTraveled).ToString();
        coinsNumber.text = player.CollectedCoins.ToString();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("EndlessRunner");
    }
}
