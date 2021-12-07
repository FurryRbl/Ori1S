using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to support creating error configuration records.</summary>
	// Token: 0x02000005 RID: 5
	public interface IConfigErrorInfo
	{
		/// <summary>Gets a string specifying the file name related to the configuration details.</summary>
		/// <returns>A string specifying a filename.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000030 RID: 48
		string Filename { get; }

		/// <summary>Gets an integer specifying the line number related to the configuration details.</summary>
		/// <returns>An integer specifying a line number.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49
		int LineNumber { get; }
	}
}
