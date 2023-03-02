using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] float shiftingForce = 0.002f;
    private Vector3 originPosition;

    void Start()
    {
        originPosition = transform.position;
    }

    void Update()
    {
        ShiftForMouse();
    }

    private void ShiftForMouse()
	{
        Vector3 dif = new Vector3(
            Mathf.Clamp(Input.mousePosition.x - Screen.width / 2, -Screen.width / 2, Screen.width / 2), 0,
            Mathf.Clamp(Input.mousePosition.y - Screen.height / 2, -Screen.height / 2, Screen.height / 2));
        transform.position = originPosition - dif * shiftingForce;
	}
}
