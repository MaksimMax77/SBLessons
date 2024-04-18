using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Input
{
    [GenerateAuthoringComponent]
    public struct InputData : IComponentData
    {
        [HideInInspector] public float2 moveStickDirection;
        [HideInInspector] public bool isShootButtonClick;
    }
}
