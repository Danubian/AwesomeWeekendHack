using System;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace strange.examples.signals
{
	public interface IAddShipService
	{
		void Request(string name, Color shipColor);
		//Instead of an EventDispatcher, we put the actual Signals into the Interface
		FulfillAddShipRequestSignal fulfillSignal{get;}
	}
}

