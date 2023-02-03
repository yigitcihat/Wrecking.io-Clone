using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBallCollision : MonoBehaviour
{
    const float FORCE = 4f;
    private void OnCollisionEnter(Collision collision)
    {
        EnemyBrain enemy = collision.gameObject.GetComponent<EnemyBrain>();
        PlayerBrain player = collision.gameObject.GetComponent<PlayerBrain>();
        if (enemy != null )
        {
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            rb.WakeUp();
            rb.AddForce((enemy.transform.position - transform.position + new Vector3(0,2,0)).normalized *FORCE + new Vector3(0, Random.Range(8, 12), 0) ,ForceMode.VelocityChange);
          
        }
        if (player !=null )
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.WakeUp();
            rb.AddForce((player.transform.position - transform.position+ new Vector3(0,2,0)).normalized * FORCE + new Vector3(0, Random.Range(8, 12), 0), ForceMode.VelocityChange);
            rb.velocity = Vector3.zero;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("PowerBox"))
        {
            PlayerBrain playerBrain = transform.root.GetComponentInChildren<PlayerBrain>();
            EnemyBrain enemyBrain = transform.root.GetComponentInChildren<EnemyBrain>();

            if (enemyBrain != null)
            {
                Debug.Log("Enemy PowerUp");
                Destroy(collision.gameObject);
            }
            if (playerBrain != null)
            {
                playerBrain.PowerActivate(collision.gameObject.GetComponent<Collider>());
                Debug.Log("Player PowerUp");
                Destroy(collision.gameObject);
            }
        }
    }
}
