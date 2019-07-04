using CsvHelper;
using TobiVanHelsiki.PeriodicTableControl.Model;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace TobiVanHelsiki.PeriodicTableControl.WPF.IO
{
    static class GeneralIO
    {
        internal static IEnumerable<Element> CreateElements()
        {
            var ret = new List<Element>();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("PeriodicTableofElements.csv"));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (TextReader fileReader = new StreamReader(stream))
            {
                var csv = new CsvReader(fileReader);
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = ";";
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    ret.Add(new Element
                    {
                        Number = int.TryParse(csv.GetField("Atomic Number"), out int number) ? number : -1,
                        Name = csv.GetField("Element"),
                        Symbol = csv.GetField("Symbol"),
                        ElectronConfig = csv.GetField("Electron Configuration"),
                        Period = int.TryParse(csv.GetField("Period"), out int period) ? period : -1,
                        Group = int.TryParse(csv.GetField("Display Column"), out int column) ? column : -1,
                        Type = csv.GetField("Type") switch
                        {
                            "Actinide" => Element.ElementTypes.Actinide,
                            "Alkali Metal" => Element.ElementTypes.AlkaliMetal,
                            "Alkaline Earth Metal" => Element.ElementTypes.AlkalineEarthMetal,
                            "Halogen" => Element.ElementTypes.Halogen,
                            "Lanthanide" => Element.ElementTypes.Lanthanide,
                            "Metal" => Element.ElementTypes.Metal,
                            "Metalloid" => Element.ElementTypes.Metalloid,
                            "Noble Gas" => Element.ElementTypes.NobleGas,
                            "Nonmetal" => Element.ElementTypes.Nonmetal,
                            "Transactinide" => Element.ElementTypes.Transactinide,
                            "Transition Metal" => Element.ElementTypes.TransitionMetal,
                            _ => Element.ElementTypes.Unknown,
                        },
                        AtomicWeight = int.TryParse(csv.GetField("Atomic Weight").Replace(".", ""), out int aw) ? aw : -1,
                        Phase = csv.GetField("Phase") switch
                        {
                            "Solid" => Element.ElementPhases.Solid,
                            "Liquid" => Element.ElementPhases.Liquid,
                            "Gas" => Element.ElementPhases.Gas,
                            "Artificial" => Element.ElementPhases.Artificial,
                            _ => Element.ElementPhases.Unknown,
                        },
                    });
                }
            }
            return ret;
        }

    }
}
