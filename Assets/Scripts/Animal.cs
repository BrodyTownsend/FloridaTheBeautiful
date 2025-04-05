using UnityEngine;

public class Animal : MonoBehaviour, IInteractable
{
    [Header("1 - Green Anole")]
    [Header("2 - Brown Anole")]
    [Header("3 - Deer")]
    [Header("4 - Bat")]
    [Header("5 - Snake")]
    [Header("6 - Tortoise")]

    public GameObject outlineObject;
    public FoundAnimalManager animalManager;

    public int myType;

    private bool isTargeted = false;
    private bool foundAnimal;

    //Planning
    //Single script for all animals

    //myType to determine animal

    //How to get right animal when checking if the animal is already found?

    void Start()
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(false);
        }
    }

    public void MarkAsTargeted()
    {
        FoundCheck();
        if (!foundAnimal)
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

    void FoundCheck()
    {
        switch (myType)
        {
            case 1:
                //Check if Green Anole has been found
                foundAnimal = animalManager.GetComponent<FoundAnimalManager>().foundGreenAnole;
                break;
            case 2:
                //Check if Brown Anole has been found
                foundAnimal = animalManager.GetComponent<FoundAnimalManager>().foundBrownAnole;
                break;
            case 3:
                //Check if Deer has been found
                foundAnimal = animalManager.GetComponent<FoundAnimalManager>().foundDeer;
                break;
            case 4:
                //Check if Bat has been found
                foundAnimal = animalManager.GetComponent<FoundAnimalManager>().foundBat;
                break;
            case 5:
                //Check if Snake has been found
                foundAnimal = animalManager.GetComponent<FoundAnimalManager>().foundSnake;
                break;
            case 6:
                //Check if Tortoise has been found
                foundAnimal = animalManager.GetComponent<FoundAnimalManager>().foundTortoise;
                break;
        }
    }

    void AnimalInteract()
    {
        //when interacting with an animal, set their corresponding foundAnimal boolean to true in the Manager
        switch (myType)
        {
            case 1:
                animalManager.GreenAnole();
                break;
            case 2:
                animalManager.BrownAnole();
                break;
            case 3:
                animalManager.Deer();
                break;
            case 4:
                animalManager.Bat();
                break;
            case 5:
                animalManager.Snake();
                break;
            case 6:
                animalManager.Tortoise();
                break;
        }
    }

    public void Interact()
    {
        AnimalInteract();
    }
}
