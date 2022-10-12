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

        // 기준점(VR: 헤드 마운트 장비)의 트랜스폼을 가져오기 위한 함수
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


        // 왼손의 위치 벡터 프로퍼티(속성)
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

        // 입력 함수 3종
        public static bool GetDown(Button input, Controller mask)
        {

            return false;
        }


    }
}

