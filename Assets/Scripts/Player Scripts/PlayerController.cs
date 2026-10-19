using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] public float speed = 3.0f;

    [Header ("References")]
    [SerializeField] private Rigidbody2D characterController;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] public string shopSceneName = "";
    [SerializeField] public SpriteRenderer playerRender;

    private Vector2 currentMovement;

    // Start is called before the first frame update
    void Start()
    {
        if (playerRender == null) playerRender = GetComponent<SpriteRenderer>();
        if (characterController == null) characterController = GetComponent<Rigidbody2D>();
        if (playerInputHandler == null) playerInputHandler = FindFirstObjectByType<PlayerInputHandler>();
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
            if (playerInputHandler.MovementInput.x < 0)
            {
                playerRender.flipX = true;
            }
            else if (playerInputHandler.MovementInput.x > 0)
            {
                playerRender.flipX = false;
            }
            Vector2 inputDirection = new Vector2(playerInputHandler.MovementInput.x, playerInputHandler.MovementInput.y);
            Vector2 worldDirection = transform.TransformDirection(inputDirection);
            return worldDirection.normalized;
        }
        else
        {
            if (playerInputHandler.MovementInput.x < 0)
            {
                playerRender.flipX = true;
            }
            else if (playerInputHandler.MovementInput.x > 0)
            {
                playerRender.flipX = false;
            }
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