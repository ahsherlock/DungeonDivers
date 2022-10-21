using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D theRB;
    public Transform gunArm;

    private Camera theCamera;

    public Animator theAnimator;
    // ----------BULLET STUFF--------------
    public GameObject bulletToFire;
    public Transform firePoint;

    public SpriteRenderer bodySR;

    public float timeBetweenShot;

    private float shotCounter;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibility = .5f;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCounter;

    public int dashSound, shotSound;

    //Awake happens before start
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set Camera at start of game, because setting it every frame at 60FPS is too resource intensive 
        theCamera = Camera.main;
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();


        theRB.velocity = moveInput * activeMoveSpeed;

        // get mouse position 
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = theCamera.WorldToScreenPoint(transform.localPosition);


        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, -1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        // Rotate the gun arm
        Vector2 offSet = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float theAngle = Mathf.Atan2(offSet.y, offSet.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, theAngle);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            AudioManager.instance.PlaySFX(shotSound);
            shotCounter = timeBetweenShot;

        }
        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(shotSound);
                shotCounter = timeBetweenShot;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                PlayerHealthController.instance.invincCount = PlayerHealthController.instance.invinceLength;
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 0.5f);
                AudioManager.instance.PlaySFX(dashSound);
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                theAnimator.SetTrigger("Dash");
            }


        }
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        if (moveInput != Vector2.zero)
        {
            // case sensitive use same spelling as in unity
            theAnimator.SetBool("isMoving", true);
        }
        else
        {
            theAnimator.SetBool("isMoving", false);
        }


    }
}
