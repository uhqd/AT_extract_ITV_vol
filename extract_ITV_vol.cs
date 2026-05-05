using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.4")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
// [assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{
    public class Script
    {
        public Script()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(VMS.TPS.Common.Model.API.ScriptContext context /*, System.Windows.Window window, ScriptEnvironment environment*/)
        {
            var structnames = File.ReadLines(@"data\structName.csv");
            string csvPath = @"data\out\results.csv";

           // string msg = string.Empty;


            using (StreamWriter sw = new StreamWriter(csvPath, false, Encoding.UTF8))
            {
                sw.WriteLine("StructureSetId;StructureId;VolumeCc;x;y;z");
                double x, y, z;
                foreach (StructureSet ss in context.Patient.StructureSets)
                {

                    foreach (string structname in structnames)
                    {

                        var s = ss.Structures.FirstOrDefault(s1 => s1.Id == structname);

                        if (s != null)
                        {
                            x = s.MeshGeometry.Bounds.X + (0.5) * (s.MeshGeometry.Bounds.SizeX);
                            y = s.MeshGeometry.Bounds.Y + (0.5) * (s.MeshGeometry.Bounds.SizeY);
                            z = s.MeshGeometry.Bounds.Z + (0.5) * (s.MeshGeometry.Bounds.SizeZ);

                            sw.WriteLine(ss.Id + ";" + s.Id + ";" + s.Volume.ToString("F3") + ";" + x.ToString("F3") + ";" + y.ToString("F3") + ";" + z.ToString("F3"));
                            //msg += ss.Id + " contains " + s.Id + " " + s.Volume + " cc\n";

                        }

                    }


                }
               
            }
            MessageBox.Show("Fait !!  \u2764\uFE0F");
        }
    }
}
