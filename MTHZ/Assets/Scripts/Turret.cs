using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class Turret : MonoBehaviour {

	//The controller assigned within the inspector
	public XboxController controller;
	//Speed at whcih the turret rotates
	public float rotationSpeed = 50f;

	//The point at which projectiles will spawn
	public Transform spawnPoint;

	//Whether the turret can shoot
	private bool canShoot = true;
	//The time between shots
	public float timeBetweenShots = 0.4f;
	//Damage done by the turret, this is applied to armour, health, etc
	private int turretDamage = 80;
	//The current Weapon selected
	public int weapon = 1;
	//Weapon 1 = Gatling Gun
	//Weapon 2 = Tether
	//Weapon 3 = Grenade Launcher
	//Weapon 4 = Missile Launcher
	//What objects are available to be shot depending on their layers
	public LayerMask whatCanBeShot;

	private Vector3 rotateUp;
	private Vector3 rotateRight;

	public Vector3 movementStorage;

	private bool canMoveDown = true;
	private bool canMoveUp = true;
	private bool canMoveRight = true;
	private bool canMoveLeft = true;

	private float rightStickY = 0f;
	private float rightStickX = 0f;

	public Camera cameraTurret;
	//public GameObject targetScreenMarker;
	private float aimSpeed = 400f;

	private float xLocation;
	private float yLocation;
	private float zLocation;

	public LayerMask turretMask;
	private Vector3 location;
	public RectTransform targetReticule;
	private Vector3 startPosition;

	private float halfWidth;
	private float halfHeight;

	public bool blue = false;
	public bool green = false;
	public bool yellow = false;
	public bool red = false;

	public GameObject hitParticle;
	public GameObject damageText;
	private TextMesh textBox;

	// Use this for initialization
	void Start () {
		SetCamera ();
	}
	
	// Update is called once per frame
	void Update () {
		//WeaponSwitching();
		Shooting();
		//TurretRotationV ();
		RotatingTurret();
	}

	private void SetCamera(){

		if (blue == true){
		//Blue Start
		targetReticule.position = new Vector3 (cameraTurret.pixelWidth / 2, cameraTurret.pixelHeight * 1.5f, 0);
		startPosition = targetReticule.position;
		}else if (green){
		//Green Start
		targetReticule.position = new Vector3 (cameraTurret.pixelWidth * 1.5f, cameraTurret.pixelHeight * 1.5f, 0);
		startPosition = targetReticule.position;
		}else if (yellow){
		//Yellow Start
		targetReticule.position = new Vector3 (cameraTurret.pixelWidth / 2, cameraTurret.pixelHeight / 2, 0);
		startPosition = targetReticule.position;
		}else if (red){
		//Red Start
		targetReticule.position = new Vector3 (cameraTurret.pixelWidth * 1.5f , cameraTurret.pixelHeight / 2, 0);
		startPosition = targetReticule.position;
		}

		halfWidth = cameraTurret.pixelWidth / 2;
		halfHeight = cameraTurret.pixelHeight / 2;
	}

	//---------------------------------------------------------------
	//	ResetShootBool()
	// Called to reset the shooting ability of the turret
	//
	// Param:
	//		Vector3 resetPosition - The position to reset the player to
	//		Vector3 resetRotation - The rotation to reset the player to
	//		int playerLives - The amount of lives to reset the player to
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void ResetShootBool(){
		
		canShoot = true;
	}

	//---------------------------------------------------------------
	//	WeaponSwitching()
	// Allows for weapon switching based on controller input 
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void WeaponSwitching(){
		
		if ((XCI.GetButtonDown(XboxButton.RightBumper, controller)) == true){
			if (weapon != 4){
				weapon += 1;
			}
			else {
				weapon = 1;
			}
		}
		if ((XCI.GetButtonDown(XboxButton.LeftBumper, controller)) == true) {
			if (weapon != 1){
				weapon -= 1;
			}
			else {
				weapon = 4;
			}
		}
		//Debug.Log(weapon);
	}

	//---------------------------------------------------------------
	//	Shooting()
	// Shoots out a raycast and sets the hitmarker position, then based on bumper input and current weapon applies appropriate damage
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void Shooting(){

		Ray ray = cameraTurret.ScreenPointToRay(targetReticule.position);
		float distanceOfRay = 300;

		RaycastHit hit;
		if (Physics.Raycast(ray,out hit,distanceOfRay,whatCanBeShot,QueryTriggerInteraction.Collide)){	

			//Weapon 1 - Gatling Gun
			if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && canShoot == true && weapon == 1){
				GetComponentInChildren<ParticleSystem>().Play();
				var startPos = hit.point;
				var startRot = Quaternion.LookRotation (hit.normal);
				var numberRot = Quaternion.Euler (0, hit.transform.rotation.y, 0);

				if (hit.transform.tag == "Enemy"){
					hit.collider.GetComponentInParent<MonsterTruck>().TakeDamage(turretDamage);
					GameObject GO = Instantiate (hitParticle, startPos, startRot);
					Destroy (GO, 2);

					GameObject text = Instantiate (damageText, startPos, numberRot);
					textBox = text.GetComponent<TextMesh> ();
					textBox.text = (MonsterTruck.truckDamage.ToString());
					Destroy (text, 0.5f);

				}
				if (hit.transform.tag == "Armour"){
					//Do Damage to Enemy
					hit.transform.GetComponent<Armour>().TakeDamage(turretDamage);
					GameObject GO = Instantiate (hitParticle, startPos, startRot);
					Destroy (GO, 2);

					GameObject text = Instantiate (damageText, startPos, numberRot);
					textBox = text.GetComponent<TextMesh> ();
					textBox.text = (MonsterTruck.truckDamage.ToString());
					Destroy (text, 0.5f);
				}
				if (hit.transform.tag == "Missile"){
					hit.transform.gameObject.SendMessage ("ShotDown");
					GameObject GO = Instantiate (hitParticle, startPos, startRot);
					Destroy (GO, 2);

					GameObject text = Instantiate (damageText, startPos, numberRot);
					textBox = text.GetComponent<TextMesh> ();
					textBox.text = (MonsterTruck.truckDamage.ToString());
					Destroy (text, 0.5f);
				}

				canShoot = false;
				Invoke("ResetShootBool",timeBetweenShots);
			}

			//Weapon 2 - Tether
			if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && canShoot == true && weapon == 2){
			
			}
			//Weapon 3 - Grenade Launcher
			if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && canShoot == true && weapon == 3){

			}
			//Weapon 4 - Missile Launcher
			if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && canShoot == true && weapon == 4){

			}
		}
	}

	//---------------------------------------------------------------
	//	RotatingTurret()
	// Rotates the turret based on right joystick input
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void RotatingTurret(){

		if (targetReticule.position.y <= startPosition.y - halfHeight){
			canMoveDown = false;
			canMoveUp = true;
		}

		if (targetReticule.position.y < startPosition.y + halfHeight && targetReticule.position.y > startPosition.y - halfHeight){
			canMoveDown = true;
			canMoveUp = true;
		}

		if (targetReticule.position.y >= startPosition.y + halfHeight){
			canMoveDown = true;
			canMoveUp = false;
		}

		if (targetReticule.position.x >= startPosition.x + halfWidth){
			canMoveRight = false;
			canMoveLeft = true;
		}

		if (targetReticule.position.x < startPosition.x + halfWidth && targetReticule.position.x > startPosition.x - halfWidth){
			canMoveRight = true;
			canMoveLeft = true;
		}

		if (targetReticule.position.x <= startPosition.x - halfWidth){
			canMoveRight = true;
			canMoveLeft = false;
		}
			
		rightStickY = XCI.GetAxis(XboxAxis.RightStickY, controller);
		rightStickX	= XCI.GetAxis(XboxAxis.RightStickX, controller);

		if (!canMoveDown){
			if (rightStickY < 0){
				rightStickY = 0;
			}
		}
		if (!canMoveUp){
			if(rightStickY > 0){
				rightStickY = 0;
			}
		}

		if(!canMoveRight){
			if(rightStickX > 0){
				rightStickX = 0;
			}
		}
		if(!canMoveLeft){
			if(rightStickX < 0){
				rightStickX = 0;
			}
		}

		targetReticule.position += new Vector3 (rightStickX,rightStickY , 0)* aimSpeed * Time.deltaTime;

	}
				
}
