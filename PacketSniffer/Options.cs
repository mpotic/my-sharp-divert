using MySharpDivert;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PacketSniffer
{
	internal class Options : IOptions
	{
		ISharpDivertApi sharpDivertApi = new SharpDivertApi();

		IInterceptor interceptor;

		Dictionary<InputAction, Action> actions = new Dictionary<InputAction, Action>();

		public Options()
		{
			interceptor = new Interceptor(sharpDivertApi);
			actions.Add(InputAction.Invalid, () => { DisplayMenu(); });
			actions.Add(InputAction.Intercept, () => { InterceptAndForward(); });
			actions.Add(InputAction.StopIntercepting, () => { StopIntercepting(); });
		}

		public void Begin()
		{
			DisplayMenu();

			while (true)
			{
				InputAction inputAction = ReadInput();
				if(inputAction == InputAction.Exit)
				{
					return;
				}

				actions[inputAction].Invoke();
			}
		}

		private InputAction ReadInput()
		{
			string line = Console.ReadLine();
			if(!Enum.TryParse(line, out InputAction action))
			{
				return InputAction.Invalid;
			}

			return action;
		}

		public void InterceptAndForward()
		{
			Task.Run(() => { interceptor.InterceptAndForward(); });
		}

		public void StopIntercepting()
		{
			Console.WriteLine("Waiting for interception to stop...");
			interceptor.StopIntercepting();
			Console.WriteLine("Successfully stopped.");
		}

		public void InterceptAndModify()
		{
			throw new NotImplementedException();
		}

		private void DisplayMenu()
		{
			Console.WriteLine("Press:\n\t* 1 to intercept and forward messages." +
				"\n\t* Press -1 to stop." +
				"\n\t* Press 0 to exit." +
				"\n\t* Press any other key for menu.");
		}
	}
}
