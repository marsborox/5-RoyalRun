using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float adjustChangeMoveSpeedAmount = 3f;

    LevelGenerator _levelGenerator;
    /*private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }*/
    public void Init(LevelGenerator levelGenerator)
    {
        this._levelGenerator = levelGenerator;
    }
    protected override void OnPickup()
    {
        //Debug.Log("add 100 points");
        _levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
    }
}
