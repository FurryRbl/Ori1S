using System;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE) that contains a qualifier. The qualifier, represented by an <see cref="T:System.Security.AccessControl.AceQualifier" /> object, specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms. The <see cref="T:System.Security.AccessControl.QualifiedAce" /> class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonAce" /> and <see cref="T:System.Security.AccessControl.ObjectAce" /> classes.</summary>
	// Token: 0x0200058A RID: 1418
	public abstract class QualifiedAce : KnownAce
	{
		// Token: 0x060036EE RID: 14062 RVA: 0x000B2634 File Offset: 0x000B0834
		internal QualifiedAce(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AceQualifier aceQualifier, bool isCallback, byte[] opaque) : base(inheritanceFlags, propagationFlags)
		{
			this.ace_qualifier = aceQualifier;
			this.is_callback = isCallback;
			this.SetOpaque(opaque);
		}

		/// <summary>Gets a value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</summary>
		/// <returns>A value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</returns>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x000B2658 File Offset: 0x000B0858
		public AceQualifier AceQualifier
		{
			get
			{
				return this.ace_qualifier;
			}
		}

		/// <summary>Specifies whether this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data.</summary>
		/// <returns>true if this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data; otherwise, false.</returns>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x000B2660 File Offset: 0x000B0860
		public bool IsCallback
		{
			get
			{
				return this.is_callback;
			}
		}

		/// <summary>Gets the length of the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object. This property is valid only for callback Access Control Entries (ACEs).</summary>
		/// <returns>The length of the opaque callback data.</returns>
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x000B2668 File Offset: 0x000B0868
		public int OpaqueLength
		{
			get
			{
				return this.opaque.Length;
			}
		}

		/// <summary>Returns the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object. </summary>
		/// <returns>An array of byte values that represents the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</returns>
		// Token: 0x060036F2 RID: 14066 RVA: 0x000B2674 File Offset: 0x000B0874
		public byte[] GetOpaque()
		{
			return (byte[])this.opaque.Clone();
		}

		/// <summary>Sets the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</summary>
		/// <param name="opaque">An array of byte values that represents the opaque callback data for this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</param>
		// Token: 0x060036F3 RID: 14067 RVA: 0x000B2688 File Offset: 0x000B0888
		public void SetOpaque(byte[] opaque)
		{
			if (opaque == null)
			{
				throw new ArgumentNullException("opaque");
			}
			this.opaque = (byte[])opaque.Clone();
		}

		// Token: 0x04001745 RID: 5957
		private AceQualifier ace_qualifier;

		// Token: 0x04001746 RID: 5958
		private bool is_callback;

		// Token: 0x04001747 RID: 5959
		private byte[] opaque;
	}
}
