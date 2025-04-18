using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CableConnector : MonoBehaviour
{
    public CableConnector correct;
    public CableConnector connected;
    [SerializeField] private GameObject wire;
    private bool correctConnected;
    private bool isDragging;
    private GameObject wireInstance;
    private Camera mainCamera;
    public AudioClip wrong;
    
    private Transform wireParent;
    private void Start()
    {
        isDragging = false;
        correctConnected = false;
        mainCamera = Camera.main;
    }
    public void SetWireParent(Transform parent)
    {
        wireParent = parent;
    }
    

    private void Update()
    {
        if (!isDragging) return;

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        Vector3 start = transform.position;
        Vector3 end = mouseWorld;

        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        // Set position to the midpoint
        

        // Set scale to stretch between points (make sure your wire sprite is 1 unit tall and anchored from center)
        wireInstance.transform.localScale = new Vector3(1, distance, 1f); // 0.1f is thickness

        // Set rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        wireInstance.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    public void destroyWire()
    {
        if(!wireInstance) return;
        Destroy(wireInstance);
    }

    private void OnMouseDown()
    {
        if (wireInstance)
        {
            Destroy(wireInstance);
            wireInstance = null;
            connected.connected = null;
            connected = null;
        }
        if(isDragging)return;
        isDragging = true;
        wireInstance = Instantiate(wire, transform.position, quaternion.identity, wireParent);
    }
    
    public void SetCorrect(CableConnector other)
    {
        correct = other;
    }

    public bool IsCorrectlyConnected()
    {
        return connected == correct;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        // Radius check for more forgiving hit detection
        Collider2D[] hits = Physics2D.OverlapCircleAll(mouseWorld, 0.2f);
        foreach (Collider2D hit in hits)
        {
            CableConnector target = hit.GetComponent<CableConnector>();

            // Exclude self and already connected targets
            if (target != null && target != this && target.connected == null)
            {
                connected = target;
                target.connected = this;

                Vector3 start = transform.position;
                Vector3 end = target.transform.position;
                Vector3 direction = (end - start).normalized;
                float distance = Vector3.Distance(start, end);

                
                wireInstance.transform.localScale = new Vector3(1, distance, 1f);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                wireInstance.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

                if (target == correct)
                {
                    correctConnected = true;
                    Debug.Log("✅ Correct wire connected!");
                }
                else
                {
                    SoundManager.Instance.PlaySound(wrong, 0.6f);
                    Debug.Log("❌ Wrong connection.");
                }
                FindObjectOfType<CableManager>().CheckWinCondition();
                return;
            }
        }

        // No valid connection, destroy the wire
        Destroy(wireInstance);
        connected = null;
    }
}
