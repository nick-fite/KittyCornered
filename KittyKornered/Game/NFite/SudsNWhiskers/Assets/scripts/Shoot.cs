using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform _startShootingPos;
    public bool CanShoot = true;
    Vector3 _shootMousePos;
    Camera _mainCamera;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject goodBubble;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    private void ShootGun()
    {
        _animator.SetTrigger("shoot");
        AudioManager.AudioInstance.PlayShoot();
        Vector3 direction = _shootMousePos - _startShootingPos.position;
        RaycastHit hit;
        if (Physics.Raycast(_startShootingPos.position, direction, out hit, Mathf.Infinity) && hit.collider.gameObject != gameObject)
        {
            GameObject bubble = Instantiate(goodBubble, transform.position, transform.rotation);
            bubble.transform.LookAt(_shootMousePos);
            Debug.Log(hit.collider.name);
            Health healthComp = hit.collider.GetComponentInParent<Health>();   
            if(healthComp)
            {
            if (hit.collider.tag == "bubble") {
            
                healthComp.AddHealth(-10);
                Debug.Log("HIT");
                }
            }
        }
    }

    public void OnMousePress(InputAction.CallbackContext context)
    {
        if(context.performed && CanShoot)
        {
            ShootGun();
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if(context.performed && CanShoot)
        {
            Ray ray = _mainCamera.ScreenPointToRay(context.ReadValue<Vector2>());    
            if(Physics.Raycast(ray, out RaycastHit hit)) {
                _shootMousePos = hit.point;
                Vector3 targetPos = new Vector3(_shootMousePos.x, transform.position.y, _shootMousePos.z);
                transform.LookAt(targetPos);
                /*Vector3 lookPos = transform.position - hit.collider.transform.position;
                lookPos.y = 0;
                Quaternion rot = Quaternion.LookRotation(lookPos);
                transform.rotation = rot;*/
            }
        }

    }
}
