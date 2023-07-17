using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketSniffer
{
	enum InputAction
	{
		Invalid,
		Exit = 0,
		Intercept = 1,
		StopIntercepting = -1
	}
}
