using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCountUI : MonoBehaviour
{
    [Header("���Ԑ���(�b)")]
    [SerializeField] private float timeLimitSeconds = 60f;

    [Header("�c�莞�ԕ\���e�L�X�g")]
    [SerializeField] private TextMeshProUGUI timeCount;

    private float remainTime;  //  �c�莞��
    private bool isMeasuring;  //  ���Ԃ��v���Ă��邩�ǂ���

    void Start()
    {
        isMeasuring = true;
        remainTime = timeLimitSeconds;
        UpdateTimerUI();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMeasuring) return;
        remainTime -= Time.deltaTime;  //  1�b�Â��炷

        if(remainTime <= 0f)
        {
            remainTime = 0f;
            isMeasuring = false;

        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(remainTime);
        timeCount.text = $"{seconds}";
    }
}
