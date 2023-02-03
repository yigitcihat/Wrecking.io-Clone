using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDestroyer : MonoBehaviour
{
    private float groundDestoyTime;
    private void Start()
    {
        groundDestoyTime = Random.Range(5f, 10f);
    }
    private void Update()
    {
        //if (!GameManager.Instance.IsGameStart)
        //{
        //    return;
        //}

        groundDestoyTime -= Time.deltaTime;
        if (groundDestoyTime <= 0)
        {
            transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            Destroy(transform.GetChild(0).GetComponent<Rigidbody>());
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            }


            if (transform.GetChild(0).childCount > 0)
            {
                StartCoroutine(DestroyGroundPart(transform.GetChild(0).GetChild(0)));
            }


            groundDestoyTime = Random.Range(7f, 15f);
        }
        IEnumerator DestroyGroundPart(Transform part)
        {
            Renderer renderer = part.GetComponent<Renderer>();
            renderer.material.DOColor(Color.red, 0.5f);
            yield return new WaitForSeconds(1.5f);
            part.GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForSeconds(1.5f);
            Destroy(part.gameObject);
        }

    }
}
