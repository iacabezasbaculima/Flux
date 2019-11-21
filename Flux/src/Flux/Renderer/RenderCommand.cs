
namespace Flux.src.Flux.Renderer
{
	public class RenderCommand : Platform.OpenGL.OpenGLRenderer
	{
		public static void EnableDepthTest()
		{
			GLEnableDepthTest();
		}
		public static void EnableMSAA()
		{
			GLEnableMSAA();
		}
		public static void SetClearColor(OpenTK.Graphics.Color4 color)
		{
			GLSetClearColor(color);
		}
		public static void Clear()
		{
			GLClear();
		}
		protected static void DrawIndexed(Platform.OpenGL.OpenGLVertexArray vertexArrray)
		{
			GLDrawIndexed(vertexArrray);
		}
		public static void SetViewport(int x, int y, int width, int height)
		{
			GLViewport(x, y, width, height);
		}
	}
}
