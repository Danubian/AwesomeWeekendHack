/// No change

using System;
using UnityEngine;

public class AwesomeSpaceShipModel : ISpaceShipModel
{
	[Inject]
	public ColorChangedSignal colorChangedSignal{ get; set;}
	
	private Color m_color;
	public Color color
	{
		get
		{
			return m_color;
		}
		set
		{
			m_color = value;
			colorChangedSignal.Dispatch(m_color);
		}
	}

	private string m_name = "temp";
	public string name
	{
		get
		{
			return m_name;
		}
		set
		{
			m_name = value;
		}
	}
	//public string data {get;set;}
	
	public AwesomeSpaceShipModel ()
	{

	}
}

