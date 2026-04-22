using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public float speed = 1f, waitTime, waitTimeStart = 2f;
    public Transform[] listPoints;
    public Transform centerPosition;
    
    private int index = 0;
    private Vector2 posAct;
    private SpriteRenderer sr;

    public float visionLimit = 1f, movementLimit = 0.8f;
    public Transform playerPos;


    // Start is called before the first frame update
    void Start()
    {
        waitTime = waitTimeStart;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        posAct = transform.position;
        if (Vector2.Distance(posAct, playerPos.position) >= visionLimit || Vector2.Distance(centerPosition.position, playerPos.position) >= movementLimit)
        {
            Patrol();
        }
        else
        {
            FollowPlayer();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(posAct, visionLimit);
        Gizmos.DrawWireSphere(centerPosition.position, movementLimit);
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(posAct, listPoints[index].position, speed * Time.deltaTime);
        if (Vector2.Distance(posAct, listPoints[index].position) <= 0.1f)
        {
            if (waitTime <= 0)
            {
                if (listPoints[index] != listPoints[listPoints.Length - 1])
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                waitTime = waitTimeStart;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(posAct, playerPos.position, speed * Time.deltaTime);
    }

    private void Flip()
    {

    }
}
