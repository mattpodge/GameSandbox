using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerInput : MonoBehaviour
{

    float playerSpeed = 6f;
    float playerGravity = -10f;
    Vector2 velocity;
    Vector2 rawInput;

	float velocitySmoothing;
    float accTime = 0.2f;

    CharacterController2D controller;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void FixedUpdate()
    {
        CalcVelocity();
        controller.Move(velocity * Time.deltaTime);
	}

    public void OnMovement(InputAction.CallbackContext context) {
        rawInput = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
	}

    public void OnJump(InputAction.CallbackContext context) {
        Debug.Log("Jump!");
    }

    void CalcVelocity() {
		float targetVelocity = rawInput.x * playerSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity, ref velocitySmoothing, accTime);
		velocity.y += playerGravity * Time.deltaTime;
	}
}
