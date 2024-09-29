using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class FileManager
    {
        public static void Main(string[] args)
        {
            var inlineNodeStyleText = "color: blue; font-size: 46px;  font-family:MinecrafterReg.ttf, sans  ";
            var inlineNodeStyle = inlineNodeStyleText.Split(';', StringSplitOptions.TrimEntries)
                .Select(rule =>
                {
                    string[] parts = rule.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Count() == 2) return new Tuple<string, string>(parts[0].Trim(), parts[1].Trim());
                    return new Tuple<string, string>("", "");
                })
                .Where(rule => rule.Item1 != "" || rule.Item2 != "").ToList();
            Console.WriteLine(inlineNodeStyle);

            Console.WriteLine("File Manager Main.");
        }
    }
}
