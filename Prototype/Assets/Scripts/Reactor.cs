using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{ 
    Animator anim;
    public GameObject iteract, wall;
    public AudioSource Sound;
    bool stayIn = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(stayIn)
        {
            if(!anim.GetBool("Off"))
                iteract.SetActive(true);
            else
                iteract.SetActive(false);
            if(Input.GetKey(KeyCode.E))
            {
                Sound.Play();
                GameObject.Destroy(wall, 0);
                anim.SetBool("Off", true);
            }
        }  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            stayIn = true;

            if(!anim.GetBool("Off"))
                iteract.SetActive(true);
            else
                iteract.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            iteract.SetActive(false);
            stayIn = false;
        }
            
    }
}
