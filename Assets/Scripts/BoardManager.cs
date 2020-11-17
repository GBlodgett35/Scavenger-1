using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public static int roomNumber = 0;
    public class Room
    {
        public GameObject thisObj;
        public GameObject player;
        public GameObject gate;
        public GameObject exit;
        public GameObject lockedWall;
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;
        public GameObject[] enemyTiles;
        public GameObject[] outerWallTiles;
        public GameObject key;
        private Transform boardHolder;
        private List<Vector3> gridPositions = new List<Vector3>();

        public int columns = 8;
        public int rows = 8;

        public Room(
                        string roomName,
                        char[][] roomArr,
                        GameObject[] _floorTiles,
                        GameObject[] _wallTiles,
                        GameObject[] _foodTiles,
                        GameObject[] _enemyTiles,
                        GameObject[] _outerWallTiles,
                        GameObject _gate,
                        GameObject _exit,
                        GameObject _lockedWall,
                        GameObject _key
                   )
        {
            floorTiles = _floorTiles;
            wallTiles = _wallTiles;
            foodTiles = _foodTiles;
            enemyTiles = _enemyTiles;
            outerWallTiles = _outerWallTiles;
            gridPositions.Clear();
            player = GameObject.FindGameObjectWithTag("Player");
            gate = _gate;
            exit = _exit;
            lockedWall = _lockedWall;
            key = _key;
            for (int x = 1; x < columns - 1; x++)
            {
                for (int y = 1; y < rows - 1; y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
            thisObj = new GameObject("Room " + roomName);
            boardHolder = thisObj.transform;
            
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    GameObject toInstantiate = getTileType(roomArr[x][y]);
                   
                    if(roomArr[x][y] == 'Z' || 
                       roomArr[x][y] == 'F' || 
                       roomArr[x][y] == 'x' || 
                       roomArr[x][y] == 'K' ||
                       roomArr[x][y] == 'W')
                    {
                        GameObject t = Instantiate(floorTiles[0], new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                        t.transform.SetParent(boardHolder);
                    }
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);

                }
            }

        }

        public GameObject getTileType(char c)
        {
            GameObject go = null;
            switch (c)
            {
                case 'X':
                    go = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    break;
                case 'E':
                    go = exit;
                    break;
                case 'Z':
                    go = enemyTiles[Random.Range(0, enemyTiles.Length)];
                    break;
                case 'W':
                    go = wallTiles[Random.Range(0, wallTiles.Length)];
                    break;
                case ' ':
                    go = floorTiles[Random.Range(0, floorTiles.Length)];
                    break;
                case 'F':
                    go = foodTiles[Random.Range(0, foodTiles.Length)];
                    break;
                case 'x':
                    go = lockedWall;
                    break;
                case 'K':
                    go = key;
                    break;
                case '1':
                    go = gate;
                    go.gameObject.tag = "1";
                    break;
                case '2':
                    go = gate;
                    go.gameObject.tag = "2";
                    break;
                case '3':
                    go = gate;
                    go.gameObject.tag = "3";
                    break;
                case '4':
                    go = gate;
                    go.gameObject.tag = "4";
                    break;
                case '5':
                    go = gate;
                    go.gameObject.tag = "5";
                    break;
                case '6':
                    go = gate;
                    go.gameObject.tag = "6";
                    break;
                default:
                    go = floorTiles[0];
                    break;
            }
            return go;
        }
    }



    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    public Room[] rooms = new Room[6];
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject gate;
    public GameObject lockedWall;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public GameObject key;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        string[] lines = new string[16];
        string fileName = "Assets/Resources/Map.txt";
        string line = null;
        var reader = new System.IO.StreamReader(fileName);
        int l = 0;
        int roomCount = 0;
        char[][] c = new char[8][];
        int cCount = 0;
        while((line = reader.ReadLine()) != null) {
            //lines[l] = line;
            c[cCount] = line.ToCharArray();
            cCount++;
            l++;
            if(l % 8 == 0)
            {
                rooms[roomCount] = new Room(roomCount.ToString(), c, floorTiles, wallTiles, foodTiles, enemyTiles, outerWallTiles, gate, exit, lockedWall, key);
                c = new char[8][];
                cCount = 0;
                roomCount++;
            }
        }
        

        setActiveRoom(0);       
    }
    public void SetActiveRecursivelyExt(GameObject obj, bool state)
    {
        obj.SetActive(state);
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursivelyExt(child.gameObject, state);
        }
    }
    public void setActiveRoom(int roomIndex)
    {

        roomNumber = roomIndex;
        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i].thisObj.SetActive(false);
        }
        SetActiveRecursivelyExt(rooms[roomIndex].thisObj, true);
        
    }

   
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
