using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class VehicleCameraControl : MonoBehaviour
{

	public Transform playerCar;
	private Rigidbody playerRigid;
	public float distance = 10.0f;
	public float height = 5.0f;
	private float defaultHeight = 0f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public float defaultFOV = 60f;
	public float zoomMultiplier = 0.3f;
    private float prevCarHeight;

	//read only
		
	void Start(){
		
		// Early out if we don't have a target
		if (!playerCar)
			return;

        prevCarHeight = playerCar.position.y;
		
		playerRigid = playerCar.GetComponent<Rigidbody>();

	}
	
	void Update(){
		if (!playerRigid)
			return;
		if(playerRigid != playerCar.GetComponent<Rigidbody>())
			playerRigid = playerCar.GetComponent<Rigidbody>();

		GetComponent<Camera>().fieldOfView = defaultFOV + playerRigid.velocity.magnitude * zoomMultiplier;
		
	}
	
	void FixedUpdate (){
		
		// Early out if we don't have a target
		if (!playerCar || !playerRigid)
			return;

		//calculates speed in local space. positive if going forward, negative if reversing
		float speed = (playerRigid.transform.InverseTransformDirection(playerRigid.velocity).z) * 3f;
		
		// Calculate the current rotation angles.
		Vector3 wantedRotationAngle = playerCar.eulerAngles;
        float wantedHeight = playerCar.position.y + height - Mathf.Min(1, (playerCar.position.y - prevCarHeight)*10);
        prevCarHeight = playerCar.position.y;
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		
		if(speed < -5)
			wantedRotationAngle.y = playerCar.eulerAngles.y + 180;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle.y, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = playerCar.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		transform.position = new Vector3(transform.position.x, currentHeight + defaultHeight, transform.position.z);

		// Always look at the target
		transform.LookAt (playerCar);


		float alcohol_level = playerCar.GetComponentInChildren<AlcoholNoiseGenerator>().alcohol_level;
		if(alcohol_level > 0) {
			BlurOptimized blur = GetComponent<BlurOptimized>();
			blur.enabled = true;
			blur.blurSize = 1.0f + 3 * alcohol_level;
			if(alcohol_level > 0.6) {
				blur.blurIterations = 3;
			} else {
				blur.blurIterations = 2;
			}
		} else {
			GetComponent<BlurOptimized>().enabled = false;
		}

	}



}