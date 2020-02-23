﻿using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    public void CenterCamera(Field field)
    {
        _camera.transform.position = new Vector3(field.Width / 2, field.Height / 2, -50);
    }

    public void SizeOfTheCamera(float size)
    {
        _camera.orthographicSize = size;
    }
}
