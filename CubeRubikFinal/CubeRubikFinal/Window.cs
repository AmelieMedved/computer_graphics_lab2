using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace CubeRubikFinal
{
  public class Window : GameWindow // С помощью класса GameWindow класс Window является базовым окном
  {
    // Определяем вершины в NDC для куба.
    private readonly float[] _verticesCube =
    {
            // Positions         
            -0.5f, -0.5f, -0.5f, 
             0.5f, -0.5f, -0.5f, 
             0.5f,  0.5f, -0.5f, 
             0.5f,  0.5f, -0.5f, 
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f, 

            -0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,

            -0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,

             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,

            -0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f, -0.5f,

            -0.5f,  0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f
        };

    // Для прямоугольника (плоскости).
    private readonly float[] _verticesPlane =
    {
       // Position         Normals             Texture coordinates
      -0.5f, -0.5f, -0.5f, 0.0f, 0.0f, -1.0f,  0.0f, 0.0f,
       0.5f, -0.5f, -0.5f, 0.0f, 0.0f, -1.0f,  1.0f, 0.0f,
       0.5f,  0.5f, -0.5f, 0.0f, 0.0f, -1.0f,  1.0f, 1.0f,
       0.5f,  0.5f, -0.5f, 0.0f, 0.0f, -1.0f,  1.0f, 1.0f,
      -0.5f,  0.5f, -0.5f, 0.0f, 0.0f, -1.0f,  0.0f, 1.0f,
      -0.5f, -0.5f, -0.5f, 0.0f, 0.0f, -1.0f,  0.0f, 0.0f,
    };

    private readonly Vector3[] pos =
    {
      new Vector3(1.1f, 0.0f, 0.0f), // 0
      new Vector3(-1.1f, 0.0f, 0.0f), // 0
      new Vector3(1.1f, 1.1f, 0.0f), // 0
      new Vector3(1.1f, -1.1f, 0.0f), // 0
      new Vector3(-1.1f, 1.1f, 0.0f), // 0
      new Vector3(-1.1f, -1.1f, 0.0f), // 0
      new Vector3(0.0f, 1.1f, 0.0f), // 0
      new Vector3(0.0f, -1.1f, 0.0f), // 0

      new Vector3(1.1f, 1.1f, -1.1f), // 0
      new Vector3(1.1f, 0.0f, -1.1f), // 0
      new Vector3(1.1f, -1.1f, -1.1f), // 11
      new Vector3(1.1f, 1.1f, -2.2f), // 0
      new Vector3(1.1f, 0.0f, -2.2f), // 0
      new Vector3(1.1f, -1.1f, -2.2f), // 0
      new Vector3(1.1f, 1.1f, -3.3f), // 15
      new Vector3(1.1f, 0.0f, -3.3f), // 16
      new Vector3(1.1f, -1.1f, -3.3f), // 17

      new Vector3(-1.1f, 1.1f, -1.1f), // 0
      new Vector3(-1.1f, 0.0f, -1.1f), // 0
      new Vector3(-1.1f, -1.1f, -1.1f), // 20
      new Vector3(-1.1f, 1.1f, -2.2f), // 0
      new Vector3(-1.1f, 0.0f, -2.2f), // 0
      new Vector3(-1.1f, -1.1f, -2.2f), // 0
      new Vector3(-1.1f, 1.1f, -3.3f), // 24
      new Vector3(-1.1f, 0.0f, -3.3f), // 25
      new Vector3(-1.1f, -1.1f, -3.3f), // 26

      new Vector3(1.1f, 0.0f, -3.4f), // 27
      new Vector3(-1.1f, 0.0f, -3.4f), // 28
      new Vector3(1.1f, 1.1f, -3.4f), // 29
      new Vector3(1.1f, -1.1f,-3.4f), // 30
      new Vector3(-1.1f, 1.1f, -3.4f), // 31
      new Vector3(-1.1f, -1.1f, -3.4f), // 32
      new Vector3(0.0f, 1.1f, -3.4f), // 33
      new Vector3(0.0f, -1.1f, -3.4f), // 34
      new Vector3(0.0f, 0.0f, -3.4f), // 35

      new Vector3(-1.1f, -1.2f, -3.3f), // 36
      new Vector3(0.0f, -1.2f, -3.3f), // 37
      new Vector3(1.1f, -1.2f, -3.3f), // 38
      new Vector3(-1.1f, -1.2f, -1.1f), // 39
      new Vector3(0.0f, -1.2f, -1.1f), // 0
      new Vector3(1.1f, -1.2f, -1.1f), // 0
      new Vector3(-1.1f, -1.2f, -2.2f), // 0
      new Vector3(0.0f, -1.2f, -2.2f), // 0
      new Vector3(1.1f, -1.2f, -2.2f), // 0

      new Vector3(-1.1f, 1.2f, -1.1f), // 45
      new Vector3(0.0f, 1.2f, -1.1f), // 0
      new Vector3(1.1f, 1.2f, -1.1f), // 0
      new Vector3(-1.1f, 1.2f, -2.2f), // 0
      new Vector3(0.0f, 1.2f, -2.2f), // 0
      new Vector3(1.1f, 1.2f, -2.2f), // 0
      new Vector3(-1.1f, 1.2f, -3.3f), // 51
      new Vector3(0.0f, 1.2f, -3.3f), // 52
      new Vector3(1.1f, 1.2f, -3.3f), // 53
  };

    // Дескриптор (handle) VBO. Буфер для хранения вершин в памяти графического процессора.
    private int _vertexBufferObject;

    // VAO плоскости кубика-Рубика.
    private int[] VAO = new int[sizeof(int) * 54];

    // VAO куб-лампа.
    private int _vaoLamp;
    private Shader _lampShader;

    // Шейдеры света для плоскостей кубика Рубика.
    private readonly Vector3 _lightPos = new Vector3(2.0f, 2.0f, 2.0f);

    private Shader _lighting; 
    private Texture _diffuseMapBLUE;
    private Texture _diffuseMapGREEN;
    private Texture _diffuseMapORANGE;
    private Texture _diffuseMapRED;
    private Texture _diffuseMapWHITE;
    private Texture _diffuseMapYELLOW;
    private Texture _specularMapBLUE;
    private Texture _specularMapGREEN;
    private Texture _specularMapORANGE;
    private Texture _specularMapRED;
    private Texture _specularMapWHITE;
    private Texture _specularMapYELLOW;

    // Камера.
    private Camera _camera;
    private bool _firstMove = true;
    private Vector2 _lastPos;

    // Время.
    private double _time;

    // Конструктор для настройки базового окна.
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    // Эта функция запускается один раз с запуском программы. Любой код, связанный с инициализацией находится здесь.
    protected override void OnLoad()
    {
      base.OnLoad();

      GL.ClearColor(0.459f, 0.733f, 0.992f, 1.0f); // Фоновый цвет.
      GL.Enable(EnableCap.DepthTest); // Z-buffer.

      // Пути для шейдеров.
      _lighting = new Shader("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Shaders\\shader.vert", "C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Shaders\\lighting.frag");
      _lampShader = new Shader("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Shaders\\shader.vert", "C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Shaders\\shader.frag");

      // Пути для текстур.
      _diffuseMapBLUE = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\BLUE.png");
      _specularMapBLUE = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\BLUE_SPEC.png");
      _diffuseMapGREEN = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\GREEN.png");
      _specularMapGREEN = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\GREEN_SPEC.png");
      _diffuseMapORANGE = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\ORANGE.png");
      _specularMapORANGE = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\ORANGE_SPEC.png");
      _diffuseMapRED = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\RED.png");
      _specularMapRED = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\RED_SPEC.png");
      _diffuseMapWHITE = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\WHITE.png");
      _specularMapWHITE = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\WHITE_SPEC.png");
      _diffuseMapYELLOW = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\YELLOW.png");
      _specularMapYELLOW = Texture.LoadFromFile("C:\\Users\\manuk\\source\\repos\\CubeRubikFinal\\CubeRubikFinal\\Resources\\YELLOW_SPEC.png");

      // Кубик-Рубика.
      _vertexBufferObject = GL.GenBuffer(); // Создание VBO.
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject); // Привязка _vertexBufferObject к соответсвующему типу буфера. С этого момента
                                                                    //любые вызовы буфера в BufferTarger.ArrayBuffer будут использоваться
                                                                    //для настройки текущего связанного буфера, т.е. _vertexBufferObject.
      GL.BufferData(BufferTarget.ArrayBuffer, _verticesPlane.Length * sizeof(float), _verticesPlane, BufferUsageHint.StaticDraw); 
      // Копируем созданные вершины в память буфера. Аргументы: тип буфера, размер данных, данные, способ управления заданными данными
      //(так как данные не меняются в ходе выполнения программы, применяется BufferUsageHint.StaticDraw).


      // Каждый атрибут вершины берет свои данные из памяти, управляемой VBO, и из какого VBO он берет свои данные
      //(VBO может быть несколько, в данном примере их 2), определяется VBO, который в данный момент привязан к ArrayBuffer
      //при вызове GL.VertexAttribPointer. 
      for (int i = 0; i < 54; i++)
      {
        VAO[i] = GL.GenVertexArray();
        GL.BindVertexArray(VAO[i]);

        // Указываем, как OpenGL должен интерпретировать данные вершины (для каждого атрибута вершины), используя GL.vertexAttribPointer
        var positionLocation = _lighting.GetAttribLocation("aPos");
        GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0); // https://opentk.net/learn/chapter1/2-hello-triangle.html#linking-vertex-attributes
        GL.EnableVertexAttribArray(positionLocation);

        var normalLocation = _lighting.GetAttribLocation("aNormal");
        GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(normalLocation);

        var texCoordLocation = _lighting.GetAttribLocation("aTexCoords");
        GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        GL.EnableVertexAttribArray(texCoordLocation);
      }


      // Куб-лампа.
      _vertexBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer, _verticesCube.Length * sizeof(float), _verticesCube, BufferUsageHint.StaticDraw);

      {
        _vaoLamp = GL.GenVertexArray();
        GL.BindVertexArray(_vaoLamp);

        var positionLocation = _lampShader.GetAttribLocation("aPos");
        GL.EnableVertexAttribArray(positionLocation);
        GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      }

      _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

      CursorGrabbed = true;
    }

    protected void _lighting_function()
    {
      _lighting.Use();

      _lighting.SetMatrix4("view", _camera.GetViewMatrix());
      _lighting.SetMatrix4("projection", _camera.GetProjectionMatrix());

      _lighting.SetVector3("viewPos", _camera.Position);

      _lighting.SetInt("material.diffuse", 0);
      _lighting.SetInt("material.specular", 1);
      _lighting.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
      _lighting.SetFloat("material.shininess", 15.0f);

      _lighting.SetVector3("light.position", _lightPos);
      _lighting.SetFloat("light.constant", 0.5f);
      _lighting.SetFloat("light.linear", 0.09f);
      _lighting.SetFloat("light.quadratic", 0.015f);
      _lighting.SetVector3("light.ambient", new Vector3(1.0f));
      _lighting.SetVector3("light.diffuse", new Vector3(0.5f));
      _lighting.SetVector3("light.specular", new Vector3(1.0f));
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      _time += 40.0 * e.Time; 
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Очищает экран, используя цвет, установленный в OnLoad.

      // Кубик-Рубика.
      for (int i = 0; i < 54; i++)
      {
        GL.BindVertexArray(VAO[i]);

        Matrix4 model = Matrix4.Identity;

        if (i == 0)
        {
          // VAO[0]
          _diffuseMapBLUE.Use(TextureUnit.Texture0);
          _specularMapBLUE.Use(TextureUnit.Texture1);
          _lighting_function();
        }
        else if (0 < i && i < 9)
        {
          _diffuseMapBLUE.Use(TextureUnit.Texture0);
          _specularMapBLUE.Use(TextureUnit.Texture1);
          _lighting_function();
          model = model * Matrix4.CreateTranslation(pos[i - 1]);
        }
        else if (9 <= i && i < 18)
        {
          _diffuseMapGREEN.Use(TextureUnit.Texture0);
          _specularMapGREEN.Use(TextureUnit.Texture1);
          _lighting_function();
          model = model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(-90));
          model = model * Matrix4.CreateTranslation(pos[i - 1]);
        }
        else if (18 <= i && i < 27)
        {
          _diffuseMapORANGE.Use(TextureUnit.Texture0);
          _specularMapORANGE.Use(TextureUnit.Texture1);
          _lighting_function();
          model = model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(90));
          model = model * Matrix4.CreateTranslation(pos[i - 1]);
        }
        else if (27 <= i && i < 36)
        {
          _diffuseMapRED.Use(TextureUnit.Texture0);
          _specularMapRED.Use(TextureUnit.Texture1);
          _lighting_function();
          model = model * Matrix4.CreateTranslation(pos[i - 1]);
        }
        else if (36 <= i && i < 45)
        {
          _diffuseMapWHITE.Use(TextureUnit.Texture0);
          _specularMapWHITE.Use(TextureUnit.Texture1);
          _lighting_function();
          model = model * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(-90));
          model = model * Matrix4.CreateTranslation(pos[i - 1]);
        }
        else if (45 <= i && i < 55)
        {
          _diffuseMapYELLOW.Use(TextureUnit.Texture0);
          _specularMapYELLOW.Use(TextureUnit.Texture1);
          _lighting_function();
          model = model * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(90));
          model = model * Matrix4.CreateTranslation(pos[i - 1]);
        }

        if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9 || i == 10 || i == 11 || i == 18 || i == 19 || i == 20 || i == 39 || i == 40 || i == 41 || i == 45 || i == 46 || i == 47)
        {
          model = model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(_time));
        }

        if (i == 15 || i == 16 || i == 17 || (i == 24) || (i == 25) || (i == 26) || (i == 27) || (i == 28) || i == 29 || i == 30 || i == 31 || i == 32 || i == 33 || i == 34 || i == 35 || i == 36 || i == 37 || i == 38 || i == 51 || i == 52 || i == 53)
        {
          model = model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(-(_time)));
        }
        _lighting.SetMatrix4("model", model);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
      }

      // Куб-лампа.
      {
        GL.BindVertexArray(_vaoLamp);

        _lampShader.Use();

        Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
        lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

        _lampShader.SetMatrix4("model", lampMatrix);
        _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
        _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
      }

      // Меняет местами две области (одна область отображается, другая рендерится(в процессе визуализации)).
      SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      if (!IsFocused)
      {
        return;
      }

      var input = KeyboardState;

      if (input.IsKeyDown(Keys.Escape))
      {
        Close();
      }

      const float cameraSpeed = 1.5f;
      const float sensitivity = 0.2f;

      if (input.IsKeyDown(Keys.W))
      {
        _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
      }
      if (input.IsKeyDown(Keys.S))
      {
        _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
      }
      if (input.IsKeyDown(Keys.A))
      {
        _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
      }
      if (input.IsKeyDown(Keys.D))
      {
        _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
      }
      if (input.IsKeyDown(Keys.Space))
      {
        _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
      }
      if (input.IsKeyDown(Keys.LeftShift))
      {
        _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
      }

      var mouse = MouseState;

      if (_firstMove)
      {
        _lastPos = new Vector2(mouse.X, mouse.Y);
        _firstMove = false;
      }
      else
      {
        var deltaX = mouse.X - _lastPos.X;
        var deltaY = mouse.Y - _lastPos.Y;
        _lastPos = new Vector2(mouse.X, mouse.Y);

        _camera.Yaw += deltaX * sensitivity;
        _camera.Pitch -= deltaY * sensitivity;
      }

    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
      base.OnMouseWheel(e);

      _camera.Fov -= e.OffsetY;
    }

    // OnResize запускается каждый раз, когда изменяется размер окна. GL.Viewport сопоставляет NDC с окном.
    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(0, 0, Size.X, Size.Y);
      _camera.AspectRatio = Size.X / (float)Size.Y;
    }
  }
}