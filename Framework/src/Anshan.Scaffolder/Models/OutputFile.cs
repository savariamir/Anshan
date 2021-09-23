using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Fluid;
using Fluid.Values;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Anshan.Scaffolder.Models
{
    public class OutputFile
    {
        private static readonly FluidParser Parser = new();

        public bool HasShared { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string SharedFilePath { get; set; }

        public Dictionary<string, object> Model { get; set; } = new();

        public string TemplateName { get; set; }

        public bool IsShared { get; set; }

        public bool PreventShared { get; set; }

        public void Create()
        {
            Model.Add(nameof(IsShared), IsShared);
            Model.Add(nameof(HasShared), HasShared);

            var serializedModel = JsonConvert.SerializeObject(Model);
            var model = JObject.Parse(serializedModel);

            var liquidSource = ReadLiquidTemplate();

            var options = new TemplateOptions();

            // When a property of a JObject value is accessed, try to look into its properties
            options.MemberAccessStrategy.Register<JObject, object>((source, name) => source[name]);

            // Convert JToken to FluidValue
            options.ValueConverters.Add(x => x is JObject o ? new ObjectValue(o) : null);
            options.ValueConverters.Add(x => x is JValue v ? v.Value : null);

            var template = Parser.Parse(liquidSource);

            var templateContext = new TemplateContext(options);
            templateContext.SetValue("Model", model);

            var result = template.Render(templateContext);

            var filePath = Path.Combine(IsShared && !PreventShared ? SharedFilePath : FilePath, FileName) + ".cs";
            var file = new FileInfo(filePath);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, result);
        }

        private string ReadLiquidTemplate()
        {
            var currentAssemblyPath =
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var liquidFilePath = Path.Combine(currentAssemblyPath, "Templates", TemplateName);

            var liquidSource = File.ReadAllText(liquidFilePath);
            return liquidSource;
        }
    }
}