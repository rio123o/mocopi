using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCountUI : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private float timeLimitSeconds = 60f;

    [Header("�������Ԃ�\������UI")]
    [SerializeField] private TextMeshProUGUI timeCount;

    [Header("���Ԑ؂ꂵ����ɔ�\���ɂ���Canvas")]
    [SerializeField] private GameObject currentCanvas;

    [Header("���Ԑ؂ꂵ����ɕ\������Canvas")]
    [SerializeField] private GameObject nextCanvas;

    [Header("�J�n���ɖ炷��")]
    [SerializeField] private AudioClip startClip;

    [Header("���Ԑ؂ꎞ�ɖ炷��")]
    [SerializeField] private AudioClip endClip;

    [Header("���ʉ��Đ��p��AudioSource")]
    [SerializeField] private AudioSource audioSource;

    [Header("�Q�[���J�n�̃L�[")]
    [SerializeField] private KeyCode startKey = KeyCode.K;

    private float remainTime;  //  �c�莞��
    private bool isMeasuring;  //  �v����
    private bool hasStarted;  //  �J�n�������ǂ���

    void Start()
    {
        if(currentCanvas == null)
        {
            Debug.LogError("TimeCountUI�X�N���v�g�ɁAcurrentCanvas���A�^�b�`����Ă��܂���B", this);
            enabled = false;  //  Update�Ȃǂ̏������~�܂�
            return;
        }
        if(nextCanvas == null)
        {
            Debug.LogError("TimeCountUI�X�N���v�g�ɁAnextCanvas���A�^�b�`����Ă��܂���B", this);
            enabled = false;  //  Update�Ȃǂ̏������~�܂�
            return;
        }
        if (audioSource == null)
        {
            Debug.LogError("TimeCountUI�X�N���v�g�ɁAaudioSource���A�^�b�`����Ă��܂���B�B", this);
            enabled = false;
            return;
        }

        isMeasuring = false;
        hasStarted = false;

        remainTime = timeLimitSeconds;
        UpdateTimerUI();
    }

    void Update()
    {
        //  �J�n���Ă��Ȃ����ɁA�Q�[���J�n�L�[�������ꂽ��n�߂�
        if(!hasStarted && Input.GetKeyDown(startKey))
        {
            hasStarted = true;
            isMeasuring = true;

            //  �J�n���̃T�E���h�Đ�
            if (startClip != null)
            {
                audioSource.PlayOneShot(startClip);
            }
        }

        if (!isMeasuring) return;

        remainTime -= Time.deltaTime;

        if (remainTime <= 0f)
        {
            remainTime = 0f;
            isMeasuring = false;
            OnTimeUp();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(remainTime);
        timeCount.text = $"{seconds}";
    }

    ///  <summary>
    ///  ���Ԑ؂ꂵ�����̏���
    ///  currentCanvas���\���ɂ��āAnextCanvas��\������
    ///  </summary>
    private void OnTimeUp()
    {
        // �I�����̃T�E���h�Đ�
        if (endClip != null)
        {
            audioSource.PlayOneShot(endClip);
        }

        nextCanvas.SetActive(true);
        currentCanvas.SetActive(false);
        //  Update���~�߂�
        enabled = false;
    }

}