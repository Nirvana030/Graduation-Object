using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	GameObject LocalPlayer;
	public Vector3 CameraPos_offSet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (LocalPlayer == null) {
			LocalPlayer = GameObject.FindGameObjectWithTag ("LocalPlayer");
		}
		if (LocalPlayer != null) {
			Vector3 Player_Pos = LocalPlayer.transform.position + CameraPos_offSet;
			gameObject.transform.position = new Vector3 (Player_Pos.x, gameObject.transform.position.y, Player_Pos.z);
		}
	}

}
