using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTVS_InputName
{
    public class MTVS_Input
    {
        public enum Button
        {
#if PC
            Fire1,
            Fire2,
            Fire3,
            Jump,
#elif Oculus
            Fire1 = OVRInput.Button.PrimaryIndexTrigger,
            Fire2 = OVRInput.Button.PrimaryHandTrigger,
            Fire3 = OVRInput.Button.Two,
            Jump = OVRInput.Button.One
#endif
        }



        public enum Controller
        {
#if PC
            LTouch,
            RTouch,
#elif Oculus
            LTouch = OVRInput.Controller.LTouch,
            RTouch = OVRInput.Controller.RTouch,
#endif

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

        static Transform leftHandTransform;
        public static Transform LeftHandTransform
        {
            get
            {
                leftHandTransform = GetOriginTransform().Find("LeftHandAnchor");
                return leftHandTransform;
            }
        }

        static Transform rightHandTransform;
        public static Transform RightHandTransform
        {
            get
            {
                rightHandTransform = GetOriginTransform().Find("RightHandAnchor");
                return rightHandTransform;
            }
        }
  

        // �Է� �Լ� 3��
        public static bool GetDown(Button input, Controller mask)
        {
#if PC
            return Input.GetButtonDown(input.ToString());
#elif Oculus
            return OVRInput.GetDown((OVRInput.Button)input, (OVRInput.Controller)mask);
#endif

        }

        public static bool Get(Button input, Controller mask)
        {
#if PC
            return Input.GetButton(input.ToString());
#elif Oculus
            return OVRInput.Get((OVRInput.Button)input, (OVRInput.Controller)mask);
#endif

        }


        public static bool GetUp(Button input, Controller mask)
        {
#if PC
            return Input.GetButtonUp(input.ToString());
#elif Oculus
            return OVRInput.GetUp((OVRInput.Button)input, (OVRInput.Controller)mask);
#endif

        }
        
        // View ������ �Լ�
        public static void Recenter()
        {
            OVRManager.display.RecenterPose();
        }


    }

    class VibrationManager : MonoBehaviour
    {
        public static VibrationManager instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        /// <summary>
        /// ����Ʈ2 ��Ʈ�ѷ� ���� �Լ�
        /// </summary>
        /// <param name="frequency">���� Ƚ��</param>
        /// <param name="amplitude">����</param>
        /// <param name="time">�ð�</param>
        /// <param name="mask">��Ʈ�ѷ�</param>
        public void StartViberation(float frequency, float amplitude, float time, MTVS_Input.Controller mask)
        {
            StopAllCoroutines();

            StartCoroutine(ControllerVibrationTimer(frequency, amplitude, time, mask));
        }


        /// <summary>
        /// ���� �ڷ�ƾ �Լ�
        /// </summary>
        /// <param name="frequency">���� Ƚ��</param>
        /// <param name="amplitude">����</param>
        /// <param name="time">�ð�</param>
        /// <param name="mask">��Ʈ�ѷ�</param>
        /// <returns></returns>
        IEnumerator ControllerVibrationTimer(float frequency, float amplitude, float time, MTVS_Input.Controller mask)
        {
            // ���� ����
            OVRInput.SetControllerVibration(frequency, amplitude, (OVRInput.Controller)mask);

            yield return new WaitForSeconds(time);

            // ���� ��
            OVRInput.SetControllerVibration(0, 0, (OVRInput.Controller)mask);
        }
    }
}

