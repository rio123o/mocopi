using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDestroyAndCount : MonoBehaviour
{
    private FoodObject foodObj;

    private void Awake()
    {
        // 同じオブジェクトにアタッチされた FoodObject を取得
        foodObj = GetComponent<FoodObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 「特定のトリガーオブジェクト」と衝突した場合のみスコア加算し、破棄
        if (other.CompareTag("DestroyFood"))
        {
            FoodScore.Instance.AddCount(foodObj.foodType);
            Destroy(gameObject);
        }
    }
}
