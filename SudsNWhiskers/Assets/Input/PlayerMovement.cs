using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] Camera _cam;
    [SerializeField] float _turnSmoothVelocity;
    [SerializeField] float _turnSmoothTime;
    [SerializeField] int _health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shoot = GetComponent<Shoot>();
        _charCont = gameObject.GetComponent<CharacterController>();
        _splatterClean = gameObject.GetComponentInChildren<SplatterCleanUp>();
        _splatterClean.enabled = false;
        _cam = Camera.main;
        health = _health;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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
        _charCont.Move(moveDir.normalized * (speed * Time.fixedDeltaTime));
    }

    private void gravity()
    {
        _gravVelocity = -9.81f * _gravMultiplyer * Time.deltaTime;
    }

    public override void Death()
    {
        base.Death();
        //Destroy(gameObject);
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
                _shoot.CanShoot = false;
                _splatterClean.enabled = true;
            }
            else 
            {
                _shoot.CanShoot = true;
                _splatterClean.enabled = false;

            }
        }
    }
}
