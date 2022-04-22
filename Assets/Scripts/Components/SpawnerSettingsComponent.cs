using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct SpawnerSettingsComponent : IComponentData {
    public int countEntities;
    public float bordersSpawner;
    public uint randomSeed;

}

