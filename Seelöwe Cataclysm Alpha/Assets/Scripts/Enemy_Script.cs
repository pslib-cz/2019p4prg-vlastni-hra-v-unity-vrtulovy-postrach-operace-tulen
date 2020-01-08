using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public int Health;
    public Transform Target;
    public float Speed;
    public float turnSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Health = 20;
        Speed = 3.5f;
        turnSpeed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
        Turn();
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();
            Health -= bullet.Bullet_Damage;
            Debug.Log("Hit");
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    void Turn()
    {
        Vector3 pos = Target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(-pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    void Move()
    {
        transform.position -= transform.forward * Speed * Time.deltaTime;
    }

}
