using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
using MoreMountains.Feedbacks;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Core.Player
{
    public class BallController : MonoBehaviour, IDamageable
    {
        public VariableJoystick variableJoystick;
        public bool overrideMobileControls = false;

        [SerializeField]
        float speed = 10f, currentSpeed, maxSpeed = 15f, jumpForce = 20f, DashMultiplier = 1.5f;
        float inputThreshold = 0.1f; // Adjust this threshold value as needed

        [SerializeField]
        int maxJumps = 2, jumpCount = 0;


        private Rigidbody rb;
        private float horizontalInput, verticalInput;
        private Vector3 movement;
        public Vector3 lastGroundPoint;
        public bool isInDialogue, isGrounded, inWater;


        [SerializeField] Image UiFillImage;
        [SerializeField] Button dashButton;
        [SerializeField] List<GameObject> LifePoints = new List<GameObject>();

        public int Health { get; set; } = 2;

        public CinemachineVirtualCamera cinemachineCamera;

        public MMFeedback Feedbacks;

        Interact interact = null;

        [SerializeField] private bool AdjustPlayerRotation;

        public bool dead = false;

        [Header("Damage effects")]
        public float invincibilityDuration = 1;
        public MeshRenderer mesh;
        public float flickerSpeed;
        public float flickerTimes;
        bool isInvincible = false;

        [Header("Squash")]
        public GameObject parent;
        public float squashScale;
        public float squashDuration;
        public LeanTweenType squashEase;

        private LTDescr squashTween;
        private LTDescr stretchTween;
        private LTDescr restoreSizeTween;

        private void OnEnable()
        {

#if  UNITY_EDITOR || UNITY_STANDALONE  
            overrideMobileControls = false;
            var parentObject = variableJoystick.transform.parent;
            parentObject.gameObject.SetActive(false);
#endif

        }

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();

        }
        private void Update()
        {   

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            if (Input.GetKeyDown(KeyCode.LeftShift)) 
                InteractAndDash(); 
             
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (dead) return;

            if (!isInDialogue)
            {
                Move();
            }

            if (isGrounded && rb.velocity.magnitude > 0.05f && rb.velocity.magnitude < 0.9f && Mathf.Abs(verticalInput) < inputThreshold && Mathf.Abs(horizontalInput) < inputThreshold)
            {
                rb.velocity = Vector3.zero;
            }

            if (isGrounded && rb.velocity.magnitude < .05f && AdjustPlayerRotation)
            {
                Quaternion targetRotation = Quaternion.LookRotation(GetCameraForward()) * Quaternion.Euler(0f, -90f, 0f);
                float rotationSpeed = 5f; // Adjust the rotation speed as needed
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

        }

        public void Jump()
        {
            if (!isGrounded && jumpCount < maxJumps)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++;
            }

            if (isGrounded && jumpCount == 0)
            {

                isGrounded = false;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++;
                AbstractVFXManager.Instance.PlayVFX("Normal Jump VFX", new Vector3(0f, 0.05f, 0f), gameObject);
            }
        }

        public void InteractAndDash()
        {
            if (interact != null && interact.isInteractable)
                Interact(); 
            else if (!isGrounded && jumpCount == maxJumps)
            {
                rb.AddForce(Vector3.down * jumpForce * DashMultiplier, ForceMode.Impulse);
                AbstractVFXManager.Instance.PlayVFX("Normal Jump VFX", new Vector3(0f, 0.05f, 0f), gameObject);
                //Feedbacks.Play(transform.position);  
                //StartCoroutine(CountDownLoop());
            }
            else if ( jumpCount != maxJumps && (interact == null || !interact.isInteractable))
            { 
                Vector3 dashDirection = rb.velocity.normalized; 
                rb.AddForce(dashDirection * DashMultiplier * 2, ForceMode.Impulse); 
                AbstractVFXManager.Instance.PlayVFX("Normal Jump VFX", new Vector3(0f, 0.05f, 0f), gameObject);
            }

        }


        #region Interaction

        void Interact()
        {
            if (interact != null)
            {
                interact.InteractWith();
            }

            interact.isInteractable = false;
            interact = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                interact = other.gameObject.GetComponent<Interact>();
            }
        }

        #endregion


        Vector3 GetCameraForward()
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0f; // Set the y-component to zero to eliminate vertical movement
            cameraForward.Normalize(); // Normalize the vector to ensure it's a unit vector

            return cameraForward;
        }

        private void Move()
        {

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            if (overrideMobileControls)
            {
                horizontalInput = variableJoystick.Horizontal;
                verticalInput = variableJoystick.Vertical;
            }

            currentSpeed = isGrounded ? speed : speed / 2;


            //movement = new Vector3(horizontalInput, 0f, verticalInput) * speed;
            movement = (horizontalInput * Camera.main.transform.right + verticalInput * GetCameraForward()) * currentSpeed;
            rb.AddForce(movement);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                jumpCount = 0;
                if (!isGrounded )
                {
                    //squash section
                    float collisionForce = collision.relativeVelocity.magnitude;
                    //Debug.Log("Collision force: " + collisionForce);

                    Vector3 collisionNormal = collision.contacts[0].normal;
                    Vector3 newSquashScale = CalculateSquashScale(collisionNormal);
                    float multiplier = (squashScale-1)*(collisionForce/4);

                
                    StopTweens();
                    Squash(multiplier,collisionNormal);

                    
                    
                }

                isGrounded = true;
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);


            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                TakeDamage();
            }
        }

        [ContextMenu("Squash")]
        public void Squash( float mult,Vector3 collisionNormal)
        {
            // Squash (scale down) the object
          squashTween=  LeanTween.scale(parent, new Vector3(1 + mult, 1/ (1 + mult), 1 + mult), squashDuration)
                .setEase(squashEase)
                .setIgnoreTimeScale(true)  // Ignore changes in time scale
                .setOnComplete(() => Stretch(mult,collisionNormal)); // After squashing, call the Stretch function
        }
        [ContextMenu("Stretch")]

        public void Stretch(float mult, Vector3 collisionNormal)
        {
            // Stretch (scale back to normal) the object
           stretchTween= LeanTween.scale(parent, new Vector3(1/(1 + mult), 1+mult, 1 / (1 + mult)), squashDuration)
                .setIgnoreTimeScale(true)  // Ignore changes in time scale
                .setEase(squashEase)
                .setOnComplete(RestoreSize);

            // After stretching, call the Squash function to create a loop
        }
        public void RestoreSize()
        {
           restoreSizeTween= LeanTween.scale(parent, Vector3.one, squashDuration)
                .setIgnoreTimeScale(true)  // Ignore changes in time scale
                .setEase(squashEase);
        }
        private Vector3 CalculateSquashScale(Vector3 collisionNormal)
        {
            // Calculate the squash scale based on the collision normal
            float squashFactor = Mathf.Clamp01(Vector3.Dot(transform.up, collisionNormal));
            return new Vector3(1f - squashFactor * (1f - squashScale), 1f, 1f - squashFactor * (1f - squashScale));
        }
        public void StopTweens()
        {
            squashTween?.pause();        // Pause and nullify the reference
            stretchTween?.pause();       // Pause and nullify the reference
            restoreSizeTween?.pause();   // Pause and nullify the reference
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {

                isGrounded = false;
                lastGroundPoint = transform.position;
            }


        }

        public void TakeDamage()
        {
            if (isInvincible)
            {
                return;
            }

            StartCoroutine(FlashMeshForInvincibility());
            print("health = " + Health);
            Health -= 1;


            if (Health == 1)
            {
                LifePoints[1].SetActive(false);
            }
            else if (Health == 0)
            {
                LifePoints[0].SetActive(false);
            }
        }

        public bool IsInvincible()
        {
            return isInvincible;
        }
        [ContextMenu("FlashMesh")]
        public void FlashMesh()
        {

            StartCoroutine(FlashMeshForInvincibility());
        }


        public IEnumerator FlashMeshForInvincibility()
        {
            isInvincible = true;
            for (int i = 0; i < flickerTimes; i++)
            {

                mesh.enabled = false;

                yield return new WaitForSeconds(flickerSpeed);

                mesh.enabled = true;

                yield return new WaitForSeconds(flickerSpeed);
            }

            isInvincible = false;

            yield return null;
        }
    }
}