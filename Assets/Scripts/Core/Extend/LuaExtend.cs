﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public static class LuaExtend
{
    #region 数学库相关
    //uid
    public static long getLUID()
    {
        return MathUtils.UniqueLID;
    }
    //uid
    public static int getSUID()
    {
        return MathUtils.UniqueID;
    }
    #endregion

    #region 资源加载 管理相关
    //加载资源
    public static void loadObj(string url, Action<GameObject> callBack)
    {
        ResMgr.Instance.getObj(url, callBack);
    }
    //销毁资源
    public static void destroyObj(GameObject obj)
    {
        ResMgr.Instance.desObj(obj);
    }
    #endregion

    #region 场景相关
    //场景加载
    public static void loadScene(string level, Action<float> progress = null)
    {
        SceneMgr.loadScene(level, progress);
    }
    #endregion

    #region 计时器相关
    public static long addMillHandler(int endCount, Action<int> eHandler, Action<int> cHandler = null, int interval = 1)
    {
        return TimerMgr.addMillHandler(endCount, eHandler, cHandler, interval);
    }
    public static long addSecHandler(int endCount, Action<int> eHandler, Action<int> cHandler = null, int interval = 1)
    {
        return TimerMgr.addSecHandler(endCount, eHandler, cHandler, interval);
    }
    public static long addMinHandler(int endCount, Action<int> eHandler, Action<int> cHandler = null, int interval = 1)
    {
        return TimerMgr.addMinHandler(endCount, eHandler, cHandler, interval);
    }
    public static long addEveryMillHandler(Action<int> eHandler, int interval = 1)
    {
        return TimerMgr.addEveryMillHandler(eHandler, interval);
    }
    public static void removeTimer(long uid)
    {
 
    }
    #endregion
}