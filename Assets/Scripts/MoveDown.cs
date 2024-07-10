using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody objectRb;
    private float bottomBoundZ = -15f;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        objectRb.AddForce(Vector3.back * speed);
        
        //Destroy objects when they get out of bounds
        if(transform.position.z < bottomBoundZ)
        {
            Destroy(gameObject);
        }
    }
}
