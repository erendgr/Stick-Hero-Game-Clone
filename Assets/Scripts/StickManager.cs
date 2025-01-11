using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickManager : MonoBehaviour
{
    [SerializeField] private StickController stickPrefab;
    [SerializeField] private PillarManager pillarManager;
    [SerializeField] private Transform targetRotate;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private ColliderDetect colliderDetect;
    [SerializeField] private float offsetX;
    [SerializeField] private PlayerController playerController;
    public List<GameObject> sticks = new List<GameObject>();
    
    private StickController current;
    public bool conditionMet = false;
    
    private void Start()
    {
        Create();
    }

    public void Create()
    {
        var position = pillarManager.CurrentPillarPosition;
        position.x += offsetX;
        var stick = Instantiate(stickPrefab, position, Quaternion.identity);
        current = stick;
        sticks.Add(stick.gameObject);
    }

    public void OnPointerDown()
    {
        current.grow = true;
    }

    public void OnPointerUp()
    {
        current.grow = false;
        
        IEnumerator Do()
        {
            var rotate = animationController.Rotate(current.transform, targetRotate);
            yield return rotate;
            yield return null;
            colliderDetect.LevelController(current.colliderPosition.position);
            yield return new WaitForSeconds(0.3f);
                    if (colliderDetect.levelPass)
                    {
                        playerController.isLevelPassed = true;
                        yield return new WaitUntil(() => conditionMet);
                        pillarManager.NextLevel();
                        playerController.isLevelPassed = false;
                        conditionMet = false;
                    }
                    else
                    {
                        playerController.isGameOver = true;
                        Debug.Log("Game Over!");
                    }
                    
        }
        StartCoroutine(Do());
    }
}