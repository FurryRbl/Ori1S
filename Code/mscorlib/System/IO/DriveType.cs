using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Defines constants for drive types, including CDRom, Fixed, Network, NoRootDirectory, Ram, Removable, and Unknown.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000238 RID: 568
	[ComVisible(true)]
	[Serializable]
	public enum DriveType
	{
		/// <summary>The drive is an optical disc device, such as a CD or DVD-ROM.</summary>
		// Token: 0x04000AFA RID: 2810
		CDRom = 5,
		/// <summary>The drive is a fixed disk.</summary>
		// Token: 0x04000AFB RID: 2811
		Fixed = 3,
		/// <summary>The drive is a network drive.</summary>
		// Token: 0x04000AFC RID: 2812
		Network,
		/// <summary>The drive does not have a root directory.</summary>
		// Token: 0x04000AFD RID: 2813
		NoRootDirectory = 1,
		/// <summary>The drive is a RAM disk.</summary>
		// Token: 0x04000AFE RID: 2814
		Ram = 6,
		/// <summary>The drive is a removable storage device, such as a floppy disk drive or a USB flash drive.</summary>
		// Token: 0x04000AFF RID: 2815
		Removable = 2,
		/// <summary>The type of drive is unknown.</summary>
		// Token: 0x04000B00 RID: 2816
		Unknown = 0
	}
}
