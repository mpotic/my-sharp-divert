using MySharpDivert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PacketSniffer
{
	internal class Options : IOptions
	{
		ISharpDivertApi sharpDivertApi = new SharpDivertApi();

		IInterceptor interceptor;

		Dictionary<InputAction, Action> actions = new Dictionary<InputAction, Action>();

		List<string> inputArgs;

		public Options()
		{
			interceptor = new Interceptor(sharpDivertApi);
			actions.Add(InputAction.Invalid, () => { DisplayMenu(); });
			actions.Add(InputAction.Forward, () => { InterceptAndForward(); });
			actions.Add(InputAction.Modify, () => { InterceptAndModify(); });
			actions.Add(InputAction.Stop, () => { StopIntercepting(); });
			actions.Add(InputAction.Menu, () => { DisplayMenu(); });
			actions.Add(InputAction.Cls, () => { ClearConsole(); });
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
			string[] input = Console.ReadLine().Trim().Split(' ');
			string command = input[0].ToLower();
			if (!Enum.TryParse(command, true, out InputAction action))
			{
				return InputAction.Invalid;
			}

			inputArgs = input.Skip(1).ToList(); // Get arguments.
			inputArgs.RemoveAll(string.IsNullOrWhiteSpace); // Remove white spaces and empty strings from arguments.

			return action;
		}

		public void InterceptAndForward()
		{
			string filter = string.Join(' ', inputArgs);
			Task.Run(() => { interceptor.PrepareInterception(filter, InterceptionMode.Forward); });
		}

		public void InterceptAndModify()
		{
			string filter = string.Join(' ', inputArgs);
			Task.Run(() => { interceptor.PrepareInterception(filter, InterceptionMode.Modify); });
		}

		public void StopIntercepting()
		{
			interceptor.StopIntercepting();
		}

		public void ClearConsole()
		{
			Console.Clear();
		}

		private void DisplayMenu()
		{
			Console.WriteLine(
				"- - - - - - - - - -  M E N U  - - - - - - - - - -\n" +
				"Intercept and forward: \"forward {filter}\"\n" +
				"Intercept, modify and forward: \"modify {filter}\"\n" +
				"Stop intercepting: \"stop\"\n" +
				"Menu: \"menu\"\n" +
				"Clear console: \"cls\"\n" +
				"- - - - - - - - - - - - - - - - - - - - - - - - -");
		}
	}
}
