using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaptureTriggerInputSystem : MonoBehaviour
{
    [Header("2Dオブジェクト生成マネージャー")]
    [SerializeField] private CaptureAndCreate2DObject captureManager;

    [Header("デバウンス処理")]
    [SerializeField] private float debounceTime = 0.3f;

    private TowerGameControls gameControls;
    private float lastTime;

    private void Awake()
    {
        if (!captureManager)
            captureManager = FindObjectOfType<CaptureAndCreate2DObject>();

        gameControls = new TowerGameControls();
        gameControls.UI.Falt.performed += OnCapturePerformed;
    }

    private void OnEnable() => gameControls.Enable();
    private void OnDisable() => gameControls.Disable();

    private void OnCapturePerformed(InputAction.CallbackContext ctx)
    {
        // デバウンス（連打・長押しの多重発火を抑制）
        if (Time.time - lastTime < debounceTime) return;
        lastTime = Time.time;

        if (captureManager)
            captureManager.CaptureAndCreate();
        else
            Debug.LogWarning("captureManagerがありません。");
    }
}
