using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerMotor : MonoBehaviour {

    public Animator anim;

    private CharacterController controller;

    private Vector3 moveVector;

    private float animationDuration = 3.0f;
    private int jump = 1;

    public static bool startScore = false;

    private float jumpForce = 6f;
    private float speed = 4.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;

    private float deathY = -2.0f;

    private bool isGameOver = false;



    //Power ups
    //1
    public float puSec = 5;

    private float oldSpeed = 0.0f;

    public static bool speedPU = false;

    //2

    //public static bool slowPU = false;

    //public float puSec2 = 5;
    //private object theobjectToIgnore;

    //3

    public static bool doublePU = false;

    public float puSec3 = 5;

    //3

    public static bool shieldPU = false;

    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();

        anim = GetComponent<Animator>();

    }

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
           
                anim.Play("grind");
                transform.position = new Vector3(0, controller.transform.position.y, controller.transform.position.z);
                transform.rotation = Quaternion.Euler(0, -90, 0);
                verticalVelocity = jumpForce;

            
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;

        }
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
        anim.Play("slide");
        transform.position = new Vector3(0, controller.transform.position.y, controller.transform.position.z);
        controller = GetComponent<CharacterController>();
        controller.height = 2.0F;
        controller.center = new Vector3(0, -0.2F, 0);

        Wait(1.5f, () => {
            controller = GetComponent<CharacterController>();
            controller.height = 4.0F;
            controller.center = new Vector3(0, 0.8F, 0);
        });
    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
    }

    // Update is called once per frame
    void Update() {

        if (isGameOver == true)
            return;

        if (Time.time < animationDuration)
        {

            controller.Move(Vector3.forward * speed * Time.deltaTime);

            return;
        }
        
        if (jump == 1)
        {
            
          //  anim.Play("grind");
          //  transform.rotation = Quaternion.Euler(0, -90, 0);
          //  transform.position = new Vector3(0, -controller.transform.position.y, controller.transform.position.z);
          //  verticalVelocity = -gravity * Time.deltaTime;
          //  verticalVelocity = jumpForce;
           // jump = 2;
            

            startScore = true;

        }

        moveVector = Vector3.zero;

        //Y - down
        moveVector.y = verticalVelocity;

        //X

        //Z
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }


        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                transform.position = new Vector3(0, controller.transform.position.y, controller.transform.position.z);
                anim.Play("grind");
                transform.rotation = Quaternion.Euler(0, -90, 0);
                verticalVelocity = jumpForce;

                
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;

        }


        if (Input.GetKeyDown("space"))
        {
            anim.Play("slide");
            //transform.position = new Vector3(0, 0.6f, controller.transform.position.z);
            controller = GetComponent<CharacterController>();
            controller.height = 2.0F;
            controller.center = new Vector3(0, -0.2F, 0);

            Wait(1.5f, () => {
                controller = GetComponent<CharacterController>();
                controller.height = 4.0F;
                controller.center = new Vector3(0, 0.8F, 0);
            });
        }

        //SLIDE
        //controller = GetComponent<CharacterController>();
        //controller.height = 2.0F;
        //controller.center = new Vector3(0, -0.2F, 0);

        if (controller.transform.position.y <= deathY)
        {
            Death();
        }

        //Power ups

        if (speedPU == false)
        {
            oldSpeed = speed;
        }

        if (speedPU == true)
        {
            SetColliderOff();

            puSec -= Time.smoothDeltaTime;
            if (puSec >= 0)
            {
                speed = 20.0f;

            }
            else
            {

                speed = oldSpeed;

                puSec = 5;

                speedPU = false;

                string name = "mineAnimation (1)";

                GameObject go = GameObject.Find(name);

                Destroy(go.gameObject);

                SetColliderOn();

            }
        }

        //if (slowPU == true)
        //{
            //SetColliderOffPU();

            //puSec2 -= Time.smoothDeltaTime;
            //if (puSec2 >= 0)
            //{
                //speed = 4.0f;
            //}
            //else
            //{

                //speed = oldSpeed;

                //puSec2 = 5;

                //slowPU = false;

                //string name = "mineAnimation (1)";

                //GameObject go = GameObject.Find(name);

                //Destroy(go.gameObject);

                //SetColliderOnPU();

            //}
        //}

        if (doublePU == true)
        {

            puSec3 -= Time.smoothDeltaTime;
            if (puSec3 >= 0)
            {
                Coin.coinSpeed = 5;
            }
            else
            {

                speed = oldSpeed;

                puSec3 = 5;

                Coin.coinSpeed = 1;

                doublePU = false;
            }
        }

        if (shieldPU == true)
        {
            SetColliderOffShield();
        } else
        {   

            SetColliderOnShield();

        }

        if (doublePU == true)
        {
            SetColliderOffCoin();
        }
        else
        {
            SetColliderOnCoin();
        }

    }

    public void Wait(float seconds, Action action)
    {
        StartCoroutine(_wait(seconds, action));
    }
    IEnumerator _wait(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        //Debug.Log(hit.gameObject.name);

        if (hit.gameObject.name == ("mineAnimation (1)") && speedPU == false && shieldPU == false)
        {
            transform.position = new Vector3(-5, controller.transform.position.y, controller.transform.position.z);

            anim.Play("death");

            Death();

            

        } else if (hit.gameObject.name == ("mineAnimation (1)") && shieldPU == true)
        {
            Destroy(hit.gameObject);

            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("shieldOn");

            foreach (GameObject go in gameObjectArray)
            {
                go.transform.localScale = new Vector3(0, 0, 0);
            }

            Wait(1, () => {
                shieldPU = false;
            });
            
        }

      if (hit.gameObject.tag == ("power1") && speedPU == false)
        {
            Destroy(hit.gameObject);

            speedPU = true;

            //Score.score += 100;
        }

        //if (hit.gameObject.tag == ("power2") && slowPU == false && speedPU == false)
        //{
            //Destroy(hit.gameObject);

            //slowPU = true;

            //Score.score += 100;
        //}

        if (hit.gameObject.tag == ("power3") && doublePU == false)
        {
            Destroy(hit.gameObject);

            doublePU = true;

            //Score.score += 100;
        }

        if (hit.gameObject.tag == ("power4") && shieldPU == false)
        {
            Destroy(hit.gameObject);

            shieldPU = true;

            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("shieldOn");

            foreach (GameObject go in gameObjectArray)
            {
                go.transform.localScale = new Vector3(5, 40, 45);
            }

            //Score.score += 100;
        }

        //if (hit.gameObject.tag == ("redCoin"))
        //{
        //Destroy(hit.gameObject);

        //Score.score += 100;
        //}
        //if (hit.gameObject.tag == ("yellowCoin"))
        //{
        //Destroy(hit.gameObject);

        //Score.score += 100;
        //}
        //if (hit.gameObject.tag == ("orangeCoin"))
        //{
        //Destroy(hit.gameObject);

        //Score.score += 100;
        //}
        //if (hit.gameObject.tag == ("blueCoin"))
        //{
        //Destroy(hit.gameObject);

        //Score.score += 100;
        //}
        //if (hit.gameObject.tag == ("greenCoin"))
        //{
        //Destroy(hit.gameObject);

        //Score.score += 100;
        //}

    }

    private void Death()
    {

        isGameOver = true;

        GetComponent<Score>().onDeath();

        GetComponent<Coin>().onDeath();

    }

    private void SetColliderOn()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("mine"); //your tag name here
        foreach (var item in boxes)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = true;
            }
        }

        GameObject[] boxes1 = GameObject.FindGameObjectsWithTag("power1"); //your tag name here
        foreach (var item in boxes1)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = true;
            }
        }

       // GameObject[] boxes2 = GameObject.FindGameObjectsWithTag("power2"); //your tag name here
       // foreach (var item in boxes2)
       // {
            //if (item != this.gameObject) //for others
           // {
             //   item.GetComponent<SphereCollider>().enabled = true;
            //}
        //}
    }

    private void SetColliderOff()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("mine"); //your tag name here
        foreach (var item in boxes)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = false;
            }
        }

        GameObject[] boxes1 = GameObject.FindGameObjectsWithTag("power1"); //your tag name here
        foreach (var item in boxes1)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = false;
            }
        }

        //GameObject[] boxes2 = GameObject.FindGameObjectsWithTag("power2"); //your tag name here
        //foreach (var item in boxes2)
        //{
          //  if (item != this.gameObject) //for others
          //  {
         //       item.GetComponent<SphereCollider>().enabled = false;
          //  }
        //}
    }

    private void SetColliderOnPU()
    {

        GameObject[] boxes1 = GameObject.FindGameObjectsWithTag("power1"); //your tag name here
        foreach (var item in boxes1)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = true;
            }
        }

       // GameObject[] boxes2 = GameObject.FindGameObjectsWithTag("power2"); //your tag name here
       // foreach (var item in boxes2)
      //  {
           // if (item != this.gameObject) //for others
           // {
            //    item.GetComponent<SphereCollider>().enabled = true;
            //}
        //}

        if (doublePU == true)
        {
            GameObject[] boxes3 = GameObject.FindGameObjectsWithTag("power3"); //your tag name here
            foreach (var item in boxes3)
            {
                if (item != this.gameObject) //for others
                {
                    item.GetComponent<SphereCollider>().enabled = true;
                }
            }
        }
    }

    private void SetColliderOffPU()
    {

        GameObject[] boxes1 = GameObject.FindGameObjectsWithTag("power1"); //your tag name here
        foreach (var item in boxes1)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = false;
            }
        }

        //GameObject[] boxes2 = GameObject.FindGameObjectsWithTag("power2"); //your tag name here
        //foreach (var item in boxes2)
       // {
          //  if (item != this.gameObject) //for others
          //  {
         //       item.GetComponent<SphereCollider>().enabled = false;
         //   }
       // }

        if (doublePU == true)
        {
            GameObject[] boxes3 = GameObject.FindGameObjectsWithTag("power3"); //your tag name here
            foreach (var item in boxes3)
            {
                if (item != this.gameObject) //for others
                {
                    item.GetComponent<SphereCollider>().enabled = false;
                }
            }
        }

    }

    private void SetColliderOnShield()
    {
        GameObject[] boxes4 = GameObject.FindGameObjectsWithTag("power4"); //your tag name here
        foreach (var item in boxes4)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }

    private void SetColliderOffShield()
    {
        GameObject[] boxes4 = GameObject.FindGameObjectsWithTag("power4"); //your tag name here
        foreach (var item in boxes4)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }

    private void SetColliderOnCoin()
    {
        GameObject[] boxes3 = GameObject.FindGameObjectsWithTag("power3"); //your tag name here
        foreach (var item in boxes3)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }

    private void SetColliderOffCoin()
    {
        GameObject[] boxes3 = GameObject.FindGameObjectsWithTag("power3"); //your tag name here
        foreach (var item in boxes3)
        {
            if (item != this.gameObject) //for others
            {
                item.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }

    public void SetSpeed(float changer)
    {
        speed = 4.0f + changer;
        Debug.Log(speed);
    }
}