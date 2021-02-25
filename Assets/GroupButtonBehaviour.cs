using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupButtonBehaviour : MonoBehaviour
{
    public ButtonBehaviour buttonLeft;
    public ButtonBehaviour buttonRight;
    public DoorBehaviour door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonLeft.activeButton&&buttonRight.activeButton){
            door.conditionOpen=true;
        }
    }
}
