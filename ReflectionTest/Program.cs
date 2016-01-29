using ReflectionTest.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;) {
                FindThem();
                var input = Console.ReadKey();
            }
        }

        static void FindThem()
        {
            var sw = new Stopwatch();
            sw.Start();
            var interfaceType = typeof(ICommand);
            var allTypes = Assembly.GetExecutingAssembly().GetTypes();
            var commandTypes = new Dictionary<string, ICommand>();

            foreach (var type in allTypes)
            {
                if (interfaceType.IsAssignableFrom(type) && type.Name != "ICommand")
                {
                    var newInstance = Activator.CreateInstance(type);
                    commandTypes.Add(type.Name, (ICommand)newInstance);
                }
            }

            sw.Stop();
            
            // now, just to prove that we're actually creating new instances
            foreach(var command in commandTypes.Values)
            {
                var extraValue = Guid.NewGuid().ToString();
                Console.WriteLine(string.Format("Command results: {0}", command.Execute(extraValue)));
            }

            Console.WriteLine(string.Format("Total time: {0} ms", sw.ElapsedMilliseconds));
            Console.WriteLine(string.Format("Total commands found: {0}", commandTypes.Count));

        }
    }
}
