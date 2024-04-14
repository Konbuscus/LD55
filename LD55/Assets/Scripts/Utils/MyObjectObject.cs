using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyObjectObject<T1, T2>
{
    public T1 obj1;
    public T2 obj2;

    public MyObjectObject(T1 obj1, T2 obj2)
    {
        this.obj1 = obj1;
        this.obj2 = obj2;
    }

    public static Dictionary<T1, T2> ListToDict(List<MyObjectObject<T1, T2>> list)
    {
        Dictionary<T1, T2> dict = new Dictionary<T1, T2>();
        foreach(MyObjectObject<T1, T2> e in list)
        {
            dict.Add(e.obj1, e.obj2);
        }
        return dict;
    }
}
