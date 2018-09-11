using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct StopTag : IComponentData
{
}

public class StopComponent : ComponentDataWrapper<StopTag> { }
