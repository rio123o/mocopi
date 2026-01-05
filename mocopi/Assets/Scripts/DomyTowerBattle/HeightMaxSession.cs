using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeightMaxSession : MonoBehaviour
{
    [SerializeField] private HeightMeter2D heightMeter;

    //  このシーンに存在しているか
    private static bool exsistsScene = false;

    private void Awake()
    {
        //  シーン側に複数置かれた場合の二重にならないための対策
        if (exsistsScene)
        {
            Destroy(gameObject);
            return;
        }
        exsistsScene = true;
        DontDestroyOnLoad(gameObject);

        if (!heightMeter) heightMeter = FindObjectOfType<HeightMeter2D>();

        if (!heightMeter)
        {
            //  高さ計測用のオブジェクトが見つからなかった場合は破棄する
            exsistsScene = false;
            Destroy(gameObject);
            return;
        }


    }


    private void OnDestroy()
    {
        SeaneManager.sceneLoaded -= OnSceneLoaded;
        UnhookMeter(heightMeter);
        if (exsistsScene) exsistsScene = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UnhookMeter(meter);
        meter = FindObjectOfType<HeightMeter2D>();

        //  次のシーンに高さ計測用オブジェクトが存在しなかった場合は破棄する
        if (!heightMeter)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            exsistsScene = false;
            Destroy(gameObject);
            return;
        }


    }

    private void ApplySavedToMator(HeightMeter2D meter)
    {
        if (!meter) return;
    }
}