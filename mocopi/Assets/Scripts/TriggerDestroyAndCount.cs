using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDestroyAndCount : MonoBehaviour
{
    private FoodObject foodObj;

    private void Awake()
    {
        // �����I�u�W�F�N�g�ɃA�^�b�`���ꂽ FoodObject ���擾
        foodObj = GetComponent<FoodObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �u����̃g���K�[�I�u�W�F�N�g�v�ƏՓ˂����ꍇ�̂݃X�R�A���Z���A�j��
        if (other.CompareTag("DestroyFood"))
        {
            FoodScore.Instance.AddCount(foodObj.foodType);
            Destroy(gameObject);
        }
    }
}
