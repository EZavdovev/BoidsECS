using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
[UpdateAfter(typeof(BoidsSpawnerSystem))]
[UpdateBefore(typeof(TransformSystem))]
public class BoidMoveSystem : SystemBase {

    private EntityQuery query;
    protected override void OnCreate() {
        query = GetEntityQuery(typeof(Translation), typeof(BoidVelocityComponent));

    }
    protected override void OnUpdate() {
        var boidsCount = query.CalculateEntityCount();

        NativeArray<Entity> boidArray = query.ToEntityArray(Allocator.Temp);
        NativeArray<Translation> transformsArray = query.ToComponentDataArray<Translation>(Allocator.Temp);
        NativeArray<BoidVelocityComponent> velocitiesArray = query.ToComponentDataArray<BoidVelocityComponent>(Allocator.Temp);

        var boids = boidArray.AsReadOnly();
        var transforms = transformsArray.AsReadOnly();
        var velocities = velocitiesArray.AsReadOnly();

        Entities.ForEach((Entity boid, ref BoidVelocityComponent boidVelocity, in BoidMoveSettingsComponent boidSettings, in Translation pos) => {
            var separation = float3.zero;
            var cohesion = pos.Value;
            var alighnment = boidVelocity.velocity;
            int boidSepCount = 0;
            // && boidSepCount < boidSettings.maxNeighbours
            for (int i = 0; i < boidsCount; i++) {
                var dist = math.distance(pos.Value, transforms[i].Value);
                if (boid != boids[i] && dist < boidSettings.radius) {
                    separation += (pos.Value - transforms[i].Value) / dist;
                    cohesion += transforms[i].Value;
                    alighnment += velocities[i].velocity;
                    boidSepCount++;
                }
            }

            if (boidSepCount > 0) {
                separation /= boidSepCount;
                cohesion /= (boidSepCount + 1);
                alighnment /= (boidSepCount + 1);
                boidVelocity.velocity += math.normalizesafe(separation) * boidSettings.separationCoeff
                + math.normalizesafe(cohesion - pos.Value) * boidSettings.cohesionCoeff
                + math.normalizesafe(alighnment) * boidSettings.alignmentCoeff;
            }
        }
        ).Run();

        boidArray.Dispose();
        transformsArray.Dispose();
        velocitiesArray.Dispose();
    }

}