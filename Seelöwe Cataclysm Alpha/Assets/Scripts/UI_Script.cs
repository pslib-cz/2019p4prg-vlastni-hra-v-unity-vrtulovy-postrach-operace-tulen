using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public Canvas _UI;
    public Text _Speed;
    public Text _Altitude;
    public Text _Health;
    public Text _Boost;
    public Text _Destroyed;
    public Text _MGAmmo;
    public Text _CNNAmmo;
    public Text _Fuel;
    public Text _Notification;
    public Text _Firemode;
    public Spitfire_Script _SS;


    // Start is called before the first frame update
    void Start()
    {
        _SS = GetComponent<GameObject>().GetComponentInParent<Spitfire_Script>();
        _UI = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rychlost při jízdě na zemi
        if (!_SS.openFlightControl)
        {
            _Speed.text = ("Speed: " + Mathf.Round((87.35f * _SS.Drive_Speed)).ToString() + " km/h");
            _Notification.text = "Press and hold SHIFT key to take off";
        }

        //Rychlost letu
        if (_SS.openFlightControl)
        {
            _Speed.text = ("Speed: " + Mathf.Round((87.35f * _SS.Fly_Speed)).ToString() + " km/h");
            _Notification.text = "";
        }

        //Boost rychlosti
        if (_SS.isBoosting)
        {
            _Boost.text = "Warning - Motor is overclocked!";
        }

        //Zničení letadla hráče
        if (_SS.isDestroyed)
        {
            _Destroyed.text = "Vehicle destroyed.";
        }

        //Vymazání
        if (!_SS.isBoosting)
        {
            _Boost.text = "";
        }

        //Vymazání
        if (!_SS.isDestroyed)
        {
            _Destroyed.text = "";
        }

        //Munice, výška, palivo
        _MGAmmo.text = ("MG Ammo: " + _SS.Ammo_MG);
        _CNNAmmo.text = ("CNN Ammo: " + _SS.Ammo_Cannons);
        _Fuel.text = ("Fuel: " + Mathf.Floor(_SS.Fuel / 60) + " Minutes");
        _Altitude.text = ("Altitude: " + Mathf.Round(_SS.Altitude * 41.5f).ToString() + " meters");
        
        switch (_SS.Gun_Mode)
        {
            case 0:
                _Firemode.text = ("Firemode: MG + CNN");
                break;
            case 1:
                _Firemode.text = ("Firemode: MG");
                break;
            case 2:
                _Firemode.text = ("Firemode: CNN");
                break;
        }
    }
}
