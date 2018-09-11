using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SyncECSTransformSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        [ReadOnly] public ComponentDataArray<Rotation> rotation;
        public ComponentArray<Transform> Output;
    }

    [Inject]
    Data enemies;

    protected override void OnUpdate()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies.Output[i].rotation = enemies.rotation[i].Value;
        }
    }

    //public struct Data
    //{

    //    [ReadOnly] public RotationCube rotation;
    //    public Transform Output;
    //}

    //protected override void OnUpdate()
    //{
    //    foreach (var entity in GetEntities<Data>())
    //    {
    //        entity.Output.rotation = entity.rotation.Value;
    //    }
    //}
}
