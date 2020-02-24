using UnityEngine;
using System.Collections.Generic;

public class PlayerView : MonoBehaviour
{
    private Vector3 _fp;
    private Vector3 _lp;
    private float _dragDistance = Screen.height * 20 / 100;

    [SerializeField]
    private Data _data;

    public PlayerModel PlayerModel { private get; set; }

    private void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        PhonePlayerMovement();
#elif UNITY_STANDALONE
        ComputerPlayerMovement();
#endif
    }

    private void ComputerPlayerMovement()
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

    private void PhonePlayerMovement()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fp = touch.position;
                _lp = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _lp = touch.position;

                if (Mathf.Abs(_lp.x - _fp.x) > _dragDistance || Mathf.Abs(_lp.y - _fp.y) > _dragDistance)
                {
                    if (Mathf.Abs(_lp.x - _fp.x) > Mathf.Abs(_lp.y - _fp.y))
                    {
                        if ((_lp.x > _fp.x))
                        {
                            if (PlayerModel.PlayerDirection != Direction.Left)
                                PlayerModel.PlayerDirection = Direction.Right;
                            else
                            {
                                _data.Hp--;
                            }
                        }
                        else
                        {
                            if (PlayerModel.PlayerDirection != Direction.Right)
                                PlayerModel.PlayerDirection = Direction.Left;
                            else
                            {
                                _data.Hp--;
                            }
                        }
                    }
                    else
                    {
                        if (_lp.y > _fp.y)
                        {
                            if (PlayerModel.PlayerDirection != Direction.Bottom)
                                PlayerModel.PlayerDirection = Direction.Top;
                            else
                            {
                                _data.Hp--;
                            }
                        }
                        else
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
            }
        }
    }
}