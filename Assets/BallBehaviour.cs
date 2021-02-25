using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
     float speed = 1.0f;
     float acceleration = 1.0f;
     void Update() {
         var move = new Vector3(Input.GetAxis("Vertical"),0, Input.GetAxis("Horizontal"));
         transform.position += move * speed * acceleration * Time.deltaTime;
         acceleration=acceleration+speed;
     }
}
