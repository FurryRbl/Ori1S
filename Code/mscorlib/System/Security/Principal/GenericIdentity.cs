using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents a generic user.</summary>
	// Token: 0x0200065C RID: 1628
	[ComVisible(true)]
	[Serializable]
	public class GenericIdentity : IIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class representing the user with the specified name and authentication type.</summary>
		/// <param name="name">The name of the user on whose behalf the code is running. </param>
		/// <param name="type">The type of authentication used to identify the user. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null.-or- The <paramref name="type" /> parameter is null. </exception>
		// Token: 0x06003E07 RID: 15879 RVA: 0x000D5874 File Offset: 0x000D3A74
		public GenericIdentity(string name, string type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.m_name = name;
			this.m_type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class representing the user with the specified name.</summary>
		/// <param name="name">The name of the user on whose behalf the code is running. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003E08 RID: 15880 RVA: 0x000D58B8 File Offset: 0x000D3AB8
		public GenericIdentity(string name) : this(name, string.Empty)
		{
		}

		/// <summary>Gets the type of authentication used to identify the user.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003E09 RID: 15881 RVA: 0x000D58C8 File Offset: 0x000D3AC8
		public virtual string AuthenticationType
		{
			get
			{
				return this.m_type;
			}
		}

		/// <summary>Gets the user's name.</summary>
		/// <returns>The name of the user on whose behalf the code is being run.</returns>
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x000D58D0 File Offset: 0x000D3AD0
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets a value indicating whether the user has been authenticated.</summary>
		/// <returns>true if the user was has been authenticated; otherwise, false.</returns>
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003E0B RID: 15883 RVA: 0x000D58D8 File Offset: 0x000D3AD8
		public virtual bool IsAuthenticated
		{
			get
			{
				return this.m_name.Length > 0;
			}
		}

		// Token: 0x04001AAF RID: 6831
		private string m_name;

		// Token: 0x04001AB0 RID: 6832
		private string m_type;
	}
}
