using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Journal : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = -1;
    bool rotate = false;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject forwardButton;


    private void Start()
    {
        backButton.SetActive(false);
    }
    public void RotateForward()
    {
        if (rotate == true) { return; }
        index++;
        float angle = 180; //This variable will be used to rotate the pages by 180 degress on the y axis

        ForwardButtonAction();
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    public void ForwardButtonAction()
    {
        if (backButton.activeInHierarchy == false)
        {
            backButton.SetActive(true);//this turns the back button everytime we move a page forward
        }

        if (index== pages.Count - 1)
        {
            forwardButton.SetActive(false);//once we get to the last page, the forward button is deactivated
        }
    }
    public void RotateBack()
    {
        if (rotate == true) { return; }
        float angle = 0; //This variable will be used to rotate the page back to its original rotation at 0 degrees
        pages[index].SetAsLastSibling();
        BackButtonAction();
        StartCoroutine(Rotate(angle, false));
    }

    public void BackButtonAction()
    {
        if(forwardButton.activeInHierarchy == false)
        {
            forwardButton.SetActive(true);// every time we turn the page back, the forward will activate
        }
        if(index - 1 == -1)
        {
            backButton.SetActive(false);//if we're at the first page, the back button is deactivated
        }
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;

        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value);//smooth turn for the pages
            float angle1 = Quaternion.Angle(pages[index].rotation, targetRotation);//calculates the angle between the given angle of rotation and the current angle of rotation

            if (angle1 < 0.1f)
            {
                if (forward == false)
                {
                    index--;
                }
                rotate = false;
                break;
            }
            yield return null;
        }
    }
}
