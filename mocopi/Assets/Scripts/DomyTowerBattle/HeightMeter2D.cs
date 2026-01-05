using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMeter2D : MonoBehaviour
{
    [Header("対象のTowerPieceSpawnerを指定する")]
    [SerializeField] private TowerPieceSpawner spaner;

    [Header("床の高さ(Y座標)を指定する")]
    [SerializeField] private Transform floorTransform;

    //  現在の高さを取得する
    public float CurrentHeight { get; private set; } = 0f;

    //  最高到達された高さを取得する
    public float MaxHeight { get; private set; } = 0f;

    //  現在の高さが変化した時の通知イベント
    public event Action<float> OnHightChanged;

    //  ゲーム起動中の最高到達された高さが更新された時の通知イベント
    public event Action<float> OnMaxHeightUpdate;

    //  外部から最高到達された高さを更新する
    public void InitializeMax(float value)
    {
        MaxHeight = Mathf.Max(0f, value);
    }

    //  高さを再計測する
    public void MasureNow() => Recalc();

    private void Recalc()
    {
        if (!spaner || !spaner.SpawnParent)
        {
            Debug.LogWarning("HeightMeter2D: TowerPieceSpawnerが設定されていません。");
            return;
        }
        if (!floorTransform)
        {
            Debug.LogWarning("HeightMeter2D: 床のTransformが設定されていません。");
            return;
        }

        ver paerent = spaner.SpawnParent;  //  スポーンされたパーツの親オブジェクト
        float floorY = floorTransform.position.y;  //  床のY座標

        float topY = float.NegativeInfinity;
        bool foundAny = false;


    }

}