using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public string whichOne;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BallPrefab")
        {
            collision.gameObject.GetComponent<BallBehaviour>().AddForceToLeftOrRight(whichOne);
        }
    }
}
