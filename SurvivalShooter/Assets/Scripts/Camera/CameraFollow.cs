using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraFollow : MonoBehaviour
{
	
	Transform target;
	public float smoothing = 5f;
	public bool start = false;
	GameObject[] players;
	
	

	private Vector3 offset;

	void Start()
	{
		
		
	}

	void FixedUpdate()
	{
		if (start == false)
		{
		
			players = GameObject.FindGameObjectsWithTag("Player");

			for(int i = 0; i < players.Length; i++)
            {
				if (players[i].GetComponent<PlayerMovement>().isLocalPlayer)
                {
					target = players[i].transform;
					offset = transform.position - target.position;
					start = true;
				}
            }
			

		}

		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
