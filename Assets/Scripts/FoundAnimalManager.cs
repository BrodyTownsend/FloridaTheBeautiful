using UnityEngine;

public class FoundAnimalManager : MonoBehaviour
{

    public bool foundGreenAnole;
    public bool foundBrownAnole;
    public bool foundDeer;
    public bool foundBat;
    public bool foundSnake;
    public bool foundTortoise;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foundGreenAnole = false;
        foundBrownAnole = false;
        foundDeer = false;
        foundBat = false;
        foundSnake = false;
        foundTortoise = false;
    }


    public void GreenAnole()
    {
        Debug.Log("Green Anole found!");
        foundGreenAnole = true;
    }

    public void BrownAnole()
    {
        Debug.Log("Brown Anole found!");
        foundBrownAnole = true;
    }

    public void Deer()
    {
        Debug.Log("Deer found!");
        foundDeer = true;
    }

    public void Bat()
    {
        Debug.Log("Bat found!");
        foundBat = true;
    }

    public void Snake()
    {
        Debug.Log("Snake found!");
        foundSnake = true;
    }

    public void Tortoise()
    {
        Debug.Log("Tortoise found!");
        foundTortoise = true;
    }
}
