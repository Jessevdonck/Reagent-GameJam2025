using UnityEngine;

public class FistDropper : MonoBehaviour
{
    [SerializeField] private float dropDepth = 1.5f;
    [SerializeField] private float dropSpeed = 10f;
    [SerializeField] private float stayTime = 0.1f;
    [SerializeField] private float smashRadius = 0.5f;
    [SerializeField] private LayerMask cockroachLayer;

    private Vector3 upPosition;
    private Vector3 downPosition;
    private bool isDropping = false;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isDropping) return;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, -5, 0);

        if (Input.GetMouseButtonDown(0))
        {
            mouseWorldPos.z = 0;

            upPosition = new Vector3(mouseWorldPos.x, mouseWorldPos.y + dropDepth, 0);
            downPosition = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

            transform.position = upPosition;

            StartCoroutine(DropAndReturn());
        }
    }

    private System.Collections.IEnumerator DropAndReturn()
    {
        isDropping = true;

        // Drop down
        while (Vector3.Distance(transform.position, downPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, downPosition, dropSpeed * Time.deltaTime);
            yield return null;
        }

        // 💥 Check for squish
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, smashRadius, cockroachLayer);
        foreach (var hit in hits)
        {
            Debug.Log("Hit");
            Destroy(hit.gameObject); // squish!
        }

        yield return new WaitForSeconds(stayTime);

        // Return to idle position
        Vector3 idlePosition = new Vector3(transform.position.x, -5, transform.position.z);
        while (Vector3.Distance(transform.position, idlePosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, idlePosition, dropSpeed * Time.deltaTime);
            yield return null;
        }

        isDropping = false;
    }
    
    
    
    
}
