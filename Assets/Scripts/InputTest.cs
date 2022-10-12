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

        // 1. 오른손 콘트롤러의 인덱스 트리거 버튼을 눌렀을 때
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            print("버튼 눌렀다!");
            rightHandText.text = "버튼 눌렀다!";
        }

        // 2. 오른손 콘트롤러의 인덱스 트리거 버튼을 누르고 있는 내내
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            print("버튼 꾹!");
        }

        // 3. 오른손 콘트롤러의 인덱스 트리거 버튼을 뗐을 때
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, myController))
        {
            print("버튼 뗐다!");
            rightHandText.text = "버튼 뗐다!";
        }
        

        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        float triggerPow = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        
        leftHandText.text = $"X축 : {axis.x} \r\n Y축: {axis.y} \r\n 트리거: {triggerPow}";

        // 손가락을 집었을 때 집었다고 출력하자!
        float strength = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        logText.text = strength.ToString();
        logText.text += "\r\n" + rightHand.GetFingerConfidence(OVRHand.HandFinger.Index).ToString();
        //if(rightHand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
        //{
        //    logText.text = "집었다!";
        //}
        //else
        //{
        //    logText.text = "뗐다!";
        //}

    }
}
