using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody RigidBodyComp;

    [SerializeField] float thrust;
    [SerializeField] float rotate;
    // Start is called before the first frame update
    void Start()
    {
        RigidBodyComp = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            // Process input for Thrust
            RigidBodyComp.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D)))
        {
            // Process Turn Left Rotation
            RotateRocket(rotate);
        }
        else if(Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.A)))
        {
            // Process Turn Right Roatation
            RotateRocket(-rotate);
        }
    }

    void RotateRocket(float rotationValue)
    {
        // Freeze rotation so we can manually rotate
        RigidBodyComp.freezeRotation = true;

        // Apply rotation to rocket mesh
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime, Space.World);
        
        // Unfreeze rotation to reapply physics
        RigidBodyComp.freezeRotation = false;
        RigidBodyComp.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
}
