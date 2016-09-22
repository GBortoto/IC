using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class RollerBall : MonoBehaviour {

	public GameObject ViewCamera = null;
	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip CoinSound = null;
	public Rigidbody Bola = null;

	private Rigidbody mRigidBody = null;
	private AudioSource mAudioSource = null;
	private bool mFloorTouched = false;

	public GameObject Greenball;
	public int coeficiente_de_forca;

	public GameObject Overlayer;
	private KinectOverlayer Overlayer2;

	//public KinectOverlayer kinect;

	// MODIFICAÇÃO
	// Leia da ConfigUNITY.txt
	// TODO: IDENTIFICAR SE O ConfigUNITY.TXT existe
	void setFromConfig(){
		print ("PLAYER - setFromConfig()");
		string[] lines = System.IO.File.ReadAllLines (@"ConfigUNITY.txt");
		coeficiente_de_forca = Int32.Parse(lines [2]);
	}


	void Start () {
		mRigidBody = GetComponent<Rigidbody> ();
		mAudioSource = GetComponent<AudioSource> ();
		Overlayer2 = Overlayer.GetComponent<KinectOverlayer> ();
		Bola = GetComponent<Rigidbody> ();
		//kinect = GetComponent<KinectOverlayer> ();
	}

	void Update() {
		if(transform.position.y < -2) {
			arrumaY ();
		}
	}

	void arrumaY() {
		transform.position = new Vector3 (10,10,5);
	}

	void FixedUpdate () {

		//if(.transform.position.y)

		if (mRigidBody != null) {
			
			/*
			if (Input.GetButton ("Horizontal")) {
				mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal")*10);
			}
			if (Input.GetButton ("Vertical")) {
				mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical")*10);
			}
			if (Input.GetButtonDown("Jump")) {
				if(mAudioSource != null && JumpSound != null){
					mAudioSource.PlayOneShot(JumpSound);
				}
				mRigidBody.AddForce(Vector3.up*200);
			}
			*/
			Vector3 Force = Overlayer2.forca;

			Force = new Vector3 (Force.x, 0, Force.y-1);
			/*
			if (Force.y > 1) {
				Force = new Vector3 (Force.x, 0, Force.y-1);
			}
			if (Force.y < 1) {
				Force = new Vector3 (Force.x, 0, Force.y-1);
			}
			*/
			//Vector3 Force = new Vector3 ( (mRigidBody.transform.position.x - Greenball.transform.position.x)*(-1), 0, (mRigidBody.transform.position.z - Greenball.transform.position.z)*(-1) );
			//print (Force);
			mRigidBody.AddForce (Force * coeficiente_de_forca);
			
			/*
			//print ("teste");
			mRigidBody.position.Set (Greenball.transform.position.x, mRigidBody.transform.position.y, Greenball.transform.position.z);
			*/
		}
		if (ViewCamera != null) {
			Vector3 direction = (Vector3.up*2+Vector3.back)*2;
			RaycastHit hit;
			Debug.DrawLine(transform.position,transform.position+direction,Color.red);
			if(Physics.Linecast(transform.position,transform.position+direction,out hit)){
				ViewCamera.transform.position = hit.point;
			}else{
				ViewCamera.transform.position = transform.position+direction;
			}
			ViewCamera.transform.LookAt(transform.position);
		}
	}

	void OnCollisionEnter(Collision coll){
		/*
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = true;
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		} else {
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		}
		*/
	}

	void OnCollisionExit(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Coin")) {
			if(mAudioSource != null && CoinSound != null){
				mAudioSource.PlayOneShot(CoinSound);
			}
			Destroy(other.gameObject);
		}
	}
}
