using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    public AudioSource Sound;
    const float waiting = 0.5f;
    public int level;

    void Start()
    {
        if(Sound == null)
            Sound = GameObject.FindGameObjectWithTag("Alarm").GetComponent<AudioSource>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !Player.hiden)
            StartCoroutine(Alarm(other.gameObject));
    }

    IEnumerator Alarm(GameObject player)
    {
        Time.timeScale = 0.5f;
        player.GetComponent<Animator>().SetFloat("Jump", 0);
        player.GetComponent<Player>().enabled = false;
        if(!Sound.isPlaying)
            Sound.Play();
        yield return new WaitForSeconds(waiting);
        SceneManager.LoadScene(level);
    }
}
