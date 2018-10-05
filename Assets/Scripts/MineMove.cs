using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMove : MonoBehaviour {

    public float speed = 5f;

    private int shouldMove = 0;

    public GameObject mineanim;

    private float spawnPos;

    // Use this for initialization
    void Start () {

        mineanim = GameObject.Find("mineanim");

        spawnPos = mineanim.transform.position.z;

       

    }

    // Update is called once per frame
    void Update() {

        if (mineanim.transform.position.z <= spawnPos + 18 && shouldMove == 0)
        {
            mineanim.transform.Translate(0, 0, speed * Time.deltaTime);

            if (mineanim.transform.position.z >= spawnPos + 18)
            {
                shouldMove = 1;
            }

        } else if (mineanim.transform.position.z >= spawnPos && shouldMove == 1)
        {
            mineanim.transform.Translate(0, 0, -speed * Time.deltaTime);

            if (mineanim.transform.position.z <= spawnPos)
            {
                shouldMove = 0;
            }

        }
	}
}
