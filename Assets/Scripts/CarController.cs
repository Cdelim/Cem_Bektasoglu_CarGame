using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    private float movementSpeed = 20f;

    //Entrance and exit data
    private  List<Vector3> initialPositions = new List<Vector3>();
    private  List<Vector3> exitPositions = new List<Vector3>();
    private GameObject entrance;
    private GameObject exit;
    [SerializeField] private SpawnPoint pointData;

    //Car instance and prefab
    [SerializeField] private GameObject prefab; // Car Prefab Reference
    private GameObject currentCar; // Active Car Prefab
    private Car currentCarIns; // Active Car Script
    private int numberOfCar;
    private List<Car> cars = new List<Car>();

    //User's first touch to screen
    private bool isTouched = false;


    void Awake()
    {
        initialPositions = pointData.enterancePoints;
        exitPositions = pointData.exitPoints;
        
        numberOfCar =0;
        currentCar = Instantiate(prefab, initialPositions[numberOfCar], prefab.transfrom.rotation);
        currentCarIns=currentCar.GetComponent<Car>();
        currentCarIns.setInitialPosition(initialPositions[numberOfCar]);
        currentCarIns.setExitPosition(exitPositions[numberOfCar]);
        entrance = GameObject.Find("Entrance");
        entrance.transform.position = initialPositions[numberOfCar];
        exit = GameObject.Find("Exit");
        exit.transform.position = exitPositions[numberOfCar];
        PauseGame();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            isTouched = true; // Unfreeze the time 
        }
        if (isTouched)
        {
            ResumeGame();
        }
        currentCar.transform.Translate(currentCar.transform.up * Time.deltaTime*movementSpeed, Space.World);
        currentCar.transform.position = new Vector3(currentCar.transform.position.x, currentCar.transform.position.y, 0);
        Movement();

        /*if (Input.GetKey(KeyCode.A))
        {
            currentCar.transform.Rotate(0.0f, 0.0f, 100.0f*Time.deltaTime, Space.World);
            Debug.Log("Left Click");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentCar.transform.Rotate(0.0f, 0.0f, -100.0f*Time.deltaTime, Space.World);
            Debug.Log("Right click");
        }*/
        if (currentCarIns.isExit)
        {
            this.numberOfCar++;
            if (numberOfCar == 8)
            {
                //Next Level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            this.cars.Add(currentCarIns); //Add last car to cars list
            foreach (Car carObj in this.cars)
            {
                carObj.goBegining();
            }
            currentCar = Instantiate(prefab, initialPositions[this.numberOfCar], prefab.transfrom.rotation);
            currentCarIns = currentCar.GetComponent<Car>();
            currentCarIns.setInitialPosition(initialPositions[this.numberOfCar]);
            currentCarIns.setExitPosition(exitPositions[this.numberOfCar]);
            this.entrance.transform.position = initialPositions[this.numberOfCar];
            this.exit.transform.position = exitPositions[this.numberOfCar];
        }
        

    }

    //To get Inputs From User
    void Movement()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                currentCar.transform.Rotate(0.0f, 0.0f, 100.0f*Time.deltaTime, Space.World);
                Debug.Log("Left click");
            }
            else if (touch.position.x > Screen.width / 2)
            {
                currentCar.transform.Rotate(0.0f, 0.0f, -100.0f*Time.deltaTime, Space.World);
                Debug.Log("Right click");
            }
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
