using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SyncTransformSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        [ReadOnly] public ComponentArray<RotationCube> rotation;
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
