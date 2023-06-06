using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinuousMovementPhysics : MonoBehaviour
{
    public float turnSpeed = 60f;
    public float speed = 1f;
    

    public bool onlyMoveWhenGrounded = false;

    //����, ������� ����� ������� � �����������
    public InputActionProperty moveInputSource;
    //����, ������� ����� ������� � ����������� ��� ��������
    public InputActionProperty turnInputSource;
    //������������ ������� �� ������ ������ �� �����������
    public InputActionProperty jumpInputSource;

    public Rigidbody playerRigidbody;
    //���������� ��� ����������� � ����� ����������� �� ����� ���������
    public Transform directionSource;
    //���������� ��� �������� ���������� � ����������� � �����������
    private Vector2 inputMoveAxis;
    //���� ��� ������������ �������� ����� ����-����
    private float inputTurnAxis;

    public Transform turnSource;
    //�������� ���������� � ������ FixedUpdate �� ����
    private bool _isGrounded;


    private float jumpVelocity = 7f;
    public float jumpHeight = 1f;

    void Update()
    {
        inputMoveAxis = moveInputSource.action.ReadValue<Vector2>();
        //��������� ������ ���� ��� � �����������
        inputTurnAxis = turnInputSource.action.ReadValue<Vector2>().x;
        //������������ ������� �� ������
        bool jumpInput = jumpInputSource.action.WasPressedThisFrame();
    
        if (jumpInput && _isGrounded)
        {
            jumpVelocity = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y);
            playerRigidbody.velocity = Vector3.up * jumpVelocity;
        }
    }



    //��� ����������� � ������ � ����������� ��������� ���������� ������������ FixedUpdate
    private void FixedUpdate()
    {
        _isGrounded = CheckGround();
        if (!onlyMoveWhenGrounded || (onlyMoveWhenGrounded && _isGrounded))
        {
            //�������� �����������, � ������� ����� ��������� - ����, ���� ������� �����
            Quaternion lookVec = Quaternion.Euler(0, directionSource.eulerAngles.y, 0);
            //����������� ����������� �������� � ������� ������������ ����������� �������
            //� ������ �������, ����������� �� ������� �����
            Vector3 direction = lookVec * new Vector3(inputMoveAxis.x, 0, inputMoveAxis.y);
            //����������� ���������� ������ ����� � ������� MovePosition, �������� ��������� �������
            //� ��������� � ��� ����� �������� � ���������� ����������� direction
            Vector3 targetMovePosition = playerRigidbody.position + direction * Time.fixedDeltaTime * speed;

            Vector3 axis = Vector3.up;
            //������ ���� ��������
            float angle = turnSpeed * Time.fixedDeltaTime * inputTurnAxis;
            //��������� ����������� �������� ������ ���, ������������ �����
            Quaternion q = Quaternion.AngleAxis(angle, axis);
            //���������� ������� ����. ��, ��� ��� ��������, � ���� ��������� 
            //�������� ���� �� �������. ���� ����� ������ ���������������, �� ����������� rotation
            playerRigidbody.MoveRotation(playerRigidbody.rotation * q);
            Vector3 newPosition = q * (targetMovePosition
                - turnSource.position) + turnSource.position;
            playerRigidbody.MovePosition(newPosition);
        }
    }

    public CapsuleCollider bodyCollider;
    public LayerMask groundLayer;

    public bool CheckGround()
    {
        //������� ������ ���������� �� ��������� ��������� ���������� � ������� ����������
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        //����������� ���������� �� ������� ����� ������ �����, ���� ��������� �����������.
        float rayLength = bodyCollider.height / 2 - bodyCollider.radius + 0.05f;

        bool hasHit = Physics.SphereCast(start, bodyCollider.radius, Vector3.down, out RaycastHit hit, rayLength, groundLayer);
        return hasHit;
    }
}
