using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabPhysics : MonoBehaviour
{
    //����������� ������� �� ������,
    //�� ������� ������������ ��������������
    public InputActionProperty grabInputSource;

    //��������� ������, � ������ �������� ����� �����������
    //���������� �������� ��� ��������������
    public float radius = 0.1f;

    //����� ��� ������������ interatable ���������
    public LayerMask grabLayer;
    //������������� ���������� ������������ �������� �������,
    //����� ��� �������� �� ������� �������. ����������� ����� ������.
    private FixedJoint fixedJoint;
    //������������, ��������� �� � ������ �� ������� ��� ���
    private bool _isGrabbing = false;

    // ���������� FixedUpdate ��� ����������� ��������������
    void FixedUpdate()
    {
        //����������� ������� ������ ��������������
        bool isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > 0.1f;
        if (isGrabButtonPressed && !_isGrabbing)
        {
            //���� ������ ������, �� ����� ������ ������������ ����������.
            //QueryTriggerInteraction ��� �����, ����� ������������ ��������� ����������
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer, QueryTriggerInteraction.Ignore);
            //���� ����� � ����� ���� ������ ���� ��������
            if (nearbyColliders.Length > 0)
            {
                //�������� ������ � Rigidbody �������. �� ����� ��������� ���������� � ����� ���������
                Rigidbody nearbyRigidbody = nearbyColliders[0].attachedRigidbody;
                //
                fixedJoint = gameObject.AddComponent<FixedJoint>();
                //��������� ����� ����������
                fixedJoint.autoConfigureConnectedAnchor = false;
                //���� �� �������� ���� Rigidbody
                if (nearbyRigidbody)
                {
                    //����� ������������ � ���������� Rigidbody
                    fixedJoint.connectedBody = nearbyRigidbody;
                    //������������� ������� ����� �������������, ���������� ������� ���� 
                    // � ������� ����������� � ��������� ��� �������� Rigidbody
                    fixedJoint.connectedAnchor =
                        nearbyRigidbody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    //���� ������� �������� - ����� � �.�.
                    //�� ����� ���������� ���� ������ ������� ����
                    fixedJoint.connectedAnchor = transform.position;
                }
                _isGrabbing = true;
                    
            }
        }
        else if (!isGrabButtonPressed && _isGrabbing)
        {
            //���� ����� �������� ������ ��������������, � �� ����� � ������,
            // �� ���������� ������ ���� � ��� ������� fixedJoint ���������� �������� � ���� 
            //���� � ����������
            _isGrabbing = false;
            if (fixedJoint)
                Destroy(fixedJoint);
        }
    }
}
