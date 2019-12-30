using System;
using Flux.src.Flux.Renderer;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace Sandbox.src
{
	public class ObjTest : GameWindow
	{
		FreeSpaceCameraController camera;


		Shader flatShader;
		Shader texShader;
		IShape cube;

		// Obj data
		Tuple<float[], float[], uint[]> ObjData;
		VertexArray vao;
		Texture2D texCrate;
		float[] verts = null;
		float[] texcoords = null;
		uint[] indices = null;

		public ObjTest() : base(512, 512, new GraphicsMode(32, 24, 0, 4), "Test Scene: Loading .obj files.")
		{
			camera = new FreeSpaceCameraController(Vector3.UnitZ * 10, Width / (float)Height);

			cube = Renderer.CreateCube();
			
			flatShader = Shader.Create("FlatColor", "flatcolor.vert", "flatcolor.frag");
			texShader = Shader.Create("Texture", "texture.vert", "texture.frag");
					
			ObjData = ObjLoader.LoadFromFile("stall.obj");
			ObjData.Deconstruct(out verts, out texcoords, out indices);

			vao = VertexArray.Create();

			// Add vertex data to buffers
			VertexBuffer vboPos = VertexBuffer.Create(verts);
			BufferLayout layout = new BufferLayout { { ShaderDataType.Float3, "Position" } };
			vboPos.SetLayout(layout);
			vao.AddVertexBuffer(vboPos);

			//VertexBuffer vboColor = VertexBuffer.Create(verts);
			//BufferLayout colorLayout = new BufferLayout { { ShaderDataType.Float3, "Color"} };
			//vboColor.SetLayout(colorLayout);
			//vao.AddVertexBuffer(vboColor);

			VertexBuffer vboTex = VertexBuffer.Create(texcoords);
			BufferLayout texLayout = new BufferLayout { { ShaderDataType.Float2, "TexCoords"} };
			vboTex.SetLayout(texLayout);
			vao.AddVertexBuffer(vboTex);
			
			IndexBuffer ibo = IndexBuffer.Create(indices);
			vao.SetIndexBuffer(ibo);

			texCrate = Texture2D.Create("stallTexture.png");

			Console.WriteLine("\nFinished loading data.");
		}

		protected override void OnLoad(EventArgs e)
		{
			RenderCommand.Init();
			RenderCommand.SetClearColor(Color4.YellowGreen);
			RenderCommand.Clear();

			// Subscribe to game loop events
			RenderFrame += ObjTest_RenderFrame; 
			UpdateFrame += ObjTest_UpdateFrame; 
		}

		private void ObjTest_UpdateFrame(object sender, FrameEventArgs e)
		{
			if (!Focused) return;

			camera.OnUpdate(e.Time);
		}

		private void ObjTest_RenderFrame(object sender, FrameEventArgs e)
		{
			RenderCommand.Clear();

			Matrix4 model = Matrix4.Identity;
			texShader.Bind();
			texShader.SetMVPMatrix(model, camera.Camera.ViewMatrix, camera.Camera.ProjectionMatrix);

			vao.Bind();
			texCrate.Bind();
			RenderCommand.DrawIndexed(vao);
			vao.Unbind();

			SwapBuffers();
		}
		protected override void OnResize(EventArgs e)
		{
			RenderCommand.SetViewport(0, 0, Width, Height);
			camera.OnResize(Width, Height);
		}
		protected override void OnMouseMove(MouseMoveEventArgs e)
		{
			if (Focused) // check to see if the window is focused
			{
				Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
			}
		}
		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Exit();
		}
	}
}
