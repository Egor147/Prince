using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{
    SpriteRenderer spR;
    Transform trf;
    Vector2 startPos;
    public Vector2 targertPos;
    public float step;
    public Sprite [] sost;
    public float [] times, events;
    bool inProgress = false, castShadow = false;
    float progress = 0;
    int num = 0;
    void Start()
    {
        spR = gameObject.GetComponent<SpriteRenderer>();
        trf = gameObject.GetComponent<Transform>();
        startPos = new Vector2 (trf.position.x, trf.position.y);
        if(trf.Find("Shadow") != null)
            castShadow = true;
    }

    void FixedUpdate()
    {
        if(!inProgress)
        {
            
            if(num < events.Length)
            {
                if(trf.position.x >= events[num])
                StartCoroutine(Delay());
            
                progress += step;
                trf.position = Vector2.Lerp(startPos, targertPos, progress);
            }
            else
            {
                progress -= step;
                trf.position = Vector2.Lerp(startPos, targertPos, progress);
                spR.flipX = true;
                spR.enabled = true;

                if(trf.position.x <= startPos.x || castShadow)
                    GameObject.Destroy(gameObject, 0);
            }
        }
    }

    IEnumerator Delay()
    {
        inProgress = true;
        if(castShadow)
        {
            trf.Find("Shadow").gameObject.SetActive(false);
            trf.Find("Cleshna").gameObject.SetActive(false);
        }
        spR.enabled = false;
        yield return new WaitForSeconds(times[num]);
        spR.sprite = sost[num];
        if(castShadow)
        {
            spR.enabled = true;
            trf.Find("Shadow").gameObject.SetActive(true);
            if(num < events.Length-2)
                trf.Find("Cleshna").gameObject.SetActive(true);
        }
        num++;
        inProgress = false;
        if(!castShadow && num == events.Length)
        {
            spR.sortingOrder = spR.sortingOrder - 1;
            spR.color = Color.gray;
        }
    }
}