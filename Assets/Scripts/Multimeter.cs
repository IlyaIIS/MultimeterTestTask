using System;
using TMPro;
using UnityEngine;

public class Multimeter : MonoBehaviour
{
	[SerializeField] private float resistance = 1000;
	[SerializeField] private float power = 400;
	private float elCurrent;
	private float voltage;
	private float voltageAC;

	[SerializeField] private GameObject displayTextObj;
	[SerializeField] private GameObject interfaceTextObj;
	private TextMeshProUGUI displayText;
    private TextMeshProUGUI interfaceText;

    void Start()
    {
		displayText = displayTextObj.GetComponent<TextMeshProUGUI>();
		interfaceText = interfaceTextObj.GetComponent<TextMeshProUGUI>();
	}

	private void DefineValues()
	{
		elCurrent = MathF.Round(MathF.Sqrt(power / resistance), 2);
		voltage = MathF.Round(MathF.Sqrt(power * resistance), 2);
		voltageAC = 0.01f;
	}

    public void DisplayValues(ArrowState state)
	{
		DefineValues();

		switch (state)
		{
			case ArrowState.Default:
				displayText.text = "0";
				interfaceText.text = "0\n0\n0\n0";
				break;
			case ArrowState.Volt:
				displayText.text = voltage.ToString();
				interfaceText.text = string.Format("{0}\n0\n0\n0", voltage);
				break;
			case ArrowState.Ampere:
				displayText.text = elCurrent.ToString();
				interfaceText.text = string.Format("0\n{0}\n0\n0", elCurrent);
				break;
			case ArrowState.VoltAC:
				displayText.text = voltageAC.ToString();
				interfaceText.text = string.Format("0\n0\n{0}\n0", voltageAC);
				break;
			case ArrowState.Ohm:
				displayText.text = resistance.ToString();
				interfaceText.text = string.Format("0\n0\n0\n{0}", resistance);
				break;
			default:
				throw new NotImplementedException();
		}
	}
}
