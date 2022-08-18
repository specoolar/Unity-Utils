using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRoutine{
    MonoBehaviour context;
    List<MonoRoutine> routines;
    public bool IsRunning {
        get {
            foreach (var routine in routines)
                if (routine.IsRunning) return true;
            return false;
        }
    }
    public MultiRoutine(MonoBehaviour context) {
        this.context = context;
        routines = new List<MonoRoutine>();
    }

    public MonoRoutine Start(IEnumerator _routine, bool additive = false) {
        if(!additive)
            Stop();

        MonoRoutine mr = new MonoRoutine(context);
        routines.Add(mr);
        mr.Start(_routine);
        return mr;
    }
    public void Stop() {
        foreach (MonoRoutine routine in routines)
            routine.Stop();
        routines.Clear();
    }
}
