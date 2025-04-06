using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public Transform doorPivot;
    public float closedAngle = 0f;
    public float openAngle = 90f;
    public float openCloseDuration = 1f;
    public bool isOpen = false;

    public GameObject outlineObject;

    private bool isTargeted = false;
    private bool isAnimating = false;

    void Start()
    {
        if (doorPivot == null)
        {
            doorPivot = transform;
        }
        SetDoorAngle(closedAngle);

        if (outlineObject != null)
        {
            outlineObject.SetActive(false);
        }
    }

    public void Interact()
    {
        ToggleDoor();
    }

    public void MarkAsTargeted()
    {
        isTargeted = true;
    }

    void LateUpdate()
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(isTargeted);
        }
        isTargeted = false;
    }

    void ToggleDoor()
    {
        if (!isAnimating)
        {
            float targetAngle = isOpen ? closedAngle : openAngle;
            StartCoroutine(SwingDoor(targetAngle));
        }
    }

    IEnumerator SwingDoor(float targetAngle)
    {
        isAnimating = true;
        float startAngle = doorPivot.localEulerAngles.y;
        float elapsed = 0f;

        while (elapsed < openCloseDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / openCloseDuration);
            float currentAngle = Mathf.Lerp(startAngle, targetAngle, t);
            Vector3 euler = doorPivot.localEulerAngles;
            euler.y = currentAngle;
            doorPivot.localEulerAngles = euler;
            yield return null;
        }

        SetDoorAngle(targetAngle);
        isOpen = !isOpen;
        isAnimating = false;
    }

    void SetDoorAngle(float angleY)
    {
        Vector3 euler = doorPivot.localEulerAngles;
        euler.y = angleY;
        doorPivot.localEulerAngles = euler;
    }
}
