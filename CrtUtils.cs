using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrtUtils
{
    public static void InvokeAfter(MonoBehaviour context, Action action, float time, bool unscaledTime = false){
        context.StartCoroutine(_invokeR(action, time, unscaledTime));
    }

    static IEnumerator _invokeR(Action action, float time, bool unscaledTime){
        if(unscaledTime)
            yield return new WaitForSecondsRealtime(time);
        else
            yield return new WaitForSeconds(time);
        action();
    }
}