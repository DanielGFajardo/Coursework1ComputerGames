using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PatrolBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private int destPoint = 0;
    public int pathNumber;
    public Text info;
    private Vector3 initialPose = new Vector3(0.218f,0.508f,46);
    public Vector3[] setPoints1;
    public Vector3[] setPoints2;
    public Vector3[] setPoints3;
    public Vector3[] destinations;
    // Start is called before the first frame update
    void Start()
    {
        setPoints1 = new [] {new Vector3(4.9f,1,46),new Vector3(4.9f,1,56),new Vector3(7.8f,1,56),new Vector3(9.57f,1,49),new Vector3(12,1,63),new Vector3(3,1,64)}; 
        setPoints2 = new [] {new Vector3(3.41f,1,64),new Vector3(1.96f,1,56),new Vector3(-2.52f,1,45),new Vector3(-8.23f,1,55),new Vector3(-6.81f,1,64),new Vector3(-5.12f,1,59)}; 
        setPoints3 = new [] {new Vector3(-5.77f,1,53),new Vector3(-10.18f,1,46),new Vector3(-5.77f,1,53),new Vector3(-13f,1,56),new Vector3(-10f,1,64),new Vector3(-7.77f,1,60)}; 
        if(pathNumber==1)
            destinations=setPoints1;
        if(pathNumber==2)
            destinations=setPoints2;
        if(pathNumber==3)
            destinations=setPoints3;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        patrolForSphere();
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
    }
    void GotoNextPoint() {
            // Returns if no points have been set up
            if (destinations.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = destinations[destPoint];

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % destinations.Length;
        }

    void patrolForSphere(){
        Collider[] nearby = Physics.OverlapSphere(transform.position, 2);
            for (var i = 0; i < nearby.Length; i++)
            {
                if(nearby[i].gameObject.tag=="ActiveElement"){
                    StartCoroutine(Spotted());
                    nearby[i].gameObject.transform.position = initialPose;
                }
                
            }
    }
    IEnumerator Spotted(){
        int time=2;
        info.color=Color.white;
        info.text = "You have been detected!";
        yield return new WaitForSeconds(time+1);
        info.text = "";
    }
}
