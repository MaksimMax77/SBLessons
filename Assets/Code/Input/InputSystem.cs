using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Input
{
    public class InputSystem : ComponentSystem
    {
        private EntityQuery _inputDataQuery;
        private InputActions _inputActions;
        private float2 _moveInput;
        private bool _isShootButtonClick;

        protected override void OnCreate()
        {
            _inputDataQuery =  GetEntityQuery(ComponentType.ReadOnly<InputData>());
            
            _inputActions = new InputActions();
            _inputActions.Enable();
            
            _inputActions.Character.Move.started += OnMoveInputDirectionChange;
            _inputActions.Character.Move.performed += OnMoveInputDirectionChange;
            _inputActions.Character.Move.canceled += OnMoveInputDirectionChange;
        }

        protected override void OnUpdate()
        {
            Entities.With(_inputDataQuery).ForEach((Entity entity, ref InputData inputData) =>
            {
                inputData.moveStickDirection = _moveInput;
                inputData.isShootButtonClick =  _inputActions.Character.Shoot.IsPressed();
                inputData.isShieldAbilityButtonClick = _inputActions.Character.ShieldAbility.IsPressed();
            });
        }

        protected override void OnStopRunning()
        {
            _inputActions.Character.Move.started -= OnMoveInputDirectionChange;
            _inputActions.Character.Move.performed -= OnMoveInputDirectionChange;
            _inputActions.Character.Move.canceled -= OnMoveInputDirectionChange;
        }
        
        private void OnMoveInputDirectionChange(InputAction.CallbackContext obj)
        {
            _moveInput = obj.ReadValue<Vector2>();
        }
    }
}
