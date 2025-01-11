using System;
using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float animationTime;
    
    public IEnumerator Move(Transform currentTransform, Vector3 target, float time = 0)
    {
        time = time == 0 ? animationTime : time;
        var passedTime = 0f;
        var init = currentTransform.transform.position;
        while (passedTime < time)
        {
            passedTime += Time.deltaTime;
            var normalizedTime = passedTime / time;
            var current = Vector3.Lerp(init, target, normalizedTime);
            currentTransform.position = current;
            yield return null;
        }
    }
    
    public IEnumerator Scale(Transform currentTransform, Vector3 target, float time = 0)
    {
        time = time == 0 ? animationTime : time;
        var passedTime = 0f;
        var init = currentTransform.transform.localScale;
        while (passedTime < time)
        {
            passedTime += Time.deltaTime;
            var normalizedTime = passedTime / time;
            var current = Vector3.Lerp(init, target, normalizedTime);
            currentTransform.localScale = current;
            yield return null;
        }
    }
    
    public IEnumerator Rotate(Transform currentTransform, Transform target, float time = 0)
    {
        time = time == 0 ? animationTime : time;
        var passedTime = 0f;
        var init = currentTransform.rotation;
        while (passedTime < time)
        {
            passedTime += Time.deltaTime;
            var normalizedTime = passedTime / time;
            var current = Quaternion.Lerp(init, target.rotation, normalizedTime);
            currentTransform.rotation = current;
            yield return null;
        }
    }
}