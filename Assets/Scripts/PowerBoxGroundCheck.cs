using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBoxGroundCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {

            Vector3 ParachutePos = transform.GetChild(1).position;

            transform.GetChild(1).DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
            transform.GetChild(1).DOMoveY(ParachutePos.y / 2, 0.51f).OnComplete(() => transform.GetChild(1).gameObject.SetActive(false));



        }
    }
}
