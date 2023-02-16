using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    [SerializeField] private int _enginePower = 3;
    [SerializeField] private int _rotationSpeed = 5;

    private int _maxSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = 0;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (_audioSource.volume > 0)
            _audioSource.volume -= (0.5f + _audioSource.volume) * Time.deltaTime;
    }

    public void Thrust()
    {
        _audioSource.volume = 0.25f * _rigidbody.velocity.magnitude;

        if (_rigidbody.velocity.magnitude < _maxSpeed)
            _rigidbody.AddRelativeForce(Vector3.up * _enginePower * Time.deltaTime);
    }

    public void Rotate(int side)
    {
        _rigidbody.AddRelativeTorque(Vector3.forward * side * _rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Finish":
                Debug.Log("Победа");
                break;
            default:
                Debug.Log("Вы разбились");
                break;
        }
    }
}
