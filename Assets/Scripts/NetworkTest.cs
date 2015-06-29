
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using Newtonsoft.Json;
//using Assets;
using System;

public class NetworkTest : MonoBehaviour {
	//public Text textOutput;
	//public string url;
	//public int port;
	//public string route;
	
	// Use this for initialization
//	IEnumerator Start()
//	{
//		WWW www = new WWW(url + ":" + port + route);
//		yield return www;
//		Debug.Log(www.text);
//	}

	void Start()
	{
		ShipJSONModel ship = new ShipJSONModel();
		ship.id = "UnityTest2";
		float temp = 1f;
		ship.red = temp.ToString();
		ship.green = "1";
		ship.blue = "0";
		ship.alpha = "0";

		string jsonString = "{ " +
			"\"id\" : \"" + ship.id + "\", " +
			"\"red\" : \"" + ship.red + "\", " +
			"\"green\" : \"" + ship.green + "\", " +
			"\"blue\" : \"" + ship.blue + "\", " +
			"\"alpha\" : \"" + ship.alpha + "\" " +
			"}";
		//string jsonString = JsonConvert.SerializeObject(ship);
		byte[] rawData = System.Text.Encoding.ASCII.GetBytes(jsonString);

		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");
		
		string url = "54.173.208.168/ship";
		WWW www = new WWW (url, rawData, headers);
		//WWW www = new WWW (url);//, rawData);

		StartCoroutine(WaitForRequest(www));
	}
	
	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.text);
		}
		else
		{
			Debug.Log("WWW Error: " + www.error);
		}
	}

	class ShipJSONModel
	{
		public string id;
		public string red;
		public string green;
		public string blue;
		public string alpha;
	}

}