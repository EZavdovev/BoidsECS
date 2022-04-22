using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class BoidsSpawnerSystem : SystemBase {
    private BeginSimulationEntityCommandBufferSystem _commandBufferSystem;
    protected override void OnCreate() {
        _commandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    protected override void OnStartRunning() {
        var commandBuffer = _commandBufferSystem.CreateCommandBuffer();
        Entities.ForEach((in BoidPrefabComponent enemyPrefab, in SpawnerSettingsComponent settings) => {
            var random = new Random(settings.randomSeed);
            for (int i = 0; i < settings.countEntities; i++) {
                var enemy = commandBuffer.Instantiate(enemyPrefab.enemyPrefab);

                commandBuffer.SetComponent(enemy,
                    new Translation {
                        Value = new float3(random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner),
                    random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner),
                    random.NextFloat(-settings.bordersSpawner, settings.bordersSpawner))
                    });
            }
        }).Run();

    }
    protected override void OnUpdate() {


    }
}
