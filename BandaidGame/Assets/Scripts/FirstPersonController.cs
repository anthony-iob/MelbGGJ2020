using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
		public float horizontal;
		public float vertical;
		public Gun weaponScript;
        public Vector3 normalCameraLocalPos = new Vector3(0, 2.44f, 0);
        public Vector3 crouchedCameraLocalPos = new Vector3(0, 1, 0);
        public Animator cameraPivot;
        [SerializeField] private bool m_IsWalking;
        public float m_WalkSpeed; //un-serialzed and made public so the light script can make player speed 0 while in light
        public float m_RunSpeed;
        public bool m_JumpAllowed;
        //[SerializeField] private float m_WalkSpeed;
        //[SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        //[SerializeField] private MouseLook m_MouseLook;
        public MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;


        [Header("Audio")]
        [SerializeField] private AudioClip[] footstepSFX;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip[] jumpSFX;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip[] landSFX;           // the sound played when character touches back on ground.
        
        public AudioSource jumpAudioSource;
        public AudioSource landAudioSource;
        public AudioSource footstepAudioSource;
        private AudioSource m_AudioSource;
        private AudioSource source;
 
        [Header ("Movement")]
        
        //Crouch sprint toggle
        public bool toggleCrouchSprint;

        private Camera m_Camera;
        private bool m_Jump;
        private int m_JumpEarly;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;

        private bool m_fired;
        private bool m_fired2;
        private bool m_chargeSoundPlayed;

        //Crouch function
        private float characterControllerHeightOnStart;



        private float runSpeedOnStart;
        private float walkSpeedOnStart;
        
        // Use this for initialization
        private void Start()
        {
            //Application.targetFrameRate = -1;

            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_Jumping = false;
            m_JumpAllowed = true;
            m_AudioSource = GetComponent<AudioSource>();
            m_MouseLook.Init(transform, m_Camera.transform);

            characterControllerHeightOnStart = m_CharacterController.height; //Crouching
            source = GetComponent<AudioSource>();
            //m_AudioSource.clip = landSFX;

            //cameraPivot.speed = 0;

            runSpeedOnStart = m_RunSpeed;
            walkSpeedOnStart = m_WalkSpeed;
            m_chargeSoundPlayed = false;
        }

        public void ForceLockCursor()
        {
            m_MouseLook.SetCursorLock(true);
        }

        public void EnableCursor() {
            m_MouseLook.SetCursorLock(false);
        }

        public void ToggleJump()
        {
            m_JumpAllowed = false;
            Debug.Log("Triggered!");
        }

        // Update is called once per frame
        private void Update()
        {
            RotateView();
            //CheckHeartBeatAndBreathing();

            // the jump state needs to read here to make sure it is not missed
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");

            if (m_JumpEarly > 0)
            {
                m_JumpEarly--;
            }

            if (m_Jump)
            {
                m_JumpEarly = 5;
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
			//if (source.clip.name == "Footstep01")
			//{
			//	source.panStereo = -0.36f;
			//	source.spatialBlend = 0;
			//}
			//if (source.clip.name == "Footstep02")
			//{
			//	source.panStereo = 0.36f;
			//	source.spatialBlend = 0;
			//}
			//if (source.clip.name != "Footstep01" && source.clip.name != "Footstep02")
			//{
			//	source.panStereo = 0;
			//	source.spatialBlend = 1.0f;
			//}
			//CROUCHING =========================================================

			if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_JumpEarly > 0 && m_JumpAllowed)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.deltaTime;
            }

            if (GameManager.instance.disablePewPew == false && Time.timeScale != 0)
            {                           
                /* Mouse Inputs */
                if (Input.GetButtonUp("Fire1"))
                {
                    weaponScript.Shoot();
                    weaponScript.CancelCharge();
                }

                if (Input.GetButton("Fire1"))
                {
                    weaponScript.Charge();
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    weaponScript.chargeSFX.Play();
                }

                /* Controller Inputs/Right Click Exploit */

                if (Input.GetAxisRaw("Fire2") <= 0)
                {
                    if (!m_fired)
                    {
                        weaponScript.Shoot();
                        weaponScript.CancelCharge();
                        m_fired = true;
                        m_chargeSoundPlayed = false;
                    }
                }

                if (Input.GetAxisRaw("Fire2") > 0)
                {
                    m_fired = false;
                    if (!m_chargeSoundPlayed)
                    {
                        weaponScript.chargeSFX.Play();
                        m_chargeSoundPlayed = true;
                    }
                    weaponScript.Charge();
                }

                /*For the Xbox Controller exploit*/

                if (Input.GetAxisRaw("Fire3") <= 0)
                {
                    if (!m_fired2)
                    {
                        weaponScript.Shoot();
                        weaponScript.CancelCharge();
                        m_fired2 = true;
                        m_chargeSoundPlayed = false;
                    }
                }

                if (Input.GetAxisRaw("Fire3") > 0)
                {
                    m_fired2 = false;
                    if (!m_chargeSoundPlayed)
                    {
                        weaponScript.chargeSFX.Play();
                        m_chargeSoundPlayed = true;
                    }
                    weaponScript.Charge();
                }

                if (GameManager.instance.floodLevel.transform.position.y >= GameManager.instance.effectFloodLevel)
                {
                    m_JumpAllowed = false;
                    m_WalkSpeed = 5.0f;

                }
            }

            void CheckHeartBeatAndBreathing()
            {
                if (SceneManager.GetActiveScene().name != "Level1") return;

               // float BreathingVolume = -0.01f * SprintTimeRemaining + 0.2f;
               // BreathingSound.volume = BreathingVolume;

              //  HeartBeatSound.volume = Mathf.Lerp(HeartBeatSound.volume, BreathingVolume, Time.deltaTime);

                if (SprintTimeRemaining == 0)
                {
                    sprintCooldown = true;
                }

                if (SprintTimeRemaining > cooldownThreshold)
                {
                    sprintCooldown = false;
                }
            }
        }

        public float speed;
		public float SprintTimeRemaining;
		public bool sprintCooldown;
		public float cooldownThreshold = 5f;
		//public AudioSource BreathingSound;
		//public AudioSource HeartBeatSound;

		private void FixedUpdate()
        {
            GetInput(out speed);

            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
            m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }

        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }

        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, footstepSFX.Length);
            footstepAudioSource.clip = footstepSFX[n];
            footstepAudioSource.PlayOneShot(footstepAudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            footstepSFX[n] = footstepSFX[0];
            footstepSFX[0] = footstepAudioSource.clip;
        }

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
               return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }

        private void GetInput(out float speed)
        {
            // Read input
            horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            //Prevents crouch sprinting. Remove if-else and keep line 246 to revert. - MW(03/09)
            //Added menu toggle. - MW(04/09)
            switch (toggleCrouchSprint)
            {
                case true:
					if (sprintCooldown)
					{
						m_IsWalking = horizontal != 0 || vertical != 0;
					}
					else
					{
						m_IsWalking = horizontal != 0 || vertical != 0;
					}
                    break;
                case false:
                    m_IsWalking = horizontal != 0 || vertical != 0;
                    break;
            }

            if (m_IsWalking)
            {
                // Running.
                if (Input.GetButton("Fire1"))
                {
                    cameraPivot.speed = 1;
                }
                else
                {
                    cameraPivot.speed = 0.5f;
                }
            }
            else
            {
                if (Input.GetButton("Fire1"))
                {
                    cameraPivot.speed = 1;
                }
                else
                {
                    //cameraPivot.speed = 0;
                }
            }

#endif
            // set the desired speed to be walking or running
            speed = m_WalkSpeed;

			if (!m_IsWalking)
			{
				if (speed == m_RunSpeed && (horizontal != 0 || vertical != 0))
				{
					SprintTimeRemaining -= Time.deltaTime;
				}
				else
				{
					SprintTimeRemaining += Time.deltaTime;
				}
			}
			else
			{
				SprintTimeRemaining += Time.deltaTime;
			}

			SprintTimeRemaining = Mathf.Clamp(SprintTimeRemaining, 0, 20);
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }

        void PlayLandingSound()
        {
            if (Time.timeScale != 0)
            {
                // pick & play a random footstep sound from the array,
                // excluding sound at index 0
                int n = Random.Range(1, landSFX.Length);
                landAudioSource.clip = landSFX[n];
                landAudioSource.PlayOneShot(landAudioSource.clip);
                // move picked sound to index 0 so it's not picked next time
                landSFX[n] = landSFX[0];
                landSFX[0] = landAudioSource.clip;
            }
        }

        void PlayJumpSound()
        {
            if (Time.timeScale != 0)
            {

                // pick & play a random footstep sound from the array,
                // excluding sound at index 0
                int n = Random.Range(1, jumpSFX.Length);
                jumpAudioSource.clip = jumpSFX[n];
                jumpAudioSource.PlayOneShot(jumpAudioSource.clip);
                // move picked sound to index 0 so it's not picked next time
                jumpSFX[n] = jumpSFX[0];
                jumpSFX[0] = jumpAudioSource.clip;
            }
        }

        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.8f, hit.point, ForceMode.Impulse);
        }
    }
}