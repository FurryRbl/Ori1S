using System;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single regular expression match.</summary>
	// Token: 0x02000486 RID: 1158
	[Serializable]
	public class Match : Group
	{
		// Token: 0x0600295D RID: 10589 RVA: 0x00088908 File Offset: 0x00086B08
		private Match()
		{
			this.regex = null;
			this.machine = null;
			this.text_length = 0;
			this.groups = new GroupCollection(1, 1);
			this.groups.SetValue(this, 0);
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00088940 File Offset: 0x00086B40
		internal Match(Regex regex, IMachine machine, string text, int text_length, int n_groups, int index, int length) : base(text, index, length)
		{
			this.regex = regex;
			this.machine = machine;
			this.text_length = text_length;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00088964 File Offset: 0x00086B64
		internal Match(Regex regex, IMachine machine, string text, int text_length, int n_groups, int index, int length, int n_caps) : base(text, index, length, n_caps)
		{
			this.regex = regex;
			this.machine = machine;
			this.text_length = text_length;
			this.groups = new GroupCollection(n_groups, regex.Gap);
			this.groups.SetValue(this, 0);
		}

		/// <summary>Gets the empty group. All failed matches return this empty match.</summary>
		/// <returns>An empty <see cref="T:System.Text.RegularExpressions.Match" />.</returns>
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x000889C0 File Offset: 0x00086BC0
		public static Match Empty
		{
			get
			{
				return Match.empty;
			}
		}

		/// <summary>Returns a <see cref="T:System.Text.RegularExpressions.Match" /> instance equivalent to the one supplied that is suitable to share between multiple threads.</summary>
		/// <returns>A match that is suitable to share between multiple threads.</returns>
		/// <param name="inner">A match equivalent to the one expected.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is null.</exception>
		// Token: 0x06002962 RID: 10594 RVA: 0x000889C8 File Offset: 0x00086BC8
		[MonoTODO("not thread-safe")]
		public static Match Synchronized(Match inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			return inner;
		}

		/// <summary>Gets a collection of groups matched by the regular expression.</summary>
		/// <returns>The character groups matched by the pattern.</returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x000889DC File Offset: 0x00086BDC
		public virtual GroupCollection Groups
		{
			get
			{
				return this.groups;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Text.RegularExpressions.Match" /> object with the results for the next match, starting at the position at which the last match ended (at the character after the last matched character).</summary>
		/// <returns>The next regular expression match.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002964 RID: 10596 RVA: 0x000889E4 File Offset: 0x00086BE4
		public Match NextMatch()
		{
			if (this == Match.Empty)
			{
				return Match.Empty;
			}
			int num = (!this.regex.RightToLeft) ? (base.Index + base.Length) : base.Index;
			if (base.Length == 0)
			{
				num += ((!this.regex.RightToLeft) ? 1 : -1);
			}
			return this.machine.Scan(this.regex, base.Text, num, this.text_length);
		}

		/// <summary>Returns the expansion of the specified replacement pattern. </summary>
		/// <returns>The expanded version of the <paramref name="replacement" /> parameter.</returns>
		/// <param name="replacement">The replacement pattern to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">Expansion is not allowed for this pattern.</exception>
		// Token: 0x06002965 RID: 10597 RVA: 0x00088A70 File Offset: 0x00086C70
		public virtual string Result(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			if (this.machine == null)
			{
				throw new NotSupportedException("Result cannot be called on failed Match.");
			}
			return this.machine.Result(replacement, this);
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x00088AB4 File Offset: 0x00086CB4
		internal Regex Regex
		{
			get
			{
				return this.regex;
			}
		}

		// Token: 0x040019F7 RID: 6647
		private Regex regex;

		// Token: 0x040019F8 RID: 6648
		private IMachine machine;

		// Token: 0x040019F9 RID: 6649
		private int text_length;

		// Token: 0x040019FA RID: 6650
		private GroupCollection groups;

		// Token: 0x040019FB RID: 6651
		private static Match empty = new Match();
	}
}
