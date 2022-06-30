using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    const float stopRange = 0.05f, waiting = 0.5f;
    Transform trf;
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spR;
    public AudioSource Sound;
    public GameObject leftRay, rightRay;
    public float speed, leftPos, rightPos, stopPos, waitTime;
    int dir = 1;
    public int level;
    public bool stay;
    bool ready = true;
    void Awake()
    {
        trf = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spR = gameObject.GetComponent<SpriteRenderer>();
        leftRay.SetActive(false);
    }

    void FixedUpdate()
    {
        if(trf.position.x >= rightPos)
        {
            dir = -1;
            leftRay.SetActive(true);
            rightRay.SetActive(false);
            spR.flipX = true;
            ready = true;
        } 
        if(trf.position.x <= leftPos)
        {
            dir = 1;
            leftRay.SetActive(false);
            rightRay.SetActive(true);
            spR.flipX = false;
            ready = true;
        }

        if(Mathf.Abs(trf.position.x - stopPos) <= stopRange && ready)
        {
            if(stay)
                StartCoroutine(Waiting(dir));
            else
                stay = true;
            ready = false;
        }

        rb.velocity = new Vector2(speed*dir, rb.velocity.y);
    }

    IEnumerator Waiting(int x)
    {
        leftRay.SetActive(false);
        rightRay.SetActive(false);
        anim.SetBool("Idle", true);
        dir = 0;
        yield return new WaitForSeconds(waitTime);
        dir = x;
        stay = false;
        anim.SetBool("Idle", false);
        leftRay.SetActive(dir < 0);
        rightRay.SetActive(dir > 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Alarm(col.gameObject));
        }
    }

    IEnumerator Alarm(GameObject player)
    {
        player.GetComponent<Player>().enabled = false;
        Time.timeScale = 0.5f;
        player.GetComponent<Animator>().SetFloat("Jump", 0);
        player.GetComponent<Animator>().SetBool("Move", false);
        if(!Sound.isPlaying)
            Sound.Play();
        yield return new WaitForSeconds(waiting);
        SceneManager.LoadScene(level);
    }
}
