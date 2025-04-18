using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CableConnector : MonoBehaviour
{
    private CableConnector correct;
    private CableConnector connected;
    [SerializeField] private GameObject wire;
    private bool correctConnected;
    private bool isDragging;
    private GameObject wireInstance;
    private Camera mainCamera;
    private void Start()
    {
        isDragging = false;
        correctConnected = false;
        mainCamera = Camera.main;
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


    private void OnMouseDown()
    {
        if(isDragging)return;
        isDragging = true;
        wireInstance = Instantiate(wire, transform.position, quaternion.identity);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        Destroy(wireInstance);
    }
}
