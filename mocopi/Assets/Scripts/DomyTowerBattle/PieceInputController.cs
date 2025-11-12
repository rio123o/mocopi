using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PieceInputController : MonoBehaviour
{
    [Header("操作対象のDroppablePiece")]
    [SerializeField] private
        DroppablePiece current;  //  TowerGameManagerから都度セットする

    [Header("動かす方向を左右のみに制限する")]
    [SerializeField] private bool horizontalOnly = true;

    private bool canDrop = true;  //  連射対策のフラグ

    private TowerGameControls controls;
    private Vector2 moveAxis;
    private float lastMoveSignX = 0f;

    private void Awake()
    {
        controls = new TowerGameControls();

        //  左右移動入力
        //  値が変化した瞬間に1ステップだけ移動させるもの
        controls.GamePlay.Move.performed += ctx =>
        {
            moveAxis = ctx.ReadValue<Vector2>();
            if (horizontalOnly) moveAxis = new Vector2(moveAxis.x, 0f);

            if (current == null || current.HasDropped) return;

            // 0から変化した時だけMoveStepを1回呼ぶ
            float sign = Mathf.Sign(moveAxis.x);
            if (Mathf.Abs(sign) > 0f && sign != lastMoveSignX)
            {
                current.MoveStep(new Vector2(sign, 0f));   // DroppablePieceのMoveStepを呼ぶ
                lastMoveSignX = sign;
            }
        };

        controls.GamePlay.Move.canceled += _ =>
        {
            moveAxis = Vector2.zero;
            lastMoveSignX = 0f;
        };

        //  落下入力
        controls.GamePlay.Drop.performed += _ =>
        {
            if (current == null || current.HasDropped) return;
            if (!canDrop) return;     //  連射対策

            canDrop = false;
            current.Drop();
        };
        controls.GamePlay.Drop.canceled += _ =>
        {
            //  ボタンを離したらまた使えるようにする
            canDrop = true;
        };
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    /// <summary>外部のGameManagerなどから現在の操作対象を差し替える</summary>
    public void SetCurrent(DroppablePiece piece)
    {
        current = piece;
        moveAxis = Vector2.zero;
        lastMoveSignX = 0f;
        canDrop = true;
    }
}