using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void FocusOn(Transform target, bool zoomOut)
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        Camera.main.orthographicSize = zoomOut ? 10 : 5;
    }
}