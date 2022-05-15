using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CubeRubikFinal
{
  // Класс для создания шейдеров.
  public class Shader
  {
    public readonly int Handle; // Shader handle (Дескриптор).

    private readonly Dictionary<string, int> _uniformLocations;

    public Shader(string vertPath, string fragPath)
    {
      // Вершинный шейдер отвечает за перемещение по вершинам и загрузку данных во фрагментный шейдер.
      // Фрагментный шейдер отвечает за преобразование вершин во "фрагменты (fragments)", которые представляют все данные
      //необходимые OpenGL для рисования пикселя.

      // Загрузка шейдера.
      var shaderSource = File.ReadAllText(vertPath);

      // Указание типа шейдера.
      var vertexShader = GL.CreateShader(ShaderType.VertexShader);

      // Привязка исходного GLSL кода к шейдеру.
      GL.ShaderSource(vertexShader, shaderSource);

      // Компиляция
      CompileShader(vertexShader);

      // Аналогично фрагментный шейдер.
      shaderSource = File.ReadAllText(fragPath);
      var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
      GL.ShaderSource(fragmentShader, shaderSource);
      CompileShader(fragmentShader);

      // Создание шейдерной программы на основе вершинного и фрагментного шейдеров.
      Handle = GL.CreateProgram();
      GL.AttachShader(Handle, vertexShader);
      GL.AttachShader(Handle, fragmentShader);
      LinkProgram(Handle);

      // Отвязка и удаление оставшихся отдельных шейдеров,
      //ведь после связки в одну программу по отдельности они уже не нужны.
      GL.DetachShader(Handle, vertexShader);
      GL.DetachShader(Handle, fragmentShader);
      GL.DeleteShader(fragmentShader);
      GL.DeleteShader(vertexShader);

      // Кэширование uniforms.
      // Получение количества активных uniforms в шейдере.
      GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

      // Словарь для хранения данных о местоположении uniforms.
      _uniformLocations = new Dictionary<string, int>();

      // Loop over all the uniforms,
      for (var i = 0; i < numberOfUniforms; i++)
      {
        // get the name of this uniform,
        var key = GL.GetActiveUniform(Handle, i, out _, out _);

        // get the location,
        var location = GL.GetUniformLocation(Handle, key);

        // and then add it to the dictionary.
        _uniformLocations.Add(key, location);
      }
    }

    // Функция компиляции с проверкой на ошибки.
    private static void CompileShader(int shader)
    {
      // Try to compile the shader
      GL.CompileShader(shader);

      // Check for compilation errors
      GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
      if (code != (int)All.True)
      {
        // We can use `GL.GetShaderInfoLog(shader)` to get information about the error.
        var infoLog = GL.GetShaderInfoLog(shader);
        throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
      }
    }

    // Функция связывания шейдеров вместе с проверкой на ошибки.
    private static void LinkProgram(int program)
    {
      // We link the program
      GL.LinkProgram(program);

      // Check for linking errors
      GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
      if (code != (int)All.True)
      {
        // We can use `GL.GetProgramInfoLog(program)` to get information about the error.
        throw new Exception($"Error occurred whilst linking Program({program})");
      }
    }

    // A wrapper function that enables the shader program.
    public void Use()
    {
      GL.UseProgram(Handle);
    }

    public int GetAttribLocation(string attribName)
    {
      return GL.GetAttribLocation(Handle, attribName);
    }

    // Uniform setters
    // Uniforms are variables that can be set by user code, instead of reading them from the VBO.
    // You use VBOs for vertex-related data, and uniforms for almost everything else.

    // Setting a uniform is almost always the exact same, so I'll explain it here once, instead of in every method:
    //     1. Bind the program you want to set the uniform on
    //     2. Get a handle to the location of the uniform with GL.GetUniformLocation.
    //     3. Use the appropriate GL.Uniform* function to set the uniform.

    /// <summary>
    /// Set a uniform int on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetInt(string name, int data)
    {
      GL.UseProgram(Handle);
      GL.Uniform1(_uniformLocations[name], data);
    }

    /// <summary>
    /// Set a uniform float on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetFloat(string name, float data)
    {
      GL.UseProgram(Handle);
      GL.Uniform1(_uniformLocations[name], data);
    }

    /// <summary>
    /// Set a uniform Matrix4 on this shader
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    /// <remarks>
    ///   <para>
    ///   The matrix is transposed before being sent to the shader.
    ///   </para>
    /// </remarks>
    public void SetMatrix4(string name, Matrix4 data)
    {
      GL.UseProgram(Handle);
      GL.UniformMatrix4(_uniformLocations[name], true, ref data);
    }

    /// <summary>
    /// Set a uniform Vector3 on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetVector3(string name, Vector3 data)
    {
      GL.UseProgram(Handle);
      GL.Uniform3(_uniformLocations[name], data);
    }
  }
}