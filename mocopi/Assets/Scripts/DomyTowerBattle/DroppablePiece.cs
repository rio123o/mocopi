using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DroppablePiece : MonoBehaviour
{
    [Header("プレビュー時の操作量")]
    [Header("ボタン1回で回す角度")]
    [SerializeField] private float rotateDommy = 15f;  //  回転の角度
    [Header("移動量")]
    [SerializeField] private float moveDommy = 0.25f;  //  移動の距離

    [Header("デバッグ用の表示(落としたかどうか)")]
    [SerializeField] private bool hasDropped = false;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //  念のため、プレビュー開始時に停止状態でなければ停止する
        if(!rb.isKinematic && !hasDropped)
        {
            rb.isKinematic = true;
        }
    }

    public void Drop()
    {
        if (hasDropped) return;
        hasDropped = true;
        rb.isKinematic = false;  //  kinematicを解除すると、spawn時に設定済みのgravityScaleに変わる
    }

    /// <summary>
    ///  プレビュー中の左回転
    /// </summary>
    public void RotateLeft()
    {
        if(hasDropped) return;
        transform.Rotate(0f, 0f, rotateDommy);  //  Z軸左回り回転
    }

    /// <summary>
    ///  プレビュー中の右回転
    /// </summary>
    public void RotateRight()
    {
        if(hasDropped) return;
        transform.Rotate(0f, 0f, -rotateDommy);  //  Z軸右回り回転

    }

    /// <summary>
    ///  プレビュー中の微移動
    /// </summary>
    /// <param name="dir"></param>
    public void MoveStep(Vector2 dir)
    {
        if(hasDropped) return;

        if (dir.sqrMagnitude <= 0f) return;

        //  正規化して1ステップ分だけ移動する
        Vector2 step = dir.normalized * moveDommy;
        transform.position += (Vector3)step;

    }

    //  落下済みかどうかの外部参照
    public bool HasDropped => hasDropped;

}
