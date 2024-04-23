using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Collision
{
    public class Test : MonoBehaviour
    {
       [SerializeField] private GameObject _impact;
       private InputActions _inputActions;

       private void Awake()
       {
           _inputActions = new InputActions();
           _inputActions.Enable();
           _inputActions.Character.Shoot.started += CreateImpact;
       }

       private void OnDestroy()
       {
           _inputActions.Character.Shoot.started -= CreateImpact;
       }

       private void CreateImpact(InputAction.CallbackContext ctx)
       {
           if (!Physics.Raycast(transform.position, transform.forward, out var hit, 100))
           {
               return;
           }

           var contact = hit.point;
           var rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
           ResetImpactObj();
           Instantiate(_impact, contact, rotation);
       }

       private void ResetImpactObj()
       {
           _impact.gameObject.SetActive(false);
           _impact.gameObject.SetActive(true);
       }
    }
}
