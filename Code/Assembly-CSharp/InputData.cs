using System;
using System.IO;
using Core;

// Token: 0x02000185 RID: 389
public class InputData : IFrameData
{
	// Token: 0x06000F52 RID: 3922 RVA: 0x00046711 File Offset: 0x00044911
	public InputData()
	{
		this.UpdateInputs();
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x00046720 File Offset: 0x00044920
	public InputData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x0004672F File Offset: 0x0004492F
	private void CheckButtonChanged(ref bool changed, ref bool member, ref Input.InputButtonProcessor button)
	{
		if (member != button.IsPressed)
		{
			member = button.IsPressed;
			changed = true;
		}
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x0004674C File Offset: 0x0004494C
	public bool UpdateInputs()
	{
		bool result = false;
		if (this.HorizontalDigiPad != Input.HorizontalDigiPad)
		{
			this.HorizontalDigiPad = Input.HorizontalDigiPad;
			result = true;
		}
		if (this.VerticalDigiPad != Input.VerticalDigiPad)
		{
			this.VerticalDigiPad = Input.VerticalDigiPad;
			result = true;
		}
		this.CheckButtonChanged(ref result, ref this.Jump, ref Input.Jump);
		this.CheckButtonChanged(ref result, ref this.SpiritFlame, ref Input.SpiritFlame);
		this.CheckButtonChanged(ref result, ref this.SoulFlame, ref Input.SoulFlame);
		this.CheckButtonChanged(ref result, ref this.Bash, ref Input.Bash);
		this.CheckButtonChanged(ref result, ref this.ChargeJump, ref Input.ChargeJump);
		this.CheckButtonChanged(ref result, ref this.Glide, ref Input.Glide);
		this.CheckButtonChanged(ref result, ref this.Grab, ref Input.Grab);
		this.CheckButtonChanged(ref result, ref this.LeftShoulder, ref Input.LeftShoulder);
		this.CheckButtonChanged(ref result, ref this.RightShoulder, ref Input.RightShoulder);
		this.CheckButtonChanged(ref result, ref this.Select, ref Input.Select);
		this.CheckButtonChanged(ref result, ref this.Start, ref Input.Start);
		this.CheckButtonChanged(ref result, ref this.AnyStart, ref Input.AnyStart);
		this.CheckButtonChanged(ref result, ref this.LeftStick, ref Input.LeftStick);
		this.CheckButtonChanged(ref result, ref this.RightStick, ref Input.RightStick);
		this.CheckButtonChanged(ref result, ref this.MenuDown, ref Input.MenuDown);
		this.CheckButtonChanged(ref result, ref this.MenuUp, ref Input.MenuUp);
		this.CheckButtonChanged(ref result, ref this.MenuLeft, ref Input.MenuLeft);
		this.CheckButtonChanged(ref result, ref this.MenuRight, ref Input.MenuRight);
		this.CheckButtonChanged(ref result, ref this.MenuPageLeft, ref Input.MenuPageLeft);
		this.CheckButtonChanged(ref result, ref this.MenuPageRight, ref Input.MenuPageRight);
		this.CheckButtonChanged(ref result, ref this.ActionButtonA, ref Input.ActionButtonA);
		this.CheckButtonChanged(ref result, ref this.Cancel, ref Input.Cancel);
		this.CheckButtonChanged(ref result, ref this.LeftClick, ref Input.LeftClick);
		this.CheckButtonChanged(ref result, ref this.RightClick, ref Input.RightClick);
		this.CheckButtonChanged(ref result, ref this.ZoomIn, ref Input.ZoomIn);
		this.CheckButtonChanged(ref result, ref this.ZoomOut, ref Input.ZoomOut);
		this.CheckButtonChanged(ref result, ref this.Copy, ref Input.Copy);
		this.CheckButtonChanged(ref result, ref this.Delete, ref Input.Delete);
		this.CheckButtonChanged(ref result, ref this.Focus, ref Input.Focus);
		this.CheckButtonChanged(ref result, ref this.Filter, ref Input.Filter);
		this.CheckButtonChanged(ref result, ref this.Legend, ref Input.Legend);
		return result;
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000469E4 File Offset: 0x00044BE4
	public static void Record(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(1);
		int value = 1;
		binaryWriter.Write(value);
		binaryWriter.Write(Input.HorizontalDigiPad);
		binaryWriter.Write(Input.VerticalDigiPad);
		binaryWriter.Write(Input.Jump.IsPressed);
		binaryWriter.Write(Input.SpiritFlame.IsPressed);
		binaryWriter.Write(Input.SoulFlame.IsPressed);
		binaryWriter.Write(Input.Bash.IsPressed);
		binaryWriter.Write(Input.ChargeJump.IsPressed);
		binaryWriter.Write(Input.Glide.IsPressed);
		binaryWriter.Write(Input.Grab.IsPressed);
		binaryWriter.Write(Input.LeftShoulder.IsPressed);
		binaryWriter.Write(Input.RightShoulder.IsPressed);
		binaryWriter.Write(Input.Select.IsPressed);
		binaryWriter.Write(Input.Start.IsPressed);
		binaryWriter.Write(Input.AnyStart.IsPressed);
		binaryWriter.Write(Input.LeftStick.IsPressed);
		binaryWriter.Write(Input.RightStick.IsPressed);
		binaryWriter.Write(Input.MenuDown.IsPressed);
		binaryWriter.Write(Input.MenuUp.IsPressed);
		binaryWriter.Write(Input.MenuLeft.IsPressed);
		binaryWriter.Write(Input.MenuRight.IsPressed);
		binaryWriter.Write(Input.MenuPageLeft.IsPressed);
		binaryWriter.Write(Input.MenuPageRight.IsPressed);
		binaryWriter.Write(Input.ActionButtonA.IsPressed);
		binaryWriter.Write(Input.Cancel.IsPressed);
		binaryWriter.Write(Input.LeftClick.IsPressed);
		binaryWriter.Write(Input.RightClick.IsPressed);
		binaryWriter.Write(Input.ZoomIn.IsPressed);
		binaryWriter.Write(Input.ZoomOut.IsPressed);
		binaryWriter.Write(Input.Copy.IsPressed);
		binaryWriter.Write(Input.Delete.IsPressed);
		binaryWriter.Write(Input.Focus.IsPressed);
		binaryWriter.Write(Input.Filter.IsPressed);
		binaryWriter.Write(Input.Legend.IsPressed);
		binaryWriter.Write(GameController.Instance.SaveGameController.SaveFileExists);
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x00046C1C File Offset: 0x00044E1C
	public void Save(BinaryWriter binaryWriter)
	{
		int value = 1;
		binaryWriter.Write(value);
		binaryWriter.Write(this.HorizontalDigiPad);
		binaryWriter.Write(this.VerticalDigiPad);
		binaryWriter.Write(this.Jump);
		binaryWriter.Write(this.SpiritFlame);
		binaryWriter.Write(this.SoulFlame);
		binaryWriter.Write(this.Bash);
		binaryWriter.Write(this.ChargeJump);
		binaryWriter.Write(this.Glide);
		binaryWriter.Write(this.Grab);
		binaryWriter.Write(this.LeftShoulder);
		binaryWriter.Write(this.RightShoulder);
		binaryWriter.Write(this.Select);
		binaryWriter.Write(this.Start);
		binaryWriter.Write(this.AnyStart);
		binaryWriter.Write(this.LeftStick);
		binaryWriter.Write(this.RightStick);
		binaryWriter.Write(this.MenuDown);
		binaryWriter.Write(this.MenuUp);
		binaryWriter.Write(this.MenuLeft);
		binaryWriter.Write(this.MenuRight);
		binaryWriter.Write(this.MenuPageLeft);
		binaryWriter.Write(this.MenuPageRight);
		binaryWriter.Write(this.ActionButtonA);
		binaryWriter.Write(this.Cancel);
		binaryWriter.Write(this.LeftClick);
		binaryWriter.Write(this.RightClick);
		binaryWriter.Write(this.ZoomIn);
		binaryWriter.Write(this.ZoomOut);
		binaryWriter.Write(this.Copy);
		binaryWriter.Write(this.Delete);
		binaryWriter.Write(this.Focus);
		binaryWriter.Write(this.Filter);
		binaryWriter.Write(this.Legend);
		binaryWriter.Write(this.SaveFileExists);
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x00046DCC File Offset: 0x00044FCC
	public void Load(BinaryReader binaryReader)
	{
		int num = binaryReader.ReadInt32();
		this.HorizontalDigiPad = binaryReader.ReadInt32();
		this.VerticalDigiPad = binaryReader.ReadInt32();
		this.Jump = binaryReader.ReadBoolean();
		this.SpiritFlame = binaryReader.ReadBoolean();
		this.SoulFlame = binaryReader.ReadBoolean();
		this.Bash = binaryReader.ReadBoolean();
		this.ChargeJump = binaryReader.ReadBoolean();
		this.Glide = binaryReader.ReadBoolean();
		this.Grab = binaryReader.ReadBoolean();
		this.LeftShoulder = binaryReader.ReadBoolean();
		this.RightShoulder = binaryReader.ReadBoolean();
		this.Select = binaryReader.ReadBoolean();
		this.Start = binaryReader.ReadBoolean();
		this.AnyStart = binaryReader.ReadBoolean();
		this.LeftStick = binaryReader.ReadBoolean();
		this.RightStick = binaryReader.ReadBoolean();
		this.MenuDown = binaryReader.ReadBoolean();
		this.MenuUp = binaryReader.ReadBoolean();
		this.MenuLeft = binaryReader.ReadBoolean();
		this.MenuRight = binaryReader.ReadBoolean();
		this.MenuPageLeft = binaryReader.ReadBoolean();
		this.MenuPageRight = binaryReader.ReadBoolean();
		this.ActionButtonA = binaryReader.ReadBoolean();
		this.Cancel = binaryReader.ReadBoolean();
		this.LeftClick = binaryReader.ReadBoolean();
		this.RightClick = binaryReader.ReadBoolean();
		this.ZoomIn = binaryReader.ReadBoolean();
		this.ZoomOut = binaryReader.ReadBoolean();
		this.Copy = binaryReader.ReadBoolean();
		this.Delete = binaryReader.ReadBoolean();
		this.Focus = binaryReader.ReadBoolean();
		this.Filter = binaryReader.ReadBoolean();
		this.Legend = binaryReader.ReadBoolean();
		this.SaveFileExists = binaryReader.ReadBoolean();
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x00046F78 File Offset: 0x00045178
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.InputData;
	}

	// Token: 0x04000C24 RID: 3108
	public bool Jump;

	// Token: 0x04000C25 RID: 3109
	public bool SpiritFlame;

	// Token: 0x04000C26 RID: 3110
	public bool SoulFlame;

	// Token: 0x04000C27 RID: 3111
	public bool Bash;

	// Token: 0x04000C28 RID: 3112
	public bool ChargeJump;

	// Token: 0x04000C29 RID: 3113
	public bool Glide;

	// Token: 0x04000C2A RID: 3114
	public bool Grab;

	// Token: 0x04000C2B RID: 3115
	public bool LeftShoulder;

	// Token: 0x04000C2C RID: 3116
	public bool RightShoulder;

	// Token: 0x04000C2D RID: 3117
	public bool Select;

	// Token: 0x04000C2E RID: 3118
	public bool Start;

	// Token: 0x04000C2F RID: 3119
	public bool AnyStart;

	// Token: 0x04000C30 RID: 3120
	public bool LeftStick;

	// Token: 0x04000C31 RID: 3121
	public bool RightStick;

	// Token: 0x04000C32 RID: 3122
	public bool MenuDown;

	// Token: 0x04000C33 RID: 3123
	public bool MenuUp;

	// Token: 0x04000C34 RID: 3124
	public bool MenuLeft;

	// Token: 0x04000C35 RID: 3125
	public bool MenuRight;

	// Token: 0x04000C36 RID: 3126
	public bool MenuPageLeft;

	// Token: 0x04000C37 RID: 3127
	public bool MenuPageRight;

	// Token: 0x04000C38 RID: 3128
	public bool ActionButtonA;

	// Token: 0x04000C39 RID: 3129
	public bool Cancel;

	// Token: 0x04000C3A RID: 3130
	public int HorizontalDigiPad;

	// Token: 0x04000C3B RID: 3131
	public int VerticalDigiPad;

	// Token: 0x04000C3C RID: 3132
	public bool LeftClick;

	// Token: 0x04000C3D RID: 3133
	public bool RightClick;

	// Token: 0x04000C3E RID: 3134
	public bool ZoomIn;

	// Token: 0x04000C3F RID: 3135
	public bool ZoomOut;

	// Token: 0x04000C40 RID: 3136
	public bool Copy;

	// Token: 0x04000C41 RID: 3137
	public bool Delete;

	// Token: 0x04000C42 RID: 3138
	public bool Focus;

	// Token: 0x04000C43 RID: 3139
	public bool Filter;

	// Token: 0x04000C44 RID: 3140
	public bool Legend;

	// Token: 0x04000C45 RID: 3141
	public bool SaveFileExists;
}
