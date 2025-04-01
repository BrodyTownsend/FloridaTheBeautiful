using UnityEngine;

public class GarbagePile : MonoBehaviour, IInteractable
{
    public GameObject outlineObject;

    private bool isTargeted = false;

    void Start()
    {
        if (outlineObject != null)
            outlineObject.SetActive(false);
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

        // Reset for next frame — only show if targeted again
        isTargeted = false;
    }

    public void Interact()
    {
        Debug.Log("Garbage collected!");
        gameObject.SetActive(false);
    }
}
