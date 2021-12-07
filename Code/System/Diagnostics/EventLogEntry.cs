using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Encapsulates a single record in the event log. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000220 RID: 544
	[System.ComponentModel.ToolboxItem(false)]
	[System.ComponentModel.DesignTimeVisible(false)]
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[Serializable]
	public sealed class EventLogEntry : System.ComponentModel.Component, ISerializable
	{
		// Token: 0x06001282 RID: 4738 RVA: 0x00031E8C File Offset: 0x0003008C
		internal EventLogEntry(string category, short categoryNumber, int index, int eventID, string source, string message, string userName, string machineName, EventLogEntryType entryType, DateTime timeGenerated, DateTime timeWritten, byte[] data, string[] replacementStrings, long instanceId)
		{
			this.category = category;
			this.categoryNumber = categoryNumber;
			this.data = data;
			this.entryType = entryType;
			this.eventID = eventID;
			this.index = index;
			this.machineName = machineName;
			this.message = message;
			this.replacementStrings = replacementStrings;
			this.source = source;
			this.timeGenerated = timeGenerated;
			this.timeWritten = timeWritten;
			this.userName = userName;
			this.instanceId = instanceId;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00031F0C File Offset: 0x0003010C
		[MonoTODO]
		private EventLogEntry(SerializationInfo info, StreamingContext context)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization. </param>
		// Token: 0x06001284 RID: 4740 RVA: 0x00031F14 File Offset: 0x00030114
		[MonoTODO("Needs serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the text associated with the <see cref="P:System.Diagnostics.EventLogEntry.CategoryNumber" /> property for this entry.</summary>
		/// <returns>The application-specific category text.</returns>
		/// <exception cref="T:System.Exception">The space could not be allocated for one of the insertion strings associated with the category. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00031F1C File Offset: 0x0003011C
		[MonitoringDescription("The category of this event entry.")]
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets the category number of the event log entry.</summary>
		/// <returns>The application-specific category number for this entry.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00031F24 File Offset: 0x00030124
		[MonitoringDescription("An ID for the category of this event entry.")]
		public short CategoryNumber
		{
			get
			{
				return this.categoryNumber;
			}
		}

		/// <summary>Gets the binary data associated with the entry.</summary>
		/// <returns>An array of bytes that holds the binary data associated with the entry.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00031F2C File Offset: 0x0003012C
		[MonitoringDescription("Binary data associated with this event entry.")]
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		/// <summary>Gets the event type of this entry.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.EventLogEntryType" /> that indicates the event type associated with the entry in the event log.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00031F34 File Offset: 0x00030134
		[MonitoringDescription("The type of this event entry.")]
		public EventLogEntryType EntryType
		{
			get
			{
				return this.entryType;
			}
		}

		/// <summary>Gets the application-specific event identifier for the current event entry.</summary>
		/// <returns>The application-specific identifier for the event message.</returns>
		/// <filterpriority>3</filterpriority>
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00031F3C File Offset: 0x0003013C
		[Obsolete("Use InstanceId")]
		[MonitoringDescription("An ID number for this event entry.")]
		public int EventID
		{
			get
			{
				return this.eventID;
			}
		}

		/// <summary>Gets the index of this entry in the event log.</summary>
		/// <returns>The index of this entry in the event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00031F44 File Offset: 0x00030144
		[MonitoringDescription("Sequence numer of this event entry.")]
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the resource identifier that designates the message text of the event entry.</summary>
		/// <returns>A resource identifier that corresponds to a string definition in the message resource file of the event source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00031F4C File Offset: 0x0003014C
		[ComVisible(false)]
		[MonitoringDescription("The instance ID for this event entry.")]
		public long InstanceId
		{
			get
			{
				return this.instanceId;
			}
		}

		/// <summary>Gets the name of the computer on which this entry was generated.</summary>
		/// <returns>The name of the computer that contains the event log.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00031F54 File Offset: 0x00030154
		[MonitoringDescription("The Computer on which this event entry occured.")]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the localized message associated with this event entry.</summary>
		/// <returns>The formatted, localized text for the message. This includes associated replacement strings.</returns>
		/// <exception cref="T:System.Exception">The space could not be allocated for one of the insertion strings associated with the message. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x00031F5C File Offset: 0x0003015C
		[System.ComponentModel.Editor("System.ComponentModel.Design.BinaryEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The message of this event entry.")]
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		/// <summary>Gets the replacement strings associated with the entry.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> that holds the insertion strings stored in the event entry.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x00031F64 File Offset: 0x00030164
		[MonitoringDescription("Application strings for this event entry.")]
		public string[] ReplacementStrings
		{
			get
			{
				return this.replacementStrings;
			}
		}

		/// <summary>Gets the name of the application that generated this event.</summary>
		/// <returns>The name registered with the event log as the source of this event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00031F6C File Offset: 0x0003016C
		[MonitoringDescription("The source application of this event entry.")]
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets the local time at which this event was generated.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the local time at which this event was generated.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00031F74 File Offset: 0x00030174
		[MonitoringDescription("Generation time of this event entry.")]
		public DateTime TimeGenerated
		{
			get
			{
				return this.timeGenerated;
			}
		}

		/// <summary>Gets the local time at which this event was written to the log.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the local time at which this event was written to the log.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00031F7C File Offset: 0x0003017C
		[MonitoringDescription("The time at which this event entry was written to the logfile.")]
		public DateTime TimeWritten
		{
			get
			{
				return this.timeWritten;
			}
		}

		/// <summary>Gets the name of the user who is responsible for this event.</summary>
		/// <returns>The security identifier (SID) that uniquely identifies a user or group.</returns>
		/// <exception cref="T:System.SystemException">Account information could not be obtained for the user's SID. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00031F84 File Offset: 0x00030184
		[MonitoringDescription("The name of a user associated with this event entry.")]
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		/// <summary>Performs a comparison between two event log entries.</summary>
		/// <returns>true if the <see cref="T:System.Diagnostics.EventLogEntry" /> objects are identical; otherwise, false.</returns>
		/// <param name="otherEntry">The <see cref="T:System.Diagnostics.EventLogEntry" /> to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001293 RID: 4755 RVA: 0x00031F8C File Offset: 0x0003018C
		public bool Equals(EventLogEntry otherEntry)
		{
			return otherEntry == this || (otherEntry.Category == this.category && otherEntry.CategoryNumber == this.categoryNumber && otherEntry.Data.Equals(this.data) && otherEntry.EntryType == this.entryType && otherEntry.EventID == this.eventID && otherEntry.Index == this.index && otherEntry.MachineName == this.machineName && otherEntry.Message == this.message && otherEntry.ReplacementStrings.Equals(this.replacementStrings) && otherEntry.Source == this.source && otherEntry.TimeGenerated.Equals(this.timeGenerated) && otherEntry.TimeWritten.Equals(this.timeWritten) && otherEntry.UserName == this.userName);
		}

		// Token: 0x04000542 RID: 1346
		private string category;

		// Token: 0x04000543 RID: 1347
		private short categoryNumber;

		// Token: 0x04000544 RID: 1348
		private byte[] data;

		// Token: 0x04000545 RID: 1349
		private EventLogEntryType entryType;

		// Token: 0x04000546 RID: 1350
		private int eventID;

		// Token: 0x04000547 RID: 1351
		private int index;

		// Token: 0x04000548 RID: 1352
		private string machineName;

		// Token: 0x04000549 RID: 1353
		private string message;

		// Token: 0x0400054A RID: 1354
		private string[] replacementStrings;

		// Token: 0x0400054B RID: 1355
		private string source;

		// Token: 0x0400054C RID: 1356
		private DateTime timeGenerated;

		// Token: 0x0400054D RID: 1357
		private DateTime timeWritten;

		// Token: 0x0400054E RID: 1358
		private string userName;

		// Token: 0x0400054F RID: 1359
		private long instanceId;
	}
}
