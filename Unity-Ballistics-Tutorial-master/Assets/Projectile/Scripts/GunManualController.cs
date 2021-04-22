using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManualController : MonoBehaviour
{

    public Transform barrelTrans;
    public Transform endOfBarrelTrans;

    BulletData bulletData;
    LineRenderer lineRenderer;

    float timeStep;

    float rotationX, rotationY;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        bulletData = GetComponent<BulletData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeStep = Time.fixedDeltaTime;
        rotationX = transform.rotation.eulerAngles.x;
        rotationY = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))        rotationY -= 2f; 
        else if (Input.GetKey(KeyCode.D))   rotationY += 2f;
       

        if (Input.GetKey(KeyCode.W))        rotationX -= 1f;
        else if (Input.GetKey(KeyCode.S))   rotationX += 1f;


        float rotationXclamped = Mathf.Clamp(rotationX, 270, 360);

        transform.rotation = Quaternion.Euler(rotationXclamped, rotationY, 0);

        DrawTrajectoryPath();
    }



    //Display the trajectory path with a line renderer
    void DrawTrajectoryPath()
    {
        //Start values
        Vector3 currentVel = barrelTrans.up * 10f;  //bulletData.muzzleVelocity;
        Vector3 currentPos = endOfBarrelTrans.position;

        Vector3 newPos = Vector3.zero;
        Vector3 newVel = Vector3.zero;

        List<Vector3> bulletPositions = new List<Vector3>();

        //Build the trajectory line
        bulletPositions.Add(currentPos);

        //I prefer to use a maxIterations instead of a while loop 
        //so we always avoid stuck in infinite loop and have to restart Unity
        //You might have to change this value depending on your values
        int maxIterations = 10000;

        for (int i = 0; i < maxIterations; i++)
        {
            //Calculate the bullets new position and new velocity
            CurrentIntegrationMethod(timeStep, currentPos, currentVel, out newPos, out newVel);

            //Set the new value to the current values
            currentPos = newPos;
            currentVel = newVel;

            //Add the new position to the list with all positions
            bulletPositions.Add(currentPos);

            //The bullet has hit the ground because we assume 0 is ground height
            //This assumes the bullet is fired from a position above 0 or the loop will stop immediately
            if (currentPos.y < 0f)
            {
                break;
            }

            //A warning message that something might be wrong
            if (i == maxIterations - 1)
            {
                Debug.Log("The bullet newer hit anything because we reached max iterations");
            }
        }


        //Display the bullet positions with a line renderer
        lineRenderer.positionCount = bulletPositions.Count;

        lineRenderer.SetPositions(bulletPositions.ToArray());
    }



    //Choose which integration method you want to use to simulate how the bullet fly
    public static void CurrentIntegrationMethod(float timeStep, Vector3 currentPos, Vector3 currentVel, out Vector3 newPos, out Vector3 newVel)
    {
        //IntegrationMethods.BackwardEuler(timeStep, currentPos, currentVel, out newPos, out newVel);

        //IntegrationMethods.ForwardEuler(timeStep, currentPos, currentVel, out newPos, out newVel);

        //IntegrationMethods.Heuns(timeStep, currentPos, currentVel, out newPos, out newVel);

        IntegrationMethods.HeunsNoExternalForces(timeStep, currentPos, currentVel, out newPos, out newVel);
    }
}
