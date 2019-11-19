using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using Flux.src.Flux.Core;
using FL = Flux.src.Flux.Renderer;

namespace Flux.src
{
	public class SandBox : Window
	{
		FL.Shader simple;
		FL.IShape cube;
		FL.IShape quad;
		FL.IShape pyramid;

		FL.Camera camera;
		bool firstMove = true;
		Vector2 lastPos;
		const float cameraSpeed = 1.5f;
		const float sensitivity = 0.2f;

		public SandBox(int width, int height) : base(width, height) { }
		protected override void OnLoad(EventArgs e)
		{
			FL.RenderCommand.SetClearColor(Color4.CornflowerBlue);
			FL.RenderCommand.EnableDepthTest();
			FL.RenderCommand.Clear();

			simple = new FL.Shader("assets/shaders/simple.vert", "assets/shaders/simple.frag");
			if (simple != null) Console.WriteLine($"{"Shader loaded successfully."}{" ID: "+simple.Handle}");

			// Wooden crate texture
			//woodTex = new Texture("assets/images/crate.jpg");

			// Camera
			camera = new FL.Camera(Vector3.UnitZ * 5f, Width / (float)Height);
			
			// Shapes
			quad = FL.Renderer.CreateQuad();
			cube = FL.Renderer.CreateCube();
			pyramid = FL.Renderer.CreatePyramid();

			CursorVisible = false;

			base.OnLoad(e);
		}
		public double t = 0;
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			t += 3.5 + e.Time;
			FL.RenderCommand.EnableDepthTest();
			FL.RenderCommand.Clear();
			FL.Renderer.BeginScene(camera);
			{
				var md = Matrix4.Identity;
				md *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians((float)t));
				pyramid.Draw(simple, md, camera.GetViewMatrix(), camera.GetProjectionMatrix());

				md *= Matrix4.CreateScale(0.2f) * Matrix4.CreateTranslation(-3f, 0f, 0f);
				quad.Draw(simple, md, camera.GetViewMatrix(), camera.GetProjectionMatrix());

				md *= Matrix4.CreateTranslation(6f, 0f, 0f);
				cube.Draw(simple, md, camera.GetViewMatrix(), camera.GetProjectionMatrix());
			}
			FL.Renderer.EndScene();

			Context.SwapBuffers();

			base.OnRenderFrame(e);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			FL.RenderCommand.SetViewport(0, 0, Width, Height); // Maps Native Device Coordinates (NDC) to Window context
			camera.AspectRatio = Width / (float)Height;
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
			
			if (!Focused) // check to see if the window is focused
			{
				return;
			}

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
		protected override void OnMouseMove(MouseMoveEventArgs e)
		{
			base.OnMouseMove(e);
			if (Focused) // check to see if the window is focused
			{
				Mouse.SetPosition(Width / 2f, Height / 2f);
			}
		}
		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			base.OnMouseWheel(e);
			camera.Fov -= e.DeltaPrecise;
		}
		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Key == Key.Escape)
				Exit();
				
		}
		protected override void OnKeyUp(KeyboardKeyEventArgs e)
		{
			base.OnKeyUp(e);

		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			var input = Keyboard.GetState();

			//if (input.IsKeyDown(Key.N)) isready = true;

			base.OnKeyPress(e);
		}
	}
}
