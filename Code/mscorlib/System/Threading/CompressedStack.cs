using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides methods for setting and capturing the compressed stack on the current thread. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200069B RID: 1691
	[Serializable]
	public sealed class CompressedStack : ISerializable
	{
		// Token: 0x0600405E RID: 16478 RVA: 0x000DEBF0 File Offset: 0x000DCDF0
		internal CompressedStack(int length)
		{
			if (length > 0)
			{
				this._list = new ArrayList(length);
			}
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000DEC0C File Offset: 0x000DCE0C
		internal CompressedStack(CompressedStack cs)
		{
			if (cs != null && cs._list != null)
			{
				this._list = (ArrayList)cs._list.Clone();
			}
		}

		/// <summary>Creates a copy of the current compressed stack.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object representing the current compressed stack.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004060 RID: 16480 RVA: 0x000DEC3C File Offset: 0x000DCE3C
		[ComVisible(false)]
		public CompressedStack CreateCopy()
		{
			return new CompressedStack(this);
		}

		/// <summary>Captures the compressed stack from the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004061 RID: 16481 RVA: 0x000DEC44 File Offset: 0x000DCE44
		public static CompressedStack Capture()
		{
			CompressedStack compressedStack = new CompressedStack(0);
			compressedStack._list = SecurityFrame.GetStack(1);
			CompressedStack compressedStack2 = Thread.CurrentThread.GetCompressedStack();
			if (compressedStack2 != null)
			{
				for (int i = 0; i < compressedStack2._list.Count; i++)
				{
					compressedStack._list.Add(compressedStack2._list[i]);
				}
			}
			return compressedStack;
		}

		/// <summary>Gets the compressed stack for the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> for the current thread.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.-or-The request for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
		/// </PermissionSet>
		// Token: 0x06004062 RID: 16482 RVA: 0x000DECAC File Offset: 0x000DCEAC
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n   <IPermission class=\"System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                PublicKeyBlob=\"00000000000000000400000000000000\"/>\n</PermissionSet>\n")]
		public static CompressedStack GetCompressedStack()
		{
			CompressedStack compressedStack = Thread.CurrentThread.GetCompressedStack();
			if (compressedStack == null)
			{
				compressedStack = CompressedStack.Capture();
			}
			else
			{
				CompressedStack compressedStack2 = CompressedStack.Capture();
				for (int i = 0; i < compressedStack2._list.Count; i++)
				{
					compressedStack._list.Add(compressedStack2._list[i]);
				}
			}
			return compressedStack;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of this execution context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure representing the destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004063 RID: 16483 RVA: 0x000DED10 File Offset: 0x000DCF10
		[MonoTODO("incomplete")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"MemberAccess\"/>\n</PermissionSet>\n")]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		/// <summary>Runs a method in the specified compressed stack on the current thread.</summary>
		/// <param name="compressedStack">The <see cref="T:System.Threading.CompressedStack" /> to set.</param>
		/// <param name="callback">A <see cref="T:System.Threading.ContextCallback" /> that represents the method to be run in the specified security context.</param>
		/// <param name="state">The object to be passed to the callback method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="compressedStack" /> is null.</exception>
		// Token: 0x06004064 RID: 16484 RVA: 0x000DED24 File Offset: 0x000DCF24
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"Infrastructure\"/>\n</PermissionSet>\n")]
		public static void Run(CompressedStack compressedStack, ContextCallback callback, object state)
		{
			if (compressedStack == null)
			{
				throw new ArgumentException("compressedStack");
			}
			Thread currentThread = Thread.CurrentThread;
			CompressedStack compressedStack2 = null;
			try
			{
				compressedStack2 = currentThread.GetCompressedStack();
				currentThread.SetCompressedStack(compressedStack);
				callback(state);
			}
			finally
			{
				if (compressedStack2 != null)
				{
					currentThread.SetCompressedStack(compressedStack2);
				}
			}
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000DED90 File Offset: 0x000DCF90
		internal bool Equals(CompressedStack cs)
		{
			if (this.IsEmpty())
			{
				return cs.IsEmpty();
			}
			if (cs.IsEmpty())
			{
				return false;
			}
			if (this._list.Count != cs._list.Count)
			{
				return false;
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				SecurityFrame securityFrame = (SecurityFrame)this._list[i];
				SecurityFrame sf = (SecurityFrame)cs._list[i];
				if (!securityFrame.Equals(sf))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000DEE2C File Offset: 0x000DD02C
		internal bool IsEmpty()
		{
			return this._list == null || this._list.Count == 0;
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x000DEE4C File Offset: 0x000DD04C
		internal IList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x04001BAC RID: 7084
		private ArrayList _list;
	}
}
