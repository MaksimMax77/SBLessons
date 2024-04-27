using UnityEngine;

namespace Code.ComponentActions
{
    public abstract class ComponentAction : MonoBehaviour
    {
        public abstract void Execute();

        public virtual void ResetAction ()
        {
            
        }
    }
}
