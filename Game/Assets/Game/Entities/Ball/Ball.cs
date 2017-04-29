using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float _movementForce = 50;
    public Rigidbody2D RigidBody2D;

	private void Start()
	{
        RigidBody2D = GetComponent<Rigidbody2D>();
    }
	
	private void Update()
	{
        return;
        if (Input.GetKey(KeyCode.W))
        {
            RigidBody2D.AddForce(new Vector2(0, _movementForce) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            RigidBody2D.AddForce(new Vector2(0, -_movementForce) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            RigidBody2D.AddForce(new Vector2(-_movementForce, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            RigidBody2D.AddForce(new Vector2(_movementForce, 0) * Time.deltaTime);
        }
    }
}
