using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PillarManager : MonoBehaviour
{
    [SerializeField] private PillarController pillarPrefab;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private StickManager stickManager;
    
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform targetMin;
    [SerializeField] private Transform targetMax;
    [SerializeField] private Transform mainCamera;

    [SerializeField] private PillarController current;
    public Vector3 CurrentPillarPosition => current.transform.position;
    private PillarController targetPillar;

    private Vector3 offsetCamera;
    
    public List<GameObject> pillars = new List<GameObject>();
    
    private void Start()
    {
        offsetCamera = mainCamera.transform.position - current.transform.position;
        Create();
    }

    [ContextMenu(nameof(Create))]
    public void Create()
    {
        
        var pillar = Instantiate(pillarPrefab);
        
        ChangePositionX(pillar.transform, spawnPoint.position.x);

        pillar.SetRandomSize();
        
        var targetX = Random.Range(targetMin.position.x, targetMax.position.x);
        var  targetPosition = pillar.transform.position;
        targetPosition.x = targetX;
        var move = animationController.Move(pillar.transform, targetPosition);
        
        StartCoroutine(move);
        
        targetPillar = pillar;
        pillars.Add(pillar.gameObject);
    }

    private void ChangePositionX(Transform current, float x)
    {
        var position = current.transform.position;
        position.x = x;
        current.transform.position = position;
    }

    public void NextLevel()
    {
        current = targetPillar;

        var targetPosition = current.transform.position + offsetCamera;
        var move = animationController.Move(mainCamera, targetPosition, 0.2f);
        StartCoroutine(move);

        IEnumerator Do()
        {
            yield return new WaitForSeconds(0.3f);
            Create();
            
            stickManager.Create();
        }

        StartCoroutine(Do());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            NextLevel();
        }
    }
}