using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x0200058C RID: 1420
	public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to create the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		// Token: 0x060036FE RID: 14078 RVA: 0x000B276C File Offset: 0x000B096C
		public RawSecurityDescriptor(string sddlForm)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of byte values from which to create the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="offset">The offset in the  <paramref name="binaryForm" /> array at which to begin copying.</param>
		// Token: 0x060036FF RID: 14079 RVA: 0x000B2774 File Offset: 0x000B0974
		public RawSecurityDescriptor(byte[] binaryForm, int offset)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class with the specified values.</summary>
		/// <param name="flags">Flags that specify behavior of the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="owner">The owner for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="group">The primary group for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="systemAcl">The System Access Control List (SACL) for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="discretionaryAcl">The Discretionary Access Control List (DACL) for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		// Token: 0x06003700 RID: 14080 RVA: 0x000B277C File Offset: 0x000B097C
		public RawSecurityDescriptor(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
		{
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000B2784 File Offset: 0x000B0984
		public override ControlFlags ControlFlags
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the Discretionary Access Control List (DACL) for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object. The DACL contains access rules.</summary>
		/// <returns>The DACL for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x000B278C File Offset: 0x000B098C
		// (set) Token: 0x06003703 RID: 14083 RVA: 0x000B2794 File Offset: 0x000B0994
		public RawAcl DiscretionaryAcl
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000B279C File Offset: 0x000B099C
		// (set) Token: 0x06003705 RID: 14085 RVA: 0x000B27A4 File Offset: 0x000B09A4
		public override SecurityIdentifier Group
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000B27AC File Offset: 0x000B09AC
		// (set) Token: 0x06003707 RID: 14087 RVA: 0x000B27B4 File Offset: 0x000B09B4
		public override SecurityIdentifier Owner
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a byte value that represents the resource manager control bits associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>A byte value that represents the resource manager control bits associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000B27BC File Offset: 0x000B09BC
		// (set) Token: 0x06003709 RID: 14089 RVA: 0x000B27C4 File Offset: 0x000B09C4
		public byte ResourceManagerControl
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the System Access Control List (SACL) for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object. The SACL contains audit rules.</summary>
		/// <returns>The SACL for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000B27CC File Offset: 0x000B09CC
		// (set) Token: 0x0600370B RID: 14091 RVA: 0x000B27D4 File Offset: 0x000B09D4
		public RawAcl SystemAcl
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Sets the <see cref="P:System.Security.AccessControl.RawSecurityDescriptor.ControlFlags" /> property of this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object to the specified value.</summary>
		/// <param name="flags">One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</param>
		// Token: 0x0600370C RID: 14092 RVA: 0x000B27DC File Offset: 0x000B09DC
		public void SetFlags(ControlFlags flags)
		{
			throw new NotImplementedException();
		}
	}
}
