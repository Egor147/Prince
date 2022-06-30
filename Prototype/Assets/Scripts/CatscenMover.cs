using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatscenMover : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spR;
    Transform trf;
    Vector2 startPos;
    public float [] timer, events;
    public float speed;
    public bool isPlayer;
    bool ready = true;
    int num = 0;
    int horizDir = 0;

    void Start()
    {
        trf = gameObject.GetComponent<Transform>();
        startPos = new Vector2(trf.position.x, trf.position.y);
        spR = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(ready && num < timer.Length)
        {
            if(trf.position.x >= events[num])
            {
                horizDir = -1;
                spR.flipX = true;
            }
            else 
            {
                horizDir = 1;
                spR.flipX = false;
            }

            if(Mathf.Abs(trf.position.x - events[num])<=0.1f)
            {
                ready = false;
                StartCoroutine(Delay(timer[num]));
            }

            if(isPlayer)
            {
                anim.SetFloat("Jump", rb.velocity.y);
                anim.SetBool("Move", true);
            }

            rb.velocity = new Vector2(speed*horizDir, rb.velocity.y);
        }
        else if(isPlayer)
        {
            anim.SetFloat("Jump", rb.velocity.y);
            anim.SetBool("Move", false);
        }
            
    }

    IEnumerator Delay(float waiting)
    {
        horizDir = 0;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(waiting);
        ready = true;
        num++;
        startPos = new Vector2(trf.position.x, trf.position.y);
    }
}
