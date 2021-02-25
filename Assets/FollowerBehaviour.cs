using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowerBehaviour: MonoBehaviour
{
    //public GameObject followTransform;
    // Start is called before the first frame update
    public GameObject toFollow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=toFollow.transform.position;
         if (Input.GetKey ("e")) {
             transform.Rotate(0,-1.5f,0);
         }
         if (Input.GetKey ("r")) {
             transform.Rotate(0,1.5f,0);
         }
    }
}
