using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitfire_Script : MonoBehaviour
{
    private Rigidbody _rb;
    //private Camera _cam;
    //private Collider _col;
    public WheelCollider _wr, _wl, _wb, _wb2;
    public GameObject _Spitfire;
    public float Wheel_Power;
    public float Fly_Speed;
    public float Altitude;
    public float Max_Fly_Speed;
    public float Forward_Power;
    public float Drive_Speed;
    public int Ammo_Cannons;
    public int Ammo_MG;
    public int Gun_Mode;
    public float Fuel;
    public bool openFlightControl = false;
    public bool isDestroyed = false;
    public bool isBoosting = false;
    public bool isFalling = false;
    public GunController _mg1, _mg2, _mg3, _mg4, _cnn1, _cnn2;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _Spitfire = GameObject.FindGameObjectWithTag("Player");
        Wheel_Power = 1500;
        Max_Fly_Speed = 6f;
        Fuel = 1800;
        Ammo_Cannons = 120;
        Ammo_MG = 1400;
        Gun_Mode = 0;
        //_cam = Camera.main;
        //_col = GetComponent<Collider>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        /*Palivo - Munice*/

        //úbytek paliva
        Fuel -= Time.deltaTime;

        //Pokuď dojde palivo
        if (Fuel <= 0)
        {
            if (Fly_Speed != 0 && Drive_Speed != 0)
            {
                Fly_Speed -= 0.1f;
                Drive_Speed -= 0.1f;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (Ammo_MG <= 0)
            {
                _mg1.isFiring = false;
                _mg2.isFiring = false;
                _mg3.isFiring = false;
                _mg4.isFiring = false;
            }

            if (Ammo_Cannons <= 0)
            {
                _cnn2.isFiring = false;
                _cnn1.isFiring = false;
            }

            if (Ammo_MG > 0 && Gun_Mode == 1)
            {
                _mg1.isFiring = true;
                _mg2.isFiring = true;
                _mg3.isFiring = true;
                _mg4.isFiring = true;
            }

            if (Ammo_Cannons > 0 && Gun_Mode == 2)
            {
                _cnn2.isFiring = true;
                _cnn1.isFiring = true;
            }

            if (Ammo_MG > 0 && Ammo_Cannons > 0 && Gun_Mode == 0)
            {
                _mg1.isFiring = true;
                _mg2.isFiring = true;
                _mg3.isFiring = true;
                _mg4.isFiring = true;
                _cnn2.isFiring = true;
                _cnn1.isFiring = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _mg1.isFiring = false;
            _mg2.isFiring = false;
            _mg3.isFiring = false;
            _mg4.isFiring = false;
            _cnn2.isFiring = false;
            _cnn1.isFiring = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (Ammo_MG <= 0)
            {
                _mg1.isFiring = false;
                _mg2.isFiring = false;
                _mg3.isFiring = false;
                _mg4.isFiring = false;
            }

            if (Ammo_Cannons <= 0)
            {
                _cnn2.isFiring = false;
                _cnn1.isFiring = false;
            }

            if (Ammo_MG > 0 && Gun_Mode == 1)
            {
                _mg1.isFiring = true;
                _mg2.isFiring = true;
                _mg3.isFiring = true;
                _mg4.isFiring = true;
            }

            if (Ammo_Cannons > 0 && Gun_Mode == 2)
            {
                _cnn2.isFiring = true;
                _cnn1.isFiring = true;
            }

            if (Ammo_MG > 0 && Ammo_Cannons > 0 && Gun_Mode == 0)
            {
                _mg1.isFiring = true;
                _mg2.isFiring = true;
                _mg3.isFiring = true;
                _mg4.isFiring = true;
                _cnn2.isFiring = true;
                _cnn1.isFiring = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mg1.isFiring = false;
            _mg2.isFiring = false;
            _mg3.isFiring = false;
            _mg4.isFiring = false;
            _cnn2.isFiring = false;
            _cnn1.isFiring = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Gun_Mode += 1;
            if (Gun_Mode == 3) { Gun_Mode = 0; }
        }

        /*Palivo - Munice*/

        /*Ovládání Letadla - ZEMĚ*/

        //Přidávání rychlosti jízdy
        if (!openFlightControl && (Fuel > 0))
        {
            Forward_Power = Input.GetAxis("Throttle") * Wheel_Power;

            if (Forward_Power > 0)
            {
                Forward_Power = 0;
            }
        }

        //Rozhýbání kol
        _wl.motorTorque = Forward_Power;
        _wr.motorTorque = Forward_Power;
        _wb.motorTorque = Forward_Power;
        _wb2.motorTorque = Forward_Power;

        //přepočet rychlosti
        var getVelocity = _rb.velocity;
        Drive_Speed = getVelocity.magnitude;

        //Vzlétnutí
        if (Drive_Speed >= 2.0 && (!openFlightControl))
        {
            openFlightControl = true;
            Forward_Power = 0;
            Fly_Speed = Drive_Speed;
            _rb.velocity = new Vector3 (0,0,0);
        }

        //Zvedání ocasu
        if (Drive_Speed >= 1.5 && Drive_Speed < 2.0 && (!openFlightControl))
        {
            transform.Rotate(-(float)0.03, 0, 0);
        }

        /*Ovládání Letadla - ZEMĚ*/

        /*Ovládání Letadla - VZDUCH*/

        //Létání
        if (openFlightControl && (Fuel > 0) && isDestroyed == false)
        {
            //Pohyb do všech možných směrů
            transform.position -= transform.forward * Time.deltaTime * Fly_Speed;
            transform.Rotate(-Input.GetAxis("Vertical"), Input.GetAxis("Steer") * (float)0.25, Input.GetAxis("Horizontal"));


            if (transform.forward.y >= 0)
            {
                Fly_Speed += transform.forward.y * Time.deltaTime / 2;

                if (Fly_Speed >= 1.3)
                {
                    isFalling = false;
                }

                if (isFalling)
                {
                    transform.Rotate(-(float)0.3, 0, 0);
                }
            }

            if (transform.forward.y < 0)
            {
                if (Fly_Speed == 0)
                {
                    isFalling = true;
                }

                if (isFalling)
                {
                    transform.Rotate(-(float)0.3, 0, 0);
                }

                Fly_Speed += transform.forward.y * Time.deltaTime / 8;
            }

            if (Fly_Speed > Max_Fly_Speed)
            {
                Fly_Speed = Max_Fly_Speed;
            }

            if (Input.GetAxis("Throttle") != 0)
            {
                if (Input.GetAxis("Throttle") < 0)
                {
                    Fly_Speed += -Input.GetAxis("Throttle") / 100;
                    if (Fly_Speed >= 6)
                    {
                        
                        Max_Fly_Speed = (float)6.8;
                        isBoosting = true;
                        Fuel -= Time.deltaTime * 2;
                    }
                }

                if (Input.GetAxis("Throttle") > 0)
                {
                    Fly_Speed += -Input.GetAxis("Throttle") / 100;
                    isBoosting = false;
                }
            }

            if (Input.GetAxis("Throttle") == 0)
            {
                Max_Fly_Speed = 6;
                isBoosting = false;
            }
        }

        /*Ovládání Letadla - END*/

        /*Fyzikální sekce*/

        Altitude = _rb.position.y;

        if (openFlightControl && !isDestroyed)
        {
            if (Fly_Speed >= 2.0)
            {
                Physics.gravity = new Vector3(0, 0, 0);
                _rb.velocity = new Vector3(0, 0, 0);
            }

            if (Fly_Speed < 2.0)
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

        if (isDestroyed)
        {
            Physics.gravity = new Vector3(0, (float)-0.5, 0);
            if (Fly_Speed > 0) { Fly_Speed -= 0.1f; }
        }

        /*Fyzikální sekce - END*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject[] destroy = GameObject.FindGameObjectsWithTag("DestroyablePart");
        if ((collision.gameObject.CompareTag("Mapa") && Fly_Speed >= 3) || (collision.gameObject.CompareTag("Enemy")))
        {
            foreach (GameObject go in destroy)
            {
                _rb.constraints = RigidbodyConstraints.None;
                Destroy(go);
                isDestroyed = true;
            }
            
        }
    }

}

