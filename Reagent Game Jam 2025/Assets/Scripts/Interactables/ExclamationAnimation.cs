using UnityEngine;

public class ExclamationAnimation : MonoBehaviour
{
    public float bobHeight = 0.2f;
    public float bobSpeed = 2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.localPosition = startPos + new Vector3(0, offset, 0);
    }
}