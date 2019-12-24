using OpenTK;
using OpenTK.Graphics.OpenGL;
using Flux.src.Flux.Renderer;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLQuad : IShape
	{
		readonly float[] vertices = {
			 1f,  1f, 0.0f, // top right
			 1f, -1f, 0.0f, // bottom right
			-1f, -1f, 0.0f,	// bottom left
			-1f,  1f, 0.0f 	// top left
		};
		readonly uint[] elements = {  // note that we start from 0!
			0, 1, 3,   // first triangle
			1, 2, 3    // second triangle
		};
		public OpenGLVertexArray vao;
		public OpenGLQuad()
		{
			Init();
		}
		public void Init()
		{
			vao = new OpenGLVertexArray();

			OpenGLVertexBuffer vbo = new OpenGLVertexBuffer(vertices, vertices.Length, BufferUsageHint.StaticDraw);
			BufferLayout bl = new BufferLayout
			{
				{ShaderDataType.Float3, "position" }
			};
			bl.CalculateOffsetsAndStride();
			vbo.SetLayout(bl);
			vao.AddVertexBuffer(vbo);
			OpenGLIndexBuffer ibo = new OpenGLIndexBuffer(elements, BufferUsageHint.StaticDraw);
			vao.SetIndexBuffer(ibo);
		}
		public void Draw()
		{
			//vao.Bind();
			GL.DrawElements(PrimitiveType.Triangles, vao.GetIndexBuffer().GetCount(), DrawElementsType.UnsignedInt, 0);
		}
	}
}
