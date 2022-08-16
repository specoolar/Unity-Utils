using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRoutine{
    MonoBehaviour context;
    IEnumerator routine;
    Coroutine processor;
    bool _isRunning = false;

    public bool isRunning{
        get {
            return _isRunning;
        }
    }

    public MonoRoutine(MonoBehaviour context){
        this.context = context;
    }

    public void Start(IEnumerator routine) {
        Stop();
        _isRunning = true;
        this.routine = routine;
        processor = context.StartCoroutine(Wait());
        if (!_isRunning) {
            context.StopCoroutine(processor);
            processor = null;
        }
    }

    IEnumerator Wait() {
        yield return routine;
        _isRunning = false;
    }

    public void Stop() {
        if (processor != null) context.StopCoroutine(processor);
        processor = null;
        if (routine != null) context.StopCoroutine(routine);
        routine = null;
        _isRunning = false;
    }
}
