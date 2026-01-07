using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeightUI2D : MonoBehaviour
{
    [Header("HeightMeter2Dの参照を指定する")]
    [SerializeField] private HeightMeter2D meter;
    [Header("高さ表示用TextMeshProUGUIの参照を指定する")]
    [SerializeField] private TextMeshProUGUI heightText;

    [Header("表示フォーマットの設定")]
    [SerializeField] private string prefix = "Height : ";
    [SerializeField] private string suffix = " cm";  //  メートル単位
    [SerializeField] private int decimalPlaces = 2;  //  小数点以下の桁数

    [Header("cmに変換するための倍率")]
    [Header("1 Unity Unitあたりのcm数を指定する")]
    [SerializeField] private float cmPerUnit = 100f;

    private void Awake()
    {
        if(!meter)
            meter = FindObjectOfType<HeightMeter2D>();
        if(!heightText)
            heightText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (meter != null)
        {
            meter.OnHeightChanged += UpdateText;
            //  初期表示の更新
            UpdateText(meter.CurrentHeight);
        }
    }

    private void OnDisable()
    {
        if (meter != null)
        {
            meter.OnHeightChanged -= UpdateText;
        }
    }

    private void UpdateText(float height)
    {
        if(!heightText) return;
        float heightCm = height * cmPerUnit;
        heightText.text = $"{prefix}{heightCm.ToString($"F{decimalPlaces}")}{suffix}";
    }
}