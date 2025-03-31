using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandCalculator : MonoBehaviour
{
    public static HandCalculator Singleton { get; private set; }
    void Awake()
    {
        if (Singleton != null && Singleton != this) Destroy(this);
        else Singleton = this;
    }
    
    public int calculate(List<Card> hand){
        return 0;
    }
}
