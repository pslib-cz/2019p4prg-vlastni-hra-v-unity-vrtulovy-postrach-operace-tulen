using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public int Health;
    public Transform Target;
    public float Speed;
    public float turnSpeed;
    public float fov;
    public GunController _cnn1, _cnn2;
    public AudioSource cnn_source;
    public float wtf, wtf2;


    // Start is called before the first frame update
    void Start()
    {
        Health = 30;
        Speed = 4f;
        turnSpeed = 0.3f;
        fov = 160;
    }

    // Update is called once per frame
    void Update()
    {
        wtf = Vector3.Distance(Target.position, transform.position);
        wtf2 = Vector3.Angle(transform.forward, Target.transform.position - transform.position);
        Turn();
        Move();

        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }

        if ((Vector3.Distance(Target.position, transform.position) > 20) || (Vector3.Angle(transform.forward, Target.transform.position - transform.position) < fov) || (Vector3.Angle(transform.forward, Target.transform.position - transform.position) > fov + 25))
        {
            _cnn1.isFiring = false;
            _cnn2.isFiring = false;
            cnn_source.Stop();
        }

        if ((Vector3.Distance(Target.position, transform.position) < 20) && ((Vector3.Angle(transform.forward, Target.transform.position - transform.position) >= fov) && (Vector3.Angle(transform.forward, Target.transform.position - transform.position) <= fov + 25)))
        {
            _cnn1.isFiring = true;
            _cnn2.isFiring = true;
            if (cnn_source.isPlaying != true) { cnn_source.Play(); }
            Debug.Log("I see");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();
            Health -= bullet.Bullet_Damage;
            Debug.Log("Hit");
        }

        if (!collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Bullet_Enemy"))
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
