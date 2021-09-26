using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public List<WheelCollider> throttleWheels = new List<WheelCollider>();
    public List<WheelCollider> steeringWheels = new List<WheelCollider>();
    public float throttleCoefficient = 20000f;
    public float maxTurn = 20f;
    float giro = 0f;
    float acel = 1f;

    public enum Lado
    {
        Izq,
        Der
    }

    public Lado lado;
	// Update is called once per frame
	void FixedUpdate () {
        foreach (var wheel in throttleWheels) {
            wheel.motorTorque = throttleCoefficient * Time.fixedDeltaTime * acel;
        }
        foreach (var wheel in steeringWheels)
        {
            if (lado == Lado.Izq)
                wheel.steerAngle = maxTurn * InputManager.Instance.GetAxis("Horizontal1");
            else if (lado == Lado.Der)
                wheel.steerAngle = maxTurn * InputManager.Instance.GetAxis("Horizontal2");
        }
    }

    public void SetGiro(float giro) {
        this.giro = giro;
    }
    public void SetAcel(float val) {
        acel = val;
    }
}
