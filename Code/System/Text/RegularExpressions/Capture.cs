using System;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single subexpression capture. </summary>
	// Token: 0x0200046D RID: 1133
	[Serializable]
	public class Capture
	{
		// Token: 0x06002870 RID: 10352 RVA: 0x00080504 File Offset: 0x0007E704
		internal Capture(string text) : this(text, 0, 0)
		{
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x00080510 File Offset: 0x0007E710
		internal Capture(string text, int index, int length)
		{
			this.text = text;
			this.index = index;
			this.length = length;
		}

		/// <summary>The position in the original string where the first character of the captured substring was found.</summary>
		/// <returns>The zero-based starting position in the original string where the captured substring was found.</returns>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x00080530 File Offset: 0x0007E730
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>The length of the captured substring.</summary>
		/// <returns>The length of the captured substring.</returns>
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x00080538 File Offset: 0x0007E738
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		/// <summary>Gets the captured substring from the input string.</summary>
		/// <returns>The actual substring that was captured by the match.</returns>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x00080540 File Offset: 0x0007E740
		public string Value
		{
			get
			{
				return (this.text != null) ? this.text.Substring(this.index, this.length) : string.Empty;
			}
		}

		/// <summary>Gets the captured substring from the input string.</summary>
		/// <returns>The actual substring that was captured by the match.</returns>
		// Token: 0x06002875 RID: 10357 RVA: 0x0008057C File Offset: 0x0007E77C
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x00080584 File Offset: 0x0007E784
		internal string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x04001908 RID: 6408
		internal int index;

		// Token: 0x04001909 RID: 6409
		internal int length;

		// Token: 0x0400190A RID: 6410
		internal string text;
	}
}
