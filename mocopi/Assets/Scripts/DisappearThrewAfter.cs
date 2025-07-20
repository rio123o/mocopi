using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  ��������A�����ɓ��炸��莞�Ԍo�߂������Ɏ������Ł����ʉ��Đ����s���R���|�[�l���g
public class DisappearThrewAfter : MonoBehaviour
{
    //  �����ɓ���Ȃ��ꍇ�̏��ł���܂ł̎���
    public float lifeTime = 5f;

    //  ���ł������ɖ炷��
    public AudioClip disappearClip;

    //  ���ʉ��Đ��p��AudioSource
    private AudioSource audioSource;

    private void Awake()
    {
        //  AudioSource�R���|�[�l���g��ǉ�����A���[�v�Đ��̓I�t
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
    }

    private void Start()
    {
        //  �R���[�`�����J�n����
        StartCoroutine(ExpireAfterDelay());
    }

    private IEnumerator ExpireAfterDelay()
    {
        //  lifeTime�̎��Ԃ����ҋ@����(�����ɓ������ꍇ�ł���~�����)
        yield return new WaitForSeconds(lifeTime);

        //  disappearClip������Ă���ꍇ�́A���̌��ʉ����Đ�
        if(disappearClip != null )
        {
            audioSource.PlayOneShot(disappearClip);
            //  ���ʉ��̒����������ҋ@����
            yield return new WaitForSeconds(disappearClip.length);
        }

        //  �I�u�W�F�N�g��j��
        Destroy(gameObject);
    }
}
