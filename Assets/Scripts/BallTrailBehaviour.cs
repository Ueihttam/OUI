using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrailBehaviour : MonoBehaviour
{
    BoxCollider bc;
    MeshRenderer mr;
    public GameObject emptyParent;
    public Material transparentMaterial, solidMaterial;

    public List<GameObject> listeDesCubesDeTrail = new List<GameObject>();

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
    }

    public void TurnSolid()
    {
        bc.isTrigger = false;
        mr.material = solidMaterial;
        gameObject.layer = 10; //0 = default; 8 = TransparentTrail: 10 = SolidTrail
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TrailCube")
        {
            //print("Collision entre deux TrailCubes");
            if (other.GetComponent<BallTrailBehaviour>().emptyParent != emptyParent)
            {
                print("on les transforme en solide TADAAA");
                foreach (var item in other.GetComponent<BallTrailBehaviour>().emptyParent.GetComponent<ParentOfSingleTrailBehaviour>().listeDesCubesDeTrail)
                {
                    item.GetComponent<BallTrailBehaviour>().TurnSolid();
                }

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BallPrefab")
        {
            //collision.gameObject.GetComponent<BallBehaviour>().AddForceSomewhere();
            collision.gameObject.transform.rotation = transform.rotation;
            foreach (var item in emptyParent.GetComponent<ParentOfSingleTrailBehaviour>().listeDesCubesDeTrail)
            {
                Destroy(item);
            }
            emptyParent.GetComponent<ParentOfSingleTrailBehaviour>().listeDesCubesDeTrail.Clear();
        }
    }
}
