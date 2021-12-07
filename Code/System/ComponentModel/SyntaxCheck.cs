using System;
using System.IO;

namespace System.ComponentModel
{
	/// <summary>Provides methods to verify the machine name and path conform to a specific syntax. This class cannot be inherited.</summary>
	// Token: 0x020001A9 RID: 425
	public static class SyntaxCheck
	{
		/// <summary>Checks the syntax of the machine name to confirm that it does not contain "\".</summary>
		/// <returns>true if <paramref name="value" /> matches the proper machine name format; otherwise, false.</returns>
		/// <param name="value">A string containing the machine name to check. </param>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x000266DC File Offset: 0x000248DC
		public static bool CheckMachineName(string value)
		{
			return value != null && value.Trim().Length != 0 && value.IndexOf('\\') == -1;
		}

		/// <summary>Checks the syntax of the path to see whether it starts with "\\".</summary>
		/// <returns>true if <paramref name="value" /> matches the proper path format; otherwise, false.</returns>
		/// <param name="value">A string containing the path to check. </param>
		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002670C File Offset: 0x0002490C
		public static bool CheckPath(string value)
		{
			return value != null && value.Trim().Length != 0 && value.StartsWith("\\\\");
		}

		/// <summary>Checks the syntax of the path to see if it starts with "\" or drive letter "C:".</summary>
		/// <returns>true if <paramref name="value" /> matches the proper path format; otherwise, false.</returns>
		/// <param name="value">A string containing the path to check. </param>
		// Token: 0x06000EDA RID: 3802 RVA: 0x00026734 File Offset: 0x00024934
		public static bool CheckRootedPath(string value)
		{
			return value != null && value.Trim().Length != 0 && Path.IsPathRooted(value);
		}
	}
}
