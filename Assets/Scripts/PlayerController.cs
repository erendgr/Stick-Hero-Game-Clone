using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PillarManager pillarManager;
    [SerializeField] private StickManager stickManager;
    [SerializeField] private Transform colliderDetecter;
    

    public float speed;
    public bool isLevelPassed = false;
    public bool isGameOver = false;
    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }
    
    void Update()
    {
        if (pillarManager.pillars.Count > 0)
        {
            var lastPillar = pillarManager.pillars[pillarManager.pillars.Count - 1];
            
            if (isLevelPassed)
            {
                if (transform.position.x < lastPillar.transform.position.x - 0.3f)
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    stickManager.conditionMet = true;
                }
                
            }
            else if (isGameOver)
            {
                if (transform.position.x < colliderDetecter.position.x + 0.3f)
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                    if (transform.position.x >= colliderDetecter.position.x)
                    {
                        rb.gravityScale = 1;
                    }
                }
                
            }
        }
        
    }
}
