using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class MuteSource : MonoBehaviour
{
    [SerializeField] private bool muteAudioSource = true;  //  ������false�ɂ��邱�ƂŁA�~���[�g�łȂ���Ԃɂ��o����悤�ɂ���


    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!muteAudioSource)
            return;

        System.Array.Clear(data, 0, data.Length);
    }
}
