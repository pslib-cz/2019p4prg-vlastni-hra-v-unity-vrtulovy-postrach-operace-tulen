using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirfire_Script : MonoBehaviour
{
    private Rigidbody _rb;
    //private Collider _col;
    public WheelCollider _wr, _wl, _wb, _wb2;
    public float Wheel_power = 1500;
    public float Fly_Speed;
    public float Max_Fly_Speed = (float)2;
    public bool openFlightControl = false;
    public float forward_power;
    public float Drive_Speed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Ovládání Letadla - ZEMĚ*/

        if (!openFlightControl)
        {
            forward_power = Input.GetAxis("Throttle") * Wheel_power;
        }

        _wl.motorTorque = forward_power;
        _wr.motorTorque = forward_power;
        _wb.motorTorque = forward_power;
        _wb2.motorTorque = forward_power;

        var getVelocity = _rb.velocity;
        Drive_Speed = getVelocity.magnitude;

        if (Drive_Speed >= 1.8 && (!openFlightControl))
        {
            openFlightControl = true;
            forward_power = 0;
            Fly_Speed = Drive_Speed;
        }


        /*Ovládání Letadla - ZEMĚ*/

        /*Ovládání Letadla - VZDUCH*/

        //transform.Rotate(-Input.GetAxis("Mouse Y"), 0, Input.GetAxis("Mouse X"));

        if (openFlightControl)
        {
            transform.position -= transform.forward * Time.deltaTime * Fly_Speed;         //Beep
            transform.Rotate(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            Fly_Speed -= (Input.GetAxis("Throttle") / 10);         //Beep
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, Time.deltaTime * -10, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, Time.deltaTime * 10, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * -1);
        }

        if (Fly_Speed > Max_Fly_Speed)
        {
            Fly_Speed = Max_Fly_Speed;
        }

        /*Ovládání Letadla - END*/

        /*Fyzikální sekce*/

        //Beep
        Fly_Speed += transform.forward.y * Time.deltaTime * (float)0.5;

        if (openFlightControl)
        {
            if (Fly_Speed >= 1.8)
            {
                Physics.gravity = new Vector3(0, (float)-0.05, 0);
            }

            if (Fly_Speed < 1.8)
            {
                Physics.gravity = new Vector3(0, (float)-0.25, 0);
            }

            if (Fly_Speed == 0)
            {
                Physics.gravity = new Vector3(0, (float)-0.5, 0);
            }

            if (Fly_Speed < 0)
            {
                Fly_Speed = 0;
            }
        }

        /*Fyzikální sekce - END*/

    }
}

