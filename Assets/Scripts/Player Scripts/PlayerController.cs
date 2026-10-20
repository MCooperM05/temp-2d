using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] public float speed = 3.0f;
    [SerializeField] private Animator playerAnimator;
    public static float bonusSpeed = 0f;

    [Header ("References")]
    [SerializeField] private Rigidbody2D characterController;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] public string shopSceneName = "WorldHub";
    [SerializeField] public SpriteRenderer playerRender;

    [Header ("Sounds")]
    [SerializeField] private AudioSource swimSound;
    [SerializeField] private AudioSource walkSound;

    private bool isOnLand = true;
    public Vector2 currentMovement;

    // Start is called before the first frame update
    void Start()
    {
        if (playerRender == null) playerRender = GetComponent<SpriteRenderer>();
        if (characterController == null) characterController = GetComponent<Rigidbody2D>();
        if (playerInputHandler == null) playerInputHandler = FindFirstObjectByType<PlayerInputHandler>();
        if (playerAnimator == null) playerAnimator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isOnLand = true; // default to true, since the player starts in the shop
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
            isOnLand = false; // if the player is not in the shop, tell animator

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
            isOnLand = true; // if the player is in the shop, tell animator

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
        currentMovement.x = worldDirection.x * (speed + bonusSpeed);
        currentMovement.y = worldDirection.y * (speed + bonusSpeed);

        if (isOnLand)
        {
            playerAnimator.SetBool("IsOnLand", true);
            if (currentMovement.x != 0 || currentMovement.y != 0)
            {
                playerAnimator.SetBool("IsWalking", true);
            }
            else
            {
                playerAnimator.SetBool("IsWalking", false);
            }
        }
        else
        {
            playerAnimator.SetBool("IsOnLand", false);
        }

        characterController.linearVelocity = currentMovement;
    }

    // Access the AudioSource component and play the swim sound effect
    public void PlaySwimSound()
    {
        swimSound.Play();
    }

    // Access the AudioSource component and play the land sound effect
    public void PlayWalkSound()
    {
        walkSound.Play();
    }
}