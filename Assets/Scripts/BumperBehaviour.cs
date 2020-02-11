using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperBehaviour : MonoBehaviour
{
    public float bumperPower = 6f;
        
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BallPrefab")
        {
            collision.gameObject.GetComponent<BallBehaviour>().AddForceSomewhere(transform.position, collision.transform.position, bumperPower);
        }
    }
}
