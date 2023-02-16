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
    [SerializeField] private AudioClip _thrustSound;
    [SerializeField] private AudioClip _destroySound;
    [SerializeField] private AudioClip _levelCompleteSound;

    private int _maxSpeed = 5;
    private float _destroySoundVolume = 0.5f;
    private float _levelCompleteSoundVolume = 1f;

    public bool IsControllable {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _thrustSound;
        _audioSource.volume = 0;
        _audioSource.loop = true;
        _audioSource.Play();

        IsControllable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_audioSource.volume > 0)
            _audioSource.volume -= (0.5f + _audioSource.volume) * Time.deltaTime;
    }

    public void Thrust()
    {
        _audioSource.volume = 0.10f * _rigidbody.velocity.magnitude;

        if (_rigidbody.velocity.magnitude < _maxSpeed)
            _rigidbody.AddRelativeForce(Vector3.up * _enginePower * Time.deltaTime);
    }

    public void Rotate(int side)
    {
        _rigidbody.AddRelativeTorque(Vector3.forward * side * _rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsControllable)
            return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                OnLevelComplete();
                break;
            default:
                OnDestruction();
                break;
        }
    }

    private void OnDestruction()
    {
        IsControllable = false;
        _audioSource.PlayOneShot(_destroySound, _destroySoundVolume);
        GlobalEventManager.ObstacleCollision();
    }

    private void OnLevelComplete()
    {
        IsControllable = false;
        _audioSource.PlayOneShot(_levelCompleteSound, _levelCompleteSoundVolume);
        GlobalEventManager.LevelComplete();
    }
}
