using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRoutine{
    MonoBehaviour context;
    Coroutine routine;
    Coroutine waiter;

    public MonoRoutine(MonoBehaviour context){
        this.context = context;
    }

    public void Start(IEnumerator routine){
        Stop();
        this.routine = context.StartCoroutine(routine);
        waiter = context.StartCoroutine(Wait());
    }

    IEnumerator Wait(){
        yield return routine;
        if (waiter != null) context.StopCoroutine(waiter);
        routine = null;
        waiter = null;
    }

    public void Stop(){
        if (routine != null) context.StopCoroutine(routine);
        if (waiter != null) context.StopCoroutine(waiter);
        routine = null;
        waiter = null;
    }

    public bool isRunning(){
        return routine != null;
    }
}
