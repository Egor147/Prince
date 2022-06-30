using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    SpriteRenderer spR;
    Animator anim;
    public float startDelay, waiting, animTime;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        spR = gameObject.GetComponent<SpriteRenderer>();
        spR.enabled = false;
        StartCoroutine(AnimDelay(startDelay));
    }

    IEnumerator AnimDelay(float timer)
    {
        yield return new WaitForSeconds(timer);
        spR.enabled = true;
        anim.SetBool("Ready", true);
        yield return new WaitForSeconds(animTime);
        anim.SetBool("Ready", false);
        spR.enabled = false;
        StartCoroutine(AnimDelay(waiting));
    }
}
