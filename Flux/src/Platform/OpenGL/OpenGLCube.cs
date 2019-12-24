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

		private readonly uint[] _indices =
		{
			//left
            0, 2, 1,
			0, 3, 2,
            //back
            1, 2, 6,
			6, 5, 1,
            //right
            4, 5, 6,
			6, 7, 4,
            //top
            2, 3, 6,
			6, 3, 7,
            //front
            0, 7, 3,
			0, 4, 7,
            //bottom
            0, 1, 5,
			0, 5, 4
		};
		private readonly float[] _vertices =
		{
			-0.5f, -0.5f,  -0.5f,
			 0.5f, -0.5f,  -0.5f,
			 0.5f,  0.5f,  -0.5f,
			-0.5f,  0.5f,  -0.5f,
			-0.5f, -0.5f,   0.5f,
			 0.5f, -0.5f,   0.5f,
			 0.5f,  0.5f,   0.5f,
			-0.5f,  0.5f,   0.5f
		};
		public OpenGLVertexArray vao;

		public enum VAODataType { VERTS_COLORS, VERTS_NORMALS, VERTS_INDICES}
		
		public OpenGLCube(VAODataType type)
		{
			Init(type);
		}
		private void Init(VAODataType type)
		{
			vao = new OpenGLVertexArray();

			BufferLayout layout = null;
			OpenGLVertexBuffer vbo = null;
			OpenGLIndexBuffer ibo = null;

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
				case VAODataType.VERTS_INDICES:
					layout = new BufferLayout { { ShaderDataType.Float3, "position" } };
					vbo = new OpenGLVertexBuffer(_vertices, _vertices.Length, BufferUsageHint.StaticDraw);
					ibo = new OpenGLIndexBuffer(_indices, BufferUsageHint.StaticDraw);
					break;
				default:
					break;
			}
			layout.CalculateOffsetsAndStride();
			vbo.SetLayout(layout);
			vao.AddVertexBuffer(vbo);
			vao.SetIndexBuffer(ibo);
			
		}
		public void Draw()
		{
			vao.Bind(); // REMEMBER TO BIND THE VERTEX ARRAY OBJECT BEFOREHAND (JUST IN CASE)
			
			
			GL.DrawElements(PrimitiveType.Triangles, vao.GetIndexBuffer().GetCount(), DrawElementsType.UnsignedInt, 0);
			//GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
		}
	}
}
