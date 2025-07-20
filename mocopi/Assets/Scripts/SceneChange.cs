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
    [Header("�{�^���ł̓��͂ƃV�[�����̃}�b�s���O")]
    [SerializeField] private ButtonScenePair[] mappings;

    [Header("�V�[���؂�ւ��O�ɖ炷���ʉ� (�K�v�Ȏ�)")]
    [SerializeField] private AudioClip preLoadClip;

    //  ���ʉ���炷AudioSource
    private AudioSource audioSource;

    private void Awake()
    {
        //  preLoadClip�ɉ��������Ă���ꍇ
        if (preLoadClip != null)
        {
            //  udioSource���Ȃ���Βǉ�����
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
        // �����ꂽ�A�N�V�������ǂ̃}�b�s���O����T���ăV�[���J��
        foreach (var map in mappings)
        {
            if (ctx.action == map.ActionReference.action && !string.IsNullOrEmpty(map.SceneName))
            {
                if (preLoadClip != null)
                {
                    //  ���ʉ���炵�Ă���V�[���ړ�
                    StartCoroutine(PlayThenLoad(map.SceneName));
                }
                else
                {
                    //  ���ʉ��w�肪�Ȃ���Α������[�h
                    SceneManager.LoadScene(map.SceneName);
                }
                break;
            }
        }
    }

    private IEnumerator PlayThenLoad(string sceneName)
    {
        audioSource.PlayOneShot(preLoadClip);
        //  ���ʉ��̒��������҂��Ă��烍�[�h
        yield return new WaitForSeconds(preLoadClip.length);
        SceneManager.LoadScene(sceneName);
    }
}
