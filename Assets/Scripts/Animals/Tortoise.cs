using UnityEngine;

public class Tortoise : MonoBehaviour, IInteractable
{
    public GameObject outlineObject;
    public FoundAnimalManager animalManager;

    private bool isTargeted = false;
    private bool foundTortoise;

    void Start()
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(false);
        }
    }

    public void MarkAsTargeted()
    {
        foundTortoise = animalManager.GetComponent<FoundAnimalManager>().foundTortoise;
        if (!foundTortoise)
        {
            isTargeted = true;
        }
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
        Debug.Log("Tortoise found!");
        animalManager.Tortoise();
    }
}
