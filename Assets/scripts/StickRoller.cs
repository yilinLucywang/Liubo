using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class StickRoller
{
    public RollStickEvent onStickRoll;
    public UnityEvent onStickRollerEnable;
    public UnityEvent onStickRollerDisable;
    private static StickRoller Instance;
    private bool isActive = true;

    private Random rand;

    public static StickRoller GetInstance()
    {
        if (Instance == null)
        {
            Instance = new StickRoller();
        }
        return Instance;
    }

    public StickRoller()
    {
        rand = new Random();
        onStickRoll = new RollStickEvent();
        onStickRollerEnable = new UnityEvent();
        onStickRollerDisable = new UnityEvent();
    }
    

    public (int, int) RollSticks()
    {
        if (!isActive) return (0, 0);
        var rand = this.rand.Next();
        var val1 = rand & 7;
        var val2 = (rand >> 3) & 7;
        //return (val1, val2);
        onStickRoll?.Invoke(val1, val2);
        var ret1 = countBits(val1);
        var ret2 = countBits(val2);
        return (ret1 == 0 ? 4 : ret1, ret2 == 0 ? 4 : ret2);
    }

    private void GenerateNumbers()
    {
        int result = 0;
        var rand = this.rand.Next();
        
    }

    private int countBits(int n)
    {
        int ret = 0;
        while (n > 0)
        {
            ret += n & 1;
            n >>= 1;
        }
        return ret;
    }

    public void SetActive(bool isAtive)
    {
        this.isActive = isAtive;
        if (isActive)
        {
            onStickRollerEnable.Invoke();
        }
        else
        {
            onStickRollerDisable.Invoke();
        }
    }
}

[Serializable]
public class RollStickEvent : UnityEvent<int, int > {}