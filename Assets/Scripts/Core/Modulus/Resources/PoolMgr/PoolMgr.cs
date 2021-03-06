﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PoolMgr
{
    private static PoolMgr instance;
    public static PoolMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PoolMgr();
            }
            return instance;
        }
    }
    private Transform poolRoot = null;
    public Transform PoolRoot
    {
        get
        {
            if (poolRoot == null)
            {
                GameObject go = new GameObject("_poolRoot");
                poolRoot = go.transform;
                poolRoot.transform.position = new Vector3(10000, 10000, -1000);
                GameObject.DontDestroyOnLoad(go);
            }
            return poolRoot;
        }
    }

    public void initialize()
    {
        //检测一次时间池子
        TimerMgr.addEveryMillHandler(checkUseTime, 60000);
        //TimerMgr.addSecHandler(1, null, (count) =>
        //{
        //    disposeAll();
        //    Debug.LogWarning("CS Pool释放所有清理完成");
        //    AssetMgr.clearAll();
        //    Debug.LogWarning("CS AssetMgr.clearAll()清理完成");
        //}, 30);
        //TimerMgr.addEveryMillHandler((count)=> { AssetMgr.clearAll(); },150000 );
    }

    public Dictionary<string, BasePool> pools = new Dictionary<string, BasePool>();

    //从对象池取出
    public void getObj(string url, Action<GameObject> callBack, E_PoolType pType)
    {
        if (!pools.ContainsKey(url))
        {
            BasePool bp = new BasePool(url, pType);
            pools.Add(url, bp);
        }
        pools[url].getObj(callBack);
    }

    //放回
    public void saveObj(GameObject go)
    {
        PoolObj po = go.GetComponent<PoolObj>();
        bool isDestroy = po == null || !pools.ContainsKey(po.url);
        if (isDestroy)
        {
            GameObject.Destroy(go);
        }
        else
        {
            if (pools.ContainsKey(po.url))
                pools[po.url].saveObj(po);
            else
                GameObject.Destroy(go);
        }
    }


    public void checkUseTime(int count)
    {
        disposeUseTime();
    }

    //释放时间池子
    public void disposeUseTime()
    {
        List<string> keys = new List<string>();
        var ier = pools.GetEnumerator();
        while (ier.MoveNext())
        {
            if (!ier.Current.Value.IsAlive)
            {
                keys.Add(ier.Current.Key);
            }
        }
        for (int i = 0; i < keys.Count; i++)
        {
            if (pools.ContainsKey(keys[i]))
            {
                BasePool bp = pools[keys[i]];
                pools.Remove(keys[i]);
                bp.onDispose();
            }
        }

    }
    //释放关卡池子
    public void disposeLevel()
    {
        List<string> keys = new List<string>();
        var ier = pools.GetEnumerator();
        while (ier.MoveNext())
        {
            if (ier.Current.Value.PType == E_PoolType.Level)
            {
                keys.Add(ier.Current.Key);
            }
        }
        for (int i = 0; i < keys.Count; i++)
        {
            if (pools.ContainsKey(keys[i]))
            {
                pools.Remove(keys[i]);
            }
        }
        keys.Clear();
    }
    //释放全局池子
    public void disposeGlobal()
    {

    }

    //释放所有池子
    public void disposeAll()
    {
        List<string> keys = new List<string>();
        var ier = pools.GetEnumerator();
        while (ier.MoveNext())
        {
            ier.Current.Value.onDispose();
        }
        pools.Clear();
    }
}

