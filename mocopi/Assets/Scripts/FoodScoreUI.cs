using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI donutText;
    [SerializeField] private TextMeshProUGUI vegetableText;

    void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        if (FoodScore.Instance == null) return;

        donutText.text = $"{FoodScore.Instance.GetDonutCount()}";
        vegetableText.text = $"{FoodScore.Instance.GetVegetableCount()}";
    }
}