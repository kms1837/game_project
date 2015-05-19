using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	private Vector2 playerPosition;
	private float speed;
	// Use this for initialization
	void Start () {
		speed = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		float positionX = Input.GetAxisRaw("Horizontal") * speed;
		float positionY = this.transform.position.y;
		float positionZ = this.transform.position.z;

		this.transform.Translate(positionX, 0, 0);
	}
}
