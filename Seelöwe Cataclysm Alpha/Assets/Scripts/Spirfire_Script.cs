using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirfire_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Input.GetAxis("Mouse Y") * -1, 0, -Input.GetAxis("Mouse X") * -1);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -1, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * -1);
        }
    }
}

