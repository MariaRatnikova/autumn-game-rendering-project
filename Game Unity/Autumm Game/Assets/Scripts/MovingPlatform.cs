using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> points;
    int goalPoint = 0;
    public float moveSpeed =2;
    public Transform platform;

    private void Update()
    {
        MovetoNextPoint();

    }
    private void MovetoNextPoint(){
        // change the position of the platform(move towards the goal point)
        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position,1*Time.deltaTime*moveSpeed);
        //Check if we are in very close proximity  of the next point
        if(Vector2.Distance(platform.position,points[goalPoint].position)<0.1f)
        {
            if(goalPoint==points.Count-1)
                goalPoint = 0;
            else
                goalPoint++;
                
        }
        // If so change goal point to the next one 
        // Check if we reached the last point, reset to first point
    }





}
