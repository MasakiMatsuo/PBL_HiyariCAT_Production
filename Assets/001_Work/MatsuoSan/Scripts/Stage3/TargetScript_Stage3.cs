using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TargetScript_Stage3 : MonoBehaviour
{
    //Audio
    public AudioSource audioData;

    //UI
    public bool cleanFlg = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Capacity")
        {
            //Audio
            audioData.Play();

            cleanFlg = true; //UI

            gameObject.SetActive(false);
        }
    }
}

