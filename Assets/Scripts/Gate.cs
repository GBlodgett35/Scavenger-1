using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static int fromRoom = 0;
    public bool isColliding;
    public GameObject player;
    public GameObject boardManager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(enabled)
        {
            int room = int.Parse(this.gameObject.tag) - 1;
            Debug.Log(room+ 1);
            
            StopCoroutine(MovingObject.co);
            player.transform.position = getStartCoords(fromRoom, room);
            player.SendMessage("moveTo", getStartCoords(fromRoom, room));
            GameManager.instance.SendMessage("setActiveRoom", room);
            fromRoom = room;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public Vector3 getStartCoords(int fromRoom, int toRoom)
    {
        Vector3 coords;
        switch(fromRoom)
        {
            case 0:
                coords = new Vector3(4, 1, 0);
                break;
            case 1:
                if(toRoom == 0)
                {
                    coords = new Vector3(4, 6, 0);
                }
                else if(toRoom == 2)
                {
                    coords = new Vector3(2, 1, 0);
                }
                else
                {
                    coords = new Vector3(1, 4, 0);
                }
                break;
            case 2:
                if(toRoom == 1)
                {
                    coords = new Vector3(2, 6, 0);
                } else if(toRoom == 5)
                {
                    coords = new Vector3(1, 4, 0);
                } 
                else
                {
                    coords = new Vector3(6, 1, 0);
                }
                break;
            case 3:
                coords = new Vector3(6, 6, 0);
                break;
            case 4:
                coords = new Vector3(6, 3, 0);
                break;
            case 5:
                coords = new Vector3(6, 4, 0);
                break;
            default:
                coords = new Vector3(0, 0, 0);
                break;
        }
        return coords;
    }
    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }
}
