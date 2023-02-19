using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;


    [Header("Audio effects")]
    [SerializeField] private AudioClip _thrustSound;
    [SerializeField] private AudioClip _destroySound;
    [SerializeField] private AudioClip _levelCompleteSound;

    [Header("Particle effects")]
    [SerializeField] private ParticleSystem _thrustEffect;
    [SerializeField] private ParticleSystem _destroyEffect;
    [SerializeField] private ParticleSystem _levelCompleteEffect;

    private int _enginePower = 2000;
    private int _rotationSpeed = 500;
    private int _maxSpeed = 5;
    private float _destroySoundVolume = 0.25f;
    private float _levelCompleteSoundVolume = 0.5f;

    public bool IsControllable { get; private set; }
    public bool IsCollisionActive {get; set;}

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
        IsCollisionActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsControllable)
        {
            if (_audioSource.volume > 0)
                _audioSource.volume -= (0.5f + _audioSource.volume) * Time.deltaTime;
            else
                _thrustEffect.Stop();
        }
    }

    public void Thrust()
    {
        _audioSource.volume = 0.10f * _rigidbody.velocity.magnitude;

        if (!_thrustEffect.isPlaying)
            _thrustEffect.Play();

        if (_rigidbody.velocity.magnitude < _maxSpeed)
            _rigidbody.AddRelativeForce(Vector3.up * _enginePower * Time.deltaTime);
    }

    public void Rotate(int side)
    {
        _rigidbody.AddRelativeTorque(Vector3.forward * side * _rotationSpeed * Time.deltaTime);
    }

    public void SwitchCollision()
    {
        IsCollisionActive = !IsCollisionActive;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsControllable || !IsCollisionActive)
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

        _thrustEffect.Stop();
        _destroyEffect.Play();

        _audioSource.Stop();
        _audioSource.volume = _destroySoundVolume;
        _audioSource.PlayOneShot(_destroySound);

        GlobalEventManager.ObstacleCollision();
    }

    private void OnLevelComplete()
    {
        IsControllable = false;

        _thrustEffect.Stop();
        _levelCompleteEffect.Play();

        _audioSource.Stop();
        _audioSource.volume = _levelCompleteSoundVolume;
        _audioSource.PlayOneShot(_levelCompleteSound);

        GlobalEventManager.LevelComplete();
    }
}
