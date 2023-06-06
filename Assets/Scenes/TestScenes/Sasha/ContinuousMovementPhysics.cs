using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinuousMovementPhysics : MonoBehaviour
{
    public float turnSpeed = 60f;
    public float speed = 1f;
    

    public bool onlyMoveWhenGrounded = false;

    //Ввод, который будем слушать с контроллера
    public InputActionProperty moveInputSource;
    //Ввод, который будем слушать с контроллера для поворота
    public InputActionProperty turnInputSource;
    //Отслеживание нажатие на кнопку прыжка на контроллере
    public InputActionProperty jumpInputSource;

    public Rigidbody playerRigidbody;
    //переменная для определения в каком направлении мы хотим двигаться
    public Transform directionSource;
    //Переменная для хранения информации с контроллера о перемещении
    private Vector2 inputMoveAxis;
    //Поле для отслеживания поворота стика туда-сюда
    private float inputTurnAxis;

    public Transform turnSource;
    //Заменить переменную в методе FixedUpdate на поле
    private bool _isGrounded;


    private float jumpVelocity = 7f;
    public float jumpHeight = 1f;

    void Update()
    {
        inputMoveAxis = moveInputSource.action.ReadValue<Vector2>();
        //Считываем только одну ось с контроллера
        inputTurnAxis = turnInputSource.action.ReadValue<Vector2>().x;
        //Отслеживание нажатие на кнопку
        bool jumpInput = jumpInputSource.action.WasPressedThisFrame();
    
        if (jumpInput && _isGrounded)
        {
            jumpVelocity = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y);
            playerRigidbody.velocity = Vector3.up * jumpVelocity;
        }
    }



    //Для перемещения и работы с физическими объектами необходимо использовать FixedUpdate
    private void FixedUpdate()
    {
        _isGrounded = CheckGround();
        if (!onlyMoveWhenGrounded || (onlyMoveWhenGrounded && _isGrounded))
        {
            //Получаем направление, в котором хотим двигаться - туда, куда смотрит игрок
            Quaternion lookVec = Quaternion.Euler(0, directionSource.eulerAngles.y, 0);
            //Расчитываем направление движения с помощью произведения направления взгляда
            //и нового вектора, полученного от системы ввода
            Vector3 direction = lookVec * new Vector3(inputMoveAxis.x, 0, inputMoveAxis.y);
            //Передвигать физический объект лучше с помощью MovePosition, указывая стартовую позицию
            //и прибавляя к ней малою величину в конкретном направлении direction
            Vector3 targetMovePosition = playerRigidbody.position + direction * Time.fixedDeltaTime * speed;

            Vector3 axis = Vector3.up;
            //Расчёт угла поворота
            float angle = turnSpeed * Time.fixedDeltaTime * inputTurnAxis;
            //Установка кватерниона поворота вокруг оси, направленной вверх
            Quaternion q = Quaternion.AngleAxis(angle, axis);
            //Физический поворот тела. Да, тут нет смещения, а есть умножение 
            //текущего угла на поворот. Если нужно просто телепортировать, то используете rotation
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
        //Перевод центра коллайдера из локальных координат коллайдера в мировые координаты
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        //Расчитываем расстояние на котором будем искать землю, плюс маленькая погрешность.
        float rayLength = bodyCollider.height / 2 - bodyCollider.radius + 0.05f;

        bool hasHit = Physics.SphereCast(start, bodyCollider.radius, Vector3.down, out RaycastHit hit, rayLength, groundLayer);
        return hasHit;
    }
}
