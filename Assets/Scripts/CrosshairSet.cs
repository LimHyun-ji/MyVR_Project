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
        // ���� �������� ���̸� ���� �ε����� ����� �տ� ũ�ν� �� ��ġ��Ų��.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 100, ~(1<<7)))
        {
            crosshair.position = hitInfo.point + hitInfo.normal * 0.1f;
            float dist = Mathf.Max(hitInfo.distance, 1);
            crosshair.localScale = originScale * dist;
            print($"�ε��� ���: {hitInfo.collider.name}");
        }
        else
        {
            crosshair.position = head.position + head.forward * 1;
            print("�Ⱥε������!");
        }

        crosshair.forward = Camera.main.transform.forward;

    }
}
