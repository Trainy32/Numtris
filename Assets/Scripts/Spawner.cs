using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] GameBlocks;


    // Start is called before the first frame update
    void Start()
    {
        NewGameBlocks();
    }

    // Update is called once per framed
    void Update()
    {
    
   }

    public void NewGameBlocks()
    {
        Instantiate(GameBlocks[Random.Range(0, GameBlocks.Length)], transform.position, Quaternion.identity);        
    }
}
