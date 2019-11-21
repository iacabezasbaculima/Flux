using System;
using Flux.src.Flux.Renderer;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLPyramid : IShape
	{
		readonly float[] vertices =
		{
			//Position          Texture coordinates
			-0.5f, -0.5f, -0.5f, 1f, 0f, 0f, // top right
			+0.5f, -0.5f, -0.5f, 0f, 1f, 0f, // bottom right
			+0.5f, -0.5f, +0.5f, 0f, 0f, 1f, // bottom left
			-0.5f, -0.5f, +0.5f, 0f, 1f, 1f, // top left
			+0.0f, +0.5f, +0.0f, 1f, 1f, 0f
		};
		readonly uint[] indices =
		{
			// Base
			0, 2, 1,
			0, 2, 3,
			
			// Sides
			4, 0, 1,
			4, 1, 2,
			4, 2, 3,
			4, 3, 0
		};
		public OpenGLVertexArray vao;
		public OpenGLPyramid()
		{
			Init();
		}
		public void Init()
		{
			vao = new OpenGLVertexArray();
			OpenGLVertexBuffer vbo = new OpenGLVertexBuffer(vertices, vertices.Length, OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw);
			BufferLayout bl = new BufferLayout
			{
				{ShaderDataType.Float3, "Position" },
				{ShaderDataType.Float3, "Color" }
			};
			bl.CalculateOffsetsAndStride();
			vbo.SetLayout(bl);
			vao.AddVertexBuffer(vbo);
			OpenGLIndexBuffer ibo = new OpenGLIndexBuffer(indices, OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw);
			vao.SetIndexBuffer(ibo);
		}
		public void Draw()
		{
			vao.Bind();
			GL.DrawElements(PrimitiveType.Triangles, vao.GetIndexBuffer().GetCount(), DrawElementsType.UnsignedInt, 0);
		}
	}
}
