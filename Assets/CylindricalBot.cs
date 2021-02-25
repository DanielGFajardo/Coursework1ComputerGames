using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CylindricalBot : MonoBehaviour
{
    public Rigidbody body;
    public bool Leader=false;
    public Text info;
    public int level;
    private int destPoint = 0;
    public bool touched = false;
    public float moveSpeed = 0.01f; //move speed
    public float rotationSpeed = 2; //speed of turning
    public DoorBehaviour door;
    private NavMeshAgent agent;
    private GameObject RedCircle;
    private GameObject BlueCircle;
    private GameObject GreenCircle;
    private GameObject YellowCircle;
    private GameObject RedCube;
    private GameObject BlueCube;
    private GameObject GreenCube;
    private GameObject YellowCube;
    private GameObject RedActivated;
    private GameObject BlueActivated;
    private GameObject GreenActivated;
    private GameObject YellowActivated;
    public Texture blue;
    public Texture red;
    public Texture yellow;
    public Texture green;
    public Texture grey;
    private GameObject[] destinations;
    private GameObject[] confirmationButtons;
    private Texture[] textures;
    private GameObject[] buttons;
    private GameObject[] confirmators;
    // Start is called before the first frame update
    void Start()
    {
        GreenCircle = GameObject.Find("GreenCircle");
        BlueCircle = GameObject.Find("BlueCircle");
        RedCircle = GameObject.Find("RedCircle");
        YellowCircle = GameObject.Find("YellowCircle");
        GreenCube = GameObject.Find("GreenCube");
        BlueCube= GameObject.Find("BlueCube");
        RedCube = GameObject.Find("RedCube");
        YellowCube = GameObject.Find("YellowCube");

        destinations = new [] {RedCircle, BlueCircle, YellowCircle, GreenCircle}; 
        textures = new [] {red, blue, yellow, green}; 
        buttons = new [] {RedCube, BlueCube, YellowCube, GreenCube}; 
        confirmationButtons = new [] {RedActivated, BlueActivated, YellowActivated, GreenActivated}; 
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>(); //getting the rigid body element of the object
        if(Leader)
            GetComponent<Renderer> ().material.mainTexture = textures[destPoint];
        else 
            GetComponent<Renderer> ().material.mainTexture = grey;
        for(int i=0;i<buttons.Length;i++){
            GameObject button = buttons[i];
            button.GetComponent<Renderer>().material.mainTexture = grey;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!Leader){
            Vector3 target=getTrajectory();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rotationSpeed * Time.deltaTime);
            Vector3 possiblePosition = transform.position + transform.forward * Time.deltaTime * moveSpeed * 0.3f ;
            int close = othersTooClose(possiblePosition);
            if(close==3) {
                transform.position += transform.forward * Time.deltaTime * moveSpeed * 0.4f ;
            }
            
                
        }
        
        if (Leader &&!agent.pathPending && agent.remainingDistance < 3)
                
                GotoNextPoint();
        
        }
    int othersTooClose(Vector3 position){ //3 is for no one close but leader, 2 for close but not too much and 1 for too close
        Collider[] nearby = Physics.OverlapBox((position+ transform.forward * 1) ,new Vector3(1,1,1.5f),transform.rotation);
            for (var i = 0; i < nearby.Length; i++)
            {
                if(nearby[i].gameObject.tag=="Bot"&&!(nearby[i].gameObject.transform.position==transform.position)){
                    if(!nearby[i].gameObject.GetComponent<CylindricalBot>().Leader){
                        Debug.Log(1);
                        return 2;
                    }
                }
            }
        return 3;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube((transform.position+ transform.forward * 1), new Vector3(1,1,1.5f));
    }
    void GotoNextPoint() {
            // Returns if no points have been set up
            if (destinations.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = destinations[destPoint].transform.position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % destinations.Length;
            GetComponent<Renderer> ().material.mainTexture = textures[destPoint];
            touched = false;
        }

    Vector3 getTrajectory(){
        Collider[] nearby = Physics.OverlapSphere(transform.position, 100);
            for (var i = 0; i < nearby.Length; i++)
            {
                if(nearby[i].gameObject.tag=="Bot"){
                    if(nearby[i].gameObject.GetComponent<CylindricalBot>().Leader){
                        Debug.Log(nearby[i].gameObject.transform);
                        return nearby[i].gameObject.transform.position;
                    }
                }
            }
            return new Vector3(0,0,0);
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag=="Bot"){
            if (collision.gameObject.GetComponent<CylindricalBot>().Leader && !Leader)
            {
                collision.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                collision.gameObject.GetComponent<CylindricalBot>().Leader=false;
                collision.gameObject.GetComponent<Renderer> ().material.mainTexture = grey;
                collision.gameObject.GetComponent<Rigidbody>().velocity= Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().angularVelocity= Vector3.zero;
                int randomRespawn = Random.Range(0, 4);
                transform.position= (destinations[randomRespawn].transform.position); 
                body.velocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
                destPoint=((randomRespawn+1)%destinations.Length);
                GetComponent<Renderer> ().material.mainTexture = textures[destPoint];
                Leader=true;
                GetComponent<NavMeshAgent>().isStopped = false;
                GotoNextPoint();
            }
        }
        if(collision.gameObject.tag=="ActiveElement"){
            if(level==1){
                bool completed = true;
                for(int i=0;i<buttons.Length;i++){
                    GameObject button = buttons[i];
                    if(!(button.GetComponent<Renderer>().material.mainTexture == textures[i]))
                        completed=false;
                }
                if(completed){
                    door.conditionOpen=true;
                    StartCoroutine(DoorOpened());
                }
                for(int i = 0; i<textures.Length;i++){
                    Texture texture = textures[i];
                    if(GetComponent<Renderer>().material.mainTexture == texture){
                        buttons[i].GetComponent<Renderer> ().material.mainTexture = texture;
                    }
                }
            }
        }
         
        IEnumerator DoorOpened(){
        int time=2;
        info.color=Color.black;
        info.text = "The door has been opened!";
        yield return new WaitForSeconds(time+1);
        info.text = "";
    }
    }
}
