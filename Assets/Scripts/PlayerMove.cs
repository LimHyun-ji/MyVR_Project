using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTVS_InputName;

[RequireComponent(typeof(LineRenderer))]
public class PlayerMove : MonoBehaviour
{
    public float movePower = 7;

    RaycastHit hitInfo;
    //List<Vector3> lineList = new List<Vector3>();
    LineRenderer lr;

    List<Vector3> curvedList = new List<Vector3>();

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
    }

    void Update()
    {

        #region 기존 이동 방식
        //#if PC
        //        float h = Input.GetAxis("Horizontal");
        //        float v = Input.GetAxis("Vertical");
        //#elif Oculus
        //        Vector2 controllerAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        //        float h = controllerAxis.x;
        //        float v = controllerAxis.y;
        //#endif

        //        transform.position += new Vector3(h, 0, v) * 7 * Time.deltaTime;
        #endregion

        #region 텔레포트 이동 방식(직선)

        //if(MTVS_Input.Get(MTVS_Input.Button.Jump, MTVS_Input.Controller.RTouch))
        //{
        //    Ray ray = new Ray(MTVS_Input.RightHandPosition, MTVS_Input.RightHandTransform.forward);

        //    List<Vector3> rayList = new List<Vector3>();
        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        rayList.Add(MTVS_Input.RightHandPosition);
        //        //Vector3 targetPos = hitInfo.point + new Vector3(0, 0.5f, 0);
        //        rayList.Add(hitInfo.point);
        //    }
        //    lineList = rayList;

        //    lr.positionCount = lineList.Count;
        //    lr.SetPositions(lineList.ToArray());
        //}
        //else if (MTVS_Input.GetUp(MTVS_Input.Button.Jump, MTVS_Input.Controller.RTouch))
        //{
        //    transform.position = hitInfo.point + new Vector3(0, 0.5f, 0);
        //    lr.positionCount = 0;
        //}
        #endregion

        #region 텔레포트 이동 방식(곡선)

        if (MTVS_Input.Get(MTVS_Input.Button.Jump, MTVS_Input.Controller.RTouch))
        {
            Vector3 startPos = MTVS_Input.RightHandPosition;
            Vector3 direction = MTVS_Input.RightHandTransform.forward + MTVS_Input.RightHandTransform.up;
            curvedList.Clear();
            curvedList.Add(startPos);

            Vector3 maxHeight = startPos + movePower * direction * 0.713f + 0.5f * Physics.gravity * 0.713f * 0.713f;
            Debug.DrawLine(maxHeight, maxHeight + Vector3.up);

            for (int i = 0; i < 50; i++)
            {
                float timeInterval = 0.1f * (i + 1);

                // p = p0 + vt - 0.5 * g * t * t
                Vector3 predict = startPos + movePower* direction * timeInterval + 0.5f * Physics.gravity * timeInterval * timeInterval;


                Ray ray = new Ray(curvedList[i], predict - curvedList[i]);
                RaycastHit hitInfo;
                float distance = Vector3.Distance(curvedList[i], predict);

                if(Physics.Raycast(ray, out hitInfo, distance))
                {
                    curvedList.Add(hitInfo.point);
                    break;
                }

                curvedList.Add(predict);
            }

            lr.positionCount = curvedList.Count;
            lr.SetPositions(curvedList.ToArray());
        }
        else if(MTVS_Input.GetUp(MTVS_Input.Button.Jump, MTVS_Input.Controller.RTouch))
        {
            transform.position = curvedList[curvedList.Count - 1] + new Vector3(0, 0.5f, 0);
            lr.positionCount = 0;
        }


        #endregion
    }
}
