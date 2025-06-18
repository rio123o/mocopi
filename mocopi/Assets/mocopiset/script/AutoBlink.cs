using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBlink : MonoBehaviour
{
    //�Ƃ��ڂ�BlendShapes���o�^����Ă���SkinnedMeshRenderer���擾
    //�܂قΌN�̏ꍇ�͊�̃I�u�W�F�N�g�������Ă���
    public SkinnedMeshRenderer face_SkinnedMeshRenderer;

    //�Ƃ��ڂ̗v�f��
    public int eyeClose_KeyNumber = 5;

    //SetBlendShapeWeight�Ɏg�p���鐔�l
    //0�ł܂Ԃ������S�ɊJ���A100�ł܂Ԃ������S�ɕ���
    private float weight = 0.0f;

    //���Ԃ��v�����邽�߂̕ϐ�
    public float countTime = 0.0f;
    //�܂΂����̔����^�C�~���O�̕ϐ�
    public float blinkTriggerTime = 5.0f;

    [Header("�܂΂����̔����^�C�~���O�̍ŏ��̎���")]
    public float blinkTriggerTimeMin = 3.0f;

    [Header("�܂΂����̔����^�C�~���O�̍ő�̎���")]
    public float blinkTriggerTimeMax = 7.0f;

    void FixedUpdate()
    {
        //FixedUpdate�͏����ݒ�̂܂܂Ȃ�
        CheckCountTime();
    }

    void CheckCountTime()
    {
        //���Ԃ��v��
        countTime += Time.deltaTime;
        //���Ԃ��܂΂����̔����^�C�~���O�𒴂�����
        if (countTime > blinkTriggerTime)
        {
            //�v�����Ԃ����Z�b�g
            countTime = 0.0f;
            //�܂΂����̔����^�C�~���O�̕ϐ���
            //blinkTriggerTimeMin����blinkTriggerTimeMax�̊ԂŃ����_���Ȑ��l���擾
            blinkTriggerTime = UnityEngine.Random.Range(blinkTriggerTimeMin, blinkTriggerTimeMax);

            //�ڂ���鏈���J�n
            StartCoroutine("CloseEye");
        }
    }

    IEnumerator CloseEye()
    {
        //����ۂ̐▭�Ȓ��ډ���
        weight = 80.0f;
        //���ڂɂ��Ă�����Ƃ������̏�����҂�
        face_SkinnedMeshRenderer.SetBlendShapeWeight(eyeClose_KeyNumber, weight);
        //yield return new WaitForSeconds(0.05f);
        //�܂Ԃ������S�ɕ��Ă�����Ƃ������̏�����҂�
        face_SkinnedMeshRenderer.SetBlendShapeWeight(eyeClose_KeyNumber, 100.0f);
        yield return new WaitForSeconds(0.1f);

        //�ڂ��J�������J�n
        StartCoroutine("OpenEye");
    }

    IEnumerator OpenEye()
    {
        //�J���ۂ̐▭�Ȓ��ډ���
        weight = 60.0f;
        //���ڂɂ��Ă�����Ƃ������̏�����҂�
        face_SkinnedMeshRenderer.SetBlendShapeWeight(eyeClose_KeyNumber, weight);
        yield return new WaitForSeconds(0.0001f);
        //�܂Ԃ������S�ɊJ��
        face_SkinnedMeshRenderer.SetBlendShapeWeight(eyeClose_KeyNumber, 0.0f);
    }

}
