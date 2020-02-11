using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public Transform basePosition;
    public GameObject trailObject, emptyParentASpawn, emptyParentActuel;
    public float thrustPower = 3f, timeBetweenTrailCreation = 0.6f, timeOfTrailCreationPause = 0.2f;
    bool createTrail;
    public bool doitFaireApparaitreTrails; //cvette variable, si activée, active la création de trails derrière la bille
    public float timerCreateTrail;
    public float posX;
    public float posY;
    public float posZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timerCreateTrail = timeBetweenTrailCreation;
        createTrail = true;
        GameObject obj = Instantiate(emptyParentASpawn);
        obj.transform.position = transform.position;
        emptyParentActuel = obj;
    }

    void Update()
    {
        timerCreateTrail -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.P))
        {
            rb.AddForce(Vector3.left, ForceMode.VelocityChange);
        }

        if (timerCreateTrail <= 0f)
        {
            if (createTrail)
            {
                //lorsque la bille arrête de créer des trails, il n'y a plus de trail actuel
                createTrail = false;
                timerCreateTrail = timeOfTrailCreationPause;
            }
            else if (!createTrail)
            {
                //lorsque la bille commence à créer des trails, elle crée un parent dans lequel on va mettre tous les cubes de la trail
                createTrail = true; //la bille passe en mode création de trails
                timerCreateTrail = timeBetweenTrailCreation; //la bille va créer des trails pendant timeBetweenTrailCreation secondes
                GameObject obj = Instantiate(emptyParentASpawn); //on crée l'objet emptyParent qui va gérer tous les cubes de la trail
                obj.transform.position = transform.position;
                emptyParentActuel = obj; //l'empty parent qui vient d'être créé, c'est l'actuel
            }
        }

        if (createTrail && doitFaireApparaitreTrails)
        {
            GameObject obj = Instantiate(trailObject);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;

            obj.GetComponent<BallTrailBehaviour>().emptyParent = emptyParentActuel;
            emptyParentActuel.GetComponent<ParentOfSingleTrailBehaviour>().listeDesCubesDeTrail.Add(obj);
        }

        Mathf.Clamp(posX, posX, posZ);
        transform.position = new Vector3(posX, posY, posZ);

    }

    public void AddForceToLeftOrRight(string origineDeLaForce = "")
    {
        if (origineDeLaForce == "Left")
        {
            rb.AddForce(Vector3.right * thrustPower, ForceMode.VelocityChange);
        }
        else if (origineDeLaForce == "Right")
        {
            rb.AddForce(Vector3.left * thrustPower, ForceMode.VelocityChange);
        }
    }

    public void AddForceSomewhere(Vector3 origineDeLaForce, Vector3 direction, float forcePower)
    {
        rb.AddForce(direction * forcePower, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "GameOver")
        {
            foreach (var singleEmptyParent in GameObject.FindGameObjectsWithTag("EmptyParent"))
            {
                foreach (var singleBallTrail in singleEmptyParent.GetComponent<ParentOfSingleTrailBehaviour>().listeDesCubesDeTrail)
                {
                    singleBallTrail.GetComponent<BallTrailBehaviour>().TurnSolid();
                }
            }
            transform.Translate(basePosition.transform.position);
        }
    }
}
