using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRoutine{
    MonoBehaviour context;
    Coroutine coroutine;

    public MonoRoutine(MonoBehaviour context){
        this.context = context;
    }

    public void Start(IEnumerator routine){
        Stop();
        coroutine = context.StartCoroutine(Handle(routine));
    }

    public void Stop(){
        if (coroutine != null){
            context.StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public bool isRunning(){
        return coroutine != null;
    }

    IEnumerator Handle(IEnumerator routine){
        yield return routine;
        coroutine = null;
    }
}
