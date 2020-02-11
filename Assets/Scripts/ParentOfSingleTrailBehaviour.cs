using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentOfSingleTrailBehaviour : BallTrailBehaviour
{
    private void Update()
    {
        if (listeDesCubesDeTrail.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
