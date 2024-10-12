using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/UI Setting", fileName = "UIParam")]
public class UIParam : ScriptableObject
{
    public float heartPaddingRatio;
    public float skillGaugePaddingRatio;
}
