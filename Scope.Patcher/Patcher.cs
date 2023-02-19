namespace Scope.Patcher
{
    using System;
    using System.IO;
    using Models;

    public static class Patcher
    {
        public static void Patch(this PatchCollection patches, string path, out bool expected, bool checkOld = false, bool backup = false)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(nameof(path));
            }

            if (patches.Count < 1)
            {
                throw new IndexOutOfRangeException(nameof(patches));
            }

            expected = true;
            var data = File.ReadAllBytes(path);
            foreach (var patch in patches)
            {
                if(patch.Address > data.Length)
                {
                    continue;
                }
                
                if(data[patch.Address] != patch.OldValue)
                {
                    if (checkOld)
                    {
                        throw new Exception(
                            $"Data does not match (expected: {patch.OldValue}, actual: {data[patch.Address]})");
                    }
                    else
                    {
                        expected = false;
                    }
                }
                
                data[patch.Address] = patch.NewValue;
            }
            
            if(backup)
            {
                Console.WriteLine("Backup already exists, continue? (y/n)");
                var input = Console.ReadLine()?.ToLower();
                if(input != "y")
                {
                    return;
                }
                File.Copy(path, $"{path}.old", true);
            }
            
            File.WriteAllBytes(path, data);
        }
    }
}