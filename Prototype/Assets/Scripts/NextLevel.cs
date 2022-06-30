using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    const float waiting = 1f;
    public int level;
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !Player.hiden)
            StartCoroutine(Alarm(other.gameObject));
    }

    IEnumerator Alarm(GameObject player)
    {
        player.tag = "Untagged";
        player.GetComponent<Animator>().SetFloat("Jump", 0);
        player.GetComponent<Player>().enabled = false;
        yield return new WaitForSeconds(waiting);
        SceneManager.LoadScene(level);
    }
}
