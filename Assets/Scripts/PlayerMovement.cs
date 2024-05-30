using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] makes it directly editable in Unity
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        // Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        // Input.GetAxis("Horizontal") used for the movement of A and D, it's made by Unity
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flips player when moving right (if) and left (else if)
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector2(1.5f, 1.5f);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-1.5f, 1.5f);
        }


        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        // Set animator parameters

        // Not presed --> horizontal input = 0 -> is 0 not equal to 0? = false -> run = False (player not running)
        // Pressed --> horizontal input different than 0 -> is 1 not equal to 0? = true -> run = True (player running)
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded); // gives info on wether ther player is grounded or not
    }

    private void Jump()

    {
        // Can only jump if grounded == true
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            anim.SetTrigger("jump");
            grounded = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
