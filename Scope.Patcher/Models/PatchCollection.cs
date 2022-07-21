namespace Scope.Patcher.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    public class PatchCollection : List<Patch>
    {
        public string AssemblyName { get; internal set; }
        public bool CanPatch => AssemblyName is not null && Count > 0;

        public PatchCollection()
        {
        }

        public PatchCollection(string assemblyName, IEnumerable<Patch> patches)
        {
            if(patches == null)
            {
                throw new ArgumentNullException(nameof(patches));
            }
            
            AddRange(patches);
            AssemblyName = assemblyName;
        }
    }
}