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
    public float throwPower = 5;
    public float torquePower = 5;
    public GrabObjectSetter stick;

    Transform grabedObject_Left;
    Transform grabedObject_Right;

    // Start is called before the first frame update
    void Start()
    {
        if (VibrationManager.instance == null)
        {
            new GameObject("ViberationManager", typeof(VibrationManager));
        }


    }

    // Update is called once per frame
    void Update()
    {
        #region �Է� �׽�Ʈ
        //// 1. ������ ��Ʈ�ѷ��� �ε��� Ʈ���� ��ư�� ������ ��
        //if (MTVS_Input.GetDown(MTVS_Input.Button.Fire1, MTVS_Input.Controller.RTouch))
        //{
        //    print("��ư ������!");
        //    rightHandText.text = "��ư ������!";
        //}

        //// 2. ������ ��Ʈ�ѷ��� �ε��� Ʈ���� ��ư�� ������ �ִ� ����
        //if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        //{
        //    print("��ư ��!");
        //}

        //// 3. ������ ��Ʈ�ѷ��� �ε��� Ʈ���� ��ư�� ���� ��
        //if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, myController))
        //{
        //    print("��ư �ô�!");
        //    rightHandText.text = "��ư �ô�!";
        //}


        //Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        //float triggerPow = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);

        //leftHandText.text = $"X�� : {axis.x} \r\n Y��: {axis.y} \r\n Ʈ����: {triggerPow}";

        //// �հ����� ������ �� �����ٰ� �������!
        //float strength = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        //logText.text = strength.ToString();
        //logText.text += "\r\n" + rightHand.GetFingerConfidence(OVRHand.HandFinger.Index).ToString();
        ////if(rightHand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
        ////{
        ////    logText.text = "������!";
        ////}
        ////else
        ////{
        ////    logText.text = "�ô�!";
        ////}
        #endregion

        //if (MTVS_Input.GetDown(MTVS_Input.Button.Fire1, MTVS_Input.Controller.RTouch))
        //{
        //    VibrationManager.instance.StartViberation(0.5f, 0.5f, 0.5f, MTVS_Input.Controller.RTouch);
        //}

        if(MTVS_Input.GetDown(MTVS_Input.Button.Fire2, MTVS_Input.Controller.LTouch))
        {
            Grab(true);
        }
        else if(MTVS_Input.GetUp(MTVS_Input.Button.Fire2, MTVS_Input.Controller.LTouch))
        {
            Release(true);
        }

        if(MTVS_Input.GetDown(MTVS_Input.Button.Jump, MTVS_Input.Controller.LTouch))
        {
            stick.ResetPosition();
        }

    }

    // ���� ���
    void Grab(bool isLeft)
    {
        Transform grabed = isLeft ? grabedObject_Left : grabedObject_Right;
        if(grabed)
        {
            return;
        }

        Vector3 handPosition = isLeft ? MTVS_Input.LeftHandPosition : MTVS_Input.RightHandPosition;
        Collider[] cols = Physics.OverlapSphere(handPosition, 1, 1 << 6);

        if (cols.Length > 0)
        {
            print(cols[0].name);
            Rigidbody rb = cols[0].GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = true;
            }
            cols[0].transform.parent = isLeft ? MTVS_Input.LeftHandTransform : MTVS_Input.RightHandTransform;
            cols[0].transform.localPosition = Vector3.zero;
            
            if(isLeft)
            {
                grabedObject_Left = cols[0].transform;
            }
            else
            {
                grabedObject_Right = cols[0].transform;
            }
        }
    }

    // ���� ����
    void Release(bool isLeft)
    {
        Transform grabed = isLeft ? grabedObject_Left : grabedObject_Right;
        if (!grabed)
        {
            return;
        }

        grabed.parent = null;
        Rigidbody rb = grabed.GetComponent<Rigidbody>();

        if(rb)
        {
            rb.isKinematic = false;

            // ������ �������� ���� ���Ѵ�.
            //rb.AddForce()

#if Oculus
            rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch) * throwPower;
            rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.LTouch) * torquePower;
#endif
            
        }
       


        if(isLeft)
        {
            grabedObject_Left = null;
        }
        else
        {
            grabedObject_Right = null;
        }
    }

}
