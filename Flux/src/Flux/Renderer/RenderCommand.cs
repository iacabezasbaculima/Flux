
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
		public static void EnableCullFace()
		{
			GLCullFace();
		}
		public static void SetClearColor(OpenTK.Graphics.Color4 color)
		{
			GLSetClearColor(color);
		}
		public static void Clear()
		{
			GLClear();
		}
		public static void DrawIndexed(Platform.OpenGL.OpenGLVertexArray vertexArrray)
		{
			GLDrawIndexed(vertexArrray);
		}
		public static void SetViewport(int x, int y, int width, int height)
		{
			GLViewport(x, y, width, height);
		}
		public static void SetActiveTexture(OpenTK.Graphics.OpenGL.TextureUnit unit)
		{
			GLActiveTexture(unit);
		}
		public static void BindTexture(int handle)
		{
			GLBindTexture(handle);
		}
	}
}
