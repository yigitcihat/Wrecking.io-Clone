using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTurner : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalRotate(new Vector3(360.0f, 0, 0.0f), 2.0f, RotateMode.FastBeyond360)
             .SetLoops(-1, LoopType.Restart)
             .SetRelative()
             .SetEase(Ease.Linear);
    }

}
