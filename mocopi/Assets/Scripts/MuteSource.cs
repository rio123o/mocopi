using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSource : MonoBehaviour
{
    [SerializeField] private AudioSource lipsyncSource;
    [SerializeField] private bool muteAudioSource = true;  //  ������false�ɂ��邱�ƂŁA�~���[�g�łȂ���Ԃɂ��o����悤�ɂ���

    void Start()
    {
        if(lipsyncSource != null && muteAudioSource)
        {
            lipsyncSource.mute = true;  //  �~���[�g�ɂ���
        }
    }
}
