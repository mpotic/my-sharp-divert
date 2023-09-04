using MySharpDivert;
using MySharpDivert.Native;
using MySharpDivert.Native.Enums;
using PacketSniffer.Enums;
using PacketSniffer.Workers.ModificationCommands;
using System;
using System.Collections.Generic;

namespace PacketSniffer
{
	internal class Modifier : IInterceptionProcessor
	{
		ISharpDivertApi sharpDivert;

		Dictionary<SenderCode, Dictionary<FunctionCode, IPacketModifier>> senderCommands;

		public Modifier(ISharpDivertApi sharpDivert)
		{
			SetupDictsForActionMapping();
			this.sharpDivert = sharpDivert;
		}

		private void SetupDictsForActionMapping()
		{
			Dictionary<FunctionCode, IPacketModifier> requestMapping = new Dictionary<FunctionCode, IPacketModifier>
			{
				{ FunctionCode.ReadHolding, new ReadRequestModifier() },
				{ FunctionCode.ReadCoils, new ReadRequestModifier() },
				{ FunctionCode.ReadAnalogInputs, new ReadRequestModifier() },
				{ FunctionCode.ReadDiscreteInputs, new ReadRequestModifier() },
				{ FunctionCode.WriteHolding, new WriteAnalogRequestModifier() },
				{ FunctionCode.WriteCoils, new WriteDiscreteRequestModifier() }
			};

			Dictionary<FunctionCode, IPacketModifier> responseMapping = new Dictionary<FunctionCode, IPacketModifier>
			{
				{ FunctionCode.ReadHolding, new ReadAnalogResponseModifier() },
				{ FunctionCode.ReadCoils, new ReadDiscreteResponseModifier() },
				{ FunctionCode.ReadAnalogInputs, new ReadAnalogResponseModifier() },
				{ FunctionCode.ReadDiscreteInputs, new ReadDiscreteResponseModifier() }
			};

			senderCommands = new Dictionary<SenderCode, Dictionary<FunctionCode, IPacketModifier>>()
			{
				{ SenderCode.ProxyToMaster, responseMapping },
				{ SenderCode.ProxyToSlave, requestMapping }
			};
		}

		public void ProcessInterceptedMessage(IMessage message)
		{
			IMessage modifiedMessage = new Message();
			bool successfullyModified = Modify(message, modifiedMessage);

			if (successfullyModified)
			{
				sharpDivert.SendSinglePacket(modifiedMessage.Packet, message.Address);
			}
			else
			{
				throw new Exception("Failed to modify the message!");
			}
		}

		public bool Modify(IMessage message, IMessage modifiedMessage)
		{
			if (!senderCommands.TryGetValue(message.Sender, out var functionCodeCommands) ||
				!functionCodeCommands.TryGetValue(message.FuncCode, out IPacketModifier modifier))
			{
                Console.WriteLine("Failed to find the modify command!");

                return false;
			}

			modifier.Prepare(message);
			modifier.RandomizeValues();
			WinDivertAddress modifiedAddress = message.Address;

			IPacketResponse modifiedPacketResponse = sharpDivert.CalculateChecksum(
				modifier.Packet, modifiedAddress, ChecksumCalcFlags.NONE);

			if (!modifiedPacketResponse.IsSuccessful)
			{
				Console.WriteLine(modifiedPacketResponse.ErrorMessage);

				return false;
			}

			if (!modifiedMessage.PopulateMessage(modifiedPacketResponse))
			{
				Console.WriteLine("Failed to populate the modified message!");

				return false;
			}

			Console.WriteLine(modifiedMessage.ToStringWithCustomTitle("MODIFIED MESSAGE"));

			return true;
		}
	}
}
