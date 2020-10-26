using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class LevelDesigner : EditorWindow
{
    //Settings of Obstacles
    private float obstaclePosx = 0;
    private float obstaclePosy = 0;
    private float obstacleScalex = 5;
    private float obstacleScaley = 5;

    //Settings of Entrance && Exit points
    private float initialPosx = 0;
    private float initialPosy = 0;
    private float exitPosx = 0;
    private float exitPosy = 0;
    private bool isSpawnedObstacle = false;
    private int selected = 0;
    private string[] shapeObstacles = new string[2] { "Cube", "Sphere" };
    Obstacle newObstacle;
    public static GameObject initialText;
    public static GameObject exitText;

    private int countPositions=0;// Number of Entrance && Exit points

    private SpawnPoint pointData;//To save data 

    private void OnEnable()
    {
        //If there are same asset it will be deleted
        string[] result = UnityEditor.AssetDatabase.FindAssets("Points"+SceneManager.GetActiveScene().buildIndex.ToString());

        if (result.Length != 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(result[0]);
            AssetDatabase.DeleteAsset(path);
        }
        pointData = (SpawnPoint)ScriptableObject.CreateInstance(typeof(SpawnPoint));


        initialText = new GameObject();
        initialText.name = "Entrance";
        initialText.AddComponent<TextMesh>();
        initialText.GetComponent<TextMesh>().text = "Entrance!";
        initialText.transform.localScale = new Vector3(2, 2, 1);

        exitText = new GameObject();
        exitText.name = "Exit";
        exitText.AddComponent<TextMesh>();
        exitText.GetComponent<TextMesh>().text = "Exit!";
        exitText.transform.localScale = new Vector3(2, 2, 1);
        exitText.AddComponent<BoxCollider>();
        exitText.tag = "Exit";

    }
    [MenuItem("Window/LevelCreator")]
    static void OpenWindow() {
        LevelDesigner window = (LevelDesigner)GetWindow(typeof(LevelDesigner));
        window.minSize = new Vector2(300, 300);
        window.Show();
    }
    private void OnGUI()
    {
        //Obstacles
        GUILayout.Label("Spawn Obstacle", EditorStyles.boldLabel);
        selected = EditorGUILayout.Popup("Obstacle Shape", selected, shapeObstacles);
        obstaclePosx = EditorGUILayout.FloatField("Obstacle PosX", obstaclePosx);
        obstaclePosy = EditorGUILayout.FloatField("Obstacle PosY", obstaclePosy);
        obstacleScalex = EditorGUILayout.FloatField("Obstacle ScaleX", obstacleScalex);
        obstacleScaley = EditorGUILayout.FloatField("Obstacle ScaleY", obstacleScaley);
        if (GUILayout.Button("Spawn Obstacle")) {
            newObstacle = new Obstacle(shapeObstacles[selected],new Vector3(obstaclePosx,obstaclePosy,0));
            isSpawnedObstacle = true;
        }
        if (isSpawnedObstacle == true)
        {
            newObstacle.setObjPos(obstaclePosx, obstaclePosy);
            newObstacle.setScale(obstacleScalex, obstacleScaley);
        }
        //------------------------------------------------

        //Positions
        if (countPositions < 8)
        {
            GUILayout.Label("Entrance & Exit Positions", EditorStyles.boldLabel);

            initialPosx = EditorGUILayout.FloatField("Initial PosX", initialPosx);
            initialPosy = EditorGUILayout.FloatField("Initial PosY", initialPosy);
            exitPosx = EditorGUILayout.FloatField("Exit PosX", exitPosx);
            exitPosy = EditorGUILayout.FloatField("Exit PosY", exitPosy);
            initialText.transform.position = new Vector3(initialPosx, initialPosy, 0);
            exitText.transform.position = new Vector3(exitPosx, exitPosy, 0);
            if (GUILayout.Button("Okey"))
            {
                pointData.enterancePoints.Add(initialText.transform.position);
                pointData.exitPoints.Add(exitText.transform.position);
                countPositions++;

            }
        }
        //----------------------------------------------------------------------
   
    }
    private void OnDisable()
    {
        //Save Data as Asset with Scriptableobject
        EditorUtility.SetDirty(pointData);
        AssetDatabase.CreateAsset(pointData, "Assets/Points"+ SceneManager.GetActiveScene().buildIndex .ToString()+".asset");
        AssetDatabase.SaveAssets();
    }
}
