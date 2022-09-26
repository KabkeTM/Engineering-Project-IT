using UnityEngine;


	public class Posterize : MonoBehaviour {
		private Material m_material;
		private Shader shader;

		public int redComponent = 8;
		public int greenComponent = 8;
		public int blueComponent = 8;

		private Material material {
			get {
				if (m_material == null) {
					shader = Shader.Find("Custom/Posterize");
					m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				}

				return m_material;
			}
		}

		
		public void OnRenderImage(RenderTexture src, RenderTexture dest) {
			if (material) {
				redComponent = GraphicVariables.redComp;
				material.SetInt("_Red", redComponent);

				greenComponent = GraphicVariables.greenComp;
				material.SetInt("_Green", greenComponent);

				blueComponent = GraphicVariables.blueComp;
				material.SetInt("_Blue", blueComponent);

				Graphics.Blit(src, dest, material);
			}
		}

		private void OnDisable() {
			if (m_material)
				DestroyImmediate(m_material);
		}
	}