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

            _inputActions.Character.Shoot.started += OnShootButtonClick;
            _inputActions.Character.Shoot.canceled += OnShootButtonClickCanceled;
        }

        protected override void OnUpdate()
        {
            Entities.With(_inputDataQuery).ForEach((Entity entity, ref InputData inputData) =>
            {
                inputData.moveStickDirection = _moveInput;
                inputData.isShootButtonClick = _isShootButtonClick;
            });
        }

        protected override void OnStopRunning()
        {
            _inputActions.Character.Move.started -= OnMoveInputDirectionChange;
            _inputActions.Character.Move.performed -= OnMoveInputDirectionChange;
            _inputActions.Character.Move.canceled -= OnMoveInputDirectionChange;
            
            _inputActions.Character.Shoot.started -= OnShootButtonClick;
            _inputActions.Character.Shoot.canceled -= OnShootButtonClickCanceled;
        }
        
        private void OnMoveInputDirectionChange(InputAction.CallbackContext obj)
        {
            _moveInput = obj.ReadValue<Vector2>();
        }

        private void OnShootButtonClick(InputAction.CallbackContext obj)
        {
            _isShootButtonClick = true;
        }
        
        private void OnShootButtonClickCanceled(InputAction.CallbackContext obj)
        {
            _isShootButtonClick = false;
        }
    }
}
