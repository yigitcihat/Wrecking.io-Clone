using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class RopeLineRendererConnection : MonoBehaviour
{
    private LineRenderer _line;
    private Transform _origin;
    private Transform _target;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _origin = GetComponent<Transform>();
        _target = GetComponent<ConfigurableJoint>().connectedBody.transform;
    }
    void Update()
    {
        _line.SetPosition(0, _origin.position);
        _line.SetPosition(1, _target.position  + new Vector3(0,.8f,-.5f));
    }
}
