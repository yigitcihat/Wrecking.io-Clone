using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBoxSpawner : MonoBehaviour
{
   
    private float boxSpawnTime;
    [SerializeField]
    private GameObject PowerBoxPref;
    private void Start()
    {
        boxSpawnTime = Random.Range(7f, 15f);
    }
    private void Update()
    {
        if (!GameManager.Instance.IsGameStart)
        {
            return;
        }

        boxSpawnTime -= Time.deltaTime;
        if (boxSpawnTime <= 0)
        {
            Instantiate(PowerBoxPref,new Vector3(Random.Range(-12,12),transform.position.y,Random.Range(-12, 12)),Quaternion.identity,transform);
            boxSpawnTime = Random.Range(7f, 15f);
        }

        
    }
}
