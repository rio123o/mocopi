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
                SceneManager.LoadScene(map.SceneName);
                break;
            }
        }
    }
}
