namespace MySharpDivert
{
    public interface ISharpDivertApi
	{
		IResponse Open(string filter);

		IReceiveResponse ReceiveSinglePacket();

		IResponse SharpDivertSend();

		IResponse SharpDivertClose();
	}
}
