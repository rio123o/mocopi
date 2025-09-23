using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleToggle : MonoBehaviour
{
    [Header("切り替えに使うキー")]
    [SerializeField] private KeyCode key = KeyCode.H;

    [Header("操作するParticleSystem")]
    [SerializeField] private ParticleSystem particleSystem;

    //  再生しているかどうかのフラグ
    private bool isOn = false;

    void Awake()
    {
        if(particleSystem == null)
        {
            Debug.LogError("操作するParticleSystemがアタッチされていません");
        }
    }

    void Start()
    {
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Update()
    {
        if(particleSystem && Input.GetKeyDown(key))
        {
            isOn = !isOn;
            if(isOn)
                particleSystem.Play();  //  再生する
            else  //  再生を止める
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        }
    }
}
