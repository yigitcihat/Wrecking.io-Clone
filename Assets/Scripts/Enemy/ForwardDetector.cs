using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardDetector : MonoBehaviour
{
    EnemyBrain enemy;
    private void Start()
    {
        enemy = transform.root.GetComponentInChildren<EnemyBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StartCoroutine(StopTurningWithDelay());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            int randomDirection = Random.Range(0, 2);
            if (randomDirection == 0)
            {
                enemy.TurnRight = true;
            }
            else
            {
                enemy.TurnLeft = true;
            }
            Debug.Log("Turn");
        }
    }

    IEnumerator StopTurningWithDelay()
    {


        yield return new WaitForSeconds(0.5f);

        int randomDirection = Random.Range(0, 2);
        
            enemy.TurnRight = false;
            enemy.TurnLeft = false;
        
    }

}
