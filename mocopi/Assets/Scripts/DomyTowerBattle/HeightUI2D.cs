using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeightUI2D : MonoBehaviour
{
    [Header("HeightMeter2Dの参照を指定する")]
    [SerializeField] private HeightMeter2D meter;

    [Header("高さ表示用TextMeshProUGUIの参照を指定する")]
    [Header("現在の高さを表示するTextMeshProUGUIコンポーネント")]
    [SerializeField] private TextMeshProUGUI currentHeightText;
    [Header("シーケンス中の最高到達高さを表示するTextMeshProUGUIコンポーネント")]
    [SerializeField] private TextMeshProUGUI maxHeightText;


    [Header("表示フォーマットの設定")]
    [SerializeField] private string currentPrefix = "Height : ";
    [SerializeField] private string maxPrefix = "Max : ";
    [SerializeField] private string suffix = " cm";  //  メートル単位
    [SerializeField] private int decimalPlaces = 1;  //  小数点以下の桁数

    [Header("cmに変換するための倍率")]
    [SerializeField] private float cmPerUnit = 12f;

    private void Awake()
    {
        if(!meter)
            meter = FindObjectOfType<HeightMeter2D>();
        if(!currentHeightText)
            currentHeightText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (!meter)
            return;
        meter.OnHeightChanged += UpdateCurrentText;
        meter.OnMaxHeightUpdated += UpdateMaxText;
        //  初期表示の更新
        UpdateCurrentText(meter.CurrentHeight);
        UpdateMaxText(meter.MaxHeight);
    }

    private void OnDisable()
    {
        if (meter != null)
        {
            meter.OnHeightChanged -= UpdateCurrentText;
        }
    }

    private void UpdateCurrentText(float height)
    {
        if(!currentHeightText)
            return;

        float heightCm = height * cmPerUnit;
        currentHeightText.text = $"{currentPrefix}{heightCm.ToString($"F{decimalPlaces}")}{suffix}";
    }

    private void UpdateMaxText(float height)
    {
        if (!maxHeightText)
            return;

        float maxHeightCm = height * cmPerUnit;
        maxHeightText.text = $"{maxPrefix}{maxHeightCm.ToString($"F{decimalPlaces}")}{suffix}";
    }
}