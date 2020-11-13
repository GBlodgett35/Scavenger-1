using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    
    public GameObject player;
    public GameObject boardManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
            int room = int.Parse(this.gameObject.tag);
            Vector3[] playerPos = {
                new Vector3(4, 6, 0),
                new Vector3(4, 6, 0),
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0),
            };
            player.transform.position = playerPos[room - 1];
            player.SendMessage("moveTo", playerPos[room - 1]);
            Debug.Log(other.gameObject.transform.position);
            //if(this.transform.position.y == -1)
            //{
            //    Debug.Log("1");
            //    other.transform.position += new Vector3(0, -1, 0); 
            //} else if(this.transform.position.x == -1)
            //{
            //    Debug.Log("2");
            //    other.transform.position += new Vector3(0, -1, 0);
            //} else if(this.transform.position.y > 8)
            //{
            //    Debug.Log("3");
            //    other.transform.position += new Vector3(0, 1, 0);
            //} else
            //{
            //    Debug.Log("4");
            //    other.transform.position += new Vector3(0, -1, 0);
            //}

            GameManager.instance.SendMessage("setActiveRoom", room - 1);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
