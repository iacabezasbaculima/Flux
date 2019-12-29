using System;
using System.Collections.Generic;
using Flux.src.Flux.Renderer;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Flux.src
{
	public class ObjTest : GameWindow
	{
		FreeSpaceCamera camera;
		bool firstMove = true;
		Vector2 lastPos;
		const float cameraSpeed = 3.5f;
		const float sensitivity = 0.2f;


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
			camera = new FreeSpaceCamera(Vector3.UnitZ * 10, Width / (float)Height);

			cube = Renderer.CreateCube();
			
			flatShader = Shader.Create("FlatColor", "flatcolor.vert", "flatcolor.frag");
			texShader = Shader.Create("Texture", "texture.vert", "texture.frag");

			ObjData = ObjLoader.LoadFromFile("mycube.obj");
			ObjData.Deconstruct(out verts, out texcoords, out indices);

			vao = VertexArray.Create();

			// Add vertex data to buffers
			VertexBuffer vboPos = VertexBuffer.Create(verts);
			BufferLayout layout = new BufferLayout { { ShaderDataType.Float3, "Position" } };
			vboPos.SetLayout(layout);
			vao.AddVertexBuffer(vboPos);

			VertexBuffer vboColor = VertexBuffer.Create(verts);
			BufferLayout colorLayout = new BufferLayout { { ShaderDataType.Float3, "Color"} };
			vboColor.SetLayout(colorLayout);
			vao.AddVertexBuffer(vboColor);

			VertexBuffer vboTex = VertexBuffer.Create(texcoords);
			BufferLayout texLayout = new BufferLayout { { ShaderDataType.Float2, "TexCoords"} };
			vboTex.SetLayout(texLayout);
			vao.AddVertexBuffer(vboTex);
			
			IndexBuffer ibo = IndexBuffer.Create(indices);
			vao.SetIndexBuffer(ibo);

			texCrate = Texture2D.Create("crate.jpg");

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

			var input = Keyboard.GetState();

			if (input.IsKeyDown(Key.W))
				camera.Position += camera.Front * cameraSpeed * (float)e.Time; // Forward 
			if (input.IsKeyDown(Key.S))
				camera.Position -= camera.Front * cameraSpeed * (float)e.Time; // Backwards
			if (input.IsKeyDown(Key.A))
				camera.Position -= camera.Right * cameraSpeed * (float)e.Time; // Left
			if (input.IsKeyDown(Key.D))
				camera.Position += camera.Right * cameraSpeed * (float)e.Time; // Right
			if (input.IsKeyDown(Key.LShift))
				camera.Position += camera.Up * cameraSpeed * (float)e.Time; // Up 
			if (input.IsKeyDown(Key.LControl))
				camera.Position -= camera.Up * cameraSpeed * (float)e.Time; // Down


			// Get the mouse state
			var mouse = Mouse.GetState();

			if (firstMove) // this bool variable is initially set to true
			{
				lastPos = new Vector2(mouse.X, mouse.Y);
				firstMove = false;
			}
			else
			{
				// Calculate the offset of the mouse position
				var deltaX = mouse.X - lastPos.X;
				var deltaY = mouse.Y - lastPos.Y;
				lastPos = new Vector2(mouse.X, mouse.Y);

				// Apply the camera pitch and yaw (we clamp the pitch in the camera class)
				camera.Yaw += deltaX * sensitivity;
				camera.Pitch -= deltaY * sensitivity; // reversed since y-coordinates range from bottom to top
													  //Console.WriteLine("Yaw: {0}, Pitch: {1}", camera.Yaw, camera.Pitch);
			}
		}

		private void ObjTest_RenderFrame(object sender, FrameEventArgs e)
		{
			RenderCommand.Clear();

			Matrix4 model = Matrix4.Identity;
			texShader.Bind();
			texShader.SetMVPMatrix(model, camera.ViewMatrix, camera.ProjectionMatrix);

			vao.Bind();
			texCrate.Bind();
			RenderCommand.DrawIndexed(vao);
			vao.Unbind();

			SwapBuffers();
		}
		protected override void OnResize(EventArgs e)
		{
			RenderCommand.SetViewport(0, 0, Width, Height);
			camera.AspectRatio = Width / (float)Height;
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
