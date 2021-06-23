using UnityEngine;

namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        // Make all the variables.
        #region Variables.
        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        private float _gravity = 20.0f;
        private Vector3 _moveDir;
        private CharacterController _charC;
        private Animator myAnimator;
        public CustomisationGet GetG;
        public GameObject damageIndication;

        public KeyBinds keyBinds;

        public bool PausedGame;

        private float damageTimer;
        private float maxDamageTimer = 1;

        #endregion

        private void Start()
        {
            damageIndication.gameObject.SetActive(false);
            maxDamageTimer = 1;
            PausedGame = false;
            _charC = GetComponent<CharacterController>();
            myAnimator = GetComponentInChildren<Animator>();
            damageTimer = 0;
        }

        private void Update()
        {
            if (!PausedGame)
            {
                Move();
                StatUse();
                CheckDamage();
            }
        }

        // This will display the damage indication.
        private void CheckDamage()
        {
            // no dam taken.
            if (damageTimer > 0)
            {
                // timer for a second.
                damageTimer -= Time.deltaTime;
            }
            // Activate if true and timer at or below 0.
            else if (damageTimer < 0 && damageIndication.gameObject.activeSelf == true)
            {
                // Stop damage indicator game object.
                damageIndication.gameObject.SetActive(false);
                // Timer = 0.
                damageTimer = 0;
            }
        }

        private void StatUse()
        {
            if (keyBinds.GetKey("UseHealth"))
            {
                GetG.UseStat("health");
            }
            if (keyBinds.GetKey("UseMana"))
            {
                GetG.UseStat("mana");
            }
            if (keyBinds.GetKey("UseStamina"))
            {
                GetG.UseStat("stamina");
            }
            if (keyBinds.GetKey("LevelUp"))
            {
                GetG.LevelUp();
            }
        }

        private void Move()
        {
            Vector2 movementInput = new Vector2(0,0);

            if (keyBinds.GetKey("Forwards"))
            {
                movementInput.y += 1;
            }
            if (keyBinds.GetKey("Backwards"))
            {
                movementInput.y += -1;

            }
            if (keyBinds.GetKey("Left"))
            {
                movementInput.x += -1;
            }
            if (keyBinds.GetKey("Right"))
            {
                movementInput.x += 1;
            }

            if (_charC.isGrounded)
            {
                if (keyBinds.GetKey("Crouch"))
                {
                    moveSpeed = crouchSpeed;
                    myAnimator.SetFloat("speed", 0.25f);
                }
                else
                {
                    if (keyBinds.GetKey("Run"))
                    {
                        moveSpeed = runSpeed;
                        myAnimator.SetFloat("speed", 7f);
                    }
                    else if (!keyBinds.GetKey("Run"))
                    {
                        moveSpeed = walkSpeed;

                        myAnimator.SetFloat("speed", 1f);
                    }
                }

                myAnimator.SetBool("walking", movementInput.magnitude > 0.05f);

                _moveDir = transform.TransformDirection(new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed);
            }
            _moveDir.y -= _gravity * Time.deltaTime;
            _charC.Move(_moveDir * Time.deltaTime);
        }

        public void levelup(int dex)
        {
            walkSpeed = walkSpeed + (0.1f*dex);
            runSpeed = walkSpeed * 7;
            crouchSpeed = walkSpeed * 0.25f;
        }
        public void TakeDamage()
        {
            damageIndication.gameObject.SetActive(true);
            damageTimer = maxDamageTimer;
        }
    }
}