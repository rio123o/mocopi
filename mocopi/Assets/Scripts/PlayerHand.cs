using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour, IHandHolder
{
    // 手元として利用する Transform を Inspector で設定
    [SerializeField] private Transform handTransform;
    public Transform HandTransform => handTransform;

    // 現在手元に保持しているアイテム（外部からは取得のみ可能）
    public GameObject HeldItem { get; private set; } = null;

    /// <summary>
    /// 指定されたアイテムを手元にアタッチする
    /// </summary>
    public void AttachItem(GameObject item)
    {
        if (item == null) return;

        HeldItem = item;
        // アイテムを手元（handTransform）の子として設定
        item.transform.SetParent(handTransform);
        // 手元の位置と回転に合わせる
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        // 物理挙動を一時停止する（手元に保持中は物理シミュレーション不要）
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    /// <summary>
    /// 現在保持しているアイテムを手放す（デタッチする）
    /// </summary>
    public void DetachItem()
    {
        if (HeldItem == null) return;

        // 親子関係を解除して、手放す
        HeldItem.transform.SetParent(null);
        // 物理挙動を再開する
        Rigidbody rb = HeldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        HeldItem = null;
    }

    /// <summary>
    /// 現在保持しているアイテムを指定されたターゲット位置へ投げる
    /// </summary>
    /// <param name="target">投げ先の Transform</param>
    public void ThrowItem(Transform target)
    {
        if (HeldItem == null || target == null) return;

        // 投げるアイテムを取得して、手元から解除する
        GameObject itemToThrow = HeldItem;
        HeldItem = null;
        itemToThrow.transform.SetParent(null);

        // Rigidbody を取得。存在しない場合は追加する。
        Rigidbody rb = itemToThrow.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = itemToThrow.AddComponent<Rigidbody>();
        }
        rb.isKinematic = false;

        // 投げる方向を計算（手元からターゲットへの方向）
        Vector3 throwDirection = (target.position - handTransform.position).normalized;
        // 例えば、若干上方向へ持ち上げるために、上方向の補正を追加
        throwDirection += Vector3.up * 0.3f;
        throwDirection.Normalize();

        // 投げる力を設定（必要に応じて調整）
        float throwForce = 25f;
        // Rigidbody にインパルスとして力を加える
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}
