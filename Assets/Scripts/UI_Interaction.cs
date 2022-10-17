using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTVS_InputName;

public class UI_Interaction : MonoBehaviour
{
    public Transform laserPointer;
    Button_Interaction pointingButton;

    void Start()
    {
        
    }

    void Update()
    {
        //���̸� ���� �ε��� ����� ���̾ "UI_Element"��� �� ������Ʈ�� Button_Interaction ������Ʈ�� �����صд�.
        Ray ray = new Ray(MTVS_Input.RightHandTransform.position, MTVS_Input.RightHandTransform.forward);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 100, 1<<8))
        {
            pointingButton = hitInfo.transform.GetComponent<Button_Interaction>();
            laserPointer.position = hitInfo.point + hitInfo.normal * 0.1f;
            laserPointer.forward = hitInfo.normal * -1;
            //laserPointer.gameObject.SetActive(true);
            //print("Hit!!!");
        }
        else
        {
            if (pointingButton)
            {
                pointingButton.OnClickEvent("");
            }
            pointingButton = null;
            //laserPointer.gameObject.SetActive(false);
            //print("not Hit~~");
        }
        laserPointer.gameObject.SetActive(pointingButton != null);


        if(pointingButton != null)
        {
            if(MTVS_Input.GetDown(MTVS_Input.Button.Fire1, MTVS_Input.Controller.RTouch))
            {
                pointingButton.OnClickEvent("������");
            }
        }
    }
}
