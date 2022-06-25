namespace Scope.Patcher
{
    using System.Collections.Generic;
    using System.IO;
    using Models;

    public static class Deserializer
    {
        public static PatchCollection Read(string patchFile)
        {
            if (!File.Exists(patchFile))
            {
                throw new FileNotFoundException(nameof(patchFile));
            }

            var patches = Deserialize1337(File.ReadAllLines(patchFile), out var name);
            return new PatchCollection(name, patches);
        }
        
        private static IEnumerable<Patch> Deserialize1337(string[] lines, out string assemblyname)
        {
            string file = null;
            List<Patch> result = new List<Patch>();
                
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (line.StartsWith(">"))
                {
                    if (file is null)
                    {
                        file = line.Substring(1);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (file is null)
                {
                    throw new InvalidDataException("Patch file is not valid!");
                }

                var splitData = line.Replace("->", ":").Split(':');
                if (splitData.Length != 3)
                {
                    throw new InvalidDataException("Unable to read patch data!");
                }
                
                result.Add(new Patch(
                    uint.Parse(splitData[0], System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(splitData[1], System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(splitData[1], System.Globalization.NumberStyles.HexNumber)));
            }

            assemblyname = file;
            return result;
        }
    }
}