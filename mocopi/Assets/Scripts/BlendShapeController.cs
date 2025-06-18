using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �C���X�y�N�^�[�r���[�ŃV���A���C�Y���\�ȃN���X
[System.Serializable]
public class ShapeKeyPair
{
    [Tooltip("SkinnedMesh��BlendShape��")]
    public string shapeName;
    [Tooltip("���̃L�[�������ꂽ��؂�ւ���")]
    public KeyCode key;

    //  �����^�C�����Ɏ����ŃZ�b�g�����t�B�[���h
    [HideInInspector] public int index;
    [HideInInspector] public float weight;
    [HideInInspector] public bool duttonTriggered;
}

public class BlendShapeController : MonoBehaviour
{
    [Header("�Ώۂ̓����I�u�W�F�N�g")]
    public GameObject head;
    [Header("���삷��V�F�C�v�ƃL�[�̑g�ݍ��킹")]
    public ShapeKeyPair[] pairs;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    void Start()
    {
        if(head == null)
        {
            Debug.LogError("�����I�u�W�F�N�g���ݒ肳��Ă��܂���B");
            enabled = false;
            return;
        }

        //  �X�L�����b�V�������_���[���擾
        skinnedMeshRenderer = head.GetComponent<SkinnedMeshRenderer>();
        if(skinnedMeshRenderer == null)
        {
            Debug.LogError("�����I�u�W�F�N�g�ɃX�L�����b�V�������_���[�����݂��Ă��܂���B");
            enabled = false;
            return;
        }

        //  �e�V�F�C�v�̃C���f�b�N�X�Ə����l���Z�b�g����
        foreach (var pair in pairs)
        {
            pair.index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(pair.shapeName);

            if (pair.index < 0)
                Debug.LogWarning($"�u�����h�V�F�C�v��{pair.shapeName}��������܂���B");

            pair.weight = 0;
            pair.duttonTriggered = false;
        }
    }

    void Update()
    {
        foreach(var pair in pairs)
        {
            //  �L�[������Ă�����A�{�^���̏�Ԃ����Z�b�g����
            if(!Input.GetKey(pair.key))
            {
                pair.duttonTriggered = false;
                continue;
            }

            //  �Y������L�[��������Ă��鎞
            if(!pair.duttonTriggered)
            {
                pair.weight = (pair.weight == 0f) ? 100f : 0f;
                skinnedMeshRenderer.SetBlendShapeWeight(pair.index, pair.weight);
                pair.duttonTriggered = true;
            }

        }
    }
}