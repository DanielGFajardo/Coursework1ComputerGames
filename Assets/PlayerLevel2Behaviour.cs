using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerLevel2Behaviour: MonoBehaviour
{
    private Rigidbody body;
    private Transform cameraT;
    public Camera camera;
    [SerializeField] private float m_MovePower = 30; // The force added to the ball to move it.
    [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
    [SerializeField] private float m_MaxAngularVelocity = 15; // The maximum velocity the ball can rotate at.
    [SerializeField] private float m_MaxLinearVelocity = 40;
    [SerializeField] private float m_JumpPower = 2;
    
    
    // Start is called before the first frame update
    void Start()
    {
       body = GetComponent<Rigidbody>();
       cameraT =  camera.transform;
       
    }

    // Update is called once per frame
    void Update()
    {
        //getting the axis and the button for jumping in the specific platform
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool jump = CrossPlatformInputManager.GetButton("Jump");

        //converting the normal of the camera to only the x-z plane. We want to 
        //move in this plane not up and down apart from jumping
        Vector3 cameraForward = Vector3.Scale(cameraT.forward, new Vector3(1, 0, 1)).normalized; 
        //moving ???
        Vector3 moveDirection = (v*cameraForward + h*cameraT.right).normalized;
        body.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x)*m_MovePower);
        body.velocity = Vector3.ClampMagnitude(body.velocity, m_MaxLinearVelocity);
        
    }
}
