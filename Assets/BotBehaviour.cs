using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BotBehaviour : MonoBehaviour
{
    private Rigidbody body;
    public PlayerLevel2Behaviour player;
    public Transform goal;
    private int destPoint = 0;
    private Vector3[] destinations = new [] {new Vector3(353,17,446), new Vector3(657,12,517), new Vector3(468,12,305)};
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 20)
                GotoNextPoint();
        //body.AddForce(new Vector3((player.transform.position.x-transform.position.x)*1,(player.transform.position.y-transform.position.y)*1,(player.transform.position.z-transform.position.z)*1));
        //body.velocity = Vector3.ClampMagnitude(body.velocity, 5);
        
         
    }
}
