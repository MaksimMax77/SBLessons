using UnityEngine;

namespace Code.ComponentActions.CollisionActions
{
    public class DisableOnCollision : CollisionAction
    {
       [SerializeField] private GameObject _gameObject;

        public override void Execute()
        {
            _gameObject.SetActive(false);
        }

        public override void ResetAction()
        {
            _gameObject.SetActive(true);
            base.ResetAction();
        }
    }
}
