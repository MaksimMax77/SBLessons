using UnityEngine;

namespace Code.ComponentActions.AbilityActions
{
    public class RigidBodyStopAction : ComponentAction
    {
        [SerializeField] private Rigidbody _rigidbody;
        public override void Execute()
        {
            _rigidbody.isKinematic = true;
        }

        public override void ResetAction()
        {
            _rigidbody.isKinematic = false;

            base.ResetAction();
        }
    }
}
