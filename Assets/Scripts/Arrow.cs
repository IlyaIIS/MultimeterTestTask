using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 3;
	private ArrowState state = ArrowState.Default;
    public ArrowState State { get { return state; } private set { state = value; multimeter.DisplayValues(state); } }
    private bool isMouseEnter;
	private float targetAngle = 133;
	private bool isRotating { get { return MathF.Abs(targetAngle - transform.localEulerAngles.z) > 0.001f; } }

	private Outline outline;
	private Light light;
	private Multimeter multimeter;

	private void Start()
	{
		outline = GetComponentInChildren<Outline>();
		light = GetComponentInChildren<Light>();
		multimeter = GetComponentInParent<Multimeter>();
	}

	private void Update()
	{
		if (Input.mouseScrollDelta.y != 0 && isMouseEnter && !isRotating)
		{
			int stateCount = Enum.GetValues(typeof(ArrowState)).Length;
			SetTargetAngle(-Input.mouseScrollDelta.y);
			State = (ArrowState)(((int)State - (int)Input.mouseScrollDelta.y + stateCount) % stateCount);
		}

		RotateToTarget();
	}

	private void SetTargetAngle(float direction)
	{
		int stateCount = Enum.GetValues(typeof(ArrowState)).Length;
		float rotationAngle = 360 / (stateCount-1);
		if (State == ArrowState.Default || 
			(State == (ArrowState)1 && direction < 0) ||
			(State == (ArrowState)(stateCount-1) && direction > 0))
		{
			targetAngle += rotationAngle / 2 * direction;
		}
		else
		{
			targetAngle += rotationAngle * direction;
		}

		targetAngle = (targetAngle + 360) % 360;
	}

	private void RotateToTarget()
	{
		if (MathF.Abs(targetAngle - transform.localEulerAngles.z) > rotationSpeed * 2)
			transform.Rotate(0, 0, rotationSpeed * GetDirectionToRotation(transform.localEulerAngles.z, targetAngle));
		else
			transform.localEulerAngles = new Vector3(0, 0, targetAngle);
	}

	private int GetDirectionToRotation(float fromAngle, float toAngle)
	{
		float dif = (fromAngle - toAngle) / 360 * MathF.PI * 2;
		return Math.Abs(Math.Sign(dif) + Math.Sign(Math.Abs(dif) - MathF.PI)) - 1;
	}

	private void OnMouseEnter()
	{
        isMouseEnter = true;

		outline.OutlineWidth = 10;
		light.intensity = 1;
	}
	private void OnMouseExit()
	{
        isMouseEnter = false;

		outline.OutlineWidth = 0;
		light.intensity = 0;
	}
}

public enum ArrowState
{
    Default,
	Volt,
	VoltAC,
	Ampere,
	Ohm
}
