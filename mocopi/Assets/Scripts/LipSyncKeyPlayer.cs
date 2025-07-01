using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uLipSync;

public class LipSyncKeyPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private uLipSync.uLipSync lipSync;
    [SerializeField] private KeyCode keyCode = KeyCode.Space;

    void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            //  çƒê∂íÜÇÃèÍçáÇÕé~ÇﬂÇÈ
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }
}
