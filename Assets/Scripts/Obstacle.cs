using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle 
{
    //string tag = "Obstacle";
    string shape;
    GameObject obj;
    public Obstacle(string shape,Vector3 Position) {
        //  this.tag = "Obstacle";
        this.shape = shape;
        if (shape == "Cube")
        {
            this.obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.obj.transform.position = new Vector3(0, 0, 0);
            this.obj.AddComponent<Collider>();
        }
        else if (shape == "Sphere") {
            this.obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            this.obj.transform.position = new Vector3(0, 0, 0);
            this.obj.AddComponent<Collider>();
        }
    }
    public void setObjPos(float x, float y) {
        this.obj.transform.position = new Vector3(x,y,0);
    }
    public void setScale(float x, float y)
    {
        this.obj.transform.localScale = new Vector3(x, y, 0);
    }
}
