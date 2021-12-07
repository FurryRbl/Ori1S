using System;

namespace System.Media
{
	/// <summary>Retrieves sounds associated with a set of Windows operating system sound-event types. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002B6 RID: 694
	public sealed class SystemSounds
	{
		// Token: 0x06001813 RID: 6163 RVA: 0x0004234C File Offset: 0x0004054C
		private SystemSounds()
		{
		}

		/// <summary>Gets the sound associated with the Asterisk program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00042354 File Offset: 0x00040554
		public static SystemSound Asterisk
		{
			get
			{
				return new SystemSound("Asterisk");
			}
		}

		/// <summary>Gets the sound associated with the Beep program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00042360 File Offset: 0x00040560
		public static SystemSound Beep
		{
			get
			{
				return new SystemSound("Beep");
			}
		}

		/// <summary>Gets the sound associated with the Exclamation program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0004236C File Offset: 0x0004056C
		public static SystemSound Exclamation
		{
			get
			{
				return new SystemSound("Exclamation");
			}
		}

		/// <summary>Gets the sound associated with the Hand program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00042378 File Offset: 0x00040578
		public static SystemSound Hand
		{
			get
			{
				return new SystemSound("Hand");
			}
		}

		/// <summary>Gets the sound associated with the Question program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x00042384 File Offset: 0x00040584
		public static SystemSound Question
		{
			get
			{
				return new SystemSound("Question");
			}
		}
	}
}
