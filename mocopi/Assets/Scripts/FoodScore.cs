using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodScore : MonoBehaviour
{
    public static FoodScore Instance;

    private int donutCount;
    private int vegetableCount;

    private void Awake()
    {
        // �V���O���g���I�Ɉ���
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// FoodType �ɉ����ăX�R�A�����Z
    /// </summary>
    /// <param name="type">�h�[�i�c or ���</param>
    public void AddCount(FoodType type)
    {
        switch (type)
        {
            case FoodType.Donut:
                donutCount++;
                break;
            case FoodType.Vegetable:
                vegetableCount++;
                break;
        }

        Debug.Log($"[ScoreManager] Donut={donutCount}, Vegetable={vegetableCount}");
    }
}
