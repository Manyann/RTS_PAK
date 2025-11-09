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
 
    private void MoveCamera()
    {
        float positionX = Input.mousePosition.x;
        float positionY = Input.mousePosition.y;
        Vector3 movement = Vector3.zero;

        if (positionX >= 0 && positionX < ResourceManager.ScrollWidth)
        {
            movement.x -= ResourceManager.ScrollSpeed;
        }
        else if (positionX <= Screen.width && positionX > Screen.width - ResourceManager.ScrollWidth)
        {
            movement.x += ResourceManager.ScrollSpeed;
        }

        if (positionY >= 0 && positionY < ResourceManager.ScrollWidth)
        {
            movement.z -= ResourceManager.ScrollSpeed;
        }
        else if (positionY <= Screen.height && positionY > Screen.height - ResourceManager.ScrollWidth)
        {
            movement.z += ResourceManager.ScrollSpeed;
        }

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin + movement;

        destination.y = Mathf.Clamp(destination.y, ResourceManager.MinCameraHeight, ResourceManager.MaxCameraHeight);

        if (destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(
                origin,
                destination,
                Time.deltaTime * ResourceManager.ScrollSpeed
            );
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float newSize = Camera.main.orthographicSize - scroll * ResourceManager.ScrollSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(newSize,ResourceManager.MinCameraHeight, ResourceManager.MaxCameraHeight);
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
