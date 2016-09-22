using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Cam : MonoBehaviour {

    private Camera ca;
    public float alturaCamera = 15;
    public float anguloCamera = 0;
    public float distanciaCamera = 0;


	// MODIFICAÇÃO
	void setFromConfig(){
		string[] lines = System.IO.File.ReadAllLines (@"ConfigUNITY.txt");
		alturaCamera = Int32.Parse(lines [4]);
		distanciaCamera = Int32.Parse(lines [5]);
		anguloCamera = Int32.Parse(lines [6]);
	}

    // Use this for initialization
    void Start()
    {
		setFromConfig ();
        ca = GetComponent<Camera>();

        MudarCamera();
    }

    public void IniciarCamera() {

        
    }

    public void MudarCamera()
    {

        ca.transform.position = new Vector3(10, alturaCamera, -distanciaCamera);
        ca.transform.rotation = Quaternion.AngleAxis(anguloCamera, Vector3.right);

    }

}
