using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int scoreAmount = 100;
    ScoreManager _scoreManager;

    /*private void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }*/
    public void Init(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }
    protected override void OnPickup()
    {
        //Debug.Log("PowerUP");
        _scoreManager.IncreaseScore(scoreAmount);
    }
}
