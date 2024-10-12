using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/Player Setting", fileName = "PlayerParam")]
public class PlayerParam : ScriptableObject
{
    public float MoveSpeed;
    public float BigJumpPower;
    public float SmallJumpPower;
    public float SmallJumpTime;
    public float ReduceMoveSpeedForJumping;
    public float AirResistanceForJumping;
    public string Id;
    public int Hp;
    public int[] CurrentSkillGauge;
    public int[] RequiredSkillGauge;
    public int MaxSkillNumber;
    public int Attack;
    public int Defense;
    public float BotDistance;
}
