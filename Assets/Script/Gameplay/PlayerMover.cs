using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _roateSpeed;
    [SerializeField]
    private Animator _animator;
    private Action _onArrived;
    private Vector3 _target;
    private Vector3 _angleTarget;
    private bool _isMoving;


    void Update()
    {
        if (_isMoving)
        {
            if (Vector2.Distance(transform.position, _target) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _speed);
                transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, _angleTarget, Time.deltaTime * _roateSpeed);
            }
            else
            {
                _isMoving = false;
                _onArrived.Invoke();
            }
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }


    public void SetTarget(Vector3 target, Action onArrived)
    {
        _onArrived = onArrived;
        _target = target;
        Vector3 delta = transform.position - _target;
        _angleTarget = new Vector3(0, 0, CalculateAngle(delta));
        _animator.SetBool("IsMoving", true);
        _isMoving = true;
    }

    private float CalculateAngle(Vector2 delta)
    {
        float value = (-Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg);
        value += 360;
        value %= 360;
        return value;
    }


}
