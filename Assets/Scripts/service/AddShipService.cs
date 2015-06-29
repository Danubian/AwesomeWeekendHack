/// The service, now with signals

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace strange.examples.signals
{
	public class AddShipService : IAddShipService
	{
		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{get;set;}
		
		//The interface demands this signal
		[Inject]
		public FulfillAddShipRequestSignal fulfillSignal{get;set;}
		
		public AddShipService ()
		{
		}

		public void Request(string name, Color shipColor)
		{
			ShipJSONModel ship = new ShipJSONModel (name, shipColor);
			
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
			
			MonoBehaviour root = contextView.GetComponent<SignalsRoot>();
			root.StartCoroutine(WaitForRequest(www));
		}
		
		private IEnumerator WaitForRequest(WWW www)
		{	
			yield return www;
			
			// check for errors
			if (www.error == null)
			{
				Debug.Log("WWW Ok!: " + www.text);

				//Pass back some fake data via a Signal
				fulfillSignal.Dispatch(www.text);
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

			public ShipJSONModel (string name, Color color)
			{
				this.id = name;
				this.red = color.r.ToString();
				this.green = color.g.ToString();
				this.blue = color.b.ToString();
				this.alpha = color.a.ToString();
			}
		}
	}


}

