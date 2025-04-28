using System;
using System.Collections;
using UniGLTF;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timeForDestruction;

    private void Start()
    {
        StartCoroutine(DestroySelf(timeForDestruction));
    }

    private IEnumerator DestroySelf(float timeForDestruction)
    {
        yield return new WaitForSeconds(timeForDestruction);
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}