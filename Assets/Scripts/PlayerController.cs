using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    // Increased gravity to make jumps feel even less floaty
    public float gravity = 20f;

    [Header("Jump Settings")]
    // Halved jumpForce from 5f to 2.5f
    public float jumpForce = 2.5f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Camera Shake")]
    public float idleShakeIntensity = 0.015f;
    public float walkShakeIntensity = 0.04f;
    public float runShakeIntensity = 0.07f;
    public float shakeFrequency = 15f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float verticalVelocity = 0f; // For handling gravity and jumps
    private CharacterController characterController;
    private Vector3 originalCamLocalPos;
    private float shakeTimer;
    public bool journalOpen = false;

    public bool gameStart = false;

    public GameObject journalCanvas;

    public bool canMove = true;

    public Journal journalScript;

    public GameObject openingScreen;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        if (!playerCamera) playerCamera = Camera.main;
        originalCamLocalPos = playerCamera.transform.localPosition;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        ApplyCameraShake();
        HandleInteraction();

        if (Input.anyKeyDown)
        {
            openingScreen.SetActive(false);
            gameStart = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(journalScript.rotate == false)
            {
                OpenJournal();
            }
            
        }
    }

    void HandleMovement()
    {
        if (journalOpen) return;

        // Determine desired speed
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Get input
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // Convert input to world space direction
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right   = transform.TransformDirection(Vector3.right);

        // Calculate horizontal movement (X/Z)
        float moveX = currentSpeed * inputX;
        float moveZ = currentSpeed * inputZ;

        // Combine horizontal movement
        Vector3 horizontalMovement = (forward * moveZ) + (right * moveX);

        // Handle jump and gravity
        if (characterController.isGrounded)
        {
            // If grounded, reset vertical velocity
            verticalVelocity = -1f;

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && canMove)
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            // Apply gravity when not grounded
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Combine vertical velocity with horizontal movement
        moveDirection = horizontalMovement;
        moveDirection.y = verticalVelocity;

        // Move the CharacterController
        if (canMove)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    void HandleLook()
    {
        if (!canMove) return;
        if (journalOpen) return;

        if (gameStart == true)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void ApplyCameraShake()
    {
        float movementMagnitude = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude;
        bool isMoving = movementMagnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isMoving)
        {
            float shakeAmount = isRunning ? runShakeIntensity : walkShakeIntensity;
            float frequency = shakeFrequency;
            shakeTimer += Time.deltaTime * frequency;

            Vector3 shakeOffset = new Vector3(
                Mathf.Sin(shakeTimer) * shakeAmount * 0.5f,
                Mathf.Cos(shakeTimer * 2f) * shakeAmount,
                0
            );

            playerCamera.transform.localPosition = originalCamLocalPos + shakeOffset;
        }
        else
        {
            shakeTimer += Time.deltaTime * 2f;
            float idleOffsetX = Mathf.Sin(shakeTimer) * idleShakeIntensity * 0.5f;
            float idleOffsetY = Mathf.Cos(shakeTimer * 0.8f) * idleShakeIntensity;

            playerCamera.transform.localPosition = originalCamLocalPos + new Vector3(idleOffsetX, idleOffsetY, 0);
        }
    }

  void HandleInteraction()
{
    Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
    RaycastHit hit;
    float interactRange = 3f;

    if (Physics.Raycast(ray, out hit, interactRange))
    {
        IInteractable interactable = hit.collider.GetComponent<IInteractable>();
        if (interactable != null)
        {
            // Example existing code for highlighting
            if (interactable is GarbagePile garbage)
            {
                garbage.MarkAsTargeted();
            }

            if (interactable is Animal animal)
            {
                animal.MarkAsTargeted();
            }
            
            // --- ADD THIS BLOCK ---
            if (interactable is Door door)
            {
                door.MarkAsTargeted();
            }
            // ----------------------

            // If player pressed E, call Interact
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
    }
}


    void OpenJournal()
    {
        journalOpen = !journalOpen;

        if (journalOpen)
        {
            journalCanvas.SetActive(true);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
        else
        {
            journalCanvas.SetActive(false);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
    }
}
