using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceData" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022F RID: 559
	public class InstanceDataCollection : DictionaryBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollection" /> class, using the specified performance counter (which defines a performance instance).</summary>
		/// <param name="counterName">The name of the counter, which often describes the quantity that is being counted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is null. </exception>
		// Token: 0x06001325 RID: 4901 RVA: 0x00033180 File Offset: 0x00031380
		[Obsolete("Use InstanceDataCollectionCollection indexer instead.")]
		public InstanceDataCollection(string counterName)
		{
			InstanceDataCollection.CheckNull(counterName, "counterName");
			this.counterName = counterName;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0003319C File Offset: 0x0003139C
		private static void CheckNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>Gets the name of the performance counter whose instance data you want to get.</summary>
		/// <returns>The performance counter name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x000331AC File Offset: 0x000313AC
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
		}

		/// <summary>Gets the instance data associated with this counter. This is typically a set of raw counter values.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceData" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollection" /> object is indexed.</returns>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string ("") if the category contains a single instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000473 RID: 1139
		public InstanceData this[string instanceName]
		{
			get
			{
				InstanceDataCollection.CheckNull(instanceName, "instanceName");
				return (InstanceData)base.Dictionary[instanceName];
			}
		}

		/// <summary>Gets the object and counter registry keys for the objects associated with this instance data.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x000331D4 File Offset: 0x000313D4
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets the raw counter values that comprise the instance data for the counter.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's raw data values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x000331E4 File Offset: 0x000313E4
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Determines whether a performance instance with a specified name (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceData" /> objects) exists in the collection.</summary>
		/// <returns>true if the instance exists in the collection; otherwise, false.</returns>
		/// <param name="instanceName">The name of the instance to find in this collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600132B RID: 4907 RVA: 0x000331F4 File Offset: 0x000313F4
		public bool Contains(string instanceName)
		{
			InstanceDataCollection.CheckNull(instanceName, "instanceName");
			return base.Dictionary.Contains(instanceName);
		}

		/// <summary>Copies the items in the collection to the specified one-dimensional array at the specified index.</summary>
		/// <param name="instances">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The zero-based index value at which to add the new instances. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600132C RID: 4908 RVA: 0x00033210 File Offset: 0x00031410
		public void CopyTo(InstanceData[] instances, int index)
		{
			base.Dictionary.CopyTo(instances, index);
		}

		// Token: 0x04000589 RID: 1417
		private string counterName;
	}
}
