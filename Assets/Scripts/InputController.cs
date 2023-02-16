using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Rocket _rocket;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (_rocket)
            CheckInput();
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
}
