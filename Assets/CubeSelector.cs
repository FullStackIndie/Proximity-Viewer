using System.Collections.Generic;
using UnityEngine;

// Commented for further understanding and for future viewers of this code
public class CubeSelector : MonoBehaviour
{
    Vector2 CubeSpawnPosition;
    public GameObject cubePrefab; // your cube prefab
    public int CurrentCubeInList = 0;
    public static int nameCounter = 1; // keeps track of how many cubes have been created

    public static List<GameObject> CubeList = new List<GameObject>();

    void Start()
    {
        CubeSpawnPosition = new Vector2(5, 10);
    }

    void Update()
    {
        if (Input.GetKeyDown("right"))
        {
            if (CurrentCubeInList >= CubeList.Count - 1)
            {
                CurrentCubeInList = 0;
            }
            else
            {
                CurrentCubeInList += 1;
            }
        }

        if (Input.GetKeyDown("left"))
        {
           if (CurrentCubeInList <= 0)
            {
                if(CubeList.Count == 0)
                {
                    CurrentCubeInList = 0;
                }
                else
                {
                    CurrentCubeInList = CubeList.Count - 1;
                }
            }
            else
            {
                CurrentCubeInList -= 1;
            }
        }

       if(CubeList.Count > 0)
        {
            foreach (GameObject cube in CubeList)
            {
                if (cube == CubeList[CurrentCubeInList])
                {
                    cube.GetComponent<CubeController>().enabled = true; // turns On the CubeController script so only the selected cube will move
                }
                else
                {
                    cube.GetComponent<CubeController>().enabled = false;// turns Off all other CubeController scripts so only the selected cube will move
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(CubeList.Count > 0) // only runs if CubeList is populated
            {
                foreach(GameObject cube in CubeList) // Search the cube list
                {
                    if(cube == CubeList[CurrentCubeInList])
                    {
                        cube.GetComponent<CubeController>().enabled = true; // keeps selected cube on
                    }
                    else
                    {
                        cube.GetComponent<CubeController>().enabled = false; // turns off all other cubes
                    }
                }
            }

            GameObject thisCube; // Temp variable to access the instantiated prefab
            thisCube = Instantiate(cubePrefab, CubeSpawnPosition, Quaternion.identity);
            thisCube.name = $"Cube {nameCounter}";
            nameCounter++;
            thisCube.GetComponent<CubeController>().enabled = true; // makes sure new cube can move
            CubeList.Add(thisCube); // adds new cube to list
            if(CubeList.Count > 1)
            {
                CurrentCubeInList = CubeList.Count -1; // updates the CurrentCube number // actual list starts at index 0 not at index 1
            }
            Debug.Log($"Added {thisCube.name} to the CubeList :: Current count is {CubeList.Count}");
        }
    }
}



