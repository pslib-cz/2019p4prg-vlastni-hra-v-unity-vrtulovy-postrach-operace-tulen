using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;
    public BulletController _bc;
    public Spitfire_Script _Spitfire;
    public float rateOfFire;
    public float bulletTimer;
    public Transform pointOfFire;


    // Start is called before the first frame update
    void Start()
    {
        bulletTimer = rateOfFire;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            bulletTimer -= Time.deltaTime;
            if (bulletTimer <= 0)
            {
                bulletTimer = rateOfFire;
                BulletController _nbc = Instantiate(_bc, pointOfFire.position, pointOfFire.rotation) as BulletController;
                _nbc.Bullet_Speed = _bc.Bullet_Speed;
                _nbc.Bullet_Damage = _bc.Bullet_Damage;
                if (_Spitfire != null)
                {
                    if (_bc.Bullet_Damage >= 20) { _Spitfire.Ammo_Cannons -= 1; }
                    else { _Spitfire.Ammo_MG -= 1; }
                }
            }
        }
    }
}
