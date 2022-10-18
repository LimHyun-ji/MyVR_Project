using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Button_Interaction : MonoBehaviour
{
    public Text logText;
    public delegate void ClickEventDelegate(string msg);
    public delegate void TestDelegate(int msg);

    ClickEventDelegate onClicked;
    TestDelegate test;

    //public UnityAction<string> ClickEventDelegate;

    private void Start()
    {
        onClicked += OnClickEvent;
        test += TestFunc;
        test(2);
        test(3);

        List<string> funcProcedure = new List<string>();

        foreach(string funcs in funcProcedure)
        {
            BroadcastMessage(funcs, 3);
        }
    }

    public void OnClickEvent(string msg)
    {
        logText.text = msg;
        Physics.BoxCast
    }

    public void TestFunc(int num)
    {
        print(num);
    }
}
