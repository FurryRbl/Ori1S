using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Stores all the data needed to serialize or deserialize an object. This class cannot be inherited.</summary>
	// Token: 0x0200050A RID: 1290
	[ComVisible(true)]
	public sealed class SerializationInfo
	{
		// Token: 0x0600334B RID: 13131 RVA: 0x000A60D8 File Offset: 0x000A42D8
		private SerializationInfo(Type type)
		{
			this.assemblyName = type.Assembly.FullName;
			this.fullTypeName = type.FullName;
			this.converter = new FormatterConverter();
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000A612C File Offset: 0x000A432C
		private SerializationInfo(Type type, SerializationEntry[] data)
		{
			int num = data.Length;
			this.assemblyName = type.Assembly.FullName;
			this.fullTypeName = type.FullName;
			this.converter = new FormatterConverter();
			for (int i = 0; i < num; i++)
			{
				this.serialized.Add(data[i].Name, data[i]);
				this.values.Add(data[i]);
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		/// <param name="converter">The <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> used during deserialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="converter" /> is null. </exception>
		// Token: 0x0600334D RID: 13133 RVA: 0x000A61D8 File Offset: 0x000A43D8
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type", "Null argument");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter", "Null argument");
			}
			this.converter = converter;
			this.assemblyName = type.Assembly.FullName;
			this.fullTypeName = type.FullName;
		}

		/// <summary>Gets or sets the assembly name of the type to serialize during serialization only.</summary>
		/// <returns>The full name of the assembly of the type to serialize.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value the property is set to is null. </exception>
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000A6254 File Offset: 0x000A4454
		// (set) Token: 0x0600334F RID: 13135 RVA: 0x000A625C File Offset: 0x000A445C
		public string AssemblyName
		{
			get
			{
				return this.assemblyName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Argument is null.");
				}
				this.assemblyName = value;
			}
		}

		/// <summary>Gets or sets the full name of the <see cref="T:System.Type" /> to serialize.</summary>
		/// <returns>The full name of the type to serialize.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value this property is set to is null. </exception>
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000A6278 File Offset: 0x000A4478
		// (set) Token: 0x06003351 RID: 13137 RVA: 0x000A6280 File Offset: 0x000A4480
		public string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Argument is null.");
				}
				this.fullTypeName = value;
			}
		}

		/// <summary>Gets the number of members that have been added to the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The number of members that have been added to the current <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</returns>
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x000A629C File Offset: 0x000A449C
		public int MemberCount
		{
			get
			{
				return this.serialized.Count;
			}
		}

		/// <summary>Adds a value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store, where <paramref name="value" /> is associated with <paramref name="name" /> and is serialized as being of <see cref="T:System.Type" /><paramref name="type" />.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The value to be serialized. Any children of this object will automatically be serialized. </param>
		/// <param name="type">The <see cref="T:System.Type" /> to associate with the current object. This parameter must always be the type of the object itself or of one of its base classes. </param>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="name" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003353 RID: 13139 RVA: 0x000A62AC File Offset: 0x000A44AC
		public void AddValue(string name, object value, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name is null");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type is null");
			}
			if (this.serialized.ContainsKey(name))
			{
				throw new SerializationException("Value has been serialized already.");
			}
			SerializationEntry serializationEntry = new SerializationEntry(name, type, value);
			this.serialized.Add(name, serializationEntry);
			this.values.Add(serializationEntry);
		}

		/// <summary>Retrieves a value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The object of the specified <see cref="T:System.Type" /> associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the value to retrieve. If the stored value cannot be converted to this type, the system will throw a <see cref="T:System.InvalidCastException" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to <paramref name="type" />. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003354 RID: 13140 RVA: 0x000A6328 File Offset: 0x000A4528
		public object GetValue(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name is null.");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!this.serialized.ContainsKey(name))
			{
				throw new SerializationException("No element named " + name + " could be found.");
			}
			SerializationEntry serializationEntry = (SerializationEntry)this.serialized[name];
			if (serializationEntry.Value != null && !type.IsInstanceOfType(serializationEntry.Value))
			{
				return this.converter.Convert(serializationEntry.Value, type);
			}
			return serializationEntry.Value;
		}

		/// <summary>Sets the <see cref="T:System.Type" /> of the object to serialize.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		// Token: 0x06003355 RID: 13141 RVA: 0x000A63CC File Offset: 0x000A45CC
		public void SetType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type is null.");
			}
			this.fullTypeName = type.FullName;
			this.assemblyName = type.Assembly.FullName;
		}

		/// <summary>Returns a <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> used to iterate through the name-value pairs in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> for parsing the name-value pairs contained in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</returns>
		// Token: 0x06003356 RID: 13142 RVA: 0x000A6408 File Offset: 0x000A4608
		public SerializationInfoEnumerator GetEnumerator()
		{
			return new SerializationInfoEnumerator(this.values);
		}

		/// <summary>Adds a 16-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Int16 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003357 RID: 13143 RVA: 0x000A6418 File Offset: 0x000A4618
		public void AddValue(string name, short value)
		{
			this.AddValue(name, value, typeof(short));
		}

		/// <summary>Adds a 16-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The UInt16 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003358 RID: 13144 RVA: 0x000A6434 File Offset: 0x000A4634
		[CLSCompliant(false)]
		public void AddValue(string name, ushort value)
		{
			this.AddValue(name, value, typeof(ushort));
		}

		/// <summary>Adds a 32-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Int32 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003359 RID: 13145 RVA: 0x000A6450 File Offset: 0x000A4650
		public void AddValue(string name, int value)
		{
			this.AddValue(name, value, typeof(int));
		}

		/// <summary>Adds an 8-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The byte value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600335A RID: 13146 RVA: 0x000A646C File Offset: 0x000A466C
		public void AddValue(string name, byte value)
		{
			this.AddValue(name, value, typeof(byte));
		}

		/// <summary>Adds a Boolean value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Boolean value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600335B RID: 13147 RVA: 0x000A6488 File Offset: 0x000A4688
		public void AddValue(string name, bool value)
		{
			this.AddValue(name, value, typeof(bool));
		}

		/// <summary>Adds a Unicode character value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The character value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600335C RID: 13148 RVA: 0x000A64A4 File Offset: 0x000A46A4
		public void AddValue(string name, char value)
		{
			this.AddValue(name, value, typeof(char));
		}

		/// <summary>Adds an 8-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Sbyte value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600335D RID: 13149 RVA: 0x000A64C0 File Offset: 0x000A46C0
		[CLSCompliant(false)]
		public void AddValue(string name, sbyte value)
		{
			this.AddValue(name, value, typeof(sbyte));
		}

		/// <summary>Adds a double-precision floating-point value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The double value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600335E RID: 13150 RVA: 0x000A64DC File Offset: 0x000A46DC
		public void AddValue(string name, double value)
		{
			this.AddValue(name, value, typeof(double));
		}

		/// <summary>Adds a decimal value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The decimal value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">If The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">If a value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x0600335F RID: 13151 RVA: 0x000A64F8 File Offset: 0x000A46F8
		public void AddValue(string name, decimal value)
		{
			this.AddValue(name, value, typeof(decimal));
		}

		/// <summary>Adds a <see cref="T:System.DateTime" /> value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003360 RID: 13152 RVA: 0x000A6514 File Offset: 0x000A4714
		public void AddValue(string name, DateTime value)
		{
			this.AddValue(name, value, typeof(DateTime));
		}

		/// <summary>Adds a single-precision floating-point value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The single value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003361 RID: 13153 RVA: 0x000A6530 File Offset: 0x000A4730
		public void AddValue(string name, float value)
		{
			this.AddValue(name, value, typeof(float));
		}

		/// <summary>Adds a 32-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The UInt32 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003362 RID: 13154 RVA: 0x000A654C File Offset: 0x000A474C
		[CLSCompliant(false)]
		public void AddValue(string name, uint value)
		{
			this.AddValue(name, value, typeof(uint));
		}

		/// <summary>Adds a 64-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Int64 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003363 RID: 13155 RVA: 0x000A6568 File Offset: 0x000A4768
		public void AddValue(string name, long value)
		{
			this.AddValue(name, value, typeof(long));
		}

		/// <summary>Adds a 64-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The UInt64 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003364 RID: 13156 RVA: 0x000A6584 File Offset: 0x000A4784
		[CLSCompliant(false)]
		public void AddValue(string name, ulong value)
		{
			this.AddValue(name, value, typeof(ulong));
		}

		/// <summary>Adds the specified object into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store, where it is associated with a specified name.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The value to be serialized. Any children of this object will automatically be serialized. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06003365 RID: 13157 RVA: 0x000A65A0 File Offset: 0x000A47A0
		public void AddValue(string name, object value)
		{
			if (value == null)
			{
				this.AddValue(name, value, typeof(object));
			}
			else
			{
				this.AddValue(name, value, value.GetType());
			}
		}

		/// <summary>Retrieves a Boolean value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The Boolean value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a Boolean value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003366 RID: 13158 RVA: 0x000A65D8 File Offset: 0x000A47D8
		public bool GetBoolean(string name)
		{
			object value = this.GetValue(name, typeof(bool));
			return this.converter.ToBoolean(value);
		}

		/// <summary>Retrieves an 8-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 8-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to an 8-bit unsigned integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003367 RID: 13159 RVA: 0x000A6604 File Offset: 0x000A4804
		public byte GetByte(string name)
		{
			object value = this.GetValue(name, typeof(byte));
			return this.converter.ToByte(value);
		}

		/// <summary>Retrieves a Unicode character value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The Unicode character associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a Unicode character. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003368 RID: 13160 RVA: 0x000A6630 File Offset: 0x000A4830
		public char GetChar(string name)
		{
			object value = this.GetValue(name, typeof(char));
			return this.converter.ToChar(value);
		}

		/// <summary>Retrieves a <see cref="T:System.DateTime" /> value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a <see cref="T:System.DateTime" /> value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003369 RID: 13161 RVA: 0x000A665C File Offset: 0x000A485C
		public DateTime GetDateTime(string name)
		{
			object value = this.GetValue(name, typeof(DateTime));
			return this.converter.ToDateTime(value);
		}

		/// <summary>Retrieves a decimal value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>A decimal value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a decimal. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600336A RID: 13162 RVA: 0x000A6688 File Offset: 0x000A4888
		public decimal GetDecimal(string name)
		{
			object value = this.GetValue(name, typeof(decimal));
			return this.converter.ToDecimal(value);
		}

		/// <summary>Retrieves a double-precision floating-point value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The double-precision floating-point value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a double-precision floating-point value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600336B RID: 13163 RVA: 0x000A66B4 File Offset: 0x000A48B4
		public double GetDouble(string name)
		{
			object value = this.GetValue(name, typeof(double));
			return this.converter.ToDouble(value);
		}

		/// <summary>Retrieves a 16-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 16-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 16-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600336C RID: 13164 RVA: 0x000A66E0 File Offset: 0x000A48E0
		public short GetInt16(string name)
		{
			object value = this.GetValue(name, typeof(short));
			return this.converter.ToInt16(value);
		}

		/// <summary>Retrieves a 32-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 32-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name of the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 32-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600336D RID: 13165 RVA: 0x000A670C File Offset: 0x000A490C
		public int GetInt32(string name)
		{
			object value = this.GetValue(name, typeof(int));
			return this.converter.ToInt32(value);
		}

		/// <summary>Retrieves a 64-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 64-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 64-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600336E RID: 13166 RVA: 0x000A6738 File Offset: 0x000A4938
		public long GetInt64(string name)
		{
			object value = this.GetValue(name, typeof(long));
			return this.converter.ToInt64(value);
		}

		/// <summary>Retrieves an 8-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 8-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to an 8-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x0600336F RID: 13167 RVA: 0x000A6764 File Offset: 0x000A4964
		[CLSCompliant(false)]
		public sbyte GetSByte(string name)
		{
			object value = this.GetValue(name, typeof(sbyte));
			return this.converter.ToSByte(value);
		}

		/// <summary>Retrieves a single-precision floating-point value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The single-precision floating-point value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name of the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a single-precision floating-point value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003370 RID: 13168 RVA: 0x000A6790 File Offset: 0x000A4990
		public float GetSingle(string name)
		{
			object value = this.GetValue(name, typeof(float));
			return this.converter.ToSingle(value);
		}

		/// <summary>Retrieves a <see cref="T:System.String" /> value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The <see cref="T:System.String" /> associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a <see cref="T:System.String" />. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003371 RID: 13169 RVA: 0x000A67BC File Offset: 0x000A49BC
		public string GetString(string name)
		{
			object value = this.GetValue(name, typeof(string));
			if (value == null)
			{
				return null;
			}
			return this.converter.ToString(value);
		}

		/// <summary>Retrieves a 16-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 16-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 16-bit unsigned integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003372 RID: 13170 RVA: 0x000A67F0 File Offset: 0x000A49F0
		[CLSCompliant(false)]
		public ushort GetUInt16(string name)
		{
			object value = this.GetValue(name, typeof(ushort));
			return this.converter.ToUInt16(value);
		}

		/// <summary>Retrieves a 32-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 32-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 32-bit unsigned integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003373 RID: 13171 RVA: 0x000A681C File Offset: 0x000A4A1C
		[CLSCompliant(false)]
		public uint GetUInt32(string name)
		{
			object value = this.GetValue(name, typeof(uint));
			return this.converter.ToUInt32(value);
		}

		/// <summary>Retrieves a 64-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 64-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 64-bit unsigned integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06003374 RID: 13172 RVA: 0x000A6848 File Offset: 0x000A4A48
		[CLSCompliant(false)]
		public ulong GetUInt64(string name)
		{
			object value = this.GetValue(name, typeof(ulong));
			return this.converter.ToUInt64(value);
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000A6874 File Offset: 0x000A4A74
		private SerializationEntry[] get_entries()
		{
			SerializationEntry[] array = new SerializationEntry[this.MemberCount];
			int num = 0;
			foreach (SerializationEntry serializationEntry in this)
			{
				array[num++] = serializationEntry;
			}
			return array;
		}

		// Token: 0x0400155E RID: 5470
		private Hashtable serialized = new Hashtable();

		// Token: 0x0400155F RID: 5471
		private ArrayList values = new ArrayList();

		// Token: 0x04001560 RID: 5472
		private string assemblyName;

		// Token: 0x04001561 RID: 5473
		private string fullTypeName;

		// Token: 0x04001562 RID: 5474
		private IFormatterConverter converter;
	}
}
