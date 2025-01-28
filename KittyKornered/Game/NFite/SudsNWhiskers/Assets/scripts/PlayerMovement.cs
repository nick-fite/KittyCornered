using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum CameraStyle {
        Gameplay,
        Cinematic
    }
public class PlayerMovement : Health
{
    CharacterController _charCont;
    Vector2 _rawMovementInput;
    float _gravVelocity;
    float _gravMultiplyer = 1;
    [SerializeField] Animator anim;
    Shoot _shoot;
    SplatterCleanUp _splatterClean;
    public CameraStyle CurrentStyle;
    bool holdingScrub;

    [SerializeField] float speed;
    [SerializeField] float detectionRange = 2f;
    [SerializeField] Camera _cam;
    [SerializeField] float _turnSmoothVelocity;
    [SerializeField] float _turnSmoothTime;
    [SerializeField] int _health;
    [SerializeField] GameObject broom;
    [SerializeField] GameObject gun;
    [SerializeField] Transform gunPos;
    [SerializeField] Transform broomPos;
    [SerializeField] LayerMask ratLayer;
    [SerializeField] Transform stompPos;
    GameObject broomInstance;
    GameObject gunInstance;

    public bool atDoor = false;
    public bool allowedToLeave = false;

    public int scene;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shoot = GetComponent<Shoot>();
        _charCont = gameObject.GetComponent<CharacterController>();
        _splatterClean = gameObject.GetComponentInChildren<SplatterCleanUp>();
        _cam = Camera.main;
        health = _health;
        anim.SetBool("gun", true);
        gunInstance = Instantiate(gun, gunPos.position, gunPos.rotation);
        gunInstance.transform.parent = gunPos;
        _shoot.CanShoot = true;
        _splatterClean.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (atDoor && allowedToLeave && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void MovePlayer()
    {
        if(!_charCont.isGrounded) gravity();

        Vector3 moveDir = Vector3.zero;
        moveDir.y =_gravVelocity; 
        if(_rawMovementInput != Vector2.zero)
        {

            float targetAngle = GetTargetAngle();
            float rotAngle = GetRotationAngle(targetAngle);

            Quaternion newAngle = Quaternion.Euler(0f, rotAngle, 0f);
            transform.rotation = newAngle;

             moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        
        }
        _charCont.Move(moveDir.normalized * (speed * Time.deltaTime));
    }

    private void gravity()
    {
        _gravVelocity = -9.81f * _gravMultiplyer * Time.deltaTime;
    }

    public override void Death()
    {
        base.Death();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


    private float GetTargetAngle()
    {
        Vector3 movementValue = new Vector3(_rawMovementInput.x, 0, _rawMovementInput.y).normalized;
        return Mathf.Atan2(movementValue.x, movementValue.z) * Mathf.Rad2Deg + _cam.transform.eulerAngles.y;
    }

    private float GetRotationAngle(float targetAngle)
    {
        return Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
    }

    public void WalkAction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _shoot.CanShoot = false;
            anim.SetBool("walking", true);
            _rawMovementInput = context.ReadValue<Vector2>();
        }
        if(context.canceled) 
        {
            _shoot.CanShoot = true;
            anim.SetBool("walking", false);
            _rawMovementInput = Vector2.zero;
        }
    }

    public void SetHoldingBrush(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            holdingScrub = !holdingScrub;
            if(holdingScrub)
            {
                if(gunInstance)
                {
                    Destroy(gunInstance);
                }
                broomInstance = Instantiate(broom, broomPos.position, broomPos.rotation);
                broomInstance.transform.parent = broomPos;
                anim.SetBool("gun", false);
                _shoot.CanShoot = false;
                _splatterClean.enabled = true;
            }
            else 
            {
                if(broomInstance)
                {
                    Destroy(broomInstance);
                }
                anim.SetBool("gun", true);
                gunInstance = Instantiate(gun, gunPos.position, gunPos.rotation);
                gunInstance.transform.parent = gunPos;
                _shoot.CanShoot = true;
                _splatterClean.enabled = false;

            }
        }
    }

    public void Stomp(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            AudioManager.AudioInstance.PlayStomp();


            Collider[] hitColliders = Physics.OverlapSphere(stompPos.position, detectionRange, ratLayer);
            
            anim.SetTrigger("stomp");
            if(hitColliders.Length > 0)
            {
                foreach(Collider col in hitColliders)
                {
                    Debug.Log(col.name);
                    Health hel = col.gameObject.transform.parent.GetComponent<Health>();
                    if(hel)
                    {
                        hel.AddHealth(-10);
                    }                    
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
    }
}
