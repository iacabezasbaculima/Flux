using OpenTK;
using OpenTK.Input;

namespace Flux.src.Flux.Renderer
{
	public class FreeSpaceCameraController
	{
		private const float cameraSpeed = 3.5f;
		private const float cameraSensitivity = 0.2f;
		private bool firstMove = true;
		Vector2 lastPos;

		public FreeSpaceCamera Camera { get; }

		public FreeSpaceCameraController(Vector3 position, float aspectRatio)
		{
			Camera = new FreeSpaceCamera(position, aspectRatio);
		}

		public void OnUpdate(double dt) 
		{
			var input = Keyboard.GetState();

			if (input.IsKeyDown(Key.W))
				Camera.Position += Camera.Front * cameraSpeed * (float)dt; // Forward 
			if (input.IsKeyDown(Key.S))
				Camera.Position -= Camera.Front * cameraSpeed * (float)dt; // Backwards
			if (input.IsKeyDown(Key.A))
				Camera.Position -= Camera.Right * cameraSpeed * (float)dt; // Left
			if (input.IsKeyDown(Key.D))
				Camera.Position += Camera.Right * cameraSpeed * (float)dt; // Right
			if (input.IsKeyDown(Key.LShift))
				Camera.Position += Camera.Up * cameraSpeed * (float)dt; // Up 
			if (input.IsKeyDown(Key.LControl))
				Camera.Position -= Camera.Up * cameraSpeed * (float)dt; // Down

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

				// Apply the Camera pitch and yaw (we clamp the pitch in the Camera class)
				Camera.Yaw += deltaX * cameraSensitivity;
				Camera.Pitch -= deltaY * cameraSensitivity; // reversed since y-coordinates range from bottom to top
				//Console.WriteLine("Yaw: {0}, Pitch: {1}", Camera.Yaw, Camera.Pitch);
			}
		}
		
		public void OnResize(int width, int height)
		{
			Camera.AspectRatio = width / (float)height;
		}
		
		public void OnMouseScroll(float deltaScroll)
		{
			Camera.Fov -= deltaScroll;
		}
	}
}
