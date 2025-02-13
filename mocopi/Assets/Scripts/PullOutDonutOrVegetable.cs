using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullOutDonutOrVegetable : MonoBehaviour
{
    [SerializeField] private GameObject donut;
    [SerializeField] private GameObject vegetable;

    [Header("ドーナッツか野菜が現れる位置")]
    [SerializeField] private Transform spawnPoint;

    [Header("投げ先のターゲット（Transform を指定）")]
    [SerializeField] private Transform throwTargetJ;
    [SerializeField] private Transform throwTargetK;
    [SerializeField] private Transform throwTargetL;

    [Header("プレイヤーの手元保持コンポーネント（IHandHolder を実装したコンポーネントをアサイン）")]
    [SerializeField] private MonoBehaviour domyHandHolder;

    private IHandHolder currentHandHolder;

    private void Awake()
    {
        // Inspector で domyHandHolder がアサインされていれば、そのコンポーネントを IHandHolder として設定
        if (domyHandHolder != null)
        {
            currentHandHolder = domyHandHolder as IHandHolder;
            if (currentHandHolder == null)
            {
                Debug.LogWarning("domyHandHolder に IHandHolder を実装したコンポーネントをアサインしてください。");
                currentHandHolder = new NullHandHolder();
            }
        }
        else
        {
            if (currentHandHolder == null)
            {
                currentHandHolder = new NullHandHolder();
                Debug.LogWarning("有効な IHandHolder が見つかりませんでした。NullHandHolder を使用します。");
            }
        }
    }

    private void Update()
    {
        // ① 手元にアイテムがない場合は、O または P キーで生成して手元にアタッチ
        if (currentHandHolder.HeldItem == null)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                GameObject item = SpawnObject(donut);
                if (item != null)
                    currentHandHolder.AttachItem(item);
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                GameObject item = SpawnObject(vegetable);
                if (item != null)
                    currentHandHolder.AttachItem(item);
            }
        }

        // ② すでに手元にアイテムがある場合、J・K・L キーで投げる処理を行う
        if (currentHandHolder.HeldItem != null)
        {
            if (Input.GetKeyDown(KeyCode.J) && throwTargetJ != null)
            {
                currentHandHolder.ThrowItem(throwTargetJ);
            }
            else if (Input.GetKeyDown(KeyCode.K) && throwTargetK != null)
            {
                currentHandHolder.ThrowItem(throwTargetK);
            }
            else if (Input.GetKeyDown(KeyCode.L) && throwTargetL != null)
            {
                currentHandHolder.ThrowItem(throwTargetL);
            }
        }

        // ③ I キーで単純に手放す（ThrowItem を使わない手放し処理）
        if (Input.GetKeyDown(KeyCode.I) && currentHandHolder.HeldItem != null)
        {
            currentHandHolder.DetachItem();
        }
    }

    /// <summary>
    /// 指定されたプレハブを生成し、そのオブジェクトを返す
    /// </summary>
    private GameObject SpawnObject(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("生成するプレハブが設定されていません。");
            return null;
        }

        // spawnPoint が設定されていればその位置、なければ currentHandHolder の HandTransform の位置を利用
        Vector3 position = (spawnPoint != null) ? spawnPoint.position : currentHandHolder.HandTransform.position;
        Quaternion rotation = (spawnPoint != null) ? spawnPoint.rotation : currentHandHolder.HandTransform.rotation;
        return Instantiate(prefab, position, rotation);
    }
}
