using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono.Interop;

namespace System
{
	// Token: 0x020001A2 RID: 418
	internal class __ComObject : MarshalByRefObject
	{
		// Token: 0x0600152E RID: 5422 RVA: 0x00053F0C File Offset: 0x0005210C
		public __ComObject()
		{
			this.Initialize(base.GetType());
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00053F20 File Offset: 0x00052120
		internal __ComObject(Type t)
		{
			this.Initialize(t);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00053F30 File Offset: 0x00052130
		internal __ComObject(IntPtr pItf)
		{
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			int errorCode = Marshal.QueryInterface(pItf, ref iid_IUnknown, out this.iunknown);
			Marshal.ThrowExceptionForHR(errorCode);
		}

		// Token: 0x06001531 RID: 5425
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern __ComObject CreateRCW(Type t);

		// Token: 0x06001532 RID: 5426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseInterfaces();

		// Token: 0x06001533 RID: 5427 RVA: 0x00053F60 File Offset: 0x00052160
		~__ComObject()
		{
			this.ReleaseInterfaces();
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00053F9C File Offset: 0x0005219C
		internal void Initialize(Type t)
		{
			if (this.iunknown != IntPtr.Zero)
			{
				return;
			}
			ObjectCreationDelegate objectCreationCallback = ExtensibleClassFactory.GetObjectCreationCallback(t);
			if (objectCreationCallback != null)
			{
				this.iunknown = objectCreationCallback(IntPtr.Zero);
				if (this.iunknown == IntPtr.Zero)
				{
					throw new COMException(string.Format("ObjectCreationDelegate for type {0} failed to return a valid COM object", t));
				}
			}
			else
			{
				int errorCode = __ComObject.CoCreateInstance(__ComObject.GetCLSID(t), IntPtr.Zero, 21U, __ComObject.IID_IUnknown, out this.iunknown);
				Marshal.ThrowExceptionForHR(errorCode);
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0005402C File Offset: 0x0005222C
		private static Guid GetCLSID(Type t)
		{
			if (t.IsImport)
			{
				return t.GUID;
			}
			for (Type baseType = t.BaseType; baseType != typeof(object); baseType = baseType.BaseType)
			{
				if (baseType.IsImport)
				{
					return baseType.GUID;
				}
			}
			throw new COMException("Could not find base COM type for type " + t.ToString());
		}

		// Token: 0x06001536 RID: 5430
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetInterfaceInternal(Type t, bool throwException);

		// Token: 0x06001537 RID: 5431 RVA: 0x00054098 File Offset: 0x00052298
		internal IntPtr GetInterface(Type t, bool throwException)
		{
			this.CheckIUnknown();
			return this.GetInterfaceInternal(t, throwException);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000540A8 File Offset: 0x000522A8
		internal IntPtr GetInterface(Type t)
		{
			return this.GetInterface(t, true);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000540B4 File Offset: 0x000522B4
		private void CheckIUnknown()
		{
			if (this.iunknown == IntPtr.Zero)
			{
				throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x000540E4 File Offset: 0x000522E4
		internal IntPtr IUnknown
		{
			get
			{
				if (this.iunknown == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return this.iunknown;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00054118 File Offset: 0x00052318
		internal IntPtr IDispatch
		{
			get
			{
				IntPtr @interface = this.GetInterface(typeof(IDispatch));
				if (@interface == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return @interface;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00054154 File Offset: 0x00052354
		internal static Guid IID_IUnknown
		{
			get
			{
				return new Guid("00000000-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x00054160 File Offset: 0x00052360
		internal static Guid IID_IDispatch
		{
			get
			{
				return new Guid("00020400-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0005416C File Offset: 0x0005236C
		public override bool Equals(object obj)
		{
			this.CheckIUnknown();
			if (obj == null)
			{
				return false;
			}
			__ComObject _ComObject = obj as __ComObject;
			return _ComObject != null && this.iunknown == _ComObject.IUnknown;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x000541A8 File Offset: 0x000523A8
		public override int GetHashCode()
		{
			this.CheckIUnknown();
			return this.iunknown.ToInt32();
		}

		// Token: 0x06001540 RID: 5440
		[DllImport("ole32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
		private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] [In] Guid rclsid, IntPtr pUnkOuter, uint dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, out IntPtr pUnk);

		// Token: 0x04000847 RID: 2119
		private IntPtr iunknown;

		// Token: 0x04000848 RID: 2120
		private IntPtr hash_table;
	}
}
