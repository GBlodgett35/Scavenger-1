using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static int fromRoom = 0;
    public static List<int> prevGate = new List<int>();
    public bool isColliding;
    public GameObject player;
    public GameObject boardManager;
    private SpriteRenderer sprite;

    public Color blue;
    public Color green;
    public Color red;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(enabled)
        {
            int room = int.Parse(this.gameObject.tag) - 1;
            prevGate.Add(fromRoom+1);
            
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
        sprite = GetComponent<SpriteRenderer>();

        blue = new Color(0, 0, 1);
        green = new Color(0, 1, 0);
        red = new Color(1, 0, 0);

        if (prevGate != null)
        {
            for (int i = 0; i < prevGate.Count; i++)
            {
                if (int.Parse(this.gameObject.tag) == prevGate[i])
                {
                    if (int.Parse(this.gameObject.tag) == 6)
                        GetComponent<SpriteRenderer>().color = green;
                    else if (int.Parse(this.gameObject.tag) == 4)
                        GetComponent<SpriteRenderer>().color = red;
                    else
                        GetComponent<SpriteRenderer>().color = blue;
                }
            }
        }
    }
   

    public Vector3 getStartCoords(int fromRoom, int toRoom)
    {
        Vector3 coords;
        switch(fromRoom)
        {
            case 0:
                coords = new Vector3(4, 1, 0);
                sprite.color = blue;
                break;
            case 1:
                if(toRoom == 0)
                {
                    sprite.color = blue;
                    coords = new Vector3(4, 6, 0);
                }
                else if(toRoom == 2)
                {
                    sprite.color = blue;
                    coords = new Vector3(2, 1, 0);
                }
                else
                {
                    sprite.color = blue;
                    coords = new Vector3(1, 4, 0);
                }
                break;
            case 2:
                if(toRoom == 1)
                {
                    sprite.color = blue;
                    coords = new Vector3(2, 6, 0);
                } else if(toRoom == 5)
                {
                    sprite.color = green;
                    coords = new Vector3(1, 4, 0);
                } 
                else
                {
                    sprite.color = red;
                    coords = new Vector3(6, 1, 0);
                }
                break;
            case 3:
                sprite.color = blue;
                coords = new Vector3(6, 6, 0);
                break;
            case 4:
                sprite.color = blue;
                coords = new Vector3(6, 3, 0);
                break;
            case 5:
                sprite.color = blue;
                coords = new Vector3(6, 4, 0);
                break;
            default:
                sprite.color = new Color(203, 0, 0);
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
