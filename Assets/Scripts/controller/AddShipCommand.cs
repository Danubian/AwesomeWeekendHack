/// An Asynchronous Command
/// ============================
/// This demonstrates how to use a Command to perform an asynchronous action;
/// for example, if you need to call a web service. The two most important lines
/// are the Retain() and Release() calls.

using System;
using System.Collections;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace strange.examples.signals
{
	//Again, we extend Command, not EventCommand
	public class AddShipCommand : Command
	{
		
		[Inject]
		public ISpaceShipModel model{get;set;}
		
		[Inject]
		public IAddShipService service{get;set;}
		
		//We inject the signal instead of using EventDispatcher
		[Inject]
		public ColorChangedSignal scoreChangedSignal{get;set;}
		
		public AddShipCommand()
		{
		}
		
		public override void Execute()
		{
			Retain ();

			service.fulfillSignal.AddListener(onComplete);
			service.Request(model.name, model.color);
		}
		
		//The payload is now a type-safe string
		private void onComplete(string url)
		{
			service.fulfillSignal.RemoveListener(onComplete);
			
			Release ();
		}
	}
}

