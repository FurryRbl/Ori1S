using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents the enumerator for <see cref="T:System.Security.Policy.ApplicationTrust" /> objects in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
	// Token: 0x02000633 RID: 1587
	[ComVisible(true)]
	public sealed class ApplicationTrustEnumerator : IEnumerator
	{
		// Token: 0x06003C72 RID: 15474 RVA: 0x000CF9F4 File Offset: 0x000CDBF4
		internal ApplicationTrustEnumerator(ApplicationTrustCollection collection)
		{
			this.e = collection.GetEnumerator();
		}

		/// <summary>Gets the current <see cref="T:System.Object" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>The current <see cref="T:System.Object" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.</returns>
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x000CFA08 File Offset: 0x000CDC08
		object IEnumerator.Current
		{
			get
			{
				return this.e.Current;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Policy.ApplicationTrust" /> object in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>The current <see cref="T:System.Security.Policy.ApplicationTrust" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.</returns>
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06003C74 RID: 15476 RVA: 0x000CFA18 File Offset: 0x000CDC18
		public ApplicationTrust Current
		{
			get
			{
				return (ApplicationTrust)this.e.Current;
			}
		}

		/// <summary>Moves to the next element in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003C75 RID: 15477 RVA: 0x000CFA2C File Offset: 0x000CDC2C
		public bool MoveNext()
		{
			return this.e.MoveNext();
		}

		/// <summary>Resets the enumerator to the beginning of the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		// Token: 0x06003C76 RID: 15478 RVA: 0x000CFA3C File Offset: 0x000CDC3C
		public void Reset()
		{
			this.e.Reset();
		}

		// Token: 0x04001A34 RID: 6708
		private IEnumerator e;
	}
}
