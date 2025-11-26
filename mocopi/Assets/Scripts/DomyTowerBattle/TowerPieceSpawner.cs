using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerPieceSpawner : MonoBehaviour
{
    [Header("生成物の親")]
    [SerializeField] private Transform spawnParent;

    [Header("生成位置の基準座標")]
    [SerializeField] private Vector3 spawnPosition = Vector3.zero;

    [Header("基準位置からどれだけ上に出すか")]
    [SerializeField] private float spawnHeight = 5f;

    [Header("SpriteRendererの描画順")]
    [SerializeField] private int sortingOrder = 100;

    [Header("SortingLayerの名前")]
    [SerializeField] private string sortingLayerName = "";

    [Header("落下中の重力の倍率(1が標準)")]
    [SerializeField] private float gravityScale = 1f;

    [Header("生成直後プレビュー状態にする")]
    [SerializeField] private bool startInPreview = true;

    [Header("プレビュー中だと分かるように名前を変更")]
    [SerializeField] private string previewChangeName = "[Preview]";

    public Transform SpawnParent => spawnParent;
    public Vector3 SpawnPosition => spawnPosition;
    public float SpawnHeight => spawnHeight;

    //  Spriteからピースを生成して返す
    public GameObject Spawn(Sprite sprite)
    {
        if(sprite == null)
        {
            Debug.LogError($"TowerPieseSpawnerのSpriteがnullです");
            return null;
        }

        //  GameObjectを生成して親子付けして、出現位置を決める
        var spawnedPiece = new GameObject("CapturedObject2D");
        if (spawnParent) spawnedPiece.transform.SetParent(spawnParent, true);
        spawnedPiece.transform.position = spawnPosition + Vector3.up * SpawnHeight;

        //  画像を見えるように出力する
        var spriteRen = spawnedPiece.AddComponent<SpriteRenderer>();
        spriteRen.sprite = sprite;
        spriteRen.sortingOrder = sortingOrder;
        if (!string.IsNullOrEmpty(sortingLayerName))  //  レイヤー名がある時、idを設定する
        {
            int id = SortingLayer.NameToID(sortingLayerName);
            if (id != 0) spriteRen.sortingLayerID = id;
        }

        //  当たり判定の生成
        var poly = spawnedPiece.AddComponent<PolygonCollider2D>();
        poly.isTrigger = false;

        //  物理の生成
        var rb = spawnedPiece.AddComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;

        //  プレビュー時の操作
        var drop = spawnedPiece.AddComponent<DroppablePiece>();

        //  プレビューの停止
        if(startInPreview)
        {
            rb.isKinematic = true;
            spawnedPiece.name = previewChangeName + spawnedPiece.name;
        }

        //  ピースを返す
        return spawnedPiece;
    }
}
