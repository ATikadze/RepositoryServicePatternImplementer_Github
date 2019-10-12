using System;
using System.IO;

namespace RepositoryServicePatternImplementer
{
    class Program
    {
        private const string classContent = "using System;\nusing System.Collections.Generic;\nusing System.Text;\nnamespace ClassNamespace\n{\nClassType ClassName\n{\n}\n}";
        private const string repositoryInterfacesFolder = "RepositoryInterfaces", serviceInterfacesFolder = "ServiceInterfaces";
        private static string _projectPath;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter class names separated by ',':");
            string classNames = Console.ReadLine();
            classNames = classNames.Replace(" ", string.Empty);
            string[] classNameArray = classNames.Split(new char[] { ',' });

            Console.Write("Project path: ");
            _projectPath = Console.ReadLine();

            Directory.CreateDirectory(_projectPath + "/Domain" + $"/{repositoryInterfacesFolder}");
            Directory.CreateDirectory(_projectPath + "/Domain" + $"/{serviceInterfacesFolder}");

            foreach (var cName in classNameArray)
            {
                CreateClass(cName);
                Console.WriteLine($"'{cName}'" + "Implemented!");
            }
        }

        private static void CreateClass(string className)
        {
            using (StreamWriter sw = File.CreateText(_projectPath + "/Domain" + $"/{className}.cs"))
                sw.WriteLine(GetClassContent("Domain", className, ClassTypes.Class));

            using (StreamWriter sw = File.CreateText(_projectPath + "/Domain" + $"/{repositoryInterfacesFolder}" + $"/I{className}Repository.cs"))
                sw.WriteLine(GetClassContent($"Domain.{repositoryInterfacesFolder}", $"I{className}Repository", ClassTypes.Interface));

            using (StreamWriter sw = File.CreateText(_projectPath + "/Domain" + $"/{serviceInterfacesFolder}" + $"/I{className}Service.cs"))
                sw.WriteLine(GetClassContent($"Domain.{serviceInterfacesFolder}", $"I{className}Service", ClassTypes.Interface));

            using (StreamWriter sw = File.CreateText(_projectPath + "/Repository" + $"/{className}Repository.cs"))
                sw.WriteLine(GetClassContent("Repository", $"{className}Repository", ClassTypes.Class));

            using (StreamWriter sw = File.CreateText(_projectPath + "/Service" + $"/{className}Service.cs"))
                sw.WriteLine(GetClassContent("Service", $"{className}Service", ClassTypes.Class));
        }

        private static string GetClassContent(string classNamespace, string className, ClassTypes classType)
        {
            return classContent.Replace("ClassNamespace", classNamespace).Replace("ClassName", className).Replace("ClassType", classType.ToString().ToLower());
        }

        private enum ClassTypes
        {
            Class, Interface
        }
    }
}
