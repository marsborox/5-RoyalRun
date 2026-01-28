using TMPro;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] float startTime = 5f;
    float timeLeft;
    bool gameOver = false;

    public bool GameOver => gameOver;
    private void Start()
    {
        timeLeft = startTime;
    }
    private void Update()
    {
        DecreaseTime();
    }
    public void IncreaseTime(float amount)
    {
        timeLeft += amount;
    }
    void DecreaseTime()
    {
        if (gameOver)
        {
            return;
        }
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");//this wil lforce to display only one decimal value
        if (timeLeft <= 0)
        {
            PlayerGameOver();
        }
    }

    private void PlayerGameOver()
    {
        gameOver = true;
        playerController.enabled = false;
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0.1f;
    }
    
}
