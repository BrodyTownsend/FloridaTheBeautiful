using UnityEngine;

public class GreenAnole : MonoBehaviour, IInteractable
{
    public GameObject outlineObject;
    public FoundAnimalManager animalManager;

    private bool isTargeted = false;
    private bool foundGreenAnole;

    void Start()
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(false);
        }
    }

    public void MarkAsTargeted()
    {
        foundGreenAnole = animalManager.GetComponent<FoundAnimalManager>().foundGreenAnole;
        if (!foundGreenAnole)
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
        Debug.Log("GreenAnole found!");
        animalManager.GreenAnole();
    }
}
