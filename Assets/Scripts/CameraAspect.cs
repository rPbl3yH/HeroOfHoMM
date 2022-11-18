using UnityEngine;

[ExecuteAlways]
public class CameraAspect : MonoBehaviour
{
    [SerializeField] private float _aspect;
    [SerializeField] private Camera _camera;

    private void Update() {
        float width = Screen.height * _aspect;
        float w = width / Screen.width;
        float x = (1 - w) / 2f;

        _camera.rect = new Rect(x, 0, w, 1);
    }
}