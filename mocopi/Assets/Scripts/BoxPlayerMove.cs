using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxPlayerMove : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    [Tooltip("�ړ����x�i�P��: ���j�b�g/�b�j")]
    [SerializeField] private float moveSpeed = 5f;
    [Tooltip("X���̍ŏ��l")]
    [SerializeField] private float minX = -5f;
    [Tooltip("X���̍ő�l")]
    [SerializeField] private float maxX = 5f;


    // �����������ꂽInput Action�N���X�̃C���X�^���X
    private PlayerInputSystem movementAction;

    private void Awake()
    {
        movementAction = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        // �K�v�ȃA�N�V�����}�b�v��L����
        movementAction.PlayerBoxMove.Enable();
    }


    private void OnDisable()
    {
        movementAction.PlayerBoxMove.Disable();
    }

    void Update()
    {
        // �uMove�v�A�N�V�����i2D Vector�j�̓��͒l���擾���AX�����𗘗p
        Vector2 input = movementAction.PlayerBoxMove.Move.ReadValue<Vector2>();
        float horizontal = input.x;

        // ���͂Ɋ�Â��Ĉړ��ʂ��v�Z
        Vector3 movement = new Vector3(horizontal * moveSpeed * Time.deltaTime, 0f, 0f);

        // ���݂̈ʒu�Ɉړ��ʂ����Z
        transform.position += movement;

        // X���W�� minX�`maxX �͈͓̔���Clamp
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
