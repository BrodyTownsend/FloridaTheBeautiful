using UnityEngine;

public class BrownAnole : MonoBehaviour, IInteractable
{
    public GameObject outlineObject;
    public FoundAnimalManager animalManager;

    private bool isTargeted = false;
    private bool foundBrownAnole;

    void Start()
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(false);
        }
    }

    public void MarkAsTargeted()
    {
        foundBrownAnole = animalManager.GetComponent<FoundAnimalManager>().foundBrownAnole;
        if (!foundBrownAnole)
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
        Debug.Log("BrownAnole found!");
        animalManager.BrownAnole();
    }
}
