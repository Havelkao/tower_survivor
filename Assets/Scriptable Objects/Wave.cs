using System.Collections.Generic;
using UnityEngine;
using static Types;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class Wave : ScriptableObject
{
    public WaveEnemy[] enemies;
    public GameObject boss;
}
