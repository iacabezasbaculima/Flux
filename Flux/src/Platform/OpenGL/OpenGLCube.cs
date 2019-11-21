using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Flux.src.Flux.Renderer;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLCube : IShape
	{
		private readonly float[] _verticesColors = {
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
		private readonly float[] _verticesNormals =
		{
             // Position          Normal
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
			 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
			 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
			-0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
			-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

			-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
			-0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
			-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

			-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
			-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
			-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
			-0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
			-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

			 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
			 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
			 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
			 0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
			 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

			-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
			 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
			 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
			-0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
			-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

			-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
			-0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
			-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
		};

		public OpenGLVertexArray vao;

		public enum VAODataType { VERTS_COLORS, VERTS_NORMALS}
		
		public OpenGLCube(VAODataType type)
		{
			Init(type);
		}
		private void Init(VAODataType type)
		{
			vao = new OpenGLVertexArray();

			BufferLayout layout = null;
			OpenGLVertexBuffer vbo = null;

			switch (type)
			{
				case VAODataType.VERTS_COLORS:
					layout = new BufferLayout { { ShaderDataType.Float3, "position" }, { ShaderDataType.Float3, "colors" } };
					vbo = new OpenGLVertexBuffer(_verticesColors, _verticesColors.Length, BufferUsageHint.StaticDraw);
					break;
				case VAODataType.VERTS_NORMALS:
					layout = new BufferLayout { { ShaderDataType.Float3, "position" }, { ShaderDataType.Float3, "normals" } };
					vbo = new OpenGLVertexBuffer(_verticesNormals, _verticesNormals.Length, BufferUsageHint.StaticDraw);
					break;
				default:
					break;
			}
			layout.CalculateOffsetsAndStride();
			vbo.SetLayout(layout);
			vao.AddVertexBuffer(vbo);
		}
		public void Draw()
		{
			vao.Bind();	// REMEMBER TO BIND THE VERTEX ARRAY OBJECT BEFOREHAND (JUST IN CASE)
			//shader.Use();
			//shader.SetMatrix4("model", modelMatrix);
			//shader.SetMatrix4("view", viewMatrix);
			//shader.SetMatrix4("projection", projectionMatrix);

			GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
		}
	}
}
