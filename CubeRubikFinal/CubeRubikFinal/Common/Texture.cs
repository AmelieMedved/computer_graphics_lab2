using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace CubeRubikFinal
{
  public class Texture
  {
    public readonly int Handle;

    public static Texture LoadFromFile(string path)
    {
      // Создание дескриптора.
      int handle = GL.GenTexture(); // Генерация ID

      // Привязка дескриптора к TextureTarget.Texture2D
      GL.ActiveTexture(TextureUnit.Texture0);
      GL.BindTexture(TextureTarget.Texture2D, handle); // Привязка к таргету 2D

      // Use .NET's built-in System.Drawing library для загрузки текстур.
      // Загрузка изображения
      using (var image = new Bitmap(path))
      {
        // Растровое изображение (Bitmap) загружается от верхнего левого пикселя, тогда как OpenGL
        //загружает от нижнего левого.
        // Первернем.
        image.RotateFlip(RotateFlipType.RotateNoneFlipY);

        // Получение пикселей из растрового изображения.
        // Arguments:
        //   The pixel area we want. Typically, you want to leave it as (0,0) to (width,height), but you can
        //use other rectangles to get segments of textures, useful for things such as spritesheets.
        //   The locking mode. Basically, how you want to use the pixels. Since we're passing them to OpenGL,
        //we only need ReadOnly.
        //   Next is the pixel format we want our pixels to be in. In this case, ARGB will suffice.
        //   We have to fully qualify the name because OpenTK also has an enum named PixelFormat.
        var data = image.LockBits( // добраться до массива данных
            new Rectangle(0, 0, image.Width, image.Height),
            ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        // Создание текстуры с помощью GL.TexImage2D.
        // Arguments:
        //   The type of texture we're generating: Texture2D.
        //   Level of detail.
        //   Target format of the pixels. This is the format OpenGL will store our image with.
        //   Width of the image
        //   Height of the image.
        //   Border of the image. This must always be 0; it's a legacy parameter that Khronos never got rid of
        //   The format of the pixels, explained above. Since we loaded the pixels as ARGB earlier, we need to use BGRA
        //   Data type of the pixels.
        //   Actual pixels.
        GL.TexImage2D(TextureTarget.Texture2D,
            0,
            PixelInternalFormat.Rgba,
            image.Width,
            image.Height,
            0,
            PixelFormat.Bgra,
            PixelType.UnsignedByte,
            data.Scan0);
      }

      // После загрузки текстур. Настройки рендеринга.

      // min и mag фильтр, используется при масштабировании изображения.
      // Here, we use Linear for both. OpenGL пытается смешать пиксели и в далеке они будут выглядеть размытыми.
      // You could also use (amongst other options) Nearest, which just grabs the nearest pixel, which makes the texture look pixelated if scaled too far.
      // NOTE: The default settings for both of these are LinearMipmap. If you leave these as default but don't generate mipmaps,
      // your image will fail to render at all (usually resulting in pure black instead).
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear); // Фильтрация при увеличении 
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear); // -//- уменьшении

      // Now, set the wrapping mode. S is for the X axis, and T is for the Y axis.
      // We set this to Repeat so that textures will repeat when wrapped. Not demonstrated here since the texture coordinates exactly match
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

      // Создание mipmaps.
      // Mipmaps - это уменьшенные копии текстуры в уменьшенном масштабе. Каждый уровень mipmaps в два раза меньше предыдущего.
      // Сгенерированные mimaps уменьшаются до 1-го пикселя.
      // OpenGL будет автоматически переключаться между mipmaps когда объект находится далеко.
      // Это предотвращает Муаровый узор (morié effect).
      GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

      return new Texture(handle);
    }

    public Texture(int glHandle)
    {
      Handle = glHandle;
    }

    // Активация текстуры.
    public void Use(TextureUnit unit)
    {
      GL.ActiveTexture(unit);
      GL.BindTexture(TextureTarget.Texture2D, Handle);
    }
  }
}