using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMOPlayerCtrl : MonoBehaviour {

	public Transform Playercm, character, centerPoint;
	PlayerData data;
	public bool CanMove;//動けるかどうか
	private float mouseX, mouseY;
	public float mouseSmooth = 1.0f;
	public float mouseYPosition = 1f;

	private float moveFB, moveLR;
	public float jumpPower = 8.0f;
	public float moveSpeed = 2f;
	private float yyy = 0;


	private float zoom;
	public float zoomSpeed = 2;

	public float zoomMin = -2f;
	public float zoomMax = -10f;

	public float rotationSpeed = 5f;

	public float gravity = 9.8f;

	// Use this for initialization
	void Start () {
		CanMove = true;
		zoom = -2;
		data = character.GetComponent<PlayerData>();
	}
	
	// Update is called once per frame
	void Update () {

		zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;/*ホイールでズームする*/
		if (zoom > zoomMin)
			zoom = zoomMin;
		if (zoom < zoomMax)
			zoom = zoomMax;

		if (data.nowHP <= 0)
		{
			CanMove = false;
		}

		Playercm.transform.localPosition = new Vector3(0, 1.1f - ( (zoom+2) /3), zoom);


        mouseX += Input.GetAxis("Mouse X") * mouseSmooth;/*マウス感度実装ならここを変える*/
        mouseY -= Input.GetAxis("Mouse Y") * mouseSmooth;
		

		mouseY = Mathf.Clamp(mouseY, -60f, 60f);
		Playercm.LookAt(centerPoint);
		centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
		if (CanMove)//生きてる時だけ
		{
			character.localRotation = Quaternion.Euler(0, mouseX, 0);
			moveFB = Input.GetAxis("Vertical") * moveSpeed;
			moveLR = Input.GetAxis("Horizontal") * moveSpeed;
		}

		


		Vector3 movement = new Vector3(moveLR, 0.0f, moveFB);/*y軸初期化*/
		movement.y = yyy;
		if (CanMove)
			movement = character.rotation * movement;
		if (character.GetComponent<CharacterController>().isGrounded)
		{
			movement.y = 0;

			if (Input.GetKey(KeyCode.Space) && CanMove)//スペース押したとき
			{
				movement.y += jumpPower;
			}

		}
		
		movement.y -= gravity * Time.deltaTime;//重力計算
		yyy = movement.y;
		character.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
		
		centerPoint.position = new Vector3(character.position.x, character.position.y + mouseYPosition, character.position.z);

        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") < 0)
        {

            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);

            character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);

        }


    }
}
