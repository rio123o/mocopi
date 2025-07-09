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

    private float remainTime;  //  �c�莞��
    private bool isMeasuring;

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

        isMeasuring = true;
        remainTime = timeLimitSeconds;
        UpdateTimerUI();

    }

    void Update()
    {
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
        nextCanvas.SetActive(true);
        currentCanvas.SetActive(false);
        //  Update���~�߂�
        enabled = false;
    }

}