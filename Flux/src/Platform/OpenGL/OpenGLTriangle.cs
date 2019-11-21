using Flux.src.Flux.Renderer;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLTriangle : IShape
	{
		readonly float[] vertices =
		{
			//Position          Texture coordinates
			 0.0f,  0.5f, 0.0f, 0.5f, 1.0f, // top right
			 0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
			-0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
		};
		public OpenGLVertexArray vao;
		internal OpenGLTriangle()
		{
			Init();
		}

		private void Init()
		{
			vao = new OpenGLVertexArray();

			OpenGLVertexBuffer vbo = new OpenGLVertexBuffer(vertices, vertices.Length, OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw);
			BufferLayout bl = new BufferLayout
			{
				{ShaderDataType.Float3, "position"},
				{ShaderDataType.Float2, "texture" }
			};
			bl.CalculateOffsetsAndStride();
			vbo.SetLayout(bl);
			vao.AddVertexBuffer(vbo);
		}
		public void Draw()
		{
			vao.Bind();
			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
		}
	}
}
