using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody body;
    private Transform cameraT;
    private bool touchingFloor = true;
    public int level;
    private bool onEnd = false;
    public Text info;
    private Vector3 padCenter;
    private bool visitedRoom1=false;
    private bool visitedRoom2=false;
    private bool visitedRoom3=false;
    private bool visitedRoom4=false;
    private bool visitedRoom5=false;
    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    [SerializeField] public float m_MovePower = 10; // The force added to the ball to move it.
    [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
    [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
    [SerializeField] private float m_JumpPower = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        body = GetComponent<Rigidbody>(); //getting the rigid body element of the object
        //getting the transform of the main camera
        //StartCoroutine(InitialInfo());
    }

    void Update() {
        cameraT =  Camera.main.transform;
        //getting the axis and the button for jumping in the specific platform
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool jump = CrossPlatformInputManager.GetButton("Jump");

        //converting the normal of the camera to only the x-z plane. We want to 
        //move in this plane not up and down apart from jumping
        Debug.Log(Camera.main.transform.forward);
        Vector3 cameraForward = Vector3.Scale(cameraT.forward, new Vector3(1, 0, 1)).normalized; 
        //moving ???
        Vector3 moveDirection = (v*cameraForward + h*cameraT.right).normalized;
        body.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x)*m_MovePower);
        if(jump&&touchingFloor){
            body.AddForce(new Vector3(0, 400, 0));
            touchingFloor = false;
        }
        if (transform.position.x==padCenter.x && transform.position.z==padCenter.z)
         {
             body.velocity = Vector3.zero;
             body.angularVelocity = Vector3.zero; 
         }
     }
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.name == "Floor1"&&(!visitedRoom1))
         {
             visitedRoom1=true;
             StartCoroutine(room1());
            
         }
         if (collision.gameObject.name == "Floor2"&&!visitedRoom2)
         {
             visitedRoom2=true;
             StartCoroutine(room2());
            
         }
         if (collision.gameObject.name == "Floor3"&&!visitedRoom3)
         {
             visitedRoom3=true;
             StartCoroutine(room3());
            
         }
         if (collision.gameObject.name == "Floor4"&&!visitedRoom4)
         {
             visitedRoom4=true;
             StartCoroutine(room4());
            
         }
         if (collision.gameObject.name == "Floor5"&&!visitedRoom5)
         {
             visitedRoom5=true;
             StartCoroutine(room5());
            
         }
         if (collision.gameObject.tag == "JumpingPlatform" || collision.gameObject.tag == "Obstacle" )
         {
             touchingFloor = true;   
         }
         if (collision.gameObject.tag == "Obstacle")
         {
             touchingFloor = true;
             body.velocity = Vector3.zero;
             body.angularVelocity = Vector3.zero; 
         }
         if (collision.gameObject.tag == "Pad")
         {
             body.isKinematic = true;
             Debug.Log(collision.collider.bounds.center);
             transform.position = new Vector3(collision.collider.bounds.center.x,collision.collider.bounds.center.y-0.3f,collision.collider.bounds.center.z);
             Debug.Log(transform.position);
             StartCoroutine(EndOfLevel());
            
         }
         
         
    }
    IEnumerator InitialInfo(){
        info.color=Color.white;
        m_MovePower=0;
        int time=3;
        if(level==1){
            info.text = "You have five minutes to get out of this level";
            yield return new WaitForSeconds(time);
            info.text = "Move the sphere using the arrow keys";
            yield return new WaitForSeconds(time);
            info.text = "Using the E and R keys you can turn around the camera";
            yield return new WaitForSeconds(time);
            info.text = "It is crucial for you to progress";
            yield return new WaitForSeconds(time);
            info.text = "Take a minute to familiarize yourself with the controls";
            yield return new WaitForSeconds(time);
            info.text = "Your first task is to open the door, the countdown will start once you open it";
            yield return new WaitForSeconds(time+1);
            info.text = "";
        }
        if(level==2){
            info.text = "This is the second level!";
            yield return new WaitForSeconds(time);
            info.text = "You will need to do your best to complete this under 5 minutes ";
            yield return new WaitForSeconds(time);
            info.text = "Open the door and the countdown will start";
            yield return new WaitForSeconds(time);
            info.text = "";
        }
        m_MovePower=5;
    }
    IEnumerator EndOfLevel(){
        m_MovePower=0;
        int time=3;
        info.color=Color.black;
        if(level==1){
            info.text = "First Level Completed!";
            yield return new WaitForSeconds(time+1);
            info.text = "";
            Application.LoadLevel("Level2");
        }
        if(level==2){
            info.text = "You Won The Game!";
            yield return new WaitForSeconds(time+1);
            info.text = "";
        }
        m_MovePower=5;
    }
    IEnumerator room1(){
        info.color=Color.white;
        info.text = "This is going to be tough, you need to be able to handle pressure";
        yield return new WaitForSeconds(3);
        info.text = "";
    }
    IEnumerator room2(){
        info.color=Color.white;
        info.text = "Sometimes going up is the only way to go forward";
        yield return new WaitForSeconds(3);
        info.text = "";
    }
    IEnumerator room3(){
        info.color=Color.black;
        info.text = "Colour yourself up!";
        yield return new WaitForSeconds(3);
        info.text = "";
    }
    IEnumerator room4(){
        info.color=Color.white;
        info.text = "Hide and Seek!";
        yield return new WaitForSeconds(4);
        info.text = "";
    }
    IEnumerator room5(){
        info.color=Color.black;
        info.text = "So Close!";
        yield return new WaitForSeconds(4);
        info.text = "";
    }
     
}
