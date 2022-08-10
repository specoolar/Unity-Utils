using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRoutine{
    MonoBehaviour context;
    IEnumerator routine;

    public MonoRoutine(MonoBehaviour context){
        this.context = context;
    }

    public void Start(IEnumerator routine){
        Stop();
        this.routine = routine;
        context.StartCoroutine(Handle());
    }

    public void Stop(){
        if (routine != null) context.StopCoroutine(routine);
        routine = null;
    }

    public bool isRunning(){
        return routine != null;
    }

    IEnumerator Handle(){
        yield return routine;
        routine = null;
    }
}
