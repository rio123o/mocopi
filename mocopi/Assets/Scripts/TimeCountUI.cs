using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCountUI : MonoBehaviour
{
    [Header("時間制限(秒)")]
    [SerializeField] private float timeLimitSeconds = 60f;

    [Header("残り時間表示テキスト")]
    [SerializeField] private TextMeshProUGUI timeCount;

    private float remainTime;  //  残り時間
    private bool isMeasuring;  //  時間を計っているかどうか

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
        remainTime -= Time.deltaTime;  //  1秒づつ減らす

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
