using System;
using UnityEngine;

public class StickController : MonoBehaviour
{
    public bool grow { get; set; }
    [SerializeField] private float growSpeed;
    public Transform colliderPosition;
    
    public void Update()
    {
        if (grow)
        {
            var newScale = transform.localScale;
            newScale.y += growSpeed * Time.deltaTime;
            transform.localScale = newScale;
        }
    }
}