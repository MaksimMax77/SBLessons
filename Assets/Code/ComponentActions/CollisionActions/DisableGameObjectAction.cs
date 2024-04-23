using UnityEngine;

namespace Code.ComponentActions.CollisionActions
{
    public class DisableGameObjectAction : CollisionAction
    {
        [SerializeField] private GameObject _objToDisable;
        public override void Execute()
        {
            _objToDisable.SetActive(false);
        }
    }
}
