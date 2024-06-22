using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementContoller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3.0f;

    private Vector2 movement = new Vector2();
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb2D.velocity = movement * movementSpeed;
    }
}
