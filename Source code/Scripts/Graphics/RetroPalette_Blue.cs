using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Cam.Effects
{
	[Serializable]
	public class PaletteGrading_Blue
	{
		public int size = 6;

		[Range(0f, .5f)]
		public float blending = 0.25f;
		public Color blendColor1 = Color.white;
		public Color blendColor2 = Color.black;

		[HideInInspector] public Color c1B, c2B;
	}

	public class RetroPalette_Blue : MonoBehaviour
	{
		public List<PaletteGrading_Blue> gradings = new List<PaletteGrading_Blue>();
		public Color[] colors;

		private Material m_material;
		private Shader shader;

		private Material material
		{
			get
			{
				if (m_material == null)
				{
					shader = Shader.Find("Custom/RetroPalette");
					m_material = new Material(shader) { hideFlags = HideFlags.DontSave };
				}

				return m_material;
			}
		}


		public void BlendStops()
		{
			for (int i = 0; i < gradings.Count; i++)
			{
				PaletteGrading_Blue pgBefore = i == 0 ? null : gradings[i - 1];
				PaletteGrading_Blue pgNow = gradings[i];
				PaletteGrading_Blue pgAfter = i == gradings.Count - 1 ? null : gradings[i + 1];

				if (pgBefore != null)
					pgNow.c1B = Color.Lerp(pgBefore.blendColor2, pgNow.blendColor1, 1f - pgNow.blending);
				else
					pgNow.c1B = pgNow.blendColor1;

				if (pgAfter != null)
					pgNow.c2B = Color.Lerp(pgNow.blendColor2, pgAfter.blendColor1, pgNow.blending);
				else
					pgNow.c2B = pgNow.blendColor2;
			}
		}

		public void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			BlendStops();

			int s = 0;
			foreach (PaletteGrading_Blue pg in gradings)
				s += pg.size;

			colors = new Color[s];
			int i = 0;

			foreach (PaletteGrading_Blue pg in gradings)
			{
				for (int j = 0; j < pg.size; j++, i++)
				{
					colors[i] = Color.Lerp(pg.c1B, pg.c2B, (float)j / pg.size);
				}
			}

			if (material && colors.Length > 0)
			{
				material.SetInt("_ColorCount", colors.Length);
				material.SetColorArray("_Colors", colors);

				Graphics.Blit(src, dest, material);
			}
			else
			{
				Graphics.Blit(src, dest);
			}
		}
	}
}