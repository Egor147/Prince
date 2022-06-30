using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class Operator : MonoBehaviour
{
    private Transform plrPos, _transform;
    public float left, right;

    void Awake()
    {
        plrPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>() as Transform;
        _transform = gameObject.GetComponent<Transform>();
    }
    
    void FixedUpdate()
    {
        if(plrPos.position.x > left)
            if(plrPos.position.x < right)
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(plrPos.position.x,_transform.position.y,_transform.position.z), 1);
            else
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(right,_transform.position.y,_transform.position.z), 1);
        else
            _transform.position = Vector3.Lerp(_transform.position, new Vector3(left,_transform.position.y,_transform.position.z), 1);
    }
}