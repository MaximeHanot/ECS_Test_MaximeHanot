using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEditor;
using UnityEngine;
//using Unity.Transforms;

public struct Driving : IComponentData
{
    public float speed;

    public List<Vector3> path;

    //public bool frontDetection = false;


}

public class CarEntity : MonoBehaviour
{
    private void Start()
    {
        EntityManager m = World.Active.EntityManager;
        Entity e = m.CreateEntity();
        m.AddComponent<Driving>(e);

        Driving drivingData = m.GetComponentData<Driving>(e);

        drivingData.speed = Random.Range(0, 10);

        m.SetComponentData<Driving>(e, drivingData);
        
    }
}

public class DrivingSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        EntityManager m = World.Active.EntityManager;
        Entities.ForEach((Entity entity, ref Driving _driving, Transform _transform) => 
        {
            _driving.speed = 4;
        });
    }
}