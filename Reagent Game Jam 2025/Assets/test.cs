using System;
using Unity.Mathematics;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject pg;

    private void Start()
    {
        Instantiate(pg, new Vector3(0, 0, 0), quaternion.identity);
    }
}
