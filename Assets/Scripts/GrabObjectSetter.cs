using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabObjectSetter : MonoBehaviour
{
    Vector3 originPos;
    Rigidbody rb;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("GrabObject");
        originPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public void ResetPosition()
    {
        transform.position = originPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //Quaternion rot = Quaternion.Euler(new Vector3(0, 5, 0));
        //    //rot = Quaternion.Inverse(rot);
        //    //transform.rotation *= rot;

        //    Quaternion objRot = transform.rotation;
        //    Vector3 localRightVec = objRot * new Vector3(1, 1, 1);
        //    transform.forward = localRightVec;
        //}
    }
}
