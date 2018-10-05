using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static float score = 0.0f;

    private float difLvl = 0.0f;
    private float maxDifLvl = 2.0f;
    private int scoreToNxtLvl = 500;

    private int scoreSpeed = 20;

    public Text scoreText;

    public DeathMenu deathMenu;

    private bool isGameOver = false;

	// Use this for initialization
	void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isGameOver == true)
            return;

        if(score >= scoreToNxtLvl)
        {
            LevelUp();
        }

        if (PlayerMotor.speedPU == true)
        {
            scoreSpeed = 50;
        } else
        {
            scoreSpeed = 20;
        }

        if (PlayerMotor.startScore == true)
        {
            score += Time.deltaTime * scoreSpeed;
            scoreText.text = ((int)score).ToString();
        }
        
		
	}

    void LevelUp()
    {

        if (difLvl >= maxDifLvl)
            return;

        scoreToNxtLvl += 500;

        difLvl += 0.1f;

        GetComponent<PlayerMotor>().SetSpeed(difLvl);

        Debug.Log(difLvl);
    }

    public void onDeath()
    {
        deathMenu.ToggleEndMenu(score);
        isGameOver = true;
    }
}
