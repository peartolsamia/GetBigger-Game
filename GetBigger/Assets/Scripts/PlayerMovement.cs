using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;

    //private Animator anim;
    [SerializeField] private Animator anim;


    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask groundLayer; // to set ground as a layer and perform boxcast or raycast to it

    [SerializeField] private LayerMask wallLayer;

    private float wallJumpCooldown;

    private float horizontalInput;


    [SerializeField] private float baseSpeed; // speed in smallest form


    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;


    [SerializeField] private float jumpPower;




    [SerializeField] float scaleSpeed; // how fast will character scale in game

    [SerializeField] private Transform visualTransform; // where will visuals come from?

    private Vector2 originalColliderSize;

    [SerializeField] private Transform firePoint; // projectile firepoint object





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // grab references from game object
        body = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();


        originalColliderSize = boxCollider.size;
    }



    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // horizontal input variable


        // flip player when changing direction
        if (horizontalInput > 0.01f) // moving to right
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f) // moving to left
        {
            transform.localScale = new Vector3(-1,1,1); // we are manupilating direction with setting x-axis scale vector to -1
        }


        anim.SetBool("is_moving", horizontalInput != 0); // checks if horizontal input is 0 (in that case player is not moving) and sets is_moving parameter (for animation)
        anim.SetBool("is_grounded", isGrounded());
        anim.SetBool("is_onthewall", onWall());




        if (wallJumpCooldown > 0.2f)
        {
            // setting speed dynamically depending on current scale
            float currentScale = visualTransform.localScale.x;
            float t = Mathf.InverseLerp(minScale, maxScale, currentScale);
            float dynamicSpeed = Mathf.Lerp(baseSpeed * 1.5f, baseSpeed * 0.5f, t);

            body.linearVelocity = new Vector2(horizontalInput * dynamicSpeed, body.linearVelocity.y); // horizontal movement

            if (onWall() && !isGrounded()) // player is on the wall -> player gets stuck and does not fall down
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 2;
            }

            if (Input.GetKey(KeyCode.W)) // returns 1 if A button is pressed and player is on the ground
            {
                Jump();
            }

        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }






        // dynamic in game player scaling
        
        Vector3 targetScale = visualTransform.localScale;

        if (Input.GetMouseButton(0))
        {
            targetScale = targetScale * 1.01f;
        }
        else if (Input.GetMouseButton(1))
        {
            targetScale = targetScale * 0.99f;
        }

        targetScale.x = Mathf.Clamp(targetScale.x, minScale, maxScale);
        targetScale.y = Mathf.Clamp(targetScale.y, minScale, maxScale);
        targetScale.z = 1;

        visualTransform.localScale = Vector3.Lerp(visualTransform.localScale, targetScale, Time.deltaTime * scaleSpeed);

        boxCollider.size = originalColliderSize * visualTransform.localScale; // also set player's collider on scaling



        // set firepoint location depending on dynamic scaling
        Vector3 baseOffset = new Vector3(1.5f, 0.0f, 0);
        Vector3 scaledOffset = new Vector3(baseOffset.x * visualTransform.localScale.x, baseOffset.y * visualTransform.localScale.y, 0);

        firePoint.localPosition = new Vector3(scaledOffset.x, scaledOffset.y, scaledOffset.z);





    }









    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower); // if entered this statement, moves vertically with jump power variable
            anim.SetTrigger("jump"); // triggers jump animation
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0) // player is not pressing to a horizontal input -> jump off from the wall
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0); // force against the wall (only x axis move)
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z); // turn direction of the player object
            }
            else  // player is pressing to a horizontal input -> climb
            {
                body.linearVelocity = new Vector2(0, 6); // force against the wall and jump upward (y axis move)
            }

            wallJumpCooldown = 0;

        }

    }



    private bool isGrounded() // returns 1 in case player is on the floor
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer); // parameters of BoxCast(1, 2, 3, 4, 5, 6) : 1 -> origin of the box, 2 -> size of the box, 3 -> rotate angle of the box, 4 -> direction of the box, 5 -> distance of the box (we set it just below), 6 -> which layers collisions will be checked?
        return raycastHit.collider != null;

    }


    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer); 
        return raycastHit.collider != null;

    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall(); // player can attack if he is not moving and he is on the ground, also not on the wall
    }

}
