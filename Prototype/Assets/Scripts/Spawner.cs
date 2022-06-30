using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Transform trf;
    public GameObject _object;
    public float startDelay, waiting;
    void Start()
    {
        trf = gameObject.GetComponent<Transform>();
        StartCoroutine(Spawn(startDelay));
    }

    IEnumerator Spawn(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject.Instantiate(_object, trf.position, Quaternion.identity);
        StartCoroutine(Spawn(waiting));
    }
}
