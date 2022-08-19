using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRoutine{
    MonoBehaviour context;
    IEnumerator routine;
    IEnumerator processor;
    Action onStop;
    private bool isRunning = false;

    MultiRoutine group = null;
    public LinkedListNode<MonoRoutine> GroupNode {
        get => node;
    }
    LinkedListNode<MonoRoutine> node = null;
    public bool IsRunning{
        get => isRunning;
    }
    public MonoRoutine(MonoBehaviour context) => this.context = context;

    public void AddToGroup(MultiRoutine group, LinkedListNode<MonoRoutine> node) {
        this.group = group;
        this.node = node;
    }

    public void Start(IEnumerator _routine, Action onStop = null) {
        _stop();
        this.onStop?.Invoke();
        routine = _routine;
        processor = _wait();
        this.onStop = onStop;
        isRunning = true;
        context.StartCoroutine(_wait());
    }
    public void Stop() {
        _stop();
        processor = null;
        routine = null;
        isRunning = false;
        UnlinkGroup();

        onStop?.Invoke();
        onStop = null;
    }

    private IEnumerator _wait() {
        yield return routine;
        isRunning = false;
        UnlinkGroup();

        onStop?.Invoke();
        onStop = null;
    }

    void UnlinkGroup() {
        if (group != null) {
            group.RemoveRoutine(this);
            group = null;
        }
    }

    private void _stop() {
        if (processor != null) context.StopCoroutine(processor);
        if (routine != null) context.StopCoroutine(routine);
    }
}
