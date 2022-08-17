using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRoutine{
    MonoBehaviour context;
    IEnumerator routine;
    IEnumerator processor;
    private bool isRunning = false;
    public bool IsRunning{
        get => isRunning;
    }
    public MonoRoutine(MonoBehaviour context) => this.context = context;

    public void Start(IEnumerator _routine) {
        _stop();
        routine = _routine;
        processor = _wait();
        isRunning = true;
        context.StartCoroutine(_wait());
    }
    public void Stop() {
        _stop();
        processor = null;
        routine = null;
        isRunning = false;
    }

    private IEnumerator _wait() {
        yield return routine;
        isRunning = false;
    }

    private void _stop() {
        if (processor != null) context.StopCoroutine(processor);
        if (routine != null) context.StopCoroutine(routine);
    }
}
