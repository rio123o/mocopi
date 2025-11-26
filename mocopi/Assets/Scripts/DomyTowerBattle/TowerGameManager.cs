using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class TowerGameManager : MonoBehaviour
{
    [SerializeField] private TowerPieceSpawner spawner;
    [SerializeField] private PieceInputController inputController;

    // ← 配列/リストの代わりに「順番」を保証するキュー
    private readonly Queue<Sprite> pieceQueue = new();

    [Header("静止判定しきい値")]
    [SerializeField] private float pieceParallelVelocity = 0.05f;
    [SerializeField] private float pieceAngularVelocity = 2f;
    [SerializeField] private float stopJudgmentTime = 0.7f;

    private bool hasActivePiece = false;

    private void Start()
    {
        // 最初は何もせず、キャプチャでキューに入ったら出す
    }

    /// <summary>キャプチャ側から「次に積みたいSprite」を順番に受け取る</summary>
    public void EnqueueCapturedSprite(Sprite s)
    {
        if (!s) { Debug.LogWarning("null Sprite は受け取れません"); return; }
        pieceQueue.Enqueue(s);
        // いま場にピースが無ければすぐ出す
        if (!hasActivePiece) TrySpawnNext();
    }

    /// <summary>キューに何かあれば1つ出す</summary>
    public void TrySpawnNext()
    {
        if (pieceQueue.Count == 0) { hasActivePiece = false; return; }
        var s = pieceQueue.Dequeue();
        var go = spawner.Spawn(s);
        var drop = go.GetComponent<DroppablePiece>();
        inputController.SetCurrent(drop);
        hasActivePiece = true;
    }

    /// <summary>Drop成功時に呼び出し（PieceInputController から通知）</summary>
    public void OnDropped(DroppablePiece piece)
    {
        StartCoroutine(WaitStableThenNext(piece));
    }

    private IEnumerator WaitStableThenNext(DroppablePiece piece)
    {
        var rb = piece.GetComponent<Rigidbody2D>();
        float t = 0f;

        while (true)
        {
            bool nearlyStopped =
                rb.IsSleeping() ||
                (rb.velocity.sqrMagnitude < pieceParallelVelocity * pieceParallelVelocity &&
                 Mathf.Abs(rb.angularVelocity) < pieceAngularVelocity);
            if (nearlyStopped)
            {
                t += Time.deltaTime;
                if (t >= stopJudgmentTime) break;
            }
            else t = 0f;
            yield return null;
        }

        hasActivePiece = false;
        TrySpawnNext(); // 次の「撮った順」のピースを出す
    }
}
