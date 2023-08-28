using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySharpDivert.Native.Enums
{
	internal enum WinDivertShutdown
	{
		WINDIVERT_SHUTDOWN_RECV = 1,
		WINDIVERT_SHUTDOWN_SEND = 2,
		WINDIVERT_SHUTDOWN_BOTH = WINDIVERT_SHUTDOWN_RECV | WINDIVERT_SHUTDOWN_SEND
	}
}
