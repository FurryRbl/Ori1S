using System;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single capturing group. </summary>
	// Token: 0x0200047C RID: 1148
	[Serializable]
	public class Group : Capture
	{
		// Token: 0x06002901 RID: 10497 RVA: 0x0008603C File Offset: 0x0008423C
		internal Group(string text, int index, int length, int n_caps) : base(text, index, length)
		{
			this.success = true;
			this.captures = new CaptureCollection(n_caps);
			this.captures.SetValue(this, n_caps - 1);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0008606C File Offset: 0x0008426C
		internal Group(string text, int index, int length) : base(text, index, length)
		{
			this.success = true;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x00086080 File Offset: 0x00084280
		internal Group() : base(string.Empty)
		{
			this.success = false;
			this.captures = new CaptureCollection(0);
		}

		/// <summary>Returns a Group object equivalent to the one supplied that is safe to share between multiple threads.</summary>
		/// <returns>A regular expression Group object. </returns>
		/// <param name="inner">The input <see cref="T:System.Text.RegularExpressions.Group" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is null.</exception>
		// Token: 0x06002905 RID: 10501 RVA: 0x000860AC File Offset: 0x000842AC
		[MonoTODO("not thread-safe")]
		public static Group Synchronized(Group inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			return inner;
		}

		/// <summary>Gets a collection of all the captures matched by the capturing group, in innermost-leftmost-first order (or innermost-rightmost-first order if the regular expression is modified with the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option). The collection may have zero or more items.</summary>
		/// <returns>The collection of substrings matched by the group.</returns>
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x000860C0 File Offset: 0x000842C0
		public CaptureCollection Captures
		{
			get
			{
				return this.captures;
			}
		}

		/// <summary>Gets a value indicating whether the match is successful.</summary>
		/// <returns>true if the match is successful; otherwise, false.</returns>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x000860C8 File Offset: 0x000842C8
		public bool Success
		{
			get
			{
				return this.success;
			}
		}

		// Token: 0x040019CD RID: 6605
		internal static Group Fail = new Group();

		// Token: 0x040019CE RID: 6606
		private bool success;

		// Token: 0x040019CF RID: 6607
		private CaptureCollection captures;
	}
}
