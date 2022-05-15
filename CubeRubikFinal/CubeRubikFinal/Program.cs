// Создано консольное приложение Console app (C#)
// Установлены: OpenTK, OpenTK.GLControl, System.Drawing.Common
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace CubeRubikFinal
{
  public static class Program
  {
    // Основная функция (Main).
    private static void Main()
    {
      var nativeWindowSettings = new NativeWindowSettings()
      {
        Size = new Vector2i(1024, 720),
        Title = "Rubik's cube",
      };

      using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
      {
        window.Run();
      }
    }
  }
}