using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _rigidbody.isKinematic = true;
            GetComponent<BoxCollider>().enabled= false;
            EventManager.OnEnemyDrop.Invoke();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("PowerBox"))
        {
            Debug.Log("Enemy PowerUp");
            Destroy(other.gameObject);
        }
    }
}
