using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Flux.src.Flux.Renderer;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLCube : IShape
	{
		private readonly float[] _verticesColors = {
			-0.5f, -0.5f, -0.5f, 0.583f,  0.771f,  0.014f, // triangle 1 : begin
			 0.5f, -0.5f, -0.5f, 0.609f,  0.115f,  0.436f,
			 0.5f,  0.5f, -0.5f, 0.327f,  0.483f,  0.844f, // triangle 1 : end
			 0.5f,  0.5f, -0.5f, 0.583f,  0.771f,  0.014f,// triangle 2 : begin
			-0.5f,  0.5f, -0.5f, 0.609f,  0.115f,  0.436f,
			-0.5f, -0.5f, -0.5f, 0.327f,  0.483f,  0.844f,// triangle 2 : end

			-0.5f, -0.5f,  0.5f, 0.583f,  0.771f,  0.014f,
			 0.5f, -0.5f,  0.5f, 0.609f,  0.115f,  0.436f,
			 0.5f,  0.5f,  0.5f, 0.327f,  0.483f,  0.844f,
			 0.5f,  0.5f,  0.5f, 0.583f,  0.771f,  0.014f,
			-0.5f,  0.5f,  0.5f, 0.609f,  0.115f,  0.436f,
			-0.5f, -0.5f,  0.5f, 0.327f,  0.483f,  0.844f,

			-0.5f,  0.5f,  0.5f, 0.583f,  0.771f,  0.014f,
			-0.5f,  0.5f, -0.5f, 0.609f,  0.115f,  0.436f,
			-0.5f, -0.5f, -0.5f, 0.327f,  0.483f,  0.844f,
			-0.5f, -0.5f, -0.5f, 0.583f,  0.771f,  0.014f,
			-0.5f, -0.5f,  0.5f, 0.609f,  0.115f,  0.436f,
			-0.5f,  0.5f,  0.5f, 0.327f,  0.483f,  0.844f,

			 0.5f,  0.5f,  0.5f, 0.583f,  0.771f,  0.014f,
			 0.5f,  0.5f, -0.5f, 0.609f,  0.115f,  0.436f,
			 0.5f, -0.5f, -0.5f, 0.327f,  0.483f,  0.844f,
			 0.5f, -0.5f, -0.5f, 0.583f,  0.771f,  0.014f,
			 0.5f, -0.5f,  0.5f, 0.609f,  0.115f,  0.436f,
			 0.5f,  0.5f,  0.5f, 0.327f,  0.483f,  0.844f,

			-0.5f, -0.5f, -0.5f, 0.583f,  0.771f,  0.014f,
			 0.5f, -0.5f, -0.5f, 0.609f,  0.115f,  0.436f,
			 0.5f, -0.5f,  0.5f, 0.327f,  0.483f,  0.844f,
			 0.5f, -0.5f,  0.5f, 0.583f,  0.771f,  0.014f,
			-0.5f, -0.5f,  0.5f, 0.609f,  0.115f,  0.436f,
			-0.5f, -0.5f, -0.5f, 0.327f,  0.483f,  0.844f,

			-0.5f,  0.5f, -0.5f, 0.583f,  0.771f,  0.014f,
			 0.5f,  0.5f, -0.5f, 0.609f,  0.115f,  0.436f,
			 0.5f,  0.5f,  0.5f, 0.327f,  0.483f,  0.844f,
			 0.5f,  0.5f,  0.5f, 0.583f,  0.771f,  0.014f,
			-0.5f,  0.5f,  0.5f, 0.609f,  0.115f,  0.436f,
			-0.5f,  0.5f, -0.5f, 0.327f,  0.483f,  0.844f
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
		
		public VertexArray vao;

		public enum VAODataType { VERTS, VERTS_COLORS, VERTS_NORMALS }
		
		public OpenGLCube(VAODataType type)
		{
			Init(type);
		}
		private void Init(VAODataType type)
		{
			vao = VertexArray.Create();

			BufferLayout layout = null;
			VertexBuffer vbo = null;
			IndexBuffer ibo = null;

			switch (type)
			{
				case VAODataType.VERTS_COLORS:
					layout = new BufferLayout { { ShaderDataType.Float3, "position" }, { ShaderDataType.Float3, "colors" } };
					vbo = VertexBuffer.Create(_verticesColors);
					ibo = IndexBuffer.Create(_indices);
					break;
				case VAODataType.VERTS_NORMALS:
					layout = new BufferLayout { { ShaderDataType.Float3, "position" }, { ShaderDataType.Float3, "normals" } };
					vbo = VertexBuffer.Create(_verticesNormals);
					ibo = IndexBuffer.Create(_indices);
					break;
				case VAODataType.VERTS:
					layout = new BufferLayout { { ShaderDataType.Float3, "position" } };
					vbo = VertexBuffer.Create(_vertices);
					ibo = IndexBuffer.Create(_indices);
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
