using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

/// <summary> 
/// 一个简单的结构体（ComponentData） 
/// </summary> 
[Serializable]
public struct RotationSpeed : IComponentData
{
    public float Value;
}

public class RotationSpeedComponent : ComponentDataWrapper<RotationSpeed> { }
