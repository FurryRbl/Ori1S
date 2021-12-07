using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x0200002B RID: 43
	public sealed class FilePosition : MarshalByRefObject, IEquatable<FilePosition>, IDisposable
	{
		// Token: 0x060002EE RID: 750 RVA: 0x00009D90 File Offset: 0x00007F90
		public FilePosition()
		{
			IntPtr intPtr = Stdlib.CreateFilePosition();
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException("Unable to malloc fpos_t!");
			}
			this.pos = new HandleRef(this, intPtr);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00009DF0 File Offset: 0x00007FF0
		internal HandleRef Handle
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009DF8 File Offset: 0x00007FF8
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00009E08 File Offset: 0x00008008
		private void Cleanup()
		{
			if (this.pos.Handle != IntPtr.Zero)
			{
				Stdlib.free(this.pos.Handle);
				this.pos = new HandleRef(this, IntPtr.Zero);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009E50 File Offset: 0x00008050
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"(",
				base.ToString(),
				" ",
				this.GetDump(),
				")"
			});
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009E88 File Offset: 0x00008088
		private string GetDump()
		{
			if (FilePosition.FilePositionDumpSize <= 0)
			{
				return "internal error";
			}
			StringBuilder stringBuilder = new StringBuilder(FilePosition.FilePositionDumpSize + 1);
			if (Stdlib.DumpFilePosition(stringBuilder, this.Handle, FilePosition.FilePositionDumpSize + 1) <= 0)
			{
				return "internal error dumping fpos_t";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009ED8 File Offset: 0x000080D8
		public override bool Equals(object obj)
		{
			FilePosition lhs = obj as FilePosition;
			return obj != null && !(lhs == null) && this.ToString().Equals(obj.ToString());
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009F14 File Offset: 0x00008114
		public bool Equals(FilePosition value)
		{
			return object.ReferenceEquals(this, value) || this.ToString().Equals(value.ToString());
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00009F40 File Offset: 0x00008140
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009F50 File Offset: 0x00008150
		~FilePosition()
		{
			this.Cleanup();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00009F8C File Offset: 0x0000818C
		public static bool operator ==(FilePosition lhs, FilePosition rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009F98 File Offset: 0x00008198
		public static bool operator !=(FilePosition lhs, FilePosition rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x0400011A RID: 282
		private static readonly int FilePositionDumpSize = Stdlib.DumpFilePosition(null, new HandleRef(null, IntPtr.Zero), 0);

		// Token: 0x0400011B RID: 283
		private HandleRef pos;
	}
}
