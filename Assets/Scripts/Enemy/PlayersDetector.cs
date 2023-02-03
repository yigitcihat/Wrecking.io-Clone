using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersDetector : MonoBehaviour
{

    EnemyBrain enemy;
    private void Start()
    {
        enemy = transform.root.GetComponentInChildren<EnemyBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBrain player = other.GetComponent<PlayerBrain>();
        EnemyBrain enemy = other.GetComponent<EnemyBrain>();
        if (player != null || enemy != null)
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

    private void OnTriggerExit(Collider other)
    {
        PlayerBrain player = other.GetComponent<PlayerBrain>();
        EnemyBrain enemy = other.GetComponent<EnemyBrain>();

        if (player != null || enemy != null)
        {
            StartCoroutine(StopTurningWithDelay());
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
