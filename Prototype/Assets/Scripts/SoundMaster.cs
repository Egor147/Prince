using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMaster : MonoBehaviour
{
    public AudioSource [] sound;
    public float [] startVolume;
    public Slider sliderFone, sliderSFX;
    public Text textFone, textSFX;
    private string saveKeyFone="Fone", saveKeySFX="SFX";
    public float volumeFone=0, volumeSFX=0;

    void Awake()
    {
        VolumeChange();
        sliderFone.value = volumeFone;
        sliderSFX.value = volumeSFX;
    }

    void FixedUpdate()
    {
        if(volumeFone != sliderFone.value)
        {
            PlayerPrefs.SetFloat(saveKeyFone, sliderFone.value);
            VolumeChange();
        }
        if(volumeSFX != sliderSFX.value)
        {
            PlayerPrefs.SetFloat(saveKeySFX, sliderSFX.value);
            VolumeChange();
        }
    }

    void VolumeChange()
    {
        volumeFone = PlayerPrefs.GetFloat(saveKeyFone);
        volumeSFX = PlayerPrefs.GetFloat(saveKeySFX);
        textFone.text = Mathf.RoundToInt(volumeFone*100)+"%";
        textSFX.text = Mathf.RoundToInt(volumeSFX*100)+"%";

        sound[0].volume = volumeFone*startVolume[0]*2;
        for(int i=1; i<sound.Length; i++)
            sound[i].volume = volumeSFX*startVolume[i]*2;
    }
}
