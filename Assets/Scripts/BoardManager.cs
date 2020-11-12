using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public class Room
    {
        public GameObject thisObj;
        public GameObject player;
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;
        public GameObject[] enemyTiles;
        public GameObject[] outerWallTiles;

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
                        GameObject[] _outerWallTiles
                   )
        {
            floorTiles = _floorTiles;
            wallTiles = _wallTiles;
            foodTiles = _foodTiles;
            enemyTiles = _enemyTiles;
            outerWallTiles = _outerWallTiles;
            gridPositions.Clear();
            player = GameObject.FindGameObjectWithTag("Player");

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
                    Debug.Log("X: " + x + ", Y: " + y);
                    GameObject toInstantiate = getTileType(roomArr[x][y]);
                   
                    if(toInstantiate == null)
                    {
                        Debug.Log(roomArr[x][y]);
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
                case 'P':
                    go = player;
                    break;
                case 'Z':
                    go = enemyTiles[Random.Range(0, enemyTiles.Length)];
                    break;
                case ' ':
                    go = floorTiles[Random.Range(0, floorTiles.Length)];
                    break;
                case '1':
                    go = floorTiles[0];
                    Debug.Log("Trigger 1");
                    break;
                case '2':
                    go = floorTiles[1];
                    Debug.Log("Trigger 2");
                    break;
                case '3':
                    go = floorTiles[1];
                    Debug.Log("Trigger 3");
                    break;
                case '4':
                    go = floorTiles[1];
                    Debug.Log("Trigger 4");
                    break;
                case '5':
                    go = floorTiles[1];
                    Debug.Log("Trigger 5");
                    break;
                case '6':
                    go = floorTiles[1];
                    Debug.Log("Trigger 6");
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
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
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
            if(l % 8 == 7)
            {
                rooms[roomCount] = new Room(roomCount.ToString(), c, floorTiles, wallTiles, foodTiles, enemyTiles, outerWallTiles);
                c = new char[8][];
                cCount = 0;
            }
        }
        Debug.Log("l: " + l);
        //int[] offset = { 0, 0 };
        //for(int i = 0; i < rooms.Length; i++)
        //{
        //    if(offset[0] >= lines[0].Length)
        //    {
        //        int y = offset[1];
        //        offset[1] = y + 1;
        //        offset[0] = 0;
        //    }
        //    char[][] roomArr = new char[8][];
        //    for(int j = 0; j < 8; j++)
        //    {
        //        if(lines[i][offset[0]] == ' ')
        //        {
        //            i--;
        //            break;
        //        }
        //        Debug.Log("Offset: " + (offset[0] + 8) + ", lines[0]: " + lines[0].Length);
        //        roomArr[j] = lines[i].Substring(offset[0], offset[0] + 8).ToCharArray();
        //        Debug.Log("roomArr length: " + roomArr[j].Length);
        //    }
        //    rooms[i] = new Room(i.ToString(), roomArr, floorTiles, wallTiles, foodTiles, enemyTiles, outerWallTiles);
        //    offset[0] += 8;
        //}

        setActiveRoom(0);

       // for (int x = 1; x < columns - 1; x++)
        //{
          //  for (int y = 1; y < rows - 1; y++)
            //{
              //  gridPositions.Add(new Vector3(x, y, 0f));
            //}
        //}
    }

    public void setActiveRoom(int roomIndex)
    {
        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i].thisObj.SetActive(false);
        }
        rooms[roomIndex].thisObj.SetActive(true);
    }
    // Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
    {
        // Clear our list gridPositions.
        //gridPositions.Clear();

        // Loop through x axis (columns).
        //for (int x = 1; x < columns - 1; x++)
        //{
            // Within each column, loop through y axis (rows).
            //for (int y = 1; y < rows - 1; y++)
            //{
                // At each index add a new Vector3 to our list with the x and y coordinates of that position.
               // gridPositions.Add(new Vector3(x, y, 0f));
            //}
        //}
    }

    void BoardSetup()
    {
        //boardHolder = new GameObject("Board").transform;

        //for (int x = -1; x < columns + 1; x++)
        //{
        //    for (int y = -1; y < rows + 1; y++)
        //    {
        //        GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
        //        if (x == -1 || x == columns || y == -1 || y == rows)
        //        {
        //            toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
        //        }

        //        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        //        instance.transform.SetParent(boardHolder);

        //    }
        //}
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tiles, int minmum, int maximum)
    {
        int objectCount = Random.Range(minmum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPos = RandomPosition();
            GameObject tileChoice = tiles[Random.Range(0, tiles.Length)];
            Instantiate(tileChoice, randomPos, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        //BoardSetup();
        //InitialiseList();
        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        //LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        //int enemyCount = (int)Mathf.Log(level, 2f);
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
