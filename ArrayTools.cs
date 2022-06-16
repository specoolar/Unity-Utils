using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayTools{
    public static bool IsRange<T>(this T[] arr, int i){
        return i >= 0 && i < arr.Length;
    }
    public static bool IsRange<T>(this List<T> arr, int i){
        return i >= 0 && i < arr.Count;
    }
    public static bool IsRange<T>(this LinkedList<T> arr, int i){
        return i >= 0 && i < arr.Count;
    }
    
    public static T GetRandom<T>(this T[] arr){
        return arr[Random.Range(0, arr.Length)];
    }
    public static T GetRandom<T>(this List<T> arr){
        return arr[Random.Range(0, arr.Count)];
    }
    
    public static T Last<T>(this T[] arr){
        return arr[arr.Length - 1];
    }
    public static T Last<T>(this List<T> arr){
        return arr[arr.Count - 1];
    }
}
