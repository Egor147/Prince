using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    float waiting = 0.5f;
    GameObject player;
    public GameObject iteract;
    public Transform nisha;
    bool ready = false, stayIn = false, inProgress=false;

    void FixedUpdate()
    {
        if(stayIn)
        {
            if(Input.GetKey(KeyCode.E) && !inProgress)
            {
                if(ready)
                {
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    player.transform.position = Vector3.Lerp(player.transform.position, transform.position, 1);
                    ready = false;
                    player.GetComponent<Player>().enabled = true;
                    StartCoroutine(HideDelay());
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    player.GetComponent<Player>().enabled = false;
                    player.GetComponent<Animator>().SetFloat("Jump", 0);
                    player.GetComponent<Animator>().SetBool("Move", false);
                    player.transform.position = Vector3.Lerp(player.transform.position, nisha.position, 1);
                    ready = true;
                    StartCoroutine(HideDelay());
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            iteract.SetActive(true);
            player = other.gameObject;
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

    IEnumerator HideDelay()
    {
        inProgress = true;
        yield return new WaitForSeconds(waiting);
        inProgress = false;
    }
}
