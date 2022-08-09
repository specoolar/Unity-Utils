using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoutinerBase{
    protected MonoBehaviour context;
    protected Coroutine routine;

    public void Stop(){
        if (routine != null) context.StopCoroutine(routine);
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

    public void Start(bool forceRestart = true){
        if (routine != null){
            if (forceRestart)
                context.StopCoroutine(routine);
            else
                return;
        }
        routine = context.StartCoroutine(Handle());
    }

    IEnumerator Handle(){
        yield return action();
        routine = null;
    }
}

public class Routiner<T> : RoutinerBase
{
    Func<T, IEnumerator> action;

    public Routiner(MonoBehaviour context, Func<T, IEnumerator> action){
        this.context = context;
        this.action = action;
    }

    public void Start(T t, bool forceRestart = true){
        if (routine != null){
            if (forceRestart)
                context.StopCoroutine(routine);
            else
                return;
        }
        routine = context.StartCoroutine(Handle(t));
    }

    IEnumerator Handle(T t){
        yield return action(t);
        routine = null;
    }
}

public class Routiner<T0, T1> : RoutinerBase
{
    Func<T0, T1, IEnumerator> action;

    public Routiner(MonoBehaviour context, Func<T0, T1, IEnumerator> action){
        this.context = context;
        this.action = action;
    }

    public void Start(T0 t0, T1 t1, bool forceRestart = true){
        if (routine != null){
            if (forceRestart)
                context.StopCoroutine(routine);
            else
                return;
        }
        routine = context.StartCoroutine(Handle(t0, t1));
    }

    IEnumerator Handle(T0 t0, T1 t1){
        yield return action(t0, t1);
        routine = null;
    }
}

public class Routiner<T0, T1, T2> : RoutinerBase
{
    Func<T0, T1, T2, IEnumerator> action;

    public Routiner(MonoBehaviour context, Func<T0, T1, T2, IEnumerator> action){
        this.context = context;
        this.action = action;
    }

    public void Start(T0 t0, T1 t1, T2 t2, bool forceRestart = true){
        if (routine != null){
            if (forceRestart)
                context.StopCoroutine(routine);
            else
                return;
        }
        routine = context.StartCoroutine(Handle(t0, t1, t2));
    }

    IEnumerator Handle(T0 t0, T1 t1, T2 t2){
        yield return action(t0, t1, t2);
        routine = null;
    }
}


public class Routiner<T0, T1, T2, T3> : RoutinerBase
{
    Func<T0, T1, T2, T3, IEnumerator> action;

    public Routiner(MonoBehaviour context, Func<T0, T1, T2, T3, IEnumerator> action){
        this.context = context;
        this.action = action;
    }

    public void Start(T0 t0, T1 t1, T2 t2, T3 t3, bool forceRestart = true){
        if (routine != null){
            if (forceRestart)
                context.StopCoroutine(routine);
            else
                return;
        }
        routine = context.StartCoroutine(Handle(t0, t1, t2, t3));
    }

    IEnumerator Handle(T0 t0, T1 t1, T2 t2, T3 t3){
        yield return action(t0, t1, t2, t3);
        routine = null;
    }
}
