using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{

    public Transform Sphere;
    //private Vector3 startPos;

    private Vector3 currentPos;
    private Vector3 currentVel;

    private Vector3 nextPos;
    private Vector3 nextVel;

    private float initVel = 10f;

    private BulletData bulletDataa;


    private void Awake()
    {
        //startPos = new Vector3(0f, 20f, 0f);
       
    }


    // Start is called before the first frame update
    void Start()
    {
        bulletDataa = GetComponent<BulletData>();
    }



    private void FixedUpdate()
    {
        MoveSphereOneStep();
    }

    public void MoveSphereOneStep()
    {

        float timeStep = Time.fixedDeltaTime;

        IntegrationMethods.Heuns(timeStep, currentPos, currentVel, transform.up, bulletDataa, out nextPos, out nextVel);

        currentPos = nextPos;
        currentVel = nextVel;

        transform.position = currentPos;
        transform.forward = currentVel.normalized;
    }

    public void SetStartParam(Vector3 startPos, Vector3 startDir)
    {
        this.currentPos = startPos;
        this.currentVel = initVel * startDir;

        transform.position = startPos;
        transform.forward = startDir;

    }
}
