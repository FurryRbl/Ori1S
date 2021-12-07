using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Runtime.CompilerServices
{
	/// <summary>Wraps an exception that does not derive from the <see cref="T:System.Exception" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000342 RID: 834
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x060028AF RID: 10415 RVA: 0x00091E00 File Offset: 0x00090000
		private RuntimeWrappedException()
		{
		}

		/// <summary>Gets the object that was wrapped by the <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</summary>
		/// <returns>The object that was wrapped by the <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x00091E08 File Offset: 0x00090008
		public object WrappedException
		{
			get
			{
				return this.wrapped_exception;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x060028B1 RID: 10417 RVA: 0x00091E10 File Offset: 0x00090010
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this.wrapped_exception);
		}

		// Token: 0x0400109C RID: 4252
		private object wrapped_exception;
	}
}
