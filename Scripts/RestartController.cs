using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour {

    bool right = false;
    bool left = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(right == true && left == true)
        {
            SceneManager.LoadScene("Scene_001");
        }
       
	}

    public void SetRightTrue()
    {
        right = true;
    }
    public void SetRightFalse()
    {
        right = false;
    }
    public void SetLeftTrue()
    { 
        left = true;
    }
    public void SetLeftFalse()
    {
        left = false;
    }
}
