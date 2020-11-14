using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool isColliding;
    public GameObject player;
    public GameObject boardManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding) return;
        isColliding = true;
        if (enabled)
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
            

            GameManager.instance.SendMessage("setActiveRoom", room - 1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }
}
