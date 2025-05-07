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

        donutText.text = $"Donut{FoodScore.Instance.GetDonutCount()}";
        vegetableText.text = $"Vegetable{FoodScore.Instance.GetVegetableCount()}";
    }
}
