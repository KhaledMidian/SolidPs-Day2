////////////////////////////////////////////////////////////////////////////////////////////////
// DIP Violation
////////////////////////////////////////////////////////////////////////////////////////////////

public class FileProcessor
{
    private FileReader _fileReader;
    private FileWriter _fileWriter;
    public FileProcessor()
        {
            _fileReader = new FileReader();
            _fileWriter = new FileWriter();
        }
    public void ProcessFile(string inputFilePath, string outputFilePath)
        {
            string fileContent = _fileReader.ReadFile(inputFilePath);
            // Process the file content
            _fileWriter.WriteFile(outputFilePath, fileContent);
        }
}
public class FileReader
    {
        public string ReadFile(string filePath)
        {
            // Code to read file content
            return "File content";
        }
    }
    public class FileWriter
    {
        public void WriteFile(string filePath, string content)
        {
            // Code to write file content
        }
    }

////////////////////////////////////////////////////////////////////////////////////////////////
// Identification and Resolving
////////////////////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////////////////////
// Identification:
////////////////////////////////////////////////////////////////////////////////////////////////

// In the original code, the FileProcessor class directly depends on concrete 
// implementations of FileReader and FileWriter.
// This violates the Dependency Inversion Principle (DIP) 
// because high-level modules (FileProcessor) should not depend on low-level 
// modules (FileReader, FileWriter) directly.
// Instead, they should depend on abstractions.





////////////////////////////////////////////////////////////////////////////////////////////////
//Resolving:
////////////////////////////////////////////////////////////////////////////////////////////////

// To resolve the DIP violation, we introduce interfaces for FileReader and 
// FileWriter to abstract away the concrete implementations.
// We define IFileReader and IFileWriter interfaces, which specify the contract for reading and writing files,
// respectively.
// The FileReader and FileWriter classes implement these interfaces,
// providing concrete implementations of file reading and writing operations.
// Then, we modify the FileProcessor class to depend on these interfaces instead of concrete implementations.
// This allows FileProcessor to be decoupled from specific file reading 
// and writing implementations, adhering to the Dependency Inversion Principle.




public interface IFileReader
{
     string ReadFile(string filePath);
}


public interface IFileWriter
{
     string WriteFile(string filePath);
}



public class FileReader : IFileReader
{
    public string ReadFile(string filePath)
            {
                    // Code to read file content
                    return "File content";
            }
}


public class FileWriter : IFileWriter
{
        public void WriteFile(string filePath, string content)
                {
                    // Code to write file content
                }
}




public class FileProcessor
{
        private IFileReader _fileReader;
        private IFileWriter _fileWriter;
        public FileProcessor(IFileReader fileReader, FileWriter fileWriter)
                {
                    _fileReader = fileReader;
                    _fileWriter = fileWriter;
                }
        public void ProcessFile(string inputFilePath, string outputFilePath)
                {
                    string fileContent = _fileReader.ReadFile(inputFilePath);
                    // Process the file content
                    _fileWriter.WriteFile(outputFilePath, fileContent);
                }
}


////////////////////////////////////////////////////////////////////////////////////////////////
//  Program File:
////////////////////////////////////////////////////////////////////////////////////////////////

// In the Program class, we use the WebApplicationBuilder to create a web application builder.
// We register the concrete implementations of IFileReader and 
// IFileWriter (FileReader and FileWriter) as scoped services using the builder.
// Services.AddScoped() method.
// By registering these services with the DI container, we ensure that instances of FileReader
// and FileWriter are available for injection where needed.
// This setup ensures that the dependencies are resolved correctly at runtime,
// following the principles of dependency inversion.


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IFileWriter,FileWriter>();
            builder.Services.AddScoped<IFileReader,IFileReader>();

        var app = builder.Build();
        app.Run();
    }

}
