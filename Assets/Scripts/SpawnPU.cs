using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPU : MonoBehaviour {

    public GameObject[] puPrefabs;

    // Use this for initialization
    void Start()
    {

        GameObject go;

        go = Instantiate(puPrefabs[Random.Range(0, puPrefabs.Length)]) as GameObject;

        Vector3 position = new Vector3(0, 0, 0);
        //Vector3 scale = new Vector3(6, 60, 60);

        go.transform.SetParent(transform);
        go.transform.localPosition = position;
        //go.transform.localScale = scale;

    }

    // Update is called once per frame
    void Update()
    {


    }
}
