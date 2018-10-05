
using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    private Transform lookAt;

    private Vector3 startOffset;

    private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationOffset = new Vector3(0,2,12);

	public float moveSpeed;
	public GameObject mainCamera;

	// Use this for initialization
	void Start () {

        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
	
	}
	
	// Update is called once per frame
	void Update () {
        moveVector = lookAt.position + startOffset;

        // X
        moveVector.x = -4;

        // Y
        moveVector.y = 4;


        if(transition > 1.0f)
        {
            transform.position = moveVector;
        } else
        {
            //Animation at start
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }


        
		
	}

}























