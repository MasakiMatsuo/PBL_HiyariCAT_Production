using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TargetAudio : MonoBehaviour
{
    //Audio
    public AudioSource audioData;

    private void OnTriggerEnter(Collider other)
    {
        //Audio
        audioData.Play();
    }
}
