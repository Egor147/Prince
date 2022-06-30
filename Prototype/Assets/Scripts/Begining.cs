using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begining : MonoBehaviour
{
    public GameObject player, startText, massage;
    public float timer;
    bool trig = false;
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space) && !trig)
        {
            trig = true;
            GameObject.Destroy(startText.gameObject, 0);
            massage.GetComponent<Massage>().StartCoroutine("AutoClose");
            massage.GetComponent<Massage>().Ready = true;       
            gameObject.GetComponent<Animator>().SetBool("Play", true);
            GameObject.Destroy(gameObject, timer+1);
            StartCoroutine("StartGame");
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(timer);
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Player>().enabled = true;
    }
}
