using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBallCollision : MonoBehaviour
{
    const float FORCE = 8f;
    private void OnCollisionEnter(Collision collision)
    {
        EnemyBrain enemy = collision.gameObject.GetComponent<EnemyBrain>();
        PlayerBrain player = collision.gameObject.GetComponent<PlayerBrain>();
        if (enemy != null )
        {
            Rigidbody rb = enemy.GetComponent<Rigidbody>();

            rb.AddForce((enemy.transform.position - transform.position).normalized + new Vector3(0, Random.Range(8, 15), 0) ,ForceMode.Impulse);
            Debug.Log("Enemy Crashed");
        }
        if (player !=null )
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();

            rb.AddForce((player.transform.position - transform.position).normalized+ new Vector3(0, Random.Range(8, 15), 0), ForceMode.Impulse);
            Debug.Log(" PlayerCrashed");
        }
    }
}
