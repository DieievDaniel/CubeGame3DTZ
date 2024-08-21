using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator Instance;

    [SerializeField] private ObjectPool roadPool; 
    [SerializeField] private float maxSpeed;
    [SerializeField] private int maxRoadCount;

    private List<GameObject> roads = new List<GameObject>();
    private float speed = 0;

    public float _speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetLevel();
    }

    void Update()
    {
        if (speed == 0)
        {
            return;
        }

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (roads.Count > 0 && roads[0].transform.position.z < -45)
        {
            roadPool.ReturnToPool(roads[0]);
            roads.RemoveAt(0);

            CreateNextRoad();
        }
    }

    public void ResetLevel()
    {
        speed = 10;

        while (roads.Count > 0)
        {
            roadPool.ReturnToPool(roads[0]);
            roads.RemoveAt(0);
        }

        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
         ObstacleGenerator.instance.ResetMaps();
    }

    public void StopLevel()
    {
        speed = 0;
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;

        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 15);
        }

        GameObject road = roadPool.GetFromPool();
        road.transform.position = pos;
        roads.Add(road);
    }
}
