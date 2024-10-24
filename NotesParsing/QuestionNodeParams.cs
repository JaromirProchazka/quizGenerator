using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using StyleAttributes = System.Collections.Generic.IEnumerable<System.Tuple<string, string>>;

namespace NotesParsing
{
    public abstract record class QuestionNodeParams
    {
        public QuestionNodeParams()
        {
            Checks = new List<BaseAttributeCheck> { Name, Classes, Color, FontFamily };
        }

        protected StyleAttributes ParseNodeStyle(HtmlNode node)
        {
            string inlineNodeStyleText = node.GetAttributeValue("style", "");
            return inlineNodeStyleText.Split(';', StringSplitOptions.TrimEntries)
                .Select(rule =>
                {
                    string[] parts = rule.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Count() == 2) return new Tuple<string, string>(parts[0].Trim(), parts[1].Trim());
                    return new Tuple<string, string>("", "");
                })
                .Where(rule => rule.Item1 != "" || rule.Item2 != "")
                .ToList(); 
        }

        /// <summary>
        /// The parsed style attribute of current node. Null if not needed.
        /// </summary>
        protected StyleAttributes? inlineNodeStyle = null;

        /// <summary>
        /// List of all check to be done on given <see cref="HtmlNode"/>
        /// </summary>
        internal IEnumerable<BaseAttributeCheck> Checks;

        public abstract bool Analyze(HtmlNode node);

        /// <summary>
        /// Name of the Node.
        /// </summary>
        internal NameCheck Name = new NameCheck(null);
        public QuestionNodeParams SetName(string? name)
        {
            if (name == null) return this;
            Name.Attribute = name;
            return this;
        }

        /// <summary>
        /// List of classes of the node.
        /// </summary>
        internal AnyClassesCheck Classes = new AnyClassesCheck(null);
        public QuestionNodeParams SetClasses(string? classes)
        {
            if (classes != null)
                Classes.Attribute = classes?.Split(' ').ToList();
            return this;
        }

        /// <summary>
        /// Nodes text color in HEX.
        /// </summary>
        internal ColorCheck Color = new ColorCheck(null);
        public QuestionNodeParams SetColor(string? color)
        {
            if (color == null) return this;
            Color.Attribute = color;
            return this;
        }

        /// <summary>
        /// Nodes text font-family.
        /// </summary>
        internal FontCheck FontFamily = new FontCheck(null);
        public QuestionNodeParams SetFontFamily(string? fontFamily)
        {
            if (fontFamily == null) return this;
            FontFamily.Attribute = fontFamily;
            return this;
        }
    }

    public sealed record class AndQuestionNodeParams : QuestionNodeParams
    {
        public override bool Analyze(HtmlNode node)
        {
            inlineNodeStyle = ParseNodeStyle(node);
            foreach (var check in Checks)
            {
                if (check.CheckNotSet()) continue;
                if (!check.Check(node, inlineNodeStyle))
                {
                    inlineNodeStyle = null;
                    return false;
                }
            }
            inlineNodeStyle = null;
            return true;
        }
    }

    public sealed record class OrQuestionNodeParams : QuestionNodeParams
    {
        public override bool Analyze(HtmlNode node)
        {
            inlineNodeStyle = ParseNodeStyle(node);
            foreach (var check in Checks)
            {
                if (check.CheckNotSet()) continue;
                if (check.Check(node, inlineNodeStyle))
                {
                    inlineNodeStyle = null;
                    return true;
                }
            }
            inlineNodeStyle = null;
            return false;
        }
    }
}