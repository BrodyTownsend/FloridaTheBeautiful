using UnityEngine;

public class Snake : MonoBehaviour, IInteractable
{
    public GameObject outlineObject;
    public FoundAnimalManager animalManager;

    private bool isTargeted = false;
    private bool foundSnake;

    void Start()
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(false);
        }
    }

    public void MarkAsTargeted()
    {
        foundSnake = animalManager.GetComponent<FoundAnimalManager>().foundSnake;
        if (!foundSnake)
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
        Debug.Log("Snake found!");
        animalManager.Snake();
    }
}
