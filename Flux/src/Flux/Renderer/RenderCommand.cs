
namespace Flux.src.Flux.Renderer
{
	public class RenderCommand
	{
		private static RendererAPI RendererAPI = RendererAPI.Create();

		public static void Init()
		{
			RendererAPI.Init();
		}
		public static void SetClearColor(OpenTK.Graphics.Color4 color)
		{
			RendererAPI.SetClearColor(color);
		}
		public static void Clear()
		{
			RendererAPI.Clear();
		}
		public static void SetBlend(bool isEnabled)
		{
			RendererAPI.SetBlend(isEnabled);
		}
		public static void SetCullFace(bool isEnabled)
		{
			RendererAPI.SetCullFace(isEnabled);
		}
		public static void SetDepthTest(bool isEnabled)
		{
			RendererAPI.SetDepthTest(isEnabled);
		}
		public static void SetMSAA(bool isEnabled)
		{
			RendererAPI.SetMSAA(isEnabled);
		}
		public static void DrawIndexed(VertexArray vertexArrray)
		{
			RendererAPI.DrawIndexed(vertexArrray);
		}
		public static void SetViewport(int x, int y, int width, int height)
		{
			RendererAPI.SetViewport(x, y, width, height);
		}
	}
}
