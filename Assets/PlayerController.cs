using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {

    /* public string[] initialMap = new string[]
     {"ur", "lr", "lr", "lr", "lr", "lr", "lr", "lr", "lr", "lr", "lr", "ulr", "lr",
      "ud", "", "", "", "", "", "", "", "", "", "ud", "",
      "ud", "", "", "", "", "", "", "", "", "", "ud", "",
      "dr", "lr", "ulr", "lr", "ul", "", "", "ur", "lr", "lr", "dl",
      "", "", "ud", "", "", "ud", "", "", "ud", };*/

    public Dictionary<Vector3, string> initialMap = new Dictionary<Vector3, string>()
    {
        {new Vector3(1,1,0), "ur" },
        {new Vector3(2,1,0), "lr" },
        {new Vector3(3,1,0), "lr" },
        {new Vector3(4,1,0), "lr" },
        {new Vector3(5,1,0), "lr" },
        {new Vector3(6,1,0), "lr" },
        {new Vector3(7,1,0), "lr" },
        {new Vector3(8,1,0), "lr" },
        {new Vector3(9,1,0), "lr" },
        {new Vector3(10,1,0), "lr" },
        {new Vector3(11,1,0), "lr" },
        {new Vector3(12,1,0), "ulr" },
        {new Vector3(13,1,0), "lr" },

        {new Vector3(1,2,0), "ud" },
        {new Vector3(12,2,0), "ud" },

        {new Vector3(1,3,0), "ud" },
        {new Vector3(12,3,0), "ud" },

        {new Vector3(1,4,0), "dr" },
        {new Vector3(2,4,0), "lr" },
        {new Vector3(3,4,0), "ulr" },
        {new Vector3(4,4,0), "lr" },
        {new Vector3(5,4,0), "lr" },
        {new Vector3(6,4,0), "ul" },
        {new Vector3(9,4,0), "ur" },
        {new Vector3(10,4,0), "lr" },
        {new Vector3(11,4,0), "lr" },
        {new Vector3(12,4,0), "dl" },

        {new Vector3(3,5,0), "ud" },
        {new Vector3(6,5,0), "ud" },
        {new Vector3(9,5,0), "ud" },

        {new Vector3(3,6,0), "ud" },
        {new Vector3(6,6,0), "ud" },
        {new Vector3(9,6,0), "ud" },

        {new Vector3(1,7,0), "ur" },
        {new Vector3(2,7,0), "lr" },
        {new Vector3(3,7,0), "dl" },
        {new Vector3(6,7,0), "udr" },
        {new Vector3(7,7,0), "lr" },
        {new Vector3(8,7,0), "lr" },
        {new Vector3(9,7,0), "dlr" },
        {new Vector3(10,7,0), "lr" },
        {new Vector3(11,7,0), "lr" },
        {new Vector3(12,7,0), "ulr" },
        {new Vector3(13,7,0), "lr" },

        {new Vector3(1,8,0), "ud" },
        {new Vector3(6,8,0), "ud" },
        {new Vector3(12,8,0), "ud" },

        {new Vector3(1,9,0), "ud" },
        {new Vector3(6,9,0), "ud" },
        {new Vector3(12,9,0), "ud" },

        {new Vector3(1,10,0), "dr" },
        {new Vector3(2,10,0), "lr" },
        {new Vector3(3,10,0), "lr" },
        {new Vector3(4,10,0), "lr" },
        {new Vector3(5,10,0), "lr" },
        {new Vector3(6,10,0), "udlr" },
        {new Vector3(7,10,0), "lr" },
        {new Vector3(8,10,0), "lr" },
        {new Vector3(9,10,0), "ulr" },
        {new Vector3(10,10,0), "lr" },
        {new Vector3(11,10,0), "lr" },
        {new Vector3(12,10,0), "dl" },

        {new Vector3(6, 11, 0), "ud" },
        {new Vector3(9, 11, 0), "ud" },

        {new Vector3(9, 12, 0), "ud" },
        {new Vector3(9, 12, 0), "ud" },

        {new Vector3(9, 13, 0), "ud" },
        {new Vector3(9, 13, 0), "udr" },

        {new Vector3(9, 14, 0), "ud" },
        {new Vector3(9, 14, 0), "ud" },

        {new Vector3(9, 15, 0), "ud" },
        {new Vector3(9, 15, 0), "ud" },

        {new Vector3(1,16,0), "lr" },
        {new Vector3(2,16,0), "lr" },
        {new Vector3(3,16,0), "lr" },
        {new Vector3(4,16,0), "lr" },
        {new Vector3(5,16,0), "lr" },
        {new Vector3(6,16,0), "udlr" },
        {new Vector3(7,16,0), "dr" },
        {new Vector3(8,16,0), "dr" },
        {new Vector3(9,16,0), "dr" },
    };

    public Vector3 pCoords = new Vector3(0, 0, 0);
    public float pSpeed = 1f;
    public Vector3 lPos = new Vector3(1,0,0);
    public Vector3 tPos = new Vector3(2, 1, 0);
    public Vector3 pPos = new Vector3(1, 1, 0);
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

    bool PathExists(Vector3 vFrom, Vector3 dir)
    {
        // placeholder: should check adjacency of two vertices
        char dirChar;

        if (dir == Vector3.left)
        {
            dirChar = 'l';
        }
        else if (dir == Vector3.right)
        {
            dirChar = 'r';
        }
        else if (dir == Vector3.forward)
        {
            dirChar = 'u';
        }
        else 
        {
            dirChar = 'd';
        }


        return initialMap[vFrom].Contains(dirChar);
    }
}
