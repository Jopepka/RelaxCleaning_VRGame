using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabPhysics : MonoBehaviour
{
    //Отслеживаем нажатие на кнопку,
    //на которой обозначается взаимодействие
    public InputActionProperty grabInputSource;

    //Обозначим радиус, в рамках которого будем отслеживать
    //коллайдеры объектов для взаимодействия
    public float radius = 0.1f;

    //Маска для отслеживания interatable предметов
    public LayerMask grabLayer;
    //Фиксированные соединения ограничивают движение объекта,
    //чтобы оно зависело от другого объекта. Реализуется через физику.
    private FixedJoint fixedJoint;
    //Отслеживание, захватили ли и держим мы предмет или нет
    private bool _isGrabbing = false;

    // Используем FixedUpdate для физического взаимодействия
    void FixedUpdate()
    {
        //Отслеживаем нажатие кнопки взаимодействия
        bool isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > 0.1f;
        if (isGrabButtonPressed && !_isGrabbing)
        {
            //Если кнопка нажата, то будем искать близлежайшие коллайдеры.
            //QueryTriggerInteraction тут нужен, чтобы игнорировать тригерные коллайдеры
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer, QueryTriggerInteraction.Ignore);
            //Если рядом с рукой есть хотябы один колайдер
            if (nearbyColliders.Length > 0)
            {
                //Получаем доступ к Rigidbody первого. Но лучше вычислять расстояние и брать ближайший
                Rigidbody nearbyRigidbody = nearbyColliders[0].attachedRigidbody;
                //
                fixedJoint = gameObject.AddComponent<FixedJoint>();
                //Настройка якоря соединения
                fixedJoint.autoConfigureConnectedAnchor = false;
                //Если на предмете есть Rigidbody
                if (nearbyRigidbody)
                {
                    //Тогда присоединяем к соединению Rigidbody
                    fixedJoint.connectedBody = nearbyRigidbody;
                    //Устанавливаем якорную точку присоединения, преобразуя позицию руки 
                    // в мировых координатах в локальные для текущего Rigidbody
                    fixedJoint.connectedAnchor =
                        nearbyRigidbody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    //Если предмет статичен - стена и т.п.
                    //То точку сопряжения берём равной позиции руки
                    fixedJoint.connectedAnchor = transform.position;
                }
                _isGrabbing = true;
                    
            }
        }
        else if (!isGrabButtonPressed && _isGrabbing)
        {
            //Если игрок отпустил кнопку взаимодействия, а до этого её держал,
            // то необходимо убрать флаг и при наличии fixedJoint соединения предмета и руки 
            //Надо её уничтожить
            _isGrabbing = false;
            if (fixedJoint)
                Destroy(fixedJoint);
        }
    }
}
