using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldKeySoundPlayer : MonoBehaviour
{
    [Header("�����Ă���ԁA�Đ�������L�[")]
    [SerializeField] private InputActionReference holdAction;

    [Header("�Đ�����AudioSource")]
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if(audioSource == null)
        {
            Debug.LogError($"{nameof(HoldKeySoundPlayer)}AudioSource�R���|�[�l���g��������܂���B", this);
            enabled = false;
            return;
        }

        //  �L�[������܂ŌJ��Ԃ��Đ������
        audioSource.loop = true;
    }

    private void OnEnable()
    {
        //  InputAction�̗L�����ƃR�[���o�b�N�o�^
        if (holdAction != null && holdAction.action != null)
        {
            holdAction.action.started += OnHoldStarted;
            holdAction.action.canceled += OnHoldCanceled;
            holdAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        //  �R�[���o�b�N������InputAction�̖�����
        if (holdAction != null && holdAction.action != null)
        {
            holdAction.action.started -= OnHoldStarted;
            holdAction.action.canceled -= OnHoldCanceled;
            holdAction.action.Disable();
        }
    }

    //  �L�[�������n�߂��Ƃ��ɌĂ΂��
    private void OnHoldStarted(InputAction.CallbackContext ctx)
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    //  �L�[��������Ƃ��ɌĂ΂��
    private void OnHoldCanceled(InputAction.CallbackContext ctx)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
