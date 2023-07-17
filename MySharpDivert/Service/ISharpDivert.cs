namespace MySharpDivert
{
	internal interface ISharpDivert
	{
		IResponse Open(string filter);

		IReceiveResponse ReceiveSinglePacket();
		
		void CloseHandle();
	}
}
