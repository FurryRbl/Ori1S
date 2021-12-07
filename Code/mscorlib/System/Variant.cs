using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000195 RID: 405
	[StructLayout(LayoutKind.Explicit)]
	internal struct Variant
	{
		// Token: 0x0600147A RID: 5242 RVA: 0x0005245C File Offset: 0x0005065C
		public void SetValue(object obj)
		{
			this.vt = 0;
			if (obj == null)
			{
				return;
			}
			Type type = obj.GetType();
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (type == typeof(sbyte))
			{
				this.vt = 16;
				this.cVal = (sbyte)obj;
			}
			else if (type == typeof(byte))
			{
				this.vt = 17;
				this.bVal = (byte)obj;
			}
			else if (type == typeof(short))
			{
				this.vt = 2;
				this.iVal = (short)obj;
			}
			else if (type == typeof(ushort))
			{
				this.vt = 18;
				this.uiVal = (ushort)obj;
			}
			else if (type == typeof(int))
			{
				this.vt = 3;
				this.lVal = (int)obj;
			}
			else if (type == typeof(uint))
			{
				this.vt = 19;
				this.ulVal = (uint)obj;
			}
			else if (type == typeof(long))
			{
				this.vt = 20;
				this.llVal = (long)obj;
			}
			else if (type == typeof(ulong))
			{
				this.vt = 21;
				this.ullVal = (ulong)obj;
			}
			else if (type == typeof(float))
			{
				this.vt = 4;
				this.fltVal = (float)obj;
			}
			else if (type == typeof(double))
			{
				this.vt = 5;
				this.dblVal = (double)obj;
			}
			else if (type == typeof(string))
			{
				this.vt = 8;
				this.bstrVal = Marshal.StringToBSTR((string)obj);
			}
			else if (type == typeof(bool))
			{
				this.vt = 11;
				this.lVal = ((!(bool)obj) ? 0 : -1);
			}
			else if (type == typeof(BStrWrapper))
			{
				this.vt = 8;
				this.bstrVal = Marshal.StringToBSTR(((BStrWrapper)obj).WrappedObject);
			}
			else if (type == typeof(UnknownWrapper))
			{
				this.vt = 13;
				this.pdispVal = Marshal.GetIUnknownForObject(((UnknownWrapper)obj).WrappedObject);
			}
			else if (type == typeof(DispatchWrapper))
			{
				this.vt = 9;
				this.pdispVal = Marshal.GetIDispatchForObject(((DispatchWrapper)obj).WrappedObject);
			}
			else
			{
				try
				{
					this.pdispVal = Marshal.GetIDispatchForObject(obj);
					this.vt = 9;
					return;
				}
				catch
				{
				}
				try
				{
					this.vt = 13;
					this.pdispVal = Marshal.GetIUnknownForObject(obj);
				}
				catch (Exception inner)
				{
					throw new NotImplementedException(string.Format("Variant couldn't handle object of type {0}", obj.GetType()), inner);
				}
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x000527B0 File Offset: 0x000509B0
		public object GetValue()
		{
			object result = null;
			switch (this.vt)
			{
			case 2:
				result = this.iVal;
				break;
			case 3:
				result = this.lVal;
				break;
			case 4:
				result = this.fltVal;
				break;
			case 5:
				result = this.dblVal;
				break;
			case 8:
				result = Marshal.PtrToStringBSTR(this.bstrVal);
				break;
			case 9:
			case 13:
				if (this.pdispVal != IntPtr.Zero)
				{
					result = Marshal.GetObjectForIUnknown(this.pdispVal);
				}
				break;
			case 11:
				result = (this.boolVal != 0);
				break;
			case 16:
				result = this.cVal;
				break;
			case 17:
				result = this.bVal;
				break;
			case 18:
				result = this.uiVal;
				break;
			case 19:
				result = this.ulVal;
				break;
			case 20:
				result = this.llVal;
				break;
			case 21:
				result = this.ullVal;
				break;
			}
			return result;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0005291C File Offset: 0x00050B1C
		public void Clear()
		{
			if (this.vt == 8)
			{
				Marshal.FreeBSTR(this.bstrVal);
			}
			else if ((this.vt == 9 || this.vt == 13) && this.pdispVal != IntPtr.Zero)
			{
				Marshal.Release(this.pdispVal);
			}
		}

		// Token: 0x0400080B RID: 2059
		[FieldOffset(0)]
		public short vt;

		// Token: 0x0400080C RID: 2060
		[FieldOffset(2)]
		public ushort wReserved1;

		// Token: 0x0400080D RID: 2061
		[FieldOffset(4)]
		public ushort wReserved2;

		// Token: 0x0400080E RID: 2062
		[FieldOffset(6)]
		public ushort wReserved3;

		// Token: 0x0400080F RID: 2063
		[FieldOffset(8)]
		public long llVal;

		// Token: 0x04000810 RID: 2064
		[FieldOffset(8)]
		public int lVal;

		// Token: 0x04000811 RID: 2065
		[FieldOffset(8)]
		public byte bVal;

		// Token: 0x04000812 RID: 2066
		[FieldOffset(8)]
		public short iVal;

		// Token: 0x04000813 RID: 2067
		[FieldOffset(8)]
		public float fltVal;

		// Token: 0x04000814 RID: 2068
		[FieldOffset(8)]
		public double dblVal;

		// Token: 0x04000815 RID: 2069
		[FieldOffset(8)]
		public short boolVal;

		// Token: 0x04000816 RID: 2070
		[FieldOffset(8)]
		public IntPtr bstrVal;

		// Token: 0x04000817 RID: 2071
		[FieldOffset(8)]
		public sbyte cVal;

		// Token: 0x04000818 RID: 2072
		[FieldOffset(8)]
		public ushort uiVal;

		// Token: 0x04000819 RID: 2073
		[FieldOffset(8)]
		public uint ulVal;

		// Token: 0x0400081A RID: 2074
		[FieldOffset(8)]
		public ulong ullVal;

		// Token: 0x0400081B RID: 2075
		[FieldOffset(8)]
		public int intVal;

		// Token: 0x0400081C RID: 2076
		[FieldOffset(8)]
		public uint uintVal;

		// Token: 0x0400081D RID: 2077
		[FieldOffset(8)]
		public IntPtr pdispVal;

		// Token: 0x0400081E RID: 2078
		[FieldOffset(8)]
		public BRECORD bRecord;
	}
}
