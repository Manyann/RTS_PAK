using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resource;

public class UserInput : MonoBehaviour {
 
    private Player player;
 
    // Use this for initialization
    void Start () {
        player = transform.root.GetComponent< Player >();
    }
 
    // Update is called once per frame
    void Update () {
        MoveCamera();
        RotateCamera();
    }
 
    private void MoveCamera() {
        float positionX = Input.mousePosition.x;
        float positionY = Input.mousePosition.y;
        Vector3 movement = new Vector3(0,0,0);
 
        // Horizontal
        if(positionX >= 0 && positionX < ResourceManager.ScrollWidth) {
            movement.x -= ResourceManager.ScrollSpeed;
        } else if(positionX <= Screen.width && positionX > Screen.width - ResourceManager.ScrollWidth) {
            movement.x += ResourceManager.ScrollSpeed;
        }
        // Vertical
        if(positionY >= 0 && positionY < ResourceManager.ScrollWidth) {
            movement.z -= ResourceManager.ScrollSpeed;
        } else if(positionY <= Screen.height && positionY > Screen.height - ResourceManager.ScrollWidth) {
            movement.z += ResourceManager.ScrollSpeed;
        }
 
        // Gestion du zoom
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;
        movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");
 
        // Affectation de la nouvelle position
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;
 
        // Zoom dans les max ranges
        if(destination.y > ResourceManager.MaxCameraHeight) {
            destination.y = ResourceManager.MaxCameraHeight;
        } else if(destination.y < ResourceManager.MinCameraHeight) {
            destination.y = ResourceManager.MinCameraHeight;
        }

        if(destination != origin) {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }

    private void RotateCamera(){
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;
        
        // Gestion de ALT appuyé pour trigger la rotation avec combo click droit
        if((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1)) {
            destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
        }
        
        if(destination != origin) {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }
    }
}
