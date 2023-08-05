 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _speedGainPerSecond = 0.2f;
    [SerializeField] private float _turnSpeed = 200f;

    private int _steerValue;

    // Update is called once per frame
    void Update()
    {
        _speed += _speedGainPerSecond * Time.deltaTime;

        transform.Rotate(0f, _steerValue * _turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void Steer(int value){
        _steerValue = value;
        Debug.Log(_steerValue);
    }
}
