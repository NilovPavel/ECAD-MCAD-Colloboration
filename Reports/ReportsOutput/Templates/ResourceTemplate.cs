using System.IO;
using System.Resources;

public class ResourceTemplate
{
    private string resourceName;
    private string fileName;
    private ResourceManager resourceManager;

    public Stream Stream
    { get; private set; }

    private void CopyFromResources()
    {
        byte[] binary = (byte[])resourceManager.GetObject(this.resourceName);
        using (Stream stream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
        {
            stream.Write(binary, 0, binary.Length);
        }
    }

    private Stream GetTemplateStream()
    {
        Stream memory = new MemoryStream();
        using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        {
            stream.CopyTo(memory);
        }
        return memory;
    }

    private void Initialization()
    {
        this.fileName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\" + this.fileName;
        this.resourceManager = new ResourceManager("ReportsOutput.Data", typeof(ReportsOutput.Data).Assembly);
    }

    public ResourceTemplate(string resourceName, string fileName)
    {
        this.resourceName = resourceName;
        this.fileName = fileName;

        this.Initialization();

        if (!File.Exists(this.fileName))
            this.CopyFromResources();

        this.Stream = this.GetTemplateStream();
    }
}