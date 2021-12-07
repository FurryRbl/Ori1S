using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a unique command identifier that consists of a numeric command ID and a GUID menu group identifier.</summary>
	// Token: 0x020000F5 RID: 245
	[ComVisible(true)]
	public class CommandID
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CommandID" /> class using the specified menu group GUID and command ID number.</summary>
		/// <param name="menuGroup">The GUID of the group that this menu command belongs to. </param>
		/// <param name="commandID">The numeric identifier of this menu command. </param>
		// Token: 0x06000A0C RID: 2572 RVA: 0x0001CB88 File Offset: 0x0001AD88
		public CommandID(Guid menuGroup, int commandID)
		{
			this.cID = commandID;
			this.guid = menuGroup;
		}

		/// <summary>Gets the GUID of the menu group that the menu command identified by this <see cref="T:System.ComponentModel.Design.CommandID" /> belongs to.</summary>
		/// <returns>The GUID of the command group for this command.</returns>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0001CBA0 File Offset: 0x0001ADA0
		public virtual Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		/// <summary>Gets the numeric command ID.</summary>
		/// <returns>The command ID number.</returns>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		public virtual int ID
		{
			get
			{
				return this.cID;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.CommandID" /> instances are equal.</summary>
		/// <returns>true if the specified object is equivalent to this one; otherwise, false.</returns>
		/// <param name="obj">The object to compare. </param>
		// Token: 0x06000A0F RID: 2575 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
		public override bool Equals(object obj)
		{
			return obj is CommandID && (obj == this || (((CommandID)obj).Guid.Equals(this.guid) && ((CommandID)obj).ID.Equals(this.cID)));
		}

		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06000A10 RID: 2576 RVA: 0x0001CC10 File Offset: 0x0001AE10
		public override int GetHashCode()
		{
			return this.guid.GetHashCode() ^ this.cID.GetHashCode();
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current object.</summary>
		/// <returns>A string that contains the command ID information, both the GUID and integer identifier. </returns>
		// Token: 0x06000A11 RID: 2577 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		public override string ToString()
		{
			return this.guid.ToString() + " : " + this.cID.ToString();
		}

		// Token: 0x040002AB RID: 683
		private int cID;

		// Token: 0x040002AC RID: 684
		private Guid guid;
	}
}
