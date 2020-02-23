using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private Data _data;

    public PlayerModel PlayerModel { private get; set; }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (PlayerModel.PlayerDirection != Direction.Right)
                PlayerModel.PlayerDirection = Direction.Left;
            else
            {
                _data.Hp--;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (PlayerModel.PlayerDirection != Direction.Bottom)
                PlayerModel.PlayerDirection = Direction.Top;
            else
            {
                _data.Hp--;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (PlayerModel.PlayerDirection != Direction.Left)
                PlayerModel.PlayerDirection = Direction.Right;
            else
            {
                _data.Hp--;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (PlayerModel.PlayerDirection != Direction.Top)
                PlayerModel.PlayerDirection = Direction.Bottom;
            else
            {
                _data.Hp--;
            }
        }
    }
}
