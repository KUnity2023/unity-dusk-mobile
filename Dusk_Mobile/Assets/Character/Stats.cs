using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField] private int baseStat;
    // Start is called before the first frame update
    public int GetStat()
    {
        return baseStat;
    }
    public void SetStat(int newStat)
    {
        baseStat = newStat;
    }
}
