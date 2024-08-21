using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public static ObstacleGenerator instance;

    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private List<GameObject> maps = new List<GameObject>();
    [SerializeField] private List<GameObject> activeMaps = new List<GameObject>();

    private int itemSpace = 15;
    private int itemCountInMap = 5;
    private float laneoffset = 1f;
    private int coinsCountInItem = 10;
    private float coinsHeight = 0.5f;
    private int mapSize;

    enum TrackPos { Left = -4, Center = 0, Right = 4 };
    enum CoinsStyle { Line};

    struct MapItem
    {
        public void SetValues(GameObject obstacle, TrackPos trackPos, CoinsStyle coinsStyle)
        {
            this.obstacle = obstacle; this.trackPos = trackPos; this.coinsStyle = coinsStyle;
        }

        public GameObject obstacle;
        public TrackPos trackPos;
        public CoinsStyle coinsStyle;
    }

    private void Awake()
    {
        instance = this;
        mapSize = itemCountInMap * itemSpace;
        maps.Add(MakeMap1());
        maps.Add(MakeMap1());
        maps.Add(MakeMap1());
        foreach (GameObject map in maps)
        {
            map.SetActive(false);
        }
    }

    private void Update()
    {
        if (RoadGenerator.Instance._speed == 0)
        {
            return;
        }

        foreach (GameObject map in activeMaps)
        {
            map.transform.position -= new Vector3(0, 0, RoadGenerator.Instance._speed * Time.deltaTime);
        }

        if (activeMaps[0].transform.position.z < -mapSize)
        {
            RemoveFirstActiveMap();
            AddActiveMap();
        }
    }

    private void RemoveFirstActiveMap()
    {
        activeMaps[0].SetActive(false);
        maps.Add(activeMaps[0]);
        activeMaps.RemoveAt(0);
    }

    public void ResetMaps()
    {
        while (activeMaps.Count > 0)
        {
            RemoveFirstActiveMap();
        }
        AddActiveMap();
        AddActiveMap();
    }

    private void AddActiveMap()
    {
        int r = Random.Range(0, maps.Count);
        GameObject go = maps[r];
        go.SetActive(true);
        foreach (Transform child in go.transform)
        {
            child.gameObject.SetActive(true);
        }
        go.transform.position = activeMaps.Count > 0 ?
                                activeMaps[activeMaps.Count - 1].transform.position + Vector3.forward * mapSize : new Vector3(0, 0, 10);
        maps.RemoveAt(r);
        activeMaps.Add(go);
    }

    GameObject MakeMap1()
    {
        GameObject result = new GameObject("Map1");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();
        for (int i = 0; i < itemCountInMap; i++)
        {
            item.SetValues(null, TrackPos.Center, CoinsStyle.Line);

            if (i == 2)
            {
                item.SetValues(obstacle, TrackPos.Center, CoinsStyle.Line);
            }
            else if (i == 3)
            {
                item.SetValues(obstacle, TrackPos.Right, CoinsStyle.Line);
            }
            else if (i == 4)
            {
                item.SetValues(obstacle, TrackPos.Left, CoinsStyle.Line);
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos * laneoffset, 0, i * itemSpace);
            CreateCoins(item.coinsStyle, obstaclePos, result);

            if (item.obstacle != null)
            {
                GameObject go = Instantiate(item.obstacle, obstaclePos, Quaternion.identity);
                go.transform.SetParent(result.transform);
            }
        }
        return result;
    }


    private void CreateCoins(CoinsStyle style, Vector3 pos, GameObject paremtObject)
    {
        Vector3 coinPos = Vector3.zero;
        if (style == CoinsStyle.Line)
        {
            for (int i = -coinsCountInItem / 2; i < coinsCountInItem / 2; i++)
            {
                coinPos.y = coinsHeight;
                coinPos.z = i * ((float)itemSpace / coinsCountInItem);
                GameObject go = Instantiate(coinPrefab, coinPos + pos, Quaternion.identity);
                go.transform.SetParent(paremtObject.transform);
            }
        }

    }
}