namespace MySharpDivert.Native
{
	internal enum WinDivertEvent
	{
		NetworkPacket = 0,
		FlowEstablished,
		FlowDeleted,
		SocketBind,
		SocketConnect,
		SocketListen,
		SocketAccept,
		SocketClose,
		ReflectOpen,
		ReflectClose
	}
}
