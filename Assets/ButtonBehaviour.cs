using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public int buttonKind; //0 for opening button 1 for openning one
    public DoorBehaviour door;
    public bool activeButton=false;
    public int level;
    public cronometer timer;
    // Start is called before the first frame update
    void Start()
    {
        if(buttonKind==3){
                 gameObject.GetComponent<Renderer> ().material.color = Color.red;
             }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision){
         if (collision.gameObject.tag == "ActiveElement")
         {
             if(buttonKind==0){
                 door.conditionOpen=true;
                 timer.counting = true;
             }
             if(buttonKind==1){
                 door.conditionClose=true;
             }
             if(buttonKind==2){
                 if (level==2){
                     activeButton=true;
                 }else{
                    door.conditionOpen=true;
                    door.conditionClose=false;
                 }
             }
             if(buttonKind==3){
                 activeButton=true;
                 gameObject.GetComponent<Renderer> ().material.color = Color.green;
             }
         }
    }
    void OnCollisionExit(Collision collision){
         if (collision.gameObject.tag == "ActiveElement")
         {
             if(buttonKind==0){
                 door.conditionOpen=false;
                 
             }
             if(buttonKind==1){
                 door.conditionClose=false;
             }
             if(buttonKind==2){
                 door.conditionClose=true;
                 door.conditionOpen=false;
             }
         }
     }
}
