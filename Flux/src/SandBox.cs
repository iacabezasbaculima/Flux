using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Flux.src.Flux.Core;
using Flux.src.Flux.Utility;
using FL = Flux.src.Flux.Renderer;

namespace Flux.src
{
	public class SandBox : GameWindow
	{
		FL.IShape cube;
		FL.IShape quad;
		FL.Shader lightShader;
		FL.Shader lampShader;
		FL.GuiRenderer guiRenderer;
		List<FL.GuiTexture> guis;
		FL.GuiTexture crate;
		FL.GuiTexture hulk;

		public double t = 0;
		// This is the position of both the light and the place the lamp cube will be drawn in the scene
		private readonly Vector3 _lightPos = new Vector3(2f, 0.0f, 0.0f);

		#region CAMERA 
		FL.FreeSpaceCamera camera;
		bool firstMove = true;
		Vector2 lastPos;
		const float cameraSpeed = 3.5f;
		const float sensitivity = 0.2f;
		#endregion

		public SandBox(int width, int height, string title) : base(width, height, new GraphicsMode(32, 24, 0, 8), title) { }
		protected override void OnLoad(EventArgs e)
		{
			Console.WriteLine($"OpenGL Info:\n\n" +
				$"	Vendor: {GL.GetString(StringName.Vendor)}\n" +
				$"	Renderer: {GL.GetString(StringName.Renderer)}\n" +
				$"	Version: {GL.GetString(StringName.Version)}\n" +
				$"	GLSL: {GL.GetString(StringName.ShadingLanguageVersion)}\n");
			
			FL.RenderCommand.SetClearColor(Color4.BlueViolet);
			FL.RenderCommand.SetDepthTest(true);
			FL.RenderCommand.SetMSAA(true);
			FL.RenderCommand.SetCullFace(true);
			FL.RenderCommand.Clear();

			// Camera
			camera = new FL.FreeSpaceCamera(Vector3.UnitZ * 10, Width / (float)Height);

			// Load shaders
			lightShader = FL.Shader.Create("Light", "light.vert", "light.frag");
			lampShader = FL.Shader.Create("Lamp", "light.vert", "lamp.frag");

			// Shapes
			quad = FL.Renderer.CreateQuad();
			cube = FL.Renderer.CreateCube();

			// GUI textures
			guiRenderer = new FL.GuiRenderer();
			guis = new List<FL.GuiTexture>();
			crate = new FL.GuiTexture(FL.Texture2D.Create("crate.jpg"), new Vector2(0f, 0f), new Vector2(0.25f, 0.25f));
			hulk = new FL.GuiTexture(FL.Texture2D.Create("hulk256.png"), new Vector2(.5f, .5f), new Vector2(0.5f, 0.5f));
			guis.Add(crate);
			guis.Add(hulk);

			// Subscribe to game loop events
			RenderFrame += SandBox_RenderFrame;
			UpdateFrame += SandBox_UpdateFrame;
			
			base.OnLoad(e);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SandBox_UpdateFrame(object sender, FrameEventArgs e)
		{
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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SandBox_RenderFrame(object sender, FrameEventArgs e)
		{
			t += 3.5 + e.Time;
			FL.RenderCommand.Clear();
			FL.Renderer.BeginScene(camera);
			{

				lightShader.Bind();

				lightShader.SetMatrix4("model", Matrix4.Identity);
				lightShader.SetMatrix4("view", camera.ViewMatrix);
				lightShader.SetMatrix4("projection", camera.ProjectionMatrix);
				lightShader.SetVector3("viewPos", camera.Position);

				// Here we set the material values of the cube, the material struct is just a container so to access
				// the underlying values we simply type "material.value" to get the location of the uniform
				lightShader.SetVector3("material.ambient", new Vector3(1.0f, 0.5f, 0.31f));
				lightShader.SetVector3("material.diffuse", new Vector3(1.0f, 0.5f, 0.31f));
				lightShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
				lightShader.SetFloat("material.shininess", 32.0f);

				// This is where we change the lights color over time using the sin function
				Vector3 lightColor;
				float time = DateTime.Now.Second + DateTime.Now.Millisecond / 1000f;
				lightColor.X = (float)Math.Sin(time * 2.0f);
				lightColor.Y = (float)Math.Sin(time * 0.7f);
				lightColor.Z = (float)Math.Sin(time * 1.3f);

				// The ambient light is less intensive than the diffuse light in order to make it less dominant
				Vector3 ambientColor = lightColor * new Vector3(0.2f);
				Vector3 diffuseColor = lightColor * new Vector3(0.5f);

				lightShader.SetVector3("light.position", _lightPos);
				lightShader.SetVector3("light.ambient", ambientColor);
				lightShader.SetVector3("light.diffuse", diffuseColor);
				lightShader.SetVector3("light.specular", new Vector3(1.0f, 1.0f, 1.0f));

				cube.Draw();

				// Draw lamp
				lampShader.Bind();
				lampShader.SetMatrix4("model", MathUtility.CreateTransformationMatrix(_lightPos, new Vector3(.2f)));
				lampShader.SetMatrix4("view", camera.ViewMatrix);
				lampShader.SetMatrix4("projection", camera.ProjectionMatrix);

				cube.Draw();

				// Draw GUI

				guiRenderer.Render(guis);
			}

			Context.SwapBuffers();
		}
		protected override void OnResize(EventArgs e)
		{
			FL.RenderCommand.SetViewport(0, 0, Width, Height); // Maps Native Device Coordinates (NDC) to Window context
			camera.AspectRatio = Width / (float)Height;
			//Console.WriteLine($"[{Width}, {Height}]");
			//foreach (var gui in guis)
			//{
			//	Console.WriteLine($"[{gui.Scale.X * Width}, {gui.Scale.Y * Height}], {gui.Position}]");
			//	Console.WriteLine($"Top-Left: [{gui.Position.X - (gui.Scale.X * Width) / 2f}, {gui.Position.Y + (gui.Scale.Y * Height) / 2f}]");
			//}
			base.OnResize(e);
		} 
		protected override void OnMouseMove(MouseMoveEventArgs e)
		{
			base.OnMouseMove(e);
			if (Focused) // check to see if the window is focused
			{
				Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
			}
		}
		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			camera.Fov -= e.DeltaPrecise;
		}
		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Exit();
		}
	}
}
