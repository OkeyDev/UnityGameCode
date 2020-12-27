using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyClass : MonoBehaviour
{
    // Global variables. Set in Unity Inspector
    [Header("Move")]
    public float speed			  = 5f;
    public float raycastBoxLength = 0f;
    public float raycastLength    = 0.2f;
    public float raycastGroundNotLength = 1f;
    public bool isAttackOnTrigger = true;
    public LayerMask groundLayer;
    public Vector2 offsetGroundTouch;
    public Vector2 offsetGrountNotTouch;
	public GameObject spriteRender;
    [Header("Attack")]
    public LayerMask playerLayer;
    
    
    // Local variables
    private Rigidbody2D _rigidbody;
    private Collider2D _myCollider;
    private float _speed;
    protected float _direction;
	protected PlayerHealth _playerHealth;
    // Start is called before the first frame update
    void Start()
    {
		_speed = speed; // Set enemy speed
        _rigidbody = GetComponent<Rigidbody2D>(); // Get rigidbody component
        _myCollider = GetComponent<Collider2D>();
        _direction = 1; // If 0 Enemy will not move
		_playerHealth = GameController.GetPlayerHealth();
		RotateObject();
        AfterStart();
    }
    protected virtual void AfterStart() {

    }
    protected virtual void AfterUpdate() {
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttack();
        CheckTranslate();
        AfterUpdate();
    }
    float getDirection() {
        return _direction;
    }
    protected void CheckTranslate () {
        RaycastHit2D sideHit = MyPhysics.Raycast(transform.position, new Vector2(offsetGroundTouch.x * _direction,offsetGroundTouch.y), Vector2.right * _direction, raycastLength, groundLayer, true); // Check for Collision (wall)
        RaycastHit2D bottomHit = MyPhysics.Raycast(transform.position, new Vector2(offsetGrountNotTouch.x * _direction,offsetGrountNotTouch.y), Vector2.down, raycastGroundNotLength, groundLayer, true); // Check for Collision (wall) 
        if (sideHit || !bottomHit) {
            _direction = _direction == 1 ? -1 : 1;
			RotateObject();
        }
        Translate();
    }
    void Translate() {
        if (_rigidbody.velocity.y < 0) {
            _speed = 1; //  move, if player is falling down
        }
        transform.Translate(Vector2.right * _direction * _speed * Time.deltaTime); //No Physics
    }
	void RotateObject() {
		float localDirection = _direction == 1 ? 0 : 1;
		spriteRender.transform.rotation = Quaternion.Euler(0, 180 * localDirection, 0); 
	}
    protected void CheckAttack() {
        if (isAttackOnTrigger) {
            RaycastHit2D playerHit = Physics2D.BoxCast((Vector2)transform.position + _myCollider.offset,new Vector2(1,2),0,Vector2.right * _direction,raycastBoxLength,playerLayer);
            if (playerHit) {
                playerHit.collider.gameObject.GetComponent<PlayerHealth>().GetDamage();
            }
        }
        AfterCheckAttack();
    }
    
    protected virtual void AfterCheckAttack() {

    }
    protected virtual void Attack (PlayerHealth playerHealth) {
        
    }
}
