using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float gravity = -9.81f;
    //[SerializeField] private float maxVelocity;
    private bool flipped;
    private new Rigidbody rigidbody;
    private Animator animator;
    private Vector3 velocity;
    private GameManager gameManager;
    private SphereCollider frontCollider;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        frontCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(0, gravity * Time.deltaTime * GameManager.gameSpeed, 0);
        //float maxYVelocity = Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity);
        //velocity.y = maxYVelocity;

        //causes the player to fall upwards when the gravity gets flipped
        if (flipped)
        {
            rigidbody.velocity -= velocity;
        }
        else
        {
            rigidbody.velocity += velocity;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flipped = !flipped;
            //activates the animation of the car flipping
            animator.SetBool("isFlipped", flipped);
            //lowers the velocity down to enable a quicker flipping
            velocity = new Vector3(0, rigidbody.velocity.y / 3, 0);
            rigidbody.velocity = velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //causes a gameover when the players front collides with something
        //if (collision.collider.GetType() == typeof(SphereCollider))
        if (collision.contacts[0].thisCollider == frontCollider)
        {
            //print(collision.contacts[0].thisCollider);
            print($"Died on: {collision.gameObject.name}");
            gameManager.GameOver();
        }
    }
}
