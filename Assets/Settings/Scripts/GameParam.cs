using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/Gagme Setting", fileName = "GameParam")]
public class GameParam : ScriptableObject
{
    public float Gravity;
    public Vector3 CameraAddPos;
}
