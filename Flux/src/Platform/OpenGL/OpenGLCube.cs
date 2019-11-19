using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Flux.src.Flux.Renderer;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLCube : IShape
	{
		readonly float[] vertices = {
				-1.0f,-1.0f,-1.0f, 0.583f,  0.771f,  0.014f, // triangle 1 : begin
				-1.0f,-1.0f, 1.0f, 0.609f,  0.115f,  0.436f,
				-1.0f, 1.0f, 1.0f, 0.327f,  0.483f,  0.844f, // triangle 1 : end
				1.0f, 1.0f,-1.0f,  0.583f,  0.771f,  0.014f,// triangle 2 : begin
				-1.0f,-1.0f,-1.0f, 0.609f,  0.115f,  0.436f,
				-1.0f, 1.0f,-1.0f, 0.327f,  0.483f,  0.844f,// triangle 2 : end
				1.0f,-1.0f, 1.0f,  0.583f,  0.771f,  0.014f,
				-1.0f,-1.0f,-1.0f, 0.609f,  0.115f,  0.436f,
				1.0f,-1.0f,-1.0f,  0.327f,  0.483f,  0.844f,
				1.0f, 1.0f,-1.0f,  0.583f,  0.771f,  0.014f,
				1.0f,-1.0f,-1.0f,  0.609f,  0.115f,  0.436f,
				-1.0f,-1.0f,-1.0f, 0.327f,  0.483f,  0.844f,
				-1.0f,-1.0f,-1.0f, 0.583f,  0.771f,  0.014f,
				-1.0f, 1.0f, 1.0f, 0.609f,  0.115f,  0.436f,
				-1.0f, 1.0f,-1.0f, 0.327f,  0.483f,  0.844f,
				1.0f,-1.0f, 1.0f,  0.583f,  0.771f,  0.014f,
				-1.0f,-1.0f, 1.0f, 0.609f,  0.115f,  0.436f,
				-1.0f,-1.0f,-1.0f, 0.327f,  0.483f,  0.844f,
				-1.0f, 1.0f, 1.0f, 0.583f,  0.771f,  0.014f,
				-1.0f,-1.0f, 1.0f, 0.609f,  0.115f,  0.436f,
				1.0f,-1.0f, 1.0f,  0.327f,  0.483f,  0.844f,
				1.0f, 1.0f, 1.0f,  0.583f,  0.771f,  0.014f,
				1.0f,-1.0f,-1.0f,  0.609f,  0.115f,  0.436f,
				1.0f, 1.0f,-1.0f,  0.327f,  0.483f,  0.844f,
				1.0f,-1.0f,-1.0f,  0.583f,  0.771f,  0.014f,
				1.0f, 1.0f, 1.0f,  0.609f,  0.115f,  0.436f,
				1.0f,-1.0f, 1.0f,  0.327f,  0.483f,  0.844f,
				1.0f, 1.0f, 1.0f,  0.583f,  0.771f,  0.014f,
				1.0f, 1.0f,-1.0f,  0.609f,  0.115f,  0.436f,
				-1.0f, 1.0f,-1.0f, 0.327f,  0.483f,  0.844f,
				1.0f, 1.0f, 1.0f,  0.583f,  0.771f,  0.014f,
				-1.0f, 1.0f,-1.0f, 0.609f,  0.115f,  0.436f,
				-1.0f, 1.0f, 1.0f, 0.327f,  0.483f,  0.844f,
				1.0f, 1.0f, 1.0f,  0.583f,  0.771f,  0.014f,
				-1.0f, 1.0f, 1.0f, 0.609f,  0.115f,  0.436f,
				1.0f,-1.0f, 1.0f,  0.327f,  0.483f,  0.844f
			};
		public OpenGLVertexArray vao;
		
		public OpenGLCube()
		{
			Init();
		}
		private void Init()
		{
			vao = new OpenGLVertexArray();

			OpenGLVertexBuffer vbo = new OpenGLVertexBuffer(vertices, vertices.Length, BufferUsageHint.StaticDraw);
			BufferLayout bl = new BufferLayout
			{
				{ShaderDataType.Float3, "position" },
				{ShaderDataType.Float3, "color"}
			};
			bl.CalculateOffsetsAndStride();
			vbo.SetLayout(bl);
			vao.AddVertexBuffer(vbo);
		}
		public void Draw(Shader shader, Matrix4 modelMatrix, Matrix4 viewMatrix, Matrix4 projectionMatrix)
		{
			vao.Bind();	// REMEMBER TO BIND THE VERTEX ARRAY OBJECT BEFOREHAND
			shader.Use();
			shader.SetMatrix4("model", modelMatrix);
			shader.SetMatrix4("view", viewMatrix);
			shader.SetMatrix4("projection", projectionMatrix);

			GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
		}
	}
}
