using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Points", menuName
= "Points")]
public class SpawnPoint : ScriptableObject
{
    //Save to entrance and exit points data from editor
    [SerializeField] public List<Vector3> enterancePoints = new List<Vector3>();
    [SerializeField] public List<Vector3> exitPoints=new List<Vector3>();
}
