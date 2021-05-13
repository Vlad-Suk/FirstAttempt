using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EmptyWebApplicationASP.NET.Services
{
    public class TextConfigurationProvider: ConfigurationProvider
    {
        public string FilePath { get; set; }
        public TextConfigurationProvider (string path)
        {
            FilePath = path;
        }
        public override void Load()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                using (StreamReader textreader = new StreamReader(fs))
                {
                    string line;
                    while((line = textreader.ReadLine())!=null)
                    {
                        string key = line.Trim();
                        string value = textreader.ReadLine();
                        data.Add(key, value);
                    }
                }
            }
            Data = data;
        }
    }
    public class TextConfigurationSource : IConfigurationSource
    {
        public string FilePat { get; private set; }
        public TextConfigurationSource(string filename)
        {
            FilePat = filename;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            string filePath = builder.GetFileProvider().GetFileInfo(FilePat).PhysicalPath;
            return new TextConfigurationProvider(filePath);
        }
    }
    public static class TextConfigurationExtensions
    {
        public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, string path)
        {
            if(builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("The way to file doesn't notice!");
            }
            var source = new TextConfigurationSource(path);
            builder.Add(source);
            return builder;
        }
    }
}
