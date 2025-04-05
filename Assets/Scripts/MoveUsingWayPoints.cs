using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveUsingWayPoints : MonoBehaviour
{
    public List<GameObject> waypoints;
    public float speed;
    int index;
    public bool isLoop;
    public float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = waypoints[index].transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(destination - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.05)
        {
            if(index < waypoints.Count - 1)
            {
                index++;
            }
            else
            {
                if(isLoop)
                {
                    index = 0;
                }
            }

        }
    }
}
