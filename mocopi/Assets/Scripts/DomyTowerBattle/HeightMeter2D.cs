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

    //  最高点を取得する
    public float MaxHeight { get; private set; } = 0f;



}