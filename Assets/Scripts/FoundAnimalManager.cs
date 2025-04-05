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
        foundGreenAnole = true;
    }

    public void BrownAnole()
    {
        foundBrownAnole = true;
    }

    public void Deer()
    {
        foundDeer = true;
    }

    public void Bat()
    {
        foundBat = true;
    }

    public void Snake()
    {
        foundSnake = true;
    }

    public void Tortoise()
    {
        foundTortoise = true;
    }
}
