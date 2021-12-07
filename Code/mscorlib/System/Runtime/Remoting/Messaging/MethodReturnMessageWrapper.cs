using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface to create a message that acts as a response to a method call on a remote object.</summary>
	// Token: 0x020004AD RID: 1197
	[ComVisible(true)]
	public class MethodReturnMessageWrapper : InternalMessageWrapper, IMessage, IMethodMessage, IMethodReturnMessage
	{
		/// <summary>Wraps an <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> to create a <see cref="T:System.Runtime.Remoting.Messaging.MethodReturnMessageWrapper" />. </summary>
		/// <param name="msg">A message that acts as an outgoing method call on a remote object.</param>
		// Token: 0x06003094 RID: 12436 RVA: 0x000A0044 File Offset: 0x0009E244
		public MethodReturnMessageWrapper(IMethodReturnMessage msg) : base(msg)
		{
			if (msg.Exception != null)
			{
				this._exception = msg.Exception;
				this._args = new object[0];
			}
			else
			{
				this._args = msg.Args;
				this._return = msg.ReturnValue;
				if (msg.MethodBase != null)
				{
					this._outArgInfo = new ArgInfo(msg.MethodBase, ArgInfoType.Out);
				}
			}
		}

		/// <summary>Gets the number of arguments passed to the method. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000A00B8 File Offset: 0x0009E2B8
		public virtual int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		/// <summary>Gets an array of arguments passed to the method. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000A00C4 File Offset: 0x0009E2C4
		// (set) Token: 0x06003097 RID: 12439 RVA: 0x000A00CC File Offset: 0x0009E2CC
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

		/// <summary>Gets the exception thrown during the method call, or null if the method did not throw an exception. </summary>
		/// <returns>The <see cref="T:System.Exception" /> thrown during the method call, or null if the method did not throw an exception.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000A00D8 File Offset: 0x0009E2D8
		// (set) Token: 0x06003099 RID: 12441 RVA: 0x000A00E0 File Offset: 0x0009E2E0
		public virtual Exception Exception
		{
			get
			{
				return this._exception;
			}
			set
			{
				this._exception = value;
			}
		}

		/// <summary>Gets a flag that indicates whether the method can accept a variable number of arguments. </summary>
		/// <returns>true if the method can accept a variable number of arguments; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x000A00EC File Offset: 0x0009E2EC
		public virtual bool HasVarArgs
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).HasVarArgs;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x000A0100 File Offset: 0x0009E300
		public virtual LogicalCallContext LogicalCallContext
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).LogicalCallContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method. </summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000A0114 File Offset: 0x0009E314
		public virtual MethodBase MethodBase
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodBase;
			}
		}

		/// <summary>Gets the name of the invoked method. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000A0128 File Offset: 0x0009E328
		public virtual string MethodName
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodName;
			}
		}

		/// <summary>Gets an object that contains the method signature. </summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000A013C File Offset: 0x0009E33C
		public virtual object MethodSignature
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodSignature;
			}
		}

		/// <summary>Gets the number of arguments in the method call that are marked as ref parameters or out parameters. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call that are marked as ref parameters or out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000A0150 File Offset: 0x0009E350
		public virtual int OutArgCount
		{
			get
			{
				return (this._outArgInfo == null) ? 0 : this._outArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets an array of arguments in the method call that are marked as ref parameters or out parameters. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments in the method call that are marked as ref parameters or out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000A0170 File Offset: 0x0009E370
		public virtual object[] OutArgs
		{
			get
			{
				return (this._outArgInfo == null) ? this._args : this._outArgInfo.GetInOutArgs(this._args);
			}
		}

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000A019C File Offset: 0x0009E39C
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnMessageWrapper.DictionaryWrapper(this, this.WrappedMessage.Properties);
				}
				return this._properties;
			}
		}

		/// <summary>Gets the return value of the method call. </summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the return value of the method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x000A01D4 File Offset: 0x0009E3D4
		// (set) Token: 0x060030A3 RID: 12451 RVA: 0x000A01DC File Offset: 0x0009E3DC
		public virtual object ReturnValue
		{
			get
			{
				return this._return;
			}
			set
			{
				this._return = value;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000A01E8 File Offset: 0x0009E3E8
		public virtual string TypeName
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).TypeName;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made. </summary>
		/// <returns>The URI of a remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000A01FC File Offset: 0x0009E3FC
		// (set) Token: 0x060030A6 RID: 12454 RVA: 0x000A0210 File Offset: 0x0009E410
		public string Uri
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).Uri;
			}
			set
			{
				this.Properties["__Uri"] = value;
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index. </summary>
		/// <returns>The method argument as an object.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060030A7 RID: 12455 RVA: 0x000A0224 File Offset: 0x0009E424
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
		// Token: 0x060030A8 RID: 12456 RVA: 0x000A0230 File Offset: 0x0009E430
		public virtual string GetArgName(int index)
		{
			return ((IMethodReturnMessage)this.WrappedMessage).GetArgName(index);
		}

		/// <summary>Returns the specified argument marked as a ref parameter or an out parameter. </summary>
		/// <returns>The specified argument marked as a ref parameter or an out parameter.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060030A9 RID: 12457 RVA: 0x000A0244 File Offset: 0x0009E444
		public virtual object GetOutArg(int argNum)
		{
			return this._args[this._outArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Returns the name of the specified argument marked as a ref parameter or an out parameter. </summary>
		/// <returns>The argument name, or null if the current method is not implemented.</returns>
		/// <param name="index">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060030AA RID: 12458 RVA: 0x000A025C File Offset: 0x0009E45C
		public virtual string GetOutArgName(int index)
		{
			return this._outArgInfo.GetInOutArgName(index);
		}

		// Token: 0x0400148F RID: 5263
		private object[] _args;

		// Token: 0x04001490 RID: 5264
		private ArgInfo _outArgInfo;

		// Token: 0x04001491 RID: 5265
		private MethodReturnMessageWrapper.DictionaryWrapper _properties;

		// Token: 0x04001492 RID: 5266
		private Exception _exception;

		// Token: 0x04001493 RID: 5267
		private object _return;

		// Token: 0x020004AE RID: 1198
		private class DictionaryWrapper : MethodReturnDictionary
		{
			// Token: 0x060030AB RID: 12459 RVA: 0x000A026C File Offset: 0x0009E46C
			public DictionaryWrapper(IMethodReturnMessage message, IDictionary wrappedDictionary) : base(message)
			{
				this._wrappedDictionary = wrappedDictionary;
				base.MethodKeys = MethodReturnMessageWrapper.DictionaryWrapper._keys;
			}

			// Token: 0x060030AD RID: 12461 RVA: 0x000A02A8 File Offset: 0x0009E4A8
			protected override IDictionary AllocInternalProperties()
			{
				return this._wrappedDictionary;
			}

			// Token: 0x060030AE RID: 12462 RVA: 0x000A02B0 File Offset: 0x0009E4B0
			protected override void SetMethodProperty(string key, object value)
			{
				if (key == "__Args")
				{
					((MethodReturnMessageWrapper)this._message)._args = (object[])value;
				}
				else if (key == "__Return")
				{
					((MethodReturnMessageWrapper)this._message)._return = value;
				}
				else
				{
					base.SetMethodProperty(key, value);
				}
			}

			// Token: 0x060030AF RID: 12463 RVA: 0x000A0318 File Offset: 0x0009E518
			protected override object GetMethodProperty(string key)
			{
				if (key == "__Args")
				{
					return ((MethodReturnMessageWrapper)this._message)._args;
				}
				if (key == "__Return")
				{
					return ((MethodReturnMessageWrapper)this._message)._return;
				}
				return base.GetMethodProperty(key);
			}

			// Token: 0x04001494 RID: 5268
			private IDictionary _wrappedDictionary;

			// Token: 0x04001495 RID: 5269
			private static string[] _keys = new string[]
			{
				"__Args",
				"__Return"
			};
		}
	}
}
