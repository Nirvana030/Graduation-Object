using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent:UnityEvent<bool>
{
}

public class Player : NetworkBehaviour
{
	[SerializeField] ToggleEvent onToggleShared;
	[SerializeField] ToggleEvent onToggleLocal;
	[SerializeField] ToggleEvent onToggleRemote;

	// Use this for initialization
	void Start ()
	{
		EnablePlayer ();
	}

	void DisablePlayer ()
	{
		onToggleShared.Invoke (false);

		if (isLocalPlayer) {
			onToggleLocal.Invoke (false);
		} else {
			onToggleRemote.Invoke (false);
		}
	}

	void EnablePlayer ()
	{
		onToggleShared.Invoke (true);

		if (isLocalPlayer) {
			onToggleLocal.Invoke (true);
		} else {
			onToggleRemote.Invoke (true);
		}
	}

	public void setLocalPlayer(){
		gameObject.tag = "LocalPlayer";
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
