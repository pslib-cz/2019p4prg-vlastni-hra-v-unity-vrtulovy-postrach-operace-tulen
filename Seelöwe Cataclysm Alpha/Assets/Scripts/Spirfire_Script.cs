using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirfire_Script : MonoBehaviour
{
    private Rigidbody _rb;
    //private Camera _cam;
    //private Collider _col;
    public WheelCollider _wr, _wl, _wb, _wb2;
    public float Wheel_Power = 1500;
    public float Fly_Speed;
    public float Max_Fly_Speed = (float)3;
    public float Forward_Power;
    public float Drive_Speed;
    public bool openFlightControl = false;
    public bool cameraOffsetBool;
    public float cameraOffsetValue;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_cam = Camera.main;
        //_col = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        /*Ovládání Letadla - ZEMĚ*/

        if (!openFlightControl)
        {
            Forward_Power = Input.GetAxis("Throttle") * Wheel_Power;
        }

        _wl.motorTorque = Forward_Power;
        _wr.motorTorque = Forward_Power;
        _wb.motorTorque = Forward_Power;
        _wb2.motorTorque = Forward_Power;

        var getVelocity = _rb.velocity;
        Drive_Speed = getVelocity.magnitude;


        if (Drive_Speed >= 0.25 && (!openFlightControl))
        {
            transform.Rotate(-Input.GetAxis("Vertical"), Input.GetAxis("Steer") * (float)0.25, Input.GetAxis("Horizontal"));
        }

        if (Drive_Speed >= 1.8 && (!openFlightControl))
        {
            openFlightControl = true;
            Forward_Power = 0;
            Fly_Speed = Drive_Speed * (float)1.5;
            _rb.velocity = new Vector3 (0,0,0);
        }

        if (Drive_Speed >= 1.5 && Drive_Speed < 1.8 && (!openFlightControl))
        {
            transform.Rotate(-(float)0.03, 0, 0);
        }

        /*Ovládání Letadla - ZEMĚ*/

        /*Ovládání Letadla - VZDUCH*/

        if (openFlightControl)
        {
            transform.position -= transform.forward * Time.deltaTime * Fly_Speed;
            transform.Rotate(-Input.GetAxis("Vertical"), Input.GetAxis("Steer") * (float)0.25, Input.GetAxis("Horizontal"));

            if (transform.forward.y > 0)
            {
                Fly_Speed += transform.forward.y * Time.deltaTime / 2;
            }

            if (transform.forward.y < 0)
            {
                Fly_Speed += transform.forward.y * Time.deltaTime / 8;
                if (Fly_Speed == 0)
                {
                    transform.Rotate((float)0.9, 0, 0);
                }
            }

            if (Fly_Speed > Max_Fly_Speed)
            {
                Fly_Speed = Max_Fly_Speed;
            }

            if (Input.GetAxis("Throttle") != 0)
            {
                if (Input.GetAxis("Throttle") < 0)
                {
                    Fly_Speed += -Input.GetAxis("Throttle") / 50;
                    Max_Fly_Speed = (float)3.8;
                }

                if (Input.GetAxis("Throttle") > 0)
                {
                    Fly_Speed += -Input.GetAxis("Throttle") / 100;
                }
            }

            if (Input.GetAxis("Throttle") == 0)
            {
                Max_Fly_Speed = 3;
            }
        }

        /*Ovládání Letadla - END*/

        /*Fyzikální sekce*/

        if (openFlightControl)
        {
            if (Fly_Speed >= 1.8)
            {
                Physics.gravity = new Vector3(0, 0, 0);
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

