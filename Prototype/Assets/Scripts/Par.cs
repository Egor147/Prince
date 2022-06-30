using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Par : MonoBehaviour
{
    public float waiting;
    public int level;

    void Start()
    {
        GameObject.Destroy(gameObject, waiting);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            SceneManager.LoadScene(level);
    }
}
