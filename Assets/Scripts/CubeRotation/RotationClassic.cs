using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationClassic : MonoBehaviour {

    public float Value;

	// Update is called once per frame
	void Update () {
        var quat =  transform.rotation.normalized;
        var angle = Quaternion.AngleAxis(Value * Time.deltaTime, Vector3.up);
        transform.rotation = quat * angle;
    }
}
