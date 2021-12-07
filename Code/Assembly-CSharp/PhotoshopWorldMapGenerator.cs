using System;
using System.IO;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class PhotoshopWorldMapGenerator : MonoBehaviour
{
	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06001098 RID: 4248 RVA: 0x0004BA5C File Offset: 0x00049C5C
	public Rect Bounds
	{
		get
		{
			return new Rect
			{
				width = base.transform.localScale.x,
				height = base.transform.localScale.y,
				center = base.transform.position
			};
		}
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x0004BAC0 File Offset: 0x00049CC0
	[ContextMenu("Generate world map")]
	public void GenerateCode()
	{
		SmoothCurve[] array = (SmoothCurve[])UnityEngine.Object.FindObjectsOfType(typeof(SmoothCurve));
		string empty = string.Empty;
		Vector2 vector = new Vector2(this.Bounds.width * this.PixelsPerUnit, this.Bounds.height * this.PixelsPerUnit);
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(empty + "LineTest.jsx", FileMode.Create)))
		{
			streamWriter.WriteLine("#target photoshop");
			streamWriter.WriteLine("#strict on");
			streamWriter.WriteLine("runthis();");
			streamWriter.WriteLine("function runthis()");
			streamWriter.WriteLine("{");
			streamWriter.WriteLine("   app.preferences.rulerUnits = Units.PIXELS");
			streamWriter.WriteLine("   app.preferences.typeUnits = TypeUnits.PIXELS");
			streamWriter.WriteLine("   app.displayDialogs = DialogModes.NO");
			streamWriter.WriteLine(string.Concat(new object[]
			{
				"   var docRef = app.documents.add(",
				vector.x,
				", ",
				vector.y,
				", 72, \"Simple Line\")"
			}));
			streamWriter.WriteLine("   var lineSubPathArray = new Array()");
			streamWriter.WriteLine("   var lineArray = new Array()");
			Matrix4x4 matrix4x = Matrix4x4.Scale(new Vector2(vector.x, vector.y)) * Matrix4x4.TRS(new Vector3(this.Bounds.xMin, this.Bounds.yMax), Quaternion.identity, new Vector3(this.Bounds.width, -this.Bounds.height, 1f)).inverse;
			for (int i = 0; i < array.Length; i++)
			{
				SmoothCurve smoothCurve = array[i];
				streamWriter.WriteLine("   lineArray = new Array()");
				for (int j = 0; j < smoothCurve.Nodes.Count; j++)
				{
					SmoothCurve.PathNode pathNode = smoothCurve.Nodes[j];
					Vector2 vector2 = matrix4x.MultiplyPoint(smoothCurve.transform.TransformPoint(pathNode.Position));
					Vector2 vector3 = matrix4x.MultiplyPoint(smoothCurve.transform.TransformPoint(pathNode.Position + pathNode.TangentOut));
					Vector2 vector4 = matrix4x.MultiplyPoint(smoothCurve.transform.TransformPoint(pathNode.Position + pathNode.TangentIn));
					streamWriter.WriteLine("   lineArray[" + j + "] = new PathPointInfo");
					streamWriter.WriteLine("   lineArray[" + j + "].kind = PointKind.CORNERPOINT");
					streamWriter.WriteLine(string.Concat(new object[]
					{
						"   lineArray[",
						j,
						"].anchor = Array(",
						Mathf.RoundToInt(vector2.x),
						", ",
						Mathf.RoundToInt(vector2.y),
						")"
					}));
					streamWriter.WriteLine(string.Concat(new object[]
					{
						"   lineArray[",
						j,
						"].leftDirection = Array(",
						Mathf.RoundToInt(vector3.x),
						", ",
						Mathf.RoundToInt(vector3.y),
						")"
					}));
					streamWriter.WriteLine(string.Concat(new object[]
					{
						"   lineArray[",
						j,
						"].rightDirection = Array(",
						Mathf.RoundToInt(vector4.x),
						", ",
						Mathf.RoundToInt(vector4.y),
						")"
					}));
				}
				streamWriter.WriteLine("   lineSubPathArray[" + i + "] = new SubPathInfo()");
				streamWriter.WriteLine("   lineSubPathArray[" + i + "].operation = ShapeOperation.SHAPEXOR");
				streamWriter.WriteLine(string.Concat(new object[]
				{
					"   lineSubPathArray[",
					i,
					"].closed = ",
					(!smoothCurve.ClosedShape) ? "false" : "true"
				}));
				streamWriter.WriteLine("   lineSubPathArray[" + i + "].entireSubPath = lineArray");
			}
			streamWriter.WriteLine("   var myPathItem = docRef.pathItems.add(\"A Line\", lineSubPathArray)");
			streamWriter.WriteLine("}");
		}
	}

	// Token: 0x04000E0F RID: 3599
	public float PixelsPerUnit = 16f;
}
