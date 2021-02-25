using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public Vector3 openPosition;
    public Vector3 closePosition;
    public bool conditionOpen;
    public bool conditionClose;
    public bool State; //0 close 1 open

    // Start is called before the first frame update
    void Start()
    {
        closePosition = transform.position;
        //openPosition = transform.position;
        openPosition = new Vector3(transform.position.x,transform.position.y+2,transform.position.z+1);
    }

    // Update is called once per frame
    void Update()
    {   
        if(!State&& (conditionOpen)){
            transform.position=openPosition;
            State=true;
            Debug.Log("Open");
        }
        else if(State&&(conditionClose)){
            transform.position=closePosition;
            State=false;
            Debug.Log("Close");
        }
        //else{
        //    transform.position=closePosition;
        //}
        
    }
}
