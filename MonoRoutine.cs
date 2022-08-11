using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRoutine{
    MonoBehaviour context;
    IEnumerator routine;
    Coroutine waiter;

    public MonoRoutine(MonoBehaviour context){
        this.context = context;
    }

    public void Start(IEnumerator routine){
        Stop();
        this.routine = routine;
        waiter = context.StartCoroutine(Wait());
    }

    IEnumerator Wait(){
        yield return routine;
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
