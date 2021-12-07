using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>A platform-specific type that is used to represent a pointer or a handle.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000025 RID: 37
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public struct UIntPtr : ISerializable
	{
		/// <summary>Initializes a new instance of <see cref="T:System.UIntPtr" /> using the specified 64-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 64-bit unsigned integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.UIntPtr" />. </exception>
		// Token: 0x06000373 RID: 883 RVA: 0x0000DA48 File Offset: 0x0000BC48
		public UIntPtr(ulong value)
		{
			if (value > (ulong)-1 && UIntPtr.Size < 8)
			{
				throw new OverflowException(Locale.GetText("This isn't a 64bits machine."));
			}
			this._pointer = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UIntPtr" /> structure using the specified 32-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 32-bit unsigned integer. </param>
		// Token: 0x06000374 RID: 884 RVA: 0x0000DA78 File Offset: 0x0000BC78
		public UIntPtr(uint value)
		{
			this._pointer = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.UIntPtr" /> using the specified pointer to an unspecified type.</summary>
		/// <param name="value">A pointer to an unspecified type. </param>
		// Token: 0x06000375 RID: 885 RVA: 0x0000DA84 File Offset: 0x0000BC84
		[CLSCompliant(false)]
		public unsafe UIntPtr(void* value)
		{
			this._pointer = value;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.UIntPtr" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data. </param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06000377 RID: 887 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("pointer", this._pointer);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.UIntPtr" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000378 RID: 888 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		public override bool Equals(object obj)
		{
			if (obj is UIntPtr)
			{
				UIntPtr uintPtr = (UIntPtr)obj;
				return this._pointer == uintPtr._pointer;
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000379 RID: 889 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		public override int GetHashCode()
		{
			return this._pointer;
		}

		/// <summary>Converts the value of this instance to a 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer equal to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of this instance is too large to represent as a 32-bit unsigned integer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600037A RID: 890 RVA: 0x0000DB04 File Offset: 0x0000BD04
		public uint ToUInt32()
		{
			return this._pointer;
		}

		/// <summary>Converts the value of this instance to a 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer equal to the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600037B RID: 891 RVA: 0x0000DB10 File Offset: 0x0000BD10
		public ulong ToUInt64()
		{
			return this._pointer;
		}

		/// <summary>Converts the value of this instance to a pointer to an unspecified type.</summary>
		/// <returns>A pointer to <see cref="T:System.Void" />; that is, a pointer to memory containing data of an unspecified type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600037C RID: 892 RVA: 0x0000DB1C File Offset: 0x0000BD1C
		[CLSCompliant(false)]
		public unsafe void* ToPointer()
		{
			return this._pointer;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600037D RID: 893 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public override string ToString()
		{
			return this._pointer.ToString();
		}

		/// <summary>Gets the size of this instance.</summary>
		/// <returns>The size of a pointer or handle on this platform, measured in bytes. The value of this property is 4 on a 32-bit platform, and 8 on a 64-bit platform.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000DB40 File Offset: 0x0000BD40
		public unsafe static int Size
		{
			get
			{
				return sizeof(void*);
			}
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.UIntPtr" /> are equal.</summary>
		/// <returns>true if <paramref name="value1" /> equals <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">A <see cref="T:System.UIntPtr" />. </param>
		/// <param name="value2">A <see cref="T:System.UIntPtr" />. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600037F RID: 895 RVA: 0x0000DB48 File Offset: 0x0000BD48
		public static bool operator ==(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer == value2._pointer;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.UIntPtr" /> are not equal.</summary>
		/// <returns>true if <paramref name="value1" /> does not equal <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">A <see cref="T:System.UIntPtr" />. </param>
		/// <param name="value2">A <see cref="T:System.UIntPtr" />. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000380 RID: 896 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		public static bool operator !=(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer != value2._pointer;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a 64-bit unsigned integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">A <see cref="T:System.UIntPtr" />. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000381 RID: 897 RVA: 0x0000DB74 File Offset: 0x0000BD74
		public static explicit operator ulong(UIntPtr value)
		{
			return value._pointer;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a 32-bit unsigned integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">A <see cref="T:System.UIntPtr" />. </param>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of <paramref name="value" /> is too large to represent as a 32-bit unsigned integer. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000382 RID: 898 RVA: 0x0000DB80 File Offset: 0x0000BD80
		public static explicit operator uint(UIntPtr value)
		{
			return value._pointer;
		}

		/// <summary>Converts the value of a 64-bit unsigned integer to an <see cref="T:System.UIntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 64-bit unsigned integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.UIntPtr" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000383 RID: 899 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public static explicit operator UIntPtr(ulong value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Converts the specified pointer to an unspecified type to a <see cref="T:System.UIntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A pointer to an unspecified type. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000384 RID: 900 RVA: 0x0000DB94 File Offset: 0x0000BD94
		[CLSCompliant(false)]
		public unsafe static explicit operator UIntPtr(void* value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a pointer to an unspecified type.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">A <see cref="T:System.UIntPtr" />. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000385 RID: 901 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		[CLSCompliant(false)]
		public unsafe static explicit operator void*(UIntPtr value)
		{
			return value.ToPointer();
		}

		/// <summary>Converts the value of a 32-bit unsigned integer to an <see cref="T:System.UIntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 32-bit unsigned integer. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000386 RID: 902 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public static explicit operator UIntPtr(uint value)
		{
			return new UIntPtr(value);
		}

		/// <summary>A read-only field that represents a pointer or handle that has been initialized to zero.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000058 RID: 88
		public static readonly UIntPtr Zero = new UIntPtr(0U);

		// Token: 0x04000059 RID: 89
		private unsafe void* _pointer;
	}
}
