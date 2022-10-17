using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTVS_InputName;

public class CrosshairSet : MonoBehaviour
{
    public Transform crosshair;
    Transform head;
    Vector3 originScale;

    void Start()
    {
        head = MTVS_Input.GetOriginTransform();
        originScale = crosshair.localScale;
    }

    void Update()
    {
        // 정면 방향으로 레이를 쏴서 부딪히는 대상의 앞에 크로스 헤어를 위치시킨다.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 100, ~(1<<7)))
        {
            crosshair.position = hitInfo.point + hitInfo.normal * 0.1f;
            float dist = Mathf.Max(hitInfo.distance, 1);
            crosshair.localScale = originScale * dist;
            print($"부딪힌 대상: {hitInfo.collider.name}");
        }
        else
        {
            crosshair.position = head.position + head.forward * 1;
            print("안부딪혔어요!");
        }

        crosshair.forward = Camera.main.transform.forward;

    }
}
