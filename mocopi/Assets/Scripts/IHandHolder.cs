using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandHolder
{
    //  ���������I�u�W�F�N�g���A�^�b�`����ہA���� Transform �̈ʒu�E��]���Q�Ƃ���
    Transform HandTransform { get; }

    //  ���݁A�茳�Ɏ����Ă���I�u�W�F�N�g���擾����v���p�e�B
    GameObject HeldItem { get; }

    //  �w�肳�ꂽ�I�u�W�F�N�g���茳�ɃA�^�b�`���鏈�����s�����\�b�h
    void AttachItem(GameObject item);

    //  ���ݎ茳�ɕێ����Ă���I�u�W�F�N�g��������鏈�����s�����\�b�h
    void DetachItem();

    //  �w�肳�ꂽ�^�[�Q�b�g�ցA�w�肵���͂ŃA�C�e���𓊂���
    void ThrowItem(Transform target, float force);
}