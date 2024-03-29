﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class NodeModel // 树上的所有数据来源
{
    public NodeModel(TreeNode rNode)
    {
        BaseNode = rNode;
    }
    public NodeModel() { }

    private TreeNode BaseNode;

    private Dictionary<string, int> Integer = new Dictionary<string, int>();
    private Dictionary<string, float> Float = new Dictionary<string, float>();
    private Dictionary<string, bool> Boolean = new Dictionary<string, bool>();
    private Dictionary<string, string> String = new Dictionary<string, string>();
    private Dictionary<string, object> Obj = new Dictionary<string, object>();

    private Dictionary<string, bool> TagChanged = new Dictionary<string, bool>();

    private Dictionary<string, bool> TagIsPlayed = new Dictionary<string, bool>();

    public void SetValue<T>(string key, T value)
    {
        if (TagChanged.ContainsKey(key))
            TagChanged[key] = true;
        else
            TagChanged.Add(key, true);

        if (TagIsPlayed.ContainsKey(key))
            TagIsPlayed[key] = false;
        else
            TagIsPlayed.Add(key, false);

        if (typeof(T) == typeof(int))
        {
            int i = (int)(object)value;
            if (Integer.ContainsKey(key))
                Integer[key] = i;
            else
                Integer.Add(key, i);
        }
        else if (typeof(T) == typeof(float))
        {
            float f = (float)(object)value;
            if (Float.ContainsKey(key))
                Float[key] = f;
            else
                Float.Add(key, f);
        }
        else if (typeof(T) == typeof(bool))
        {
            bool b = (bool)(object)value;
            if (Boolean.ContainsKey(key))
                Boolean[key] = b;
            else
                Boolean.Add(key, b);
        }
        else if (typeof(T) == typeof(string))
        {
            string s = (string)(object)value;
            if (String.ContainsKey(key))
                String[key] = s;
            else
                String.Add(key, s);
        }
        else
        {
            object o = (int)(object)value;
            if (Obj.ContainsKey(key))
                Obj[key] = o;
            else
                Obj.Add(key, o);
        }

    }

    public bool TryGetValue<T>(string key, out T obj)
    {
        bool isSuccess;
        if (typeof(T) == typeof(int))
        {
            int i = 0;
            isSuccess = Integer.TryGetValue(key, out i);
            obj = (T)(object)i;
        }
        else if (typeof(T) == typeof(float))
        {
            float f = 0f;
            isSuccess = Float.TryGetValue(key, out f);
            obj = (T)(object)f;
        }
        else if (typeof(T) == typeof(bool))
        {
            bool b = false;
            isSuccess = Boolean.TryGetValue(key, out b);
            obj = (T)(object)b;
        }
        else if (typeof(T) == typeof(string))
        {
            string s = "";
            isSuccess = String.TryGetValue(key, out s);
            obj = (T)(object)s;
        }
        else
        {
            object o;
            isSuccess = Obj.TryGetValue(key, out o);
            obj = (T)o;
        }
        if (!isSuccess)
        {
            obj = default(T);
            this.SetValue(key, obj);
        }
        return isSuccess;
    }

    public void SetTagIsPlayed(string tag)
    {
        if (TagIsPlayed.ContainsKey(tag))
            TagIsPlayed[tag] = true;
        else
            TagIsPlayed.Add(tag, true);
    }

    public bool CanPlay(string rTag)
    {
        bool b;
        if (TagIsPlayed.TryGetValue(rTag, out b))
            return !b;
        else
        {
            Debug.Log("不存在此tag:" + rTag);
            return true;
        }
    }

    public void OnUpdate()
    {
        var r = new List<string>();
        foreach (var tag in TagChanged)
        {
            r.Add(tag.Key);
        }
        foreach (var tag in r)
        {
            string key = tag;
            if (TagIsPlayed[key] && TagChanged[key])
            {
                TagChanged[key] = false;
            }
        }
    }
}
