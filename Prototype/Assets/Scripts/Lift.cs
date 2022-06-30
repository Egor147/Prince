using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    const float timer = 1;
    GameObject player;
    public GameObject iteract;
    public Transform liftCon;
    bool inProgress = false, stayIn = false;
    public AudioSource Sound;

    void Start()
    {
        inProgress = false;
    }

    void FixedUpdate()
    {
        if(stayIn)
        {
            if(!inProgress)
                iteract.SetActive(true);
            else
                iteract.SetActive(false);
            if(Input.GetKey(KeyCode.E))
            {
                gameObject.GetComponent<Animator>().SetBool("Close", true);
                player.gameObject.SetActive(false);
                inProgress = true;
                StartCoroutine(Ride(player.gameObject));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            iteract.SetActive(true);
            player = other.gameObject;
            stayIn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            iteract.SetActive(false);
            stayIn = false;
        }
    }

    IEnumerator Ride(GameObject player)
    {
        Sound.Play();
        yield return new WaitForSeconds(timer);
        gameObject.GetComponent<Animator>().SetBool("Close", false);
        liftCon.GetComponent<Animator>().SetBool("Open", true);
        yield return new WaitForSeconds(timer);
        player.GetComponent<Transform>().position = Vector3.Lerp(player.GetComponent<Transform>().position, new Vector3(liftCon.position.x, liftCon.position.y-0.5f,0), 1);
        liftCon.GetComponent<Animator>().SetBool("Open", false);
        player.gameObject.SetActive(true);
        Sound.Stop();
        inProgress = false;
    }
}
