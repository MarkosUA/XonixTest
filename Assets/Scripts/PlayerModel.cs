using UnityEngine;

public class PlayerModel
{
    public Direction PlayerDirection { set; get; }

    public PlayerModel(Direction direction)
    {
        PlayerDirection = direction;
    }
}
