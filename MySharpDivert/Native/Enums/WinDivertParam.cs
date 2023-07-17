using System;

namespace MySharpDivert.Native
{
	/// <summary>
	/// Generic configuration params for an opened WinDivert handle.
	/// </summary>
	[Flags]
    public enum WinDivertParam : uint
    {
        /// <summary>
        /// This option represents the maximum length of the packet queue for <seealso cref="WinDivert.WinDivertRecv(IntPtr, WinDivertBuffer, ref WinDivertAddress, ref uint)" />.
        /// </summary>
        /// <remarks>
        /// Currently the default value is 2048, the minimum is 16, and the maximum is 16384.
        /// </remarks>
        QueueLen = 0,

        /// <summary>
        /// This option represents the minimum time, in milliseconds, a packet can be queued before
        /// it is automatically dropped.
        /// </summary>
        /// <remarks>
        /// Packets cannot be queued indefinitely, and ideally, packets should be processed by the
        /// application as soon as is possible. Note that this sets the minimum time a packet can be
        /// queued before it can be dropped. The actual time may be exceed this value. Currently the
        /// default value is 1000 (1s), the minimum is 20 (20ms), and the maximum is 8000 (8s).
        /// </remarks>
        QueueTime = 1,

        /// <summary>
        /// This option represents the maximum number of bytes that can be stored in the packet queue
        /// for <seealso cref="WinDivert.WinDivertRecv(IntPtr, WinDivertBuffer, ref WinDivertAddress, ref uint)" />.
        /// </summary>
        /// <remarks>
        /// Currently the default value is 4194304 (4MB), the minimum is 65535 (64KB), and the
        /// maximum is 33554432 (32MB).
        /// </remarks>
        QueueSize = 2,
    }
}
