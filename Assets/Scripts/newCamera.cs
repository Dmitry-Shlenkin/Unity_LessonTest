using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newCamera : MonoBehaviour
{
	public LayerMask maskObstacle;
	public Transform target;
	public Vector3 offset;
	public float sensitivity = 2; // чувствительность мышки
	public float limit = 80; // ограничение вращения по Y
	public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
	public float zoomMax = 7; // макс. увеличение
	public float zoomMin = 3; // мин. увеличение
	private float X, Y;

	void Start()
	{
		limit = Mathf.Abs(limit);
		if (limit > 90) limit = 90;

		offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 1.5f);
		transform.position = target.position + offset;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
		else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
		offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

		X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		Y += Input.GetAxis("Mouse Y") * sensitivity;
		Y = Mathf.Clamp(Y, -limit, limit);
		transform.localEulerAngles = new Vector3(-Y, X, 0);
		transform.position = transform.localRotation * offset + target.position;

		RaycastHit raycastHit;
		if (Physics.Raycast(target.position, transform.position - target.position, out raycastHit, Vector3.Distance(transform.position, target.position), maskObstacle))
		{
			transform.position = raycastHit.point + new Vector3(0, 0.1f, 0);
			transform.LookAt(target);
		}
	}
}
