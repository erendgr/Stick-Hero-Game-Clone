using UnityEngine;

public class ColliderDetect : MonoBehaviour
{
    public bool levelPass;
    [SerializeField] private Transform parent;
    
    public void LevelController(Vector3 position)
    {
        levelPass = false;
        parent.position = position;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pillar"))
        {
            levelPass = true;
        }
    }
}