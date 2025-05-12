using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCountUI : MonoBehaviour
{
    [Header("")]
    [SerializeField] private float timeLimitSeconds = 60f;

    [Header("")]
    [SerializeField] private TextMeshProUGUI timeCount;

    private float remainTime;
    private bool isMeasuring;

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
        remainTime -= Time.deltaTime;

        if (remainTime <= 0f)
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