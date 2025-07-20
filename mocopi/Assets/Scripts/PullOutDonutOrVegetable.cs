using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullOutDonutOrVegetable : MonoBehaviour
{
    [SerializeField] private GameObject donut;
    [SerializeField] private GameObject vegetable;

    [Header("�h�[�i�b�c����؂������ʒu")]
    [SerializeField] private Transform spawnPoint;

    [Header("������̃^�[�Q�b�g")]
    [SerializeField] private Transform throwTargetJ;
    [SerializeField] private Transform throwTargetK;
    [SerializeField] private Transform throwTargetL;

    [Header("�v���C���[�̎茳�ێ��R���|�[�l���g")]
    [SerializeField] private MonoBehaviour domyHandHolder;

    [Header("������ݒ�")]
    [Tooltip("�������")]
    [SerializeField] private float throwForce = 10f;

    [Header("�����鎞�̌��ʉ�")]
    [SerializeField] private AudioClip throwClip;

    [Header("����炷AudioSourse")]
    [SerializeField] private AudioSource audioSource;

    [Header("�����ɓ���Ȃ��ꍇ�̏��Őݒ�")]
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private AudioClip disappearClip;

    private IHandHolder currentHandHolder;

    private void Awake()
    {
        if (domyHandHolder != null)
        {
            currentHandHolder = domyHandHolder as IHandHolder;
            if (currentHandHolder == null)
            {
                Debug.LogWarning("domyHandHolder �� IHandHolder �����������R���|�[�l���g���A�T�C�����Ă��������B");
                currentHandHolder = new NullHandHolder();
            }
        }
        else
        {
            currentHandHolder = new NullHandHolder();
            Debug.LogWarning("�L���� IHandHolder ��������܂���ł����BNullHandHolder ���g�p���܂��B");
        }

        //  AudioSource���ݒ肳��Ă��Ȃ���Ύ����擾�܂��͍쐬����
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }
        //  ���[�v���Ȃ�
        audioSource.loop = false;
    }

    private void Update()
    {
        // �@ �茳�ɃA�C�e�����Ȃ��ꍇ�́AO �܂��� P �L�[�Ő������Ď茳�ɃA�^�b�`
        if (currentHandHolder.HeldItem == null)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                GameObject item = SpawnObject(donut);
                if (item != null)
                    currentHandHolder.AttachItem(item);
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                GameObject item = SpawnObject(vegetable);
                if (item != null)
                    currentHandHolder.AttachItem(item);
            }
        }

        // �A ���łɎ茳�ɃA�C�e��������ꍇ�AJ�EK�EL �L�[�œ����鏈�����s��
        if (currentHandHolder.HeldItem != null)
        {
            if (Input.GetKeyDown(KeyCode.J) && throwTargetJ != null)
            {
                ThrowWithAutoDestroy(throwTargetJ);
            }
            else if (Input.GetKeyDown(KeyCode.K) && throwTargetK != null)
            {
                ThrowWithAutoDestroy(throwTargetK);
            }
            else if (Input.GetKeyDown(KeyCode.L) && throwTargetL != null)
            {
                ThrowWithAutoDestroy(throwTargetL);
            }
    
        }

        // �B I �L�[�Ŏ�����i�����鏈���Ƃ͕ʂɒP���Ƀf�^�b�`�j
        if (Input.GetKeyDown(KeyCode.I) && currentHandHolder.HeldItem != null)
        {
            currentHandHolder.DetachItem();
        }
    }

    /// <summary>
    /// �w�肳�ꂽ�v���n�u�𐶐����A���̃I�u�W�F�N�g��Ԃ�
    /// </summary>
    private GameObject SpawnObject(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("��������v���n�u���ݒ肳��Ă��܂���B");
            return null;
        }

        Vector3 position = (spawnPoint != null) ? spawnPoint.position : currentHandHolder.HandTransform.position;
        Quaternion rotation = (spawnPoint != null) ? spawnPoint.rotation : currentHandHolder.HandTransform.rotation;
        return Instantiate(prefab, position, rotation);
    }

    //  �����鎞�̌��ʉ��̍Đ�
    private void PlayThrowSound()
    {
        if (throwClip != null && audioSource != null)
            audioSource.PlayOneShot(throwClip);
    }
    ///  <summary>
    ///  �����āA���ʉ���炵�A�������ŃR���|�[�l���g��ǉ����āA�o�ߎ��Ԍ�ɔj������
    ///  </summary>
    private void ThrowWithAutoDestroy(Transform target)
    {
        GameObject thrown = currentHandHolder.HeldItem;
        currentHandHolder.ThrowItem(target, throwForce);  //  ������
        PlayThrowSound();  //  ��������ʉ�

        //  �������ł̃R���|�[�l���g��ǉ����āA�ݒ��n��
        var disapperThrew = thrown.AddComponent<DisappearThrewAfter>();
        disapperThrew.lifeTime = lifeTime;
        disapperThrew.disappearClip = disappearClip;
    }
}
