using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTVS_InputName
{
    public class MTVS_Input
    {
        public enum Button
        {
            Fire1,
            Fire2,
            Fire3,
            Jump,
        }

        public enum ButtonTarget
        {
#if PC
            Fire1,
            Fire2,
            Fire3,
            Jump,
//#elif Oculus

#endif
        }


        public enum Controller
        {
            LTouch,
            RTouch,

        }


        static Transform origin;

        // ������(VR: ��� ����Ʈ ���)�� Ʈ�������� �������� ���� �Լ�
        public static Transform GetOriginTransform()
        {
#if PC
            origin = Camera.main.transform;
            return origin;

#elif Oculus
            origin = GameObject.Find("TrackingSpace").transform;
            return origin;
#endif
        }


        // �޼��� ��ġ ���� ������Ƽ(�Ӽ�)
        static Vector3 leftHandPos;

        public static Vector3 LeftHandPosition
        {
            get
            {
#if PC
                leftHandPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                          Input.mousePosition.y,
                                                                          Camera.main.nearClipPlane));
                return leftHandPos;

#elif Oculus
                leftHandPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
                leftHandPos = GetOriginTransform().TransformPoint(leftHandPos);
                return leftHandPos;
#endif
            }
        }

        static Vector3 rightHandPos;
        public static Vector3 RightHandPosition
        {
            get
            {
#if PC
                rightHandPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                          Input.mousePosition.y,
                                                                          Camera.main.nearClipPlane));
                return rightHandPos;

#elif Oculus
                rightHandPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                rightHandPos = GetOriginTransform().TransformPoint(rightHandPos);
                return rightHandPos;
#endif
            }
        }

        // �Է� �Լ� 3��
        public static bool GetDown(Button input, Controller mask)
        {

            return false;
        }


    }
}

