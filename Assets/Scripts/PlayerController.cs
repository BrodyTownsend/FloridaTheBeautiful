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
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Camera Shake")]
    public float idleShakeIntensity = 0.015f;
    public float walkShakeIntensity = 0.04f;
    public float runShakeIntensity = 0.07f;
    public float shakeFrequency = 15f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    private Vector3 originalCamLocalPos;
    private float shakeTimer;
    public bool journalOpen = false;

    public GameObject journalCanvas;

    public bool canMove = true;

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenJournal();
        }
    }

    void HandleMovement()
    {
        if(journalOpen)
        {
            return;
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y -= gravity;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleLook()
    {
        if (!canMove) return;
        if (journalOpen) return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
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
            // Highlight it if it supports MarkAsTargeted
            if (interactable is GarbagePile garbage)
            {
                garbage.MarkAsTargeted();
            }

            if (interactable is Animal animal)
            {
                    animal.MarkAsTargeted();
            }

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

        if (journalOpen == true)
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
