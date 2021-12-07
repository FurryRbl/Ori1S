using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.Utility
{
	// Token: 0x02000124 RID: 292
	internal class NativeBuffer : DisposableClass
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x0000BB50 File Offset: 0x00009D50
		public NativeBuffer(byte[] managedData)
		{
			if (managedData == null || managedData.Length == 0)
			{
				throw new ArgumentException("managedData is null or empty.", "managedData");
			}
			this.ManagedData = managedData;
			this.UnmanagedSize = this.ManagedData.Length;
			this.UnmanagedMemory = Marshal.AllocHGlobal(this.UnmanagedSize);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0000BBA1 File Offset: 0x00009DA1
		public NativeBuffer(int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.UnmanagedSize = bufferSize;
			if (bufferSize == 0)
			{
				this.UnmanagedMemory = IntPtr.Zero;
				return;
			}
			this.UnmanagedMemory = Marshal.AllocHGlobal(this.UnmanagedSize);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0000BBDF File Offset: 0x00009DDF
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0000BBE7 File Offset: 0x00009DE7
		public byte[] ManagedData { get; private set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		public IntPtr UnmanagedMemory { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0000BC01 File Offset: 0x00009E01
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0000BC09 File Offset: 0x00009E09
		public int UnmanagedSize { get; private set; }

		// Token: 0x06000A06 RID: 2566 RVA: 0x0000BC12 File Offset: 0x00009E12
		protected override void CleanUpNativeResources()
		{
			Marshal.FreeHGlobal(this.UnmanagedMemory);
			this.UnmanagedMemory = IntPtr.Zero;
			base.CleanUpNativeResources();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0000BC30 File Offset: 0x00009E30
		public void ReadFromUnmanagedMemory()
		{
			this.ReadFromUnmanagedMemory(this.UnmanagedSize);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0000BC3E File Offset: 0x00009E3E
		public void ReadFromUnmanagedMemory(int bytesToRead)
		{
			if (bytesToRead < 0 || bytesToRead > this.UnmanagedSize)
			{
				throw new ArgumentOutOfRangeException("bytesToRead");
			}
			if (this.ManagedData == null)
			{
				throw new InvalidOperationException();
			}
			Marshal.Copy(this.UnmanagedMemory, this.ManagedData, 0, bytesToRead);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0000BC79 File Offset: 0x00009E79
		public void WriteToUnmanagedMemory()
		{
			if (this.ManagedData == null)
			{
				throw new InvalidOperationException();
			}
			Marshal.Copy(this.ManagedData, 0, this.UnmanagedMemory, this.UnmanagedSize);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0000BCA1 File Offset: 0x00009EA1
		public static byte[] ToBytes(int[] values)
		{
			return NativeBuffer.ToBytes<int>(values, new NativeBuffer.GetBytes<int>(BitConverter.GetBytes));
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0000BCB5 File Offset: 0x00009EB5
		public static byte[] ToBytes(long[] values)
		{
			return NativeBuffer.ToBytes<long>(values, new NativeBuffer.GetBytes<long>(BitConverter.GetBytes));
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000BCD7 File Offset: 0x00009ED7
		public static byte[] ToBytes(SteamID[] values)
		{
			return NativeBuffer.ToBytes<SteamID>(values, (SteamID id) => BitConverter.GetBytes(id.AsUInt64));
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0000BCFC File Offset: 0x00009EFC
		public static double[] ToDouble(byte[] rawData)
		{
			return NativeBuffer.FromBytes<double>(rawData, new NativeBuffer.ConvertFromBytes<double>(BitConverter.ToDouble));
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0000BD10 File Offset: 0x00009F10
		public static long[] ToLong(byte[] rawData)
		{
			return NativeBuffer.FromBytes<long>(rawData, new NativeBuffer.ConvertFromBytes<long>(BitConverter.ToInt64));
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0000BD24 File Offset: 0x00009F24
		public static int[] ToInt(byte[] rawData)
		{
			return NativeBuffer.FromBytes<int>(rawData, new NativeBuffer.ConvertFromBytes<int>(BitConverter.ToInt32));
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000BD38 File Offset: 0x00009F38
		public static NativeBuffer CopyToNative<T>(T obj) where T : struct
		{
			int bufferSize = Marshal.SizeOf(obj);
			NativeBuffer nativeBuffer = new NativeBuffer(bufferSize);
			Marshal.StructureToPtr(obj, nativeBuffer.UnmanagedMemory, false);
			return nativeBuffer;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0000BD6C File Offset: 0x00009F6C
		private static T[] FromBytes<T>(byte[] rawData, NativeBuffer.ConvertFromBytes<T> fromBytes)
		{
			int num = Marshal.SizeOf(typeof(T));
			T[] array = new T[rawData.Length / num];
			if (rawData.Length % num != 0)
			{
				throw new ArgumentException("rawData");
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = fromBytes(rawData, i * num);
			}
			return array;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000BDC8 File Offset: 0x00009FC8
		private static byte[] ToBytes<T>(T[] values, NativeBuffer.GetBytes<T> getBytes)
		{
			int num = Marshal.SizeOf(typeof(T));
			byte[] array = new byte[values.Length * num];
			for (int i = 0; i < values.Length; i++)
			{
				byte[] array2 = getBytes(values[i]);
				for (int j = 0; j < array2.Length; j++)
				{
					array[i * num + j] = array2[j];
				}
			}
			return array;
		}

		// Token: 0x02000125 RID: 293
		// (Invoke) Token: 0x06000A15 RID: 2581
		private delegate byte[] GetBytes<T>(T value);

		// Token: 0x02000126 RID: 294
		// (Invoke) Token: 0x06000A19 RID: 2585
		private delegate T ConvertFromBytes<T>(byte[] rawData, int offset);
	}
}
