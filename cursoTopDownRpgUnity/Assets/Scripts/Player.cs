using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    [SerializeField]
    private float speed;
    private Vector2 direction;
    private Animator animator;

    private void GetInput() {
        direction = Vector2.zero;

        if(Input.GetKey(KeyCode.W)) {
            direction += Vector2.up;

        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
    private void Move()
    {
        float velocity = speed * Time.deltaTime;
        transform.Translate(direction * velocity);
        AnimateMovement(direction);
    }
    private void AnimateMovement(Vector2 direction) {
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        direction = Vector2.down;
        boxCollider = GetComponent<BoxCollider2D>();
	}

	private void FixedUpdate()
	{
        GetInput();
        Move();
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Reset the moveDelta
        moveDelta = new Vector3(x, y, 0);

        // Swap sprite direction, wether you're going right or left
        //if(moveDelta.x > 0) {
        //    transform.localScale = Vector3.one;

        //} else if(moveDelta.x < 0) {
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}

        // Make sure we can move in this directions, by casting a box there first, if the box returns null, we`re free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null){
            // Make this thing move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);   
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
	}

}
