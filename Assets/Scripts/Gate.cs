using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject boardManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.SendMessage("setActiveRoom", BoardManager.roomNumber);
        //boardManager.SendMessage("setActiveRoom", BoardManager.roomNumber); 
        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
