using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMoveX2 : MonoBehaviour {

    //public static int tile = 5;

    public float speed = 5f;

    private int shouldMove = 0;

    private GameObject mineanim;

    private float spawnPos;

    // Use this for initialization
    void Start()
    {

        mineanim = GameObject.Find("mineStill1");

        spawnPos = mineanim.transform.position.x;

        //TileManager.tile8 = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (mineanim.transform.position.x <= spawnPos + 4 && shouldMove == 0)
        {
            mineanim.transform.Translate(speed * Time.deltaTime, 0, 0);

            if (mineanim.transform.position.x >= spawnPos + 4)
            {
                shouldMove = 1;
            }

        }
        else if (mineanim.transform.position.x >= spawnPos - 4 && shouldMove == 1)
        {
            mineanim.transform.Translate(-speed * Time.deltaTime, 0, 0);

            if (mineanim.transform.position.x <= spawnPos - 4)
            {
                shouldMove = 0;
            }

        }
    }
}