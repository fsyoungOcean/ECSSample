using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotationCubeSpeedSystem : ComponentSystem
{
    struct EnemyGroup
    {
        public readonly int Length;
        public ComponentArray<RotationCube> rotations;
        public ComponentArray<RotationCubeSpeed> moveSpeeds;
    }
    [Inject]
    EnemyGroup enemies;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;

        for (int i = 0; i < enemies.Length; i++)
        {
            RotationCube rotation = enemies.rotations[i];
            RotationCubeSpeed speed = enemies.moveSpeeds[i];

            var asixangle = quaternion.axisAngle(math.up(), speed.Value * dt);
            rotation.Value = math.mul(math.normalize(rotation.Value), asixangle);

        }
    }
}
