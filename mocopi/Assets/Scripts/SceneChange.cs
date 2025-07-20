using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ButtonScenePair
{
    [SerializeField] private InputActionReference actionReference;
    [SerializeField] private string sceneName;

    public InputActionReference ActionReference => actionReference;
    public string SceneName => sceneName;
}

public class SceneChange : MonoBehaviour
{
    [Header("ボタンでの入力とシーン名のマッピング")]
    [SerializeField] private ButtonScenePair[] mappings;

    [Header("シーン切り替え前に鳴らす効果音 (必要な時)")]
    [SerializeField] private AudioClip preLoadClip;

    //  効果音を鳴らすAudioSource
    private AudioSource audioSource;

    private void Awake()
    {
        //  preLoadClipに音が入っている場合
        if (preLoadClip != null)
        {
            //  udioSourceがなければ追加する
            audioSource = gameObject.GetComponent<AudioSource>();
            if(audioSource ==  null )
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.loop = false;
        }
    }

    private void OnEnable()
    {
        foreach (var map in mappings)
        {
            var action = map.ActionReference?.action;
            if (action != null)
            {
                action.performed += OnAnyAction;
                action.Enable();
            }
        }
    }

    private void OnDisable()
    {
        foreach (var map in mappings)
        {
            var action = map.ActionReference?.action;
            if (action != null)
            {
                action.performed -= OnAnyAction;
                action.Disable();
            }
        }
    }

    private void OnAnyAction(InputAction.CallbackContext ctx)
    {
        // 押されたアクションがどのマッピングかを探してシーン遷移
        foreach (var map in mappings)
        {
            if (ctx.action == map.ActionReference.action && !string.IsNullOrEmpty(map.SceneName))
            {
                if (preLoadClip != null)
                {
                    //  効果音を鳴らしてからシーン移動
                    StartCoroutine(PlayThenLoad(map.SceneName));
                }
                else
                {
                    //  効果音指定がなければ即時ロード
                    SceneManager.LoadScene(map.SceneName);
                }
                break;
            }
        }
    }

    private IEnumerator PlayThenLoad(string sceneName)
    {
        audioSource.PlayOneShot(preLoadClip);
        //  効果音の長さだけ待ってからロード
        yield return new WaitForSeconds(preLoadClip.length);
        SceneManager.LoadScene(sceneName);
    }
}
