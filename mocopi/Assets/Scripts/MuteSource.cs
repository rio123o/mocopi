using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSource : MonoBehaviour
{
    [SerializeField] private AudioSource lipsyncSource;
    [SerializeField] private bool muteAudioSource = true;  //  ここをfalseにすることで、ミュートでない状態にも出来るようにする

    void Start()
    {
        if(lipsyncSource != null && muteAudioSource)
        {
            lipsyncSource.mute = true;  //  ミュートにする
        }
    }
}
