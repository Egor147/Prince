using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Massage : MonoBehaviour
{
    public float waiting;
    public bool Ready = false;
    public GameObject [] soobsh;
    int num = 0;
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E) && Ready && num < soobsh.Length)
        {
            GameObject.Destroy(soobsh[num], 0);
            num++;
            StopCoroutine("AutoClose");
            StartCoroutine("AutoClose");
        }
    }
    public IEnumerator AutoClose()
    {
        for(int i=num; i < soobsh.Length; i++)
            soobsh[i].SetActive(true);
        yield return new WaitForSeconds(waiting);
        if(num < soobsh.Length)
        {
            GameObject.Destroy(soobsh[num], 0);
            num++;
            StartCoroutine("AutoClose");
        }
    }
}
