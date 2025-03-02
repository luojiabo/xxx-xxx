﻿using UnityEngine;
using System.Collections;

namespace Ubtrobot
{
	public class GridOverlay : MonoBehaviour
	{ 
		//public GameObject plane;

		public bool showMain = true;
		public bool showSub = false;

		public int gridSizeX;
		public int gridSizeY;
		public int gridSizeZ;

		public float smallStep;
		public float largeStep;

		public float startX;
		public float startY;
		public float startZ;

		private Material lineMaterial;

		public Color mainColor = new Color(0f, 1f, 0f, 1f);
		public Color subColor = new Color(0f, 0.5f, 0f, 1f);

		void CreateLineMaterial()
		{
			if (!lineMaterial)
			{
				// Unity has a built-in shader that is useful for drawing
				// simple colored things.
				var shader = Shader.Find("Hidden/Internal-Colored");
				lineMaterial = new Material(shader);
				lineMaterial.hideFlags = HideFlags.HideAndDontSave;
				// Turn on alpha blending
				lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				// Turn backface culling off
				lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
				// Turn off depth writes
				lineMaterial.SetInt("_ZWrite", 0);
			}
		}

		void OnPostRender()
		{
			if (showSub || showMain)
			{
				CreateLineMaterial();
				// set the current material
				lineMaterial.SetPass(0);

				GL.Begin(GL.LINES);

				if (showSub)
				{
					GL.Color(subColor);

					//Layers
					for (float j = 0; j <= gridSizeY; j += smallStep)
					{
						//X axis lines
						for (float i = 0; i <= gridSizeZ; i += smallStep)
						{
							GL.Vertex3(startX, startY + j, startZ + i);
							GL.Vertex3(startX + gridSizeX, startY + j, startZ + i);
						}

						//Z axis lines
						for (float i = 0; i <= gridSizeX; i += smallStep)
						{
							GL.Vertex3(startX + i, startY + j, startZ);
							GL.Vertex3(startX + i, startY + j, startZ + gridSizeZ);
						}
					}

					//Y axis lines
					for (float i = 0; i <= gridSizeZ; i += smallStep)
					{
						for (float k = 0; k <= gridSizeX; k += smallStep)
						{
							GL.Vertex3(startX + k, startY, startZ + i);
							GL.Vertex3(startX + k, startY + gridSizeY, startZ + i);
						}
					}
				}

				if (showMain)
				{
					GL.Color(mainColor);

					//Layers
					for (float j = 0; j <= gridSizeY; j += largeStep)
					{
						//X axis lines
						for (float i = 0; i <= gridSizeZ; i += largeStep)
						{
							GL.Vertex3(startX, startY + j, startZ + i);
							GL.Vertex3(startX + gridSizeX, startY + j, startZ + i);
						}

						//Z axis lines
						for (float i = 0; i <= gridSizeX; i += largeStep)
						{
							GL.Vertex3(startX + i, startY + j, startZ);
							GL.Vertex3(startX + i, startY + j, startZ + gridSizeZ);
						}
					}

					//Y axis lines
					for (float i = 0; i <= gridSizeZ; i += largeStep)
					{
						for (float k = 0; k <= gridSizeX; k += largeStep)
						{
							GL.Vertex3(startX + k, startY, startZ + i);
							GL.Vertex3(startX + k, startY + gridSizeY, startZ + i);
						}
					}
				}


				GL.End();
			}
		}
	}
}
