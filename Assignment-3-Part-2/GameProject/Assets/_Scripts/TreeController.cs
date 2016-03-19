using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {
	private AudioSource[] _audioSources;
	private AudioSource _waterSound;
	// Use this for initialization
	void Start () {
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._waterSound = this._audioSources [1];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Tree")) {
			this._waterSound.Play ();
		}

	}
}
