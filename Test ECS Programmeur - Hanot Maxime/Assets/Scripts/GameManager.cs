using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public int roadNumbers;
    public Vector2 roadLengths;
    GameObject ground;
    [Serializable]
    public struct road
    {
        public List<Vector3> roadPoints;
    }

    public List<road> roads;

    private void Awake()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
    }
    private void Start()
    {
        for(int i = 0; i < roads.Count; i++)
        {
            GenerateRoad(i);
        }
    }

    void GenerateFirstPoint(int i)
    {
        //set first point coordinates in ground dimensions and add to list
        float xPosition = UnityEngine.Random.Range(0.2f, ground.transform.localScale.x);
        float zPosition = UnityEngine.Random.Range(0.2f, ground.transform.localScale.z);
        roads[i].roadPoints.Add( new Vector3(xPosition, 0, zPosition) ) ;
    }

    void GenerateRoad(int i)
    {
        GenerateFirstPoint(i);

        //set road length by random between two int
        int rdmLength = UnityEngine.Random.Range((int)roadLengths.x, (int)roadLengths.y);
        //Debug.Log(rdmLength);

        //set second point to determine a direction for the road
        float xPosition = roads[i].roadPoints[0].x + UnityEngine.Random.Range(-1f, 1f);
        float zPosition = roads[i].roadPoints[0].z + UnityEngine.Random.Range(-1f, 1f);

        //add the second point in the list
        roads[i].roadPoints.Add(new Vector3(xPosition, 0, zPosition));
        

        //calulate direction for others points
        Vector3 dir;
        if (roads[i].roadPoints.Count >= 2)
            dir = roads[i].roadPoints[1] - roads[i].roadPoints[0];
        else
            dir = Vector3.zero;

        //add new point while the list count is less than the random length
        while (roads[i].roadPoints.Count < rdmLength - 1)
        {
            float xPositionNext = roads[i].roadPoints[roads[i].roadPoints.Count - 1].x + UnityEngine.Random.Range(-.5f, .5f);
            float zPositionNext = roads[i].roadPoints[roads[i].roadPoints.Count - 1].z + UnityEngine.Random.Range(-.5f, .5f);
            roads[i].roadPoints.Add(new Vector3(xPositionNext, 0, zPositionNext) + dir);
        }

        //Draw a ray to show the road on the scene
        for(int k = 2; k < roads[i].roadPoints.Count; k++)
        {
            Debug.DrawRay(roads[i].roadPoints[k-1], roads[i].roadPoints[k-2] - roads[i].roadPoints[k - 1], Color.blue,Mathf.Infinity);
        }
    }
}
