using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;

public class CharacterManager : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    private GameObject currentPassblePlatform;
    [SerializeField] private BoxCollider2D playerCollider;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private GameObject          m_atkHitbox;
    private SpriteRenderer      m_spriteRenderer;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_isWallSliding = false;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;

    private float currentTime;
    public float atkCdw = 0f;
    public bool blocking = false;
    public Transform pos;
    public bl_Joystick js;
    public Vector2 boxSize;
    public CharacterStats myStats;
    public LayerMask attackMask;

    public bool inputJump;
    public bool inputAttack;
    public bool inputGuard;
    public bool inputRoll;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        myStats = GetComponent<CharacterStats>();
        playerCollider = GetComponent<BoxCollider2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        //m_atkHitbox = transform.Find("melee").GetComponent<GameObject>();
        UIBtnManager ui = GameObject.Find("ButtonUI").GetComponent<UIBtnManager>();
        js = GameObject.Find("Joystick").GetComponent<bl_Joystick>();
        ui.Init();
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);
        dir.Normalize();
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;
        atkCdw -= Time.deltaTime;
        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }
        // input mode
        // -- Handle input and movement --
        //float inputX = Input.GetAxis("Horizontal"); //PC
        float inputX = dir.x; //Mobile
        //float inputY = Input.GetAxis("Vertical"); //PC
        float inputY = dir.y; //Mobile
        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
            pos.position = new Vector2(transform.position.x+1,transform.position.y+0.8f);
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            pos.position = new Vector2(transform.position.x-1,transform.position.y+0.8f);
        }

        // Move
        if (!m_rolling )
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        //Death
        /*
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }
            
        //Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
            m_animator.SetTrigger("Hurt");
        */
        //Attack
        //if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        if(inputAttack && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            // 충돌 감지 point = 박스 생성 위치, size = 박스 사이즈
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position,boxSize,0,attackMask);
            foreach(Collider2D collider in collider2Ds)
            {
                CharacterStats enemyStats = collider.GetComponent<CharacterStats>();
                enemyStats.TakeDamage(myStats.damage.GetStat());
                //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록
            }

            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        //else if (Input.GetMouseButtonDown(1) && !m_rolling)
        if (inputGuard && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            blocking = true; 
            m_animator.SetBool("IdleBlock", true);
        }

        else if (!inputGuard){
            blocking = false; 
            m_animator.SetBool("IdleBlock", false);
        }

        // Roll
        //else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        if (inputRoll && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }

        //Jump
        //else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        if(inputJump){
            if(dir.y <= -0.7f){
                if(currentPassblePlatform != null){
                    StartCoroutine(DisableCollision());
                }
            }
            if (m_grounded && !m_rolling) //JUMP
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }
        } 

        
        
        //Run
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }
        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }

    //Events
    public void Attack(CharacterStats targetStats){
        float atkSpd = myStats.atkSpd.GetStat() * 0.1f;
        if (atkCdw <= 0){
            targetStats.TakeDamage(myStats.damage.GetStat());
            atkCdw = 1f / atkSpd;
        }
    }

    //for debug
    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    // Animation Events
    // Called in slide animation.

    public void OnDamaged(Vector2 targetPos){
        //레이어 변화
        gameObject.layer = 10;
        //피격시 색 변화
        m_spriteRenderer.color = new Color(1, 1, 1, 0.4f);// 4번째 인자는 투명도
        //피격시 튕김리액션 
        int dirc = transform.position.x - targetPos.x  > 0 ? 1 : -1;
        m_body2d.AddForce(new Vector2(dirc, 1)*4, ForceMode2D.Impulse);
        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 3;
        m_spriteRenderer.color = new Color(1, 1, 1, 1);
    }
        private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("PassablePlatform")){
            //Debug.Log(collision.gameObject.tag);
            currentPassblePlatform = collision.gameObject;
            //Debug.Log("Enter col",currentPassblePlatform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("PassablePlatform")){
            //Debug.Log(collision.gameObject.tag);
            currentPassblePlatform = null;
            //Debug.Log("Exit col",currentPassblePlatform);
        }
    }
    private IEnumerator DisableCollision(){
        TilemapCollider2D platformCollider = currentPassblePlatform.GetComponent<TilemapCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.75f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
}
