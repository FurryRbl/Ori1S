using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Provides the fields that represent reference points in streams for seeking.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000252 RID: 594
	[ComVisible(true)]
	[Serializable]
	public enum SeekOrigin
	{
		/// <summary>Specifies the beginning of a stream.</summary>
		// Token: 0x04000B9E RID: 2974
		Begin,
		/// <summary>Specifies the current position within a stream.</summary>
		// Token: 0x04000B9F RID: 2975
		Current,
		/// <summary>Specifies the end of a stream.</summary>
		// Token: 0x04000BA0 RID: 2976
		End
	}
}
