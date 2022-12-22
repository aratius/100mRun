namespace Unity.Custom
{
  public struct Settings {
    public float focusDistance;
    public float focalLength;
    public Settings(float fd, float fl)
    {
      this.focusDistance = fd;
      this.focalLength = fl;
    }
  }

  public class DofSettings {

    public static Settings diagonalBefore = new Settings(15.8f,  50f);
    public static Settings diagonal = new Settings(15.8f, 200f);
    public static Settings slide = new Settings(3f, 50f);
    public static Settings focus = new Settings(0.1f, 1f);
    public static Settings start = new Settings(1.5f, 40f);
    public static Settings lookUp = new Settings(2.48f, 50f);
    public static Settings back = new Settings(3f, 50f);
    public static Settings side = new Settings(6f, 300f);
    public static Settings goal = new Settings(5f, 40f);

  }
}