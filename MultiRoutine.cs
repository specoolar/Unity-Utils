using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRoutine{
    MonoBehaviour context;
    LinkedList<MonoRoutine> routines;
    public bool IsRunning {
        get {
            foreach (var routine in routines)
                if (routine.IsRunning) return true;
            return false;
        }
    }
    public MultiRoutine(MonoBehaviour context) {
        this.context = context;
        routines = new LinkedList<MonoRoutine>();
    }

    public MonoRoutine StartNew(IEnumerator _routine, Action onStop = null) {
        Stop();
        MonoRoutine mr = new MonoRoutine(context);
        mr.AddToGroup(this, routines.AddLast(mr));
        mr.Start(_routine, onStop);
        return mr;
    }
    public MonoRoutine StartParallel(IEnumerator _routine, Action onStop = null) {
        MonoRoutine mr = new MonoRoutine(context);
        mr.AddToGroup(this, routines.AddLast(mr));
        mr.Start(_routine, onStop);
        return mr;
    }

    public void AddRoutine(MonoRoutine routine) {
        routine.AddToGroup(this, routines.AddLast(routine));
    }
    public void RemoveRoutine(MonoRoutine routine) {
        routines.Remove(routine.GroupNode);
    }

    public void Stop() {
        while (routines.Count>0) {
            routines.First.Value.Stop();
        }
        routines.Clear();
    }
}
