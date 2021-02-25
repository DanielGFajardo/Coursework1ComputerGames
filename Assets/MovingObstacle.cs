using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 positionInit;
    public Vector3 endPosition;
    private float velocityObs = 0.01f;
    public bool returning=true;
    // Start is called before the first frame update
    void Start()
    {
        positionInit = transform.position;
        endPosition = transform.position + new Vector3(6,0,0);
        transform.position = new Vector3(transform.position.x+Random.Range(0,5),transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        if(!returning){
           transform.position = new Vector3(transform.position.x+velocityObs,transform.position.y,transform.position.z);
        }
        if(returning){
            transform.position = new Vector3(transform.position.x-velocityObs,transform.position.y,transform.position.z);
        }
        if(positionInit.x>=currentPosition.x){
            transform.position = new Vector3(transform.position.x+velocityObs,transform.position.y,transform.position.z);
            returning=false;
        }
        if(endPosition.x<=currentPosition.x){
            transform.position = new Vector3(transform.position.x-velocityObs,transform.position.y,transform.position.z);
            returning=true;
        }
    }
}
