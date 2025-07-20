using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    //  �ŏ��ɐ������ꂽ 1 �̃C���X�^���X��ێ�����
    private static BGMPlayer instance;

    private void Awake()
    {
        //  �܂��C���X�^���X�����݂��Ă��Ȃ���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  //  �V�[����؂�ւ��Ă��j������Ȃ��悤��
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
