using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Represents additional options for creating a <see cref="T:System.IO.FileStream" /> object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000241 RID: 577
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum FileOptions
	{
		/// <summary>Indicates no additional parameters.</summary>
		// Token: 0x04000B25 RID: 2853
		None = 0,
		/// <summary>Indicates that a file is encrypted and can be decrypted only by using the same user account used for encryption.</summary>
		// Token: 0x04000B26 RID: 2854
		Encrypted = 16384,
		/// <summary>Indicates that a file is automatically deleted when it is no longer in use.</summary>
		// Token: 0x04000B27 RID: 2855
		DeleteOnClose = 67108864,
		/// <summary>Indicates that the file is to be accessed sequentially from beginning to end. The system can use this as a hint to optimize file caching. If an application moves the file pointer for random access, optimum caching may not occur; however, correct operation is still guaranteed. </summary>
		// Token: 0x04000B28 RID: 2856
		SequentialScan = 134217728,
		/// <summary>Indicates that the file is accessed randomly. The system can use this as a hint to optimize file caching.</summary>
		// Token: 0x04000B29 RID: 2857
		RandomAccess = 268435456,
		/// <summary>Indicates that a file can be used for asynchronous reading and writing. </summary>
		// Token: 0x04000B2A RID: 2858
		Asynchronous = 1073741824,
		/// <summary>Indicates that the system should write through any intermediate cache and go directly to disk.</summary>
		// Token: 0x04000B2B RID: 2859
		WriteThrough = -2147483648
	}
}
