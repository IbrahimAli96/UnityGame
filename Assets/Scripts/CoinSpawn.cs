using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{

    public GameObject[] coinPrefabs;

    // Use this for initialization
    void Start()
    {

        GameObject go;

        go = Instantiate(coinPrefabs[Random.Range(0, coinPrefabs.Length)]) as GameObject;

        Vector3 position = new Vector3(0, 0, 0);
        Vector3 scale = new Vector3(6, 60, 60);

        go.transform.SetParent(transform);
        go.transform.localPosition = position;
        go.transform.localScale = scale;

    }

    // Update is called once per frame
    void Update()
    {


    }

}
