using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform _startShootingPos;
    Vector3 _shootMousePos;
    Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    private void ShootGun()
    {
        Vector3 direction = _shootMousePos - _startShootingPos.position;
        RaycastHit hit;
        if (Physics.Raycast(_startShootingPos.position, direction, out hit, Mathf.Infinity) && hit.collider.gameObject != gameObject)
        {
            Debug.Log(hit.collider.name);
            Health healthComp = hit.collider.GetComponentInParent<Health>();   
            if(healthComp)
            {
                healthComp.AddHealth(-10);
                Debug.Log("HIT");
            }
        }
    }

    public void OnMousePress(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            ShootGun();
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Ray ray = _mainCamera.ScreenPointToRay(context.ReadValue<Vector2>()); 
            
            if(Physics.Raycast(ray, out RaycastHit hit)) {
                _shootMousePos = hit.point;
            }

        }

    }
}
