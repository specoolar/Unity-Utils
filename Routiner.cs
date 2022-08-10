using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoutinerBase{
    protected MonoBehaviour context;
    protected IEnumerator routine;

    public void Stop(){
        if (routine != null) context.StopCoroutine(routine);
        routine = null;
    }
    protected IEnumerator Handle(){
        yield return routine;
        routine = null;
    }
    
    public bool isRunning(){
        return routine != null;
    }
}

public class Routiner: RoutinerBase
{
    Func<IEnumerator> action;

    public Routiner(MonoBehaviour context, Func<IEnumerator> action){
        this.context = context;
        this.action = action;
    }

    public void Start(){
        Stop();
        routine = action();
        context.StartCoroutine(Handle());
    }
}

public class Routiner<T> : RoutinerBase
{
    Func<T, IEnumerator> action;

    public Routiner(MonoBehaviour context, Func<T, IEnumerator> action){
        this.context = context;
        this.action = action;
    }

    public void Start(T t){
        Stop();
        routine = action(t);
        context.StartCoroutine(Handle());
    }
}

public class Routiner<T0, T1> : RoutinerBase
{
    Func<T0, T1, IEnumerator> action;

    public Routiner(MonoBehaviour context, Func<T0, T1, IEnumerator> action){
        this.context = context;
        this.action = action;
    }

    public void Start(T0 t0, T1 t1){
        Stop();
        routine = action(t0, t1);
        context.StartCoroutine(Handle());
    }
}

public class Routiner<T0, T1, T2> : RoutinerBase
{
    Func<T0, T1, T2, IEnumerator> action;

    public Routiner(MonoBehaviour context, Func<T0, T1, T2, IEnumerator> action){
        this.context = context;
        this.action = action;
    }

    public void Start(T0 t0, T1 t1, T2 t2){
        Stop();
        routine = action(t0, t1, t2);
        context.StartCoroutine(Handle());
    }
}


public class Routiner<T0, T1, T2, T3> : RoutinerBase
{
    Func<T0, T1, T2, T3, IEnumerator> action;

    public Routiner(MonoBehaviour context, Func<T0, T1, T2, T3, IEnumerator> action){
        this.context = context;
        this.action = action;
    }

    public void Start(T0 t0, T1 t1, T2 t2, T3 t3){
        Stop();
        routine = action(t0, t1, t2, t3);
        context.StartCoroutine(Handle());
    }
}
