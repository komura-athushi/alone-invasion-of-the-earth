using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public string Id;
    public int Hp;
    public int Attack;
    public int Defense;
    public int Exp;
}

[CreateAssetMenu(menuName = "ScriptableObject/Enemy Setting", fileName = "EnemyParam")]
public class EnemyParam : ScriptableObject
{
    public List<EnemyData> DataList;
}
