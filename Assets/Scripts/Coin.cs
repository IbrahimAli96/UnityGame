using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

    public static float coin = 0.0f;

    public Text coinText;

    public static int coinSpeed = 1;

    private bool isGameOver = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isGameOver == true)
            return;

        if (PlayerMotor.startScore == true)
        {
            coin += Time.deltaTime * coinSpeed;
            coinText.text = ((int)coin).ToString();
        }
    }

    public void onDeath()
    {
        isGameOver = true;
    }
}
