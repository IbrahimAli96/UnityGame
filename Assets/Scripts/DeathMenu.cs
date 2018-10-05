using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {

    public Text scoreText;
    public Image backgroundImg;

    private bool isShown = true;

    private float transition = 0.0f;

    // Use this for initialization
    void Start () {

        gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isShown)
            return;

        transition += Time.deltaTime;
        backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.blue, transition);
	}

    public void ToggleEndMenu(float score)
    {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        isShown = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Time.timeSinceLevelLoad
}
