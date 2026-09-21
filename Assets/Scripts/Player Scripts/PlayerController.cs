using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float speed = 3.0f;

    [Header ("References")]
    [SerializeField] private Rigidbody2D characterController;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] public string shopSceneName = "";

    private Vector2 currentMovement;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private Vector2 CalculateWorldDirection()
    {
        if (gameObject.scene.name != shopSceneName)
        {
            Vector2 inputDirection = new Vector2(playerInputHandler.MovementInput.x, playerInputHandler.MovementInput.y);
            Vector2 worldDirection = transform.TransformDirection(inputDirection);
            return worldDirection.normalized;
        }
        else
        {
            Vector2 inputDirection = new Vector2(playerInputHandler.MovementInput.x, 0);
            Vector2 worldDirection = transform.TransformDirection(inputDirection);
            return worldDirection.normalized;
        }
    }

    private void HandleMovement()
    {
        Vector2 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * speed;
        currentMovement.y = worldDirection.y * speed;

        characterController.linearVelocity = currentMovement;
    }
}