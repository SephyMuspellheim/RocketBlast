using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /********************************************************************************************/
    /** Components                                                                              */
    /********************************************************************************************/

    // Define rigid body component so parameters variables and methods can be accessed/changed
    Rigidbody RigidBodyComp;
    // Define the audio source component so the audio source so it can be interacted with via code
    AudioSource AudioSourceComp;

    /********************************************************************************************/
    /** Parameters                                                                              */
    /********************************************************************************************/

    // Define and expose thurst and rotation variables to the user interface for tuning in real-time
    [SerializeField] float thrust;
    [SerializeField] float rotate;
    [SerializeField] AudioClip mainThrust;

    [SerializeField] ParticleSystem mainThrustersFX;
    [SerializeField] ParticleSystem leftThrusterFX;
    [SerializeField] ParticleSystem rightThrusterFX;

    // Start is called before the first frame update
    void Start()
    {
        //Set the rigid body comp with a reference to the RigidBody Component
        RigidBodyComp = GetComponent<Rigidbody>();

        // Set the audio source reference
        AudioSourceComp = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // On update: Process thrust and roation inputs from player
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D)))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.A)))
        {
            RotateRight();
        }
        else
        {
            StopRotationFX();
        }
    }

    void StartThrusting()
    {
        // Process input for Thrust
        RigidBodyComp.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!mainThrustersFX.isPlaying)
        {
            mainThrustersFX.Play();
        }
        if (!AudioSourceComp.isPlaying)
        {
            AudioSourceComp.PlayOneShot(mainThrust);
        }
    }

    void StopThrusting()
    {
        AudioSourceComp.Stop();
        mainThrustersFX.Stop();
    }

    void RotateLeft()
    {
        // Process Turn Left Rotation
        RotateRocket(rotate);
        if (!leftThrusterFX.isPlaying)
        {
            leftThrusterFX.Play();
        }
    }

    void RotateRight()
    {
        // Process Turn Right Roatation
        RotateRocket(-rotate);
        if (!rightThrusterFX.isPlaying)
        {
            rightThrusterFX.Play();
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

    void StopRotationFX()
    {
        leftThrusterFX.Stop();
        rightThrusterFX.Stop();
    }
}
