using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private List<Vector3> pathRecord = new List<Vector3>();
    private List<Quaternion> rotationRecord = new List<Quaternion>();
    private Vector3 initialPosition=new Vector3(0,0,0);
    private Vector3 exitPosition=new Vector3(0,0,0);
    public bool isExit; // Car had reach to Exit
    private int frameCounter;
    private void Start()
    {
        this.isExit = false;
        frameCounter = 0;
        transform.position =initialPosition;
    }
    
    void Update()
    {
        if (isExit)
        {
            if (frameCounter == this.pathRecord.Count-1) // Last Frame
            {
                frameCounter = 0;
            }
            else
            {
                //Replay of car movement
                frameCounter++;
                transform.position = pathRecord[frameCounter];
                transform.rotation = rotationRecord[frameCounter];
            }
        }
        else {
            //Record of car movement
            this.pathRecord.Add(transform.position);
            this.rotationRecord.Add(transform.rotation);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Exit" && Vector3.Distance(transform.position, exitPosition) < 3.5f) // Is it exit and is it its exit
        {
            this.isExit = true;
            this.frameCounter = 0;
        }
        else if (this.isExit == true && collision.transform.tag == "Car") { // Is it car
            this.frameCounter = 0;
            transform.position = initialPosition;
        }
        else
        {
            transform.position = initialPosition;
            this.pathRecord.Clear();
        }
    }

    //When other car enter the exit all car start from initial positions.
    public void goBegining() {
        this.frameCounter = 0;
        transform.position = this.initialPosition;
    }
    public void setInitialPosition(Vector3 pos) {
        this.initialPosition = pos;
    }
    public void setExitPosition(Vector3 pos)
    {
        this.exitPosition = pos;
    }

}
