using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCountUI : MonoBehaviour
{
    [Header("制限時間")]
    [SerializeField] private float timeLimitSeconds = 60f;

    [Header("制限時間を表示するUI")]
    [SerializeField] private TextMeshProUGUI timeCount;

    [Header("時間切れした後に非表示にするCanvas")]
    [SerializeField] private GameObject currentCanvas;

    [Header("時間切れした後に表示するCanvas")]
    [SerializeField] private GameObject nextCanvas;

    private float remainTime;  //  残り時間
    private bool isMeasuring;

    void Start()
    {
        if(currentCanvas == null)
        {
            Debug.LogError("TimeCountUIスクリプトに、currentCanvasがアタッチされていません。", this);
            enabled = false;  //  Updateなどの処理が止まる
            return;
        }
        if(nextCanvas == null)
        {
            Debug.LogError("TimeCountUIスクリプトに、nextCanvasがアタッチされていません。", this);
            enabled = false;  //  Updateなどの処理が止まる
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
    ///  時間切れした時の処理
    ///  currentCanvasを非表示にして、nextCanvasを表示する
    ///  </summary>
    private void OnTimeUp()
    {
        nextCanvas.SetActive(true);
        currentCanvas.SetActive(false);
        //  Updateを止める
        enabled = false;
    }

}