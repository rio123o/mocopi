using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DomyController : MonoBehaviour
{
    // �����������ꂽInput Action�N���X�̃C���X�^���X
    private DomyInputActions inputActions;

    // �ړ����x
    [SerializeField]private float moveSpeed = 5f;

    //  ���A�g���K�[���ɂ���茳�ɕێ����Ă���R���|�[�l���g
    private IHandHolder currentHolder = new NullHandHolder();
    private void Awake()
    {
        // InputAction�A�Z�b�g�̃C���X�^���X�𐶐�
        inputActions = new DomyInputActions();
    }

    private void OnEnable()
    {
        // �K�v�ȃA�N�V�����}�b�v��L����
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        // �A�N�V�����}�b�v�𖳌������Ă���
        inputActions.Player.Disable();
    }

    private void Update()
    {
        //  Move�A�N�V��������Vector2�^�̓��͒l���擾����
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        //  ���͂Ɋ�Â��ăL�����N�^�[���ړ�����
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
