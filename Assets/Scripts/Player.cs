using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{


    public float speed = 5f;

    private Rigidbody2D rb;

    private Vector2 movementInput;

    private Animator animator;







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput = movementInput.normalized;

        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.magnitude);


    }
    
    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput * speed;
    }
}
