using System;
using System.IO;
using System.Text;
using Game;
using UnityEngine;

// Token: 0x02000632 RID: 1586
public class HoloLensController : MonoBehaviour
{
	// Token: 0x06002706 RID: 9990 RVA: 0x000AA7C8 File Offset: 0x000A89C8
	private void Start()
	{
		UnityEngine.Object.DestroyImmediate(this);
		string path = Path.Combine(HoloLensController.OUTPUT_FOLDER, HoloLensController.TELEMETRY_FILE_NAME);
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x000AA7FC File Offset: 0x000A89FC
	private void FixedUpdate()
	{
		this.m_writeTelemetryTimer -= Time.deltaTime;
		if (this.m_writeTelemetryTimer <= 0f)
		{
			this.m_writeTelemetryTimer = 0.5f;
			this.WriteHoloLensData();
		}
	}

	// Token: 0x06002708 RID: 9992 RVA: 0x000AA834 File Offset: 0x000A8A34
	private void WriteHoloLensData()
	{
		try
		{
			if (this.m_IAsyncResult == null)
			{
				float index = SkillTreeManager.Instance.CombatLane.Index;
				float index2 = SkillTreeManager.Instance.EnergyLane.Index;
				float index3 = SkillTreeManager.Instance.UtilityLane.Index;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("updateId: " + this.m_updateID + "\r\n");
				stringBuilder.Append("oriPosition: " + Characters.Sein.PlatformBehaviour.PlatformMovement.Position + "\r\n");
				stringBuilder.Append("cameraPosition: " + UI.Cameras.Current.CameraTarget.TargetPosition + "\r\n");
				stringBuilder.Append("abilities: " + ((!Characters.Sein.PlayerAbilities.Stomp.HasAbility) ? "NoStomp" : "Stomp") + "\r\n");
				stringBuilder.Append(string.Concat(new object[]
				{
					"skillTree: ",
					index,
					", ",
					index2,
					", ",
					index3,
					"\r\n"
				}));
				stringBuilder.Append("health: " + Characters.Sein.Mortality.Health.MaxHealth + "\r\n");
				stringBuilder.Append("energy: " + Characters.Sein.Energy.Max + "\r\n");
				stringBuilder.Append("currentHealth: " + Characters.Sein.Mortality.Health.Amount + "\r\n");
				stringBuilder.Append("currentEnergy: " + Characters.Sein.Energy.Current + "\r\n");
				stringBuilder.Append("HUD: " + ((!SeinUI.DebugHideUI) ? "Visible" : "Hidden") + "\r\n");
				stringBuilder.Append("currentSpiritLight: " + Characters.Sein.Level.Experience + "\r\n");
				stringBuilder.Append("spiritLightNeededForNextLevel: " + Characters.Sein.Level.ExperienceNeedForNextLevel + "\r\n");
				stringBuilder.Append("%%%");
				byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
				string path = Path.Combine(HoloLensController.OUTPUT_FOLDER, HoloLensController.TELEMETRY_FILE_NAME);
				FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, bytes.Length, true);
				this.m_IAsyncResult = fileStream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(this.WriteCallback), new HoloLensFileWriteState(fileStream));
				this.m_updateID += 1L;
				this.m_fileCount++;
				if (this.m_fileCount == 101)
				{
					this.m_fileCount = 0;
				}
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	// Token: 0x06002709 RID: 9993 RVA: 0x000AAB88 File Offset: 0x000A8D88
	private void WriteCallback(IAsyncResult ar)
	{
		HoloLensFileWriteState holoLensFileWriteState = (HoloLensFileWriteState)ar.AsyncState;
		FileStream stream = holoLensFileWriteState.Stream;
		stream.EndWrite(ar);
		stream.Dispose();
		stream.Close();
		this.m_IAsyncResult = null;
	}

	// Token: 0x040021A6 RID: 8614
	private static string OUTPUT_FOLDER = "T:\\";

	// Token: 0x040021A7 RID: 8615
	private static string TELEMETRY_FILE_NAME = "telemetry.txt";

	// Token: 0x040021A8 RID: 8616
	private float m_writeTelemetryTimer = 5f;

	// Token: 0x040021A9 RID: 8617
	private long m_updateID;

	// Token: 0x040021AA RID: 8618
	private IAsyncResult m_IAsyncResult;

	// Token: 0x040021AB RID: 8619
	private int m_fileCount;
}
