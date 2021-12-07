using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> interface to create a request message that acts as a method call on a remote object.</summary>
	// Token: 0x020004A6 RID: 1190
	[ComVisible(true)]
	public class MethodCallMessageWrapper : InternalMessageWrapper, IMessage, IMethodCallMessage, IMethodMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.MethodCallMessageWrapper" /> class by wrapping an <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> interface.</summary>
		/// <param name="msg">A message that acts as an outgoing method call on a remote object.</param>
		// Token: 0x06003036 RID: 12342 RVA: 0x0009EBE8 File Offset: 0x0009CDE8
		public MethodCallMessageWrapper(IMethodCallMessage msg) : base(msg)
		{
			this._args = ((IMethodCallMessage)this.WrappedMessage).Args;
			this._inArgInfo = new ArgInfo(msg.MethodBase, ArgInfoType.In);
		}

		/// <summary>Gets the number of arguments passed to the method. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x0009EC24 File Offset: 0x0009CE24
		public virtual int ArgCount
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).ArgCount;
			}
		}

		/// <summary>Gets an array of arguments passed to the method. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x0009EC38 File Offset: 0x0009CE38
		// (set) Token: 0x06003039 RID: 12345 RVA: 0x0009EC40 File Offset: 0x0009CE40
		public virtual object[] Args
		{
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		/// <summary>Gets a value indicating whether the method can accept a variable number of arguments. </summary>
		/// <returns>true if the method can accept a variable number of arguments; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x0009EC4C File Offset: 0x0009CE4C
		public virtual bool HasVarArgs
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).HasVarArgs;
			}
		}

		/// <summary>Gets the number of arguments in the method call that are not marked as out parameters. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call that are not marked as out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x0009EC60 File Offset: 0x0009CE60
		public virtual int InArgCount
		{
			get
			{
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets an array of arguments in the method call that are not marked as out parameters. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents arguments in the method call that are not marked as out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x0009EC70 File Offset: 0x0009CE70
		public virtual object[] InArgs
		{
			get
			{
				return this._inArgInfo.GetInOutArgs(this._args);
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x0009EC84 File Offset: 0x0009CE84
		public virtual LogicalCallContext LogicalCallContext
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).LogicalCallContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method. </summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x0009EC98 File Offset: 0x0009CE98
		public virtual MethodBase MethodBase
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).MethodBase;
			}
		}

		/// <summary>Gets the name of the invoked method. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x0009ECAC File Offset: 0x0009CEAC
		public virtual string MethodName
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).MethodName;
			}
		}

		/// <summary>Gets an object that contains the method signature. </summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x0009ECC0 File Offset: 0x0009CEC0
		public virtual object MethodSignature
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).MethodSignature;
			}
		}

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x0009ECD4 File Offset: 0x0009CED4
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodCallMessageWrapper.DictionaryWrapper(this, this.WrappedMessage.Properties);
				}
				return this._properties;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x0009ED0C File Offset: 0x0009CF0C
		public virtual string TypeName
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).TypeName;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made. </summary>
		/// <returns>The URI of a remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x0009ED20 File Offset: 0x0009CF20
		// (set) Token: 0x06003044 RID: 12356 RVA: 0x0009ED34 File Offset: 0x0009CF34
		public virtual string Uri
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).Uri;
			}
			set
			{
				IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
				if (internalMessage != null)
				{
					internalMessage.Uri = value;
				}
				else
				{
					this.Properties["__Uri"] = value;
				}
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index. </summary>
		/// <returns>The method argument as an object.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003045 RID: 12357 RVA: 0x0009ED70 File Offset: 0x0009CF70
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		/// <summary>Gets the name of a method argument at a specified index. </summary>
		/// <returns>The name of the method argument.</returns>
		/// <param name="index">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003046 RID: 12358 RVA: 0x0009ED7C File Offset: 0x0009CF7C
		public virtual string GetArgName(int index)
		{
			return ((IMethodCallMessage)this.WrappedMessage).GetArgName(index);
		}

		/// <summary>Gets a method argument at a specified index that is not marked as an out parameter. </summary>
		/// <returns>The method argument that is not marked as an out parameter.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003047 RID: 12359 RVA: 0x0009ED90 File Offset: 0x0009CF90
		public virtual object GetInArg(int argNum)
		{
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Gets the name of a method argument at a specified index that is not marked as an out parameter. </summary>
		/// <returns>The name of the method argument that is not marked as an out parameter.</returns>
		/// <param name="index">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003048 RID: 12360 RVA: 0x0009EDA8 File Offset: 0x0009CFA8
		public virtual string GetInArgName(int index)
		{
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x0400146E RID: 5230
		private object[] _args;

		// Token: 0x0400146F RID: 5231
		private ArgInfo _inArgInfo;

		// Token: 0x04001470 RID: 5232
		private MethodCallMessageWrapper.DictionaryWrapper _properties;

		// Token: 0x020004A7 RID: 1191
		private class DictionaryWrapper : MethodCallDictionary
		{
			// Token: 0x06003049 RID: 12361 RVA: 0x0009EDB8 File Offset: 0x0009CFB8
			public DictionaryWrapper(IMethodMessage message, IDictionary wrappedDictionary) : base(message)
			{
				this._wrappedDictionary = wrappedDictionary;
				base.MethodKeys = MethodCallMessageWrapper.DictionaryWrapper._keys;
			}

			// Token: 0x0600304B RID: 12363 RVA: 0x0009EDEC File Offset: 0x0009CFEC
			protected override IDictionary AllocInternalProperties()
			{
				return this._wrappedDictionary;
			}

			// Token: 0x0600304C RID: 12364 RVA: 0x0009EDF4 File Offset: 0x0009CFF4
			protected override void SetMethodProperty(string key, object value)
			{
				if (key == "__Args")
				{
					((MethodCallMessageWrapper)this._message)._args = (object[])value;
				}
				else
				{
					base.SetMethodProperty(key, value);
				}
			}

			// Token: 0x0600304D RID: 12365 RVA: 0x0009EE2C File Offset: 0x0009D02C
			protected override object GetMethodProperty(string key)
			{
				if (key == "__Args")
				{
					return ((MethodCallMessageWrapper)this._message)._args;
				}
				return base.GetMethodProperty(key);
			}

			// Token: 0x04001471 RID: 5233
			private IDictionary _wrappedDictionary;

			// Token: 0x04001472 RID: 5234
			private static string[] _keys = new string[]
			{
				"__Args"
			};
		}
	}
}
