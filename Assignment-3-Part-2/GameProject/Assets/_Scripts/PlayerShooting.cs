using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	//PUBLIC MEMBER VARIABLES
	public Transform flashPoint;
	public GameObject muzzleFlash;
	public GameObject bulletImpact;
	public GameObject explosion;

    public GameObject jelly;
	

	// PRIVATE INSTANCE VARIABLES
	private Transform _transform;
    private GameController _gameController;
	// Use this for initialization
	void Start () {
		this._transform = gameObject.GetComponent<Transform> ();
        this._gameController = GameObject.FindWithTag("GameController").GetComponent("GameController") as GameController;

			
	} // end Start
	
	// Update is called once per frame
	void Update () {
		
	} // end Update


	void FixedUpdate() {
        
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate (this.muzzleFlash, flashPoint.position, Quaternion.identity);

			RaycastHit hit; // stores information from the Ray;

			if (Physics.Raycast (this._transform.position, this._transform.forward, out hit, 50f)) {

                if (hit.transform.gameObject.CompareTag("Teddy") || (hit.transform.gameObject.CompareTag("Present")))
                {
					Instantiate (this.explosion, hit.point, Quaternion.identity);
					Destroy (hit.transform.gameObject);
					this._gameController.ScoreValue += 100;
                    
                     
				} else {

					Instantiate (this.bulletImpact, hit.point, Quaternion.identity);
				}
             
                if (hit.transform.gameObject.CompareTag("Jelly"))
                {
                    Instantiate(this.explosion, hit.point, Quaternion.identity);
                    Destroy(hit.transform.gameObject);
                    this._gameController.LivesValue -= 1;


                }
                else
                {

                    Instantiate(this.bulletImpact, hit.point, Quaternion.identity);
                }


			}


		} // end if
	} // end FixedUpdate
}
