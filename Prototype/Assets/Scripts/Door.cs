using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    public GameObject iteract;
    public GameObject shadow;
    public BoxCollider2D col;
    public AudioSource Sound;
    public float waitingClose, waitingOpen;
    bool inProgress=false, stayIn = false;
    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(stayIn && !inProgress)
        {   
            iteract.SetActive(true);
            if(Input.GetKey(KeyCode.E))
            {
                if (anim.GetBool("Open"))
                {
                    inProgress = true;
                    anim.SetBool("Open",false);
                    StartCoroutine(Closing());
                }
                else
                {
                    inProgress = true;
                    anim.SetBool("Open",true);
                    StartCoroutine(Opening());
                }
            }  
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            iteract.SetActive(true);
            stayIn = true;
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
    
    IEnumerator Opening()
    {
        iteract.SetActive(false);
        Sound.Play();
        yield return new WaitForSeconds(waitingOpen);
        col.enabled = false;
        inProgress = false;
        shadow.SetActive(false);
    }

    IEnumerator Closing()
    {
        iteract.SetActive(false);
        Sound.Play();
        yield return new WaitForSeconds(waitingClose);
        col.enabled = true;
        inProgress = false;
        shadow.SetActive(true);
    }
}