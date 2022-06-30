using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretButton : MonoBehaviour
{
    public AudioSource Sound;
    public GameObject iteract;
    public GameObject enemy;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(enemy != null)
                iteract.SetActive(true);
            else
                iteract.SetActive(false);
            if(Input.GetKey(KeyCode.E))
            {
                GameObject.Destroy(enemy, 0);
                Sound.Play();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            iteract.SetActive(false);
    }
}
