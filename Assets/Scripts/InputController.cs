using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Rocket _rocket;

    // Update is called once per frame
    void Update()
    {
        if (_rocket && _rocket.IsControllable)
        {
            CheckInput();
            if (Debug.isDebugBuild)
                CheckDebugInput();
        }
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.Space))
            _rocket.Thrust();

        if (Input.GetKey(KeyCode.A))
            _rocket.Rotate(1);

        if (Input.GetKey(KeyCode.D))
            _rocket.Rotate(-1);
    }

    private void CheckDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
            GlobalEventManager.ObstacleCollision();

        if (Input.GetKeyDown(KeyCode.Q))
            GlobalEventManager.LevelComplete();

        if (Input.GetKeyDown(KeyCode.C))
            _rocket.SwitchCollision();
    }
}
