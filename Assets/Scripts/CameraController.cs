using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _camera;

    public void CenterCamera(Field field)
    {
        _camera.position = new Vector2(field.Width / 2, field.Height / 2);
    }
}
