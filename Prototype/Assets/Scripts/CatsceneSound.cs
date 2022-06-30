using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatsceneSound : MonoBehaviour
{
    SoundMaster SM;
    public Animator anim;
    public AudioSource [] sounds;
    public float [] timer;
    float curVolume = 0, startTime;
    int num = 0;

    void Start()
    {
        startTime = Time.fixedTime;
        SM = gameObject.GetComponent<SoundMaster>();
    }
    void FixedUpdate()
    {
        if((Time.fixedTime-startTime) >= timer[num] && num < 2)
        {
            sounds[num].Play();
            num++;
        }
        
        if(num == 2)
        {
            if(curVolume < (SM.volumeSFX*SM.startVolume[1]*2))
                sounds[0].volume = (SM.volumeSFX*SM.startVolume[1]*2) - curVolume;
            else
                sounds[0].Stop();
            
            if(curVolume < (SM.volumeSFX*SM.startVolume[0]*2))
                sounds[1].volume = curVolume;
            else
                sounds[1].volume = SM.volumeSFX*SM.startVolume[0]*2;

            if(curVolume < (SM.volumeSFX*SM.startVolume[2]*2))
                sounds[2].volume = (SM.volumeSFX*SM.startVolume[2]*2) - curVolume;
            else
                sounds[2].Stop();

            curVolume += 0.0005f;
        }

        if((Time.fixedTime-startTime) >= timer[2] && !anim.GetBool("End"))
        {
            anim.SetBool("End", true);
        }

        if((Time.fixedTime-startTime) > (timer[2]+10))
        {
            if(Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(0);
        }
    }
}
