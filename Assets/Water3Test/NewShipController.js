var maxSpeed = 0.1;
var sailCount = 4;
var rotateSpeed = 1.0;
private var currentSail = 0.0;
private var currentSpeed = 0.0;
private var rotation : Vector3 = Vector3.zero;
private var paused = 0;

function Update () {
 	if (Input.GetAxis("Horizontal") && currentSpeed > 0){
		rotation.y += Input.GetAxis("Horizontal") * rotateSpeed;
		rotation.x = 0;
		rotation.z = 0;
 	}
	
	transform.eulerAngles = rotation;
	
	if (Input.GetAxis("Vertical") && currentSpeed <= maxSpeed && !paused){
		if (Input.GetAxis("Vertical") > 0.0){
			currentSpeed +=  (maxSpeed/sailCount);
			if (currentSpeed > maxSpeed){
				currentSpeed = maxSpeed;
			}
		}else if (currentSpeed != 0.0){
			currentSpeed -=  (maxSpeed/sailCount);
			if (currentSpeed < 0.0){
				currentSpeed = 0;
			}
		}
		paused = 1;
	}else if (paused && !Input.GetAxis("Vertical")){
		paused = 0;
	}
	
	//transform.Translate(Vector3.forward * Time.deltaTime);
	transform.Translate(0, 0, currentSpeed);
	
}