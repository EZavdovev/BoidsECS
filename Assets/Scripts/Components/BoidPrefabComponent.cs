using Unity.Entities;

[GenerateAuthoringComponent]
public struct BoidPrefabComponent : IComponentData {
    public Entity enemyPrefab;
}
