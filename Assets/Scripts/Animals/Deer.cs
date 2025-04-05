using UnityEngine;

public class Deer : MonoBehaviour, IInteractable
{
    public GameObject outlineObject;
    public FoundAnimalManager animalManager;

    private bool isTargeted = false;
    private bool foundDeer;

    void Start()
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(false);
        }
    }

    public void MarkAsTargeted()
    {
        foundDeer = animalManager.GetComponent<FoundAnimalManager>().foundDeer;
        if (!foundDeer)
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
        Debug.Log("Deer found!");
        animalManager.Deer();
    }
}
