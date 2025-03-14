using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullHandHolder : IHandHolder
{
    // HandTransform �̓_�~�[�Ƃ��� null ��Ԃ��i�K�v�Ȃ���S�ȃ_�~�[ Transform ��Ԃ��������\�j
    public Transform HandTransform => null;

    // HeldItem �͏�ɉ����ێ����Ă��Ȃ��̂� null ��Ԃ�
    public GameObject HeldItem { get; private set; } = null;

    // �������Ȃ�����
    public void AttachItem(GameObject item) { }

    // �������Ȃ�����
    public void DetachItem() { }

    public void ThrowItem(Transform target, float force) { }
}