using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBirdController : MonoBehaviour
{  
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerEnter is called
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag ("New") || other.CompareTag("Hit"))
        {
            other.tag = "Hit";
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(rb!=null)
            {
                rb.isKinematic = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 3f);
    }
}
