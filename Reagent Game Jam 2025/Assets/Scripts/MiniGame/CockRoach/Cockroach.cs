using MiniGame.CockRoach;
using UnityEngine;

public class Cockroach : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Transform topLeft;
    private Transform bottomRight;
    private Vector2 targetPosition;

    private void Start()
    {
        Transform[] arr = CockroachGame.GetInstance().GetTransforms();
        topLeft = arr[0];
        bottomRight = arr[1];

        PickRandomTarget();
    }

    private void Update()
    {
        Vector2 currentPosition = transform.position;

        // Calculate direction
        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Move
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Rotate to face direction
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // adjust -90f depending on your sprite's default facing direction
        }

        // If close to target, pick new target
        if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
        {
            PickRandomTarget();
        }
    }

    void PickRandomTarget()
    {
        float x = Random.Range(topLeft.position.x, bottomRight.position.x);
        float y = Random.Range(bottomRight.position.y, topLeft.position.y); // Y is reversed if top is higher
        targetPosition = new Vector2(x, y);
    }

    // private void OnMouseDown()
    // {
    //     Destroy(gameObject);
    // }
}