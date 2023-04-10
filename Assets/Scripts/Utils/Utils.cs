using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    public static System.Random UtilsRandom = new System.Random();
    public static string logf(this object o) => o == null ? "NULL" : o.ToString();
    public static void Shuffle<T>(List<T> list, int seed=-1) {
        if(seed==-1) {
            seed = Random.Range(0, 100000);
        }
        Random.InitState(seed);
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    static Dictionary<string, string> lastIdMessage = new Dictionary<string, string>();
    public static void logd(string id, string message, bool ignoreDuplicates=false) {
        if(ignoreDuplicates && lastIdMessage.ContainsKey(id) && lastIdMessage[id]==message) {
            return;
        }
        Debug.Log(id+"::"+message);
        if(lastIdMessage.ContainsKey(id)) {
            lastIdMessage[id] = message;
        } else {
            lastIdMessage.Add(id, message);
        }
    }
    
    public static void logw(string id, string message, bool ignoreDuplicates=false) {
        if(ignoreDuplicates && lastIdMessage.ContainsKey(id) && lastIdMessage[id]==message) {
            return;
        }
        Debug.LogWarning(id+"::"+message);
        if(lastIdMessage.ContainsKey(id)) {
            lastIdMessage[id] = message;
        } else {
            lastIdMessage.Add(id, message);
        }
    }
    
    public static void loge(string id=null, string message=null, bool ignoreDuplicates=false) {
        if(ignoreDuplicates && lastIdMessage.ContainsKey(id) && lastIdMessage[id]==message) {
            return;
        }
        Debug.LogError(id+"::"+ message);
        if(lastIdMessage.ContainsKey(id)) {
            lastIdMessage[id] = message;
        } else {
            lastIdMessage.Add(id, message);
        }

    }
    public static void logt(string id=null, string message=null) {
        return;
    }
}
