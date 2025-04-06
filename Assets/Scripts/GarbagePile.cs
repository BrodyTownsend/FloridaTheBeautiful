using UnityEngine;

public class GarbagePile : MonoBehaviour, IInteractable
{
    public GameObject outlineObject;

    // Add this reference; wire it in the Inspector or use FindObjectOfType in Start().
    public GameManager gameManager;

    private bool isTargeted = false;

    void Start()
    {
        if (outlineObject != null)
            outlineObject.SetActive(false);

        // Optionally, if you don't want to assign in Inspector:
        // gameManager = FindObjectOfType<GameManager>();
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
        
        // Inform the GameManager that we've picked up a pile
        if (gameManager != null)
        {
            gameManager.OnGarbageCollected();
        }

        // Hide or destroy the object
        gameObject.SetActive(false);
    }
}
