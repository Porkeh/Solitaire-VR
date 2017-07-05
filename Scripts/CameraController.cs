using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {



    Vector3 Screenorigin;
    Vector3 HandPos = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 HandDiff = new Vector3(0.0f, 0.0f, 0.0f);
    Vector2 xyRotate = new Vector2(0, 0);
    Camera camera;

   GameObject palm;
    static float rotateVelocity = 20.0f;

    bool fist;

	// Use this for initialization
	void Start () {

        camera = Camera.main;// GetComponent<Camera>();
        HandPos = camera.WorldToScreenPoint(palm.transform.position);
        
        Screenorigin = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2);

	}
	
	// Update is called once per frame
	void Update () {


        if (fist)
        {
            Vector3 prevPos = HandPos;
        HandPos = camera.WorldToScreenPoint(palm.transform.position);
        HandPos.z = 0;
        //     HandDiff.x = HandPos.x - Screenorigin.x;
        //   HandDiff.y= HandPos.y - Screenorigin.y;
        //   HandDiff.z = HandPos.z- Screenorigin.z;
        //Debug.Log(HandDiff.x);*/


        if (HandPos.y < camera.pixelHeight / 4)
        {
            Debug.Log("low");
            xyRotate.y = 1;
        }


        if (HandPos.x < camera.pixelWidth / 4)
        {
            Debug.Log("l");
            xyRotate.x = -1;
        }

        if (HandPos.x > camera.pixelWidth / 4 * 3)
        {
            Debug.Log("r");
            xyRotate.x = 1;
        }

        if (HandPos.y > camera.pixelHeight / 4 * 3)
        {
            Debug.Log("hi");
            xyRotate.y = -1;
        }


       
            Rotate();
        }

        xyRotate.Set(0, 0);
	}

   
    public void setFistTrue(GameObject palm)
    {
        this.palm = palm;
        fist = true;
    }

    public void setFistFalse()
    {
        fist = false;
        palm = null;
    }

    public void Rotate()
    {

        camera.transform.Rotate(new Vector3(0, 1, 0), xyRotate.x * rotateVelocity *Time.deltaTime);
       
    }
}
