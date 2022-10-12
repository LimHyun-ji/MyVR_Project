using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MTVS_InputName;


public class InputTest : MonoBehaviour
{
    public Text rightHandText;
    public Text leftHandText;
    public Text logText;
    public OVRHand rightHand;
    public OVRInput.Controller myController;

    // Start is called before the first frame update
    void Start()
    {
         //MTVS_Input.GetOriginTransform();

    }

    // Update is called once per frame
    void Update()
    {

        if(MTVS_Input.GetDown(MTVS_Input.Button.Jump, MTVS_Input.Controller.RTouch))
        {

        }

        // 1. ������ ��Ʈ�ѷ��� �ε��� Ʈ���� ��ư�� ������ ��
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            print("��ư ������!");
            rightHandText.text = "��ư ������!";
        }

        // 2. ������ ��Ʈ�ѷ��� �ε��� Ʈ���� ��ư�� ������ �ִ� ����
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            print("��ư ��!");
        }

        // 3. ������ ��Ʈ�ѷ��� �ε��� Ʈ���� ��ư�� ���� ��
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, myController))
        {
            print("��ư �ô�!");
            rightHandText.text = "��ư �ô�!";
        }
        

        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        float triggerPow = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        
        leftHandText.text = $"X�� : {axis.x} \r\n Y��: {axis.y} \r\n Ʈ����: {triggerPow}";

        // �հ����� ������ �� �����ٰ� �������!
        float strength = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        logText.text = strength.ToString();
        logText.text += "\r\n" + rightHand.GetFingerConfidence(OVRHand.HandFinger.Index).ToString();
        //if(rightHand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
        //{
        //    logText.text = "������!";
        //}
        //else
        //{
        //    logText.text = "�ô�!";
        //}

    }
}
