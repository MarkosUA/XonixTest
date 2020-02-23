using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerModel PlayerModel { private get; set; }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerModel.PlayerDirection = Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerModel.PlayerDirection = Direction.Top;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerModel.PlayerDirection = Direction.Right;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerModel.PlayerDirection = Direction.Bottom;
        }
    }
}
