using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

	float moveSpeed = 5f;
	Rigidbody m_rigidbody;

	float dash_v = 20f;
	float dash_multiper;
	float Jump_multiper=5f;
	Vector3 Move_direct;
	Vector3 Look_direct;
	float dash_max = 1f;
	float dash_now;

	float MouseX;
	float MouseY;

	bool Grounded;
	Vector3 m_GroundNormal;
	float m_GroundCheckDistance=0.3f;

	// Use this for initialization
	void Start ()
	{
		//gameObject.GetComponent<Animator> ().applyRootMotion = true;
		m_rigidbody = gameObject.GetComponent<Rigidbody> ();
		Move_direct = Vector3.forward;
		Look_direct = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 horizon_V = new Vector3 (m_rigidbody.velocity.x, 0f, m_rigidbody.velocity.z);

		//Look direction update
		Look_direct = new Vector3 (Input.mousePosition.x - Screen.width / 2f, 0f, Input.mousePosition.y - Screen.height / 2f).normalized;
		Vector3 m_eulerAngle = Quaternion.FromToRotation (Vector3.forward, Look_direct).eulerAngles;
		gameObject.transform.rotation = Quaternion.Euler (m_eulerAngle);

		GroundCheck ();
	}

	void GroundCheck ()
	{
		RaycastHit hitInfo;
		#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine (transform.position + (Vector3.down * 0.4f), transform.position + (Vector3.down * 0.4f)+ (Vector3.down * m_GroundCheckDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast (transform.position + (Vector3.down * 0.4f), Vector3.down, out hitInfo, m_GroundCheckDistance)) {
			m_GroundNormal = hitInfo.normal;
			Grounded = true;
		} else {
			Grounded = false;
			m_GroundNormal = Vector3.up;
		}

	}

	public void Move (float h, float v, bool[] skillPress)
	{
		m_Move (h, v);
		for (int i = 0; i < skillPress.Length; i++) {
			if (skillPress [0]) { 
				dash_multiper = CrossPlatformInputManager.GetAxis ("Character1_dash");
				dash_now += Time.fixedDeltaTime;
				if (dash_now > dash_max) {
					dash_now = dash_max;
				}
				if (dash_now < dash_max) {
					m_rigidbody.velocity = Look_direct * dash_v * dash_multiper;
				}
			} else {
				dash_now = 0f;
			}
			if (skillPress [1]) { 
				if (Grounded) {
					m_rigidbody.velocity = Vector3.up * Jump_multiper;
				}
			}
		}
	}

	void m_Move (float h, float v)
	{
		Vector3 m_direct = Camera.main.transform.forward * v + Camera.main.transform.right * h;
		if (m_direct.magnitude > 1f) {
			m_direct = m_direct.normalized;
		}
		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (moveSpeed * m_direct.x, gameObject.GetComponent<Rigidbody> ().velocity.y, moveSpeed * m_direct.z);
	}
}
