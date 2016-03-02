
/*Source file name: GameController.cs
Author’s name:Padma Chakraborty
Last Modified by:Padma Chakraborty
Date last Modified: 29 February,2016
Program description:Code that executes when the monkey tries to steal bananas by avoiding interaction with the man who is safegaurding the bananas.
Revision History:01
*/
using UnityEngine;
using System.Collections;


// VELOCITY RANGE UTILITY Class +++++++++++++++++++++++
[System.Serializable]
public class VelocityRange {
	// PUBLIC INSTANCE VARIABLES ++++

	public float minimum;
	public float maximum;

	// CONSTRUCTOR ++++++++++++++++++++++++++++++++++++
	public VelocityRange(float minimum, float maximum) {
		this.minimum = minimum;
		this.maximum = maximum;
	}
}


public class monkeyController : MonoBehaviour {
	public VelocityRange velocityRange;
	public float moveForce;
	public float jumpForce;
	public Transform  groundCheck;
	public Transform camera;
	public GameController gameController;
	// PRIVATE  INSTANCE VARIABLES
	private Animator _animator;
	private float _move;
	private float _jump;
	private bool _facingRight;
	private Transform _transform;
	private Rigidbody2D _rigidBody2D;
	private bool _isGrounded;
	private AudioSource[] _audioSources;
	private AudioSource _jumpSound;
	private AudioSource _bananaSound;
	private AudioSource _enemySound;
	// Use this for initialization
	void Start () {
		this.velocityRange = new VelocityRange (300f, 20000f);



		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();
		this._rigidBody2D=gameObject.GetComponent<Rigidbody2D> ();
		this._move = 0f;
		this._jump = 0f;
		this._facingRight = true;

		//Setup audiosources
		this._audioSources=gameObject.GetComponents<AudioSource>();
		this._jumpSound = this._audioSources [0];
		this._bananaSound = this._audioSources [1];
		this._enemySound = this._audioSources [2];
		//place the girl in the correct position
		this._spawn ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 currentPosition = new Vector3 (this._transform.position.x, this._transform.position.y,-10f);
		this.camera.position = currentPosition;

		this._isGrounded = Physics2D.Linecast (this._transform.position, 
			                              this.groundCheck.position,
			1<<LayerMask.NameToLayer("Ground"));


		float forceX = 0f;
		float forceY = 0f;
		//get absolute value of velocity for our gameobject
		float absVelX = Mathf.Abs (this._rigidBody2D.velocity.x);
		float absVelY = Mathf.Abs (this._rigidBody2D.velocity.y);

		//Ensure the player is grounded before and movement checks
		if (this._isGrounded) {
			this._move = Input.GetAxis ("Horizontal");
			this._jump = Input.GetAxis ("Vertical");

			if (this._move != 0) {
				if (this._move > 0) {
					//movement force
					if (absVelX < this.velocityRange.maximum) {
						forceX = this.moveForce;
					}
	
					this._facingRight = true;
					this._flip ();
				}
				if (this._move < 0) {
					//movementforce
					if (absVelX < this.velocityRange.maximum) {
						forceX = -this.moveForce;
					}
					this._facingRight = false;
					this._flip ();
				}
				this._animator.SetInteger ("AnimState", 1);
			} else {
				this._animator.SetInteger ("AnimState", 0);
			}
			if (this._jump > 0) {
				//Jump force
				if (absVelY < this.velocityRange.maximum) {
					this._jumpSound.Play ();
					forceY = this.jumpForce;
				}
			}

		} else {
			this._animator.SetInteger ("AnimState", 1);
		}

		this._rigidBody2D.AddForce(new Vector2(forceX,forceY));
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("BananaOne")){
			this._bananaSound.Play ();
			this.gameController.ScoreValue += 10;
			Destroy (other.gameObject);
		}
		if (other.gameObject.CompareTag("Banana")){
			this._bananaSound.Play ();
			this.gameController.ScoreValue += 30;
			Destroy (other.gameObject);
		}
		if (other.gameObject.CompareTag("Death")){
			this._spawn ();
			this.gameController.LivesValue--;
		}
		if (other.gameObject.CompareTag("Enemy")){
			
			this._enemySound.Play ();
			this.gameController.LivesValue--;
			  
		}
		}

		 private  void _flip()
		{
		if (this._facingRight) {
			this._transform.localScale = new Vector2 (1, 1);
		} else {
			this._transform.localScale = new Vector2 (-1, 1);
		}
		}

	private void _spawn()
	{
		this._transform.position = new Vector3 (-1256f, 118f, 0);
	}
}