using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float Bullet_Speed;
    public int Bullet_Damage;
    public float destroyTime;
    public BulletController _bc;

    // Start is called before the first frame update
    void Start()
    {
        destroyTime = 2.5f;
        Bullet_Speed = 36f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Bullet_Speed * Time.deltaTime);
        Physics.IgnoreCollision(_bc.GetComponent<Collider>(), _bc.GetComponent<Collider>());
        Destroy(this.gameObject, destroyTime);
    }
}
