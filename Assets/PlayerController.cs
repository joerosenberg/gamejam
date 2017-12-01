using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector3 pCoords = new Vector3(0, 0, 0);
    public float pSpeed = 1f;
    public Vector3 lPos = new Vector3(0,0,0);
    public Vector3 tPos = new Vector3(0, 0, 0);
    public Vector3 pPos = new Vector3(0, 0, 0);
    public Vector3 tDir = new Vector3(1, 0, 0);
    public Vector3 pDir = new Vector3(1, 0, 0);
    public string pState = "moving";
    public int m = 0; // steps taken since reaching last vertex
    public int k = 0; // steps taken since starting turn
    public int moveSteps = 20; // nb of update steps to move between vertices
    public int turnSteps = 10; // nb of update steps to change direction
    public int turnAngle = 0;
    

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    private void Update()
    {
        transform.position = pPos;
    }

    void FixedUpdate () {
		if (pState == "moving")
        {
            pPos = pPos + pDir / moveSteps;
            m++;

            if (m == moveSteps)
            {
                pState = "finishedMoving";
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                pState = "stopped";
            }
            else if (Input.GetKey(KeyCode.S))
            {
                pState = "turning";
                Vector3 temp = lPos; // swap last and target vertices
                lPos = tPos;
                tPos = temp;
                m = moveSteps - m;
                tDir = -tDir; // set target direction to opposite direction
                turnAngle = 180;
            }
        }
        else if (pState == "finishedMoving")
        {
            m = 0;
            if (Input.GetKey(KeyCode.A))
            {
                tDir = Quaternion.AngleAxis(90, Vector3.up) * tDir; // may be opposite direction, check
                turnAngle = 90;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                tDir = Quaternion.AngleAxis(-90, Vector3.up) * tDir; // may be opposite direction, check
                turnAngle = -90;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                tDir = Quaternion.AngleAxis(180, Vector3.up) * tDir;
                turnAngle = 180;
            }
           
            if (PathExists(tPos, tPos + tDir))
            {
                if (tDir == pDir)
                {
                    pState = "moving";
                }
                else
                {
                    pState = "turning";
                }
                    lPos = tPos;
                tPos = lPos + tDir;
            }
        }
        else if (pState == "turning")
        {
            pDir = Quaternion.AngleAxis(turnAngle / turnSteps, Vector3.up) * pDir;
            k++;

            if (k == turnSteps)
            {
                pDir = tDir;
                pState = "finishedMoving";
                k = 0;
            }
        }


	}

    bool PathExists(Vector3 v1, Vector3 v2)
    {
        // placeholder: should check adjacency of two vertices
        return true;
    }
}
