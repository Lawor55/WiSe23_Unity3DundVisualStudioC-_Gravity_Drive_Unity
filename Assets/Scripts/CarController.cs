using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float gravity = -9.81f;
    //[SerializeField] private float maxVelocity;
    private float carRotation;
    private bool fliped;
    private new Rigidbody rigidbody;
    private Transform carTransform;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        carTransform = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(0, gravity * Time.deltaTime, 0);
        //float maxYVelocity = Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity);
        //velocity.y = maxYVelocity;

        if (fliped)
        {
            carRotation -= rotationSpeed * Time.deltaTime;
            rigidbody.velocity -= velocity;
        }
        else
        {
            carRotation += rotationSpeed * Time.deltaTime;
            rigidbody.velocity += velocity;
        }
        carRotation = Mathf.Clamp(carRotation, -165, -15);
        carTransform.rotation = Quaternion.Euler(new Vector3(0, 90, carRotation));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fliped = !fliped;
            velocity = new Vector3(0, rigidbody.velocity.y / 2, 0);
            rigidbody.velocity = velocity;
        }
    }
}
