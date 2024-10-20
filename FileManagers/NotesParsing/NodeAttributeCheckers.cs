using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleAttributes = System.Collections.Generic.IEnumerable<System.Tuple<string, string>>;

namespace FileManager.NotesParsing
{
    internal abstract record class BaseAttributeCheck 
    {
        /// <summary>
        /// Is called by <see cref="Check"/>. Implements the concreate checking mechanism.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="inlineNodeStyle"></param>
        /// <returns></returns>
        protected abstract bool CheckPresent(HtmlNode node, StyleAttributes? inlineNodeStyle = null);

        /// <summary>
        /// Checks if the node passes the Attribute check.
        /// </summary>
        /// <param name="node">The current Html node</param>
        /// <param name="inlineNodeStyle">parsed style attribute of the node</param>
        /// <returns>True if the node passes this check, otherwise False</returns>
        public abstract bool Check(HtmlNode node, StyleAttributes? inlineNodeStyle = null);

        /// <summary>
        /// Used to decide, if this checked should be ignored.
        /// </summary>
        /// <returns>True if the Attribute is not null, else False</returns>
        public abstract bool CheckNotSet();
    }

    /// <summary>
    /// A class used for checking if a <see cref="HtmlNode"/> instance has some attribute.
    /// </summary>
    /// <typeparam name="T">Type of the Attribute, if given</typeparam>
    /// <param name="Attribute"></param>
    internal abstract record class AttributeCheck<T> : BaseAttributeCheck 
        where T : class
    {
        public T? Attribute { get; set; }

        public AttributeCheck(T? attribute) {
            Attribute = attribute;
        }

        public override bool Check(HtmlNode node, StyleAttributes? inlineNodeStyle = null)
        {
            if (Attribute == null) return true;
            var result = CheckPresent(node, inlineNodeStyle);
            return result;
        }

        public override bool CheckNotSet()
        {
            if (Attribute == null) return true;
            return false;
        }
    }

    internal sealed record class NameCheck : AttributeCheck<string>
    {
        public NameCheck(string? Name) : base(Name) { }

        protected override bool CheckPresent(HtmlNode node, StyleAttributes? inlineNodeStyle = null)
        {
            if (node.Name != Attribute) return false;
            return true;
        }
    }

    internal sealed record class AnyClassesCheck : AttributeCheck<List<string>>
    {
        public AnyClassesCheck(List<string>? classes) : base(classes) { }

        protected override bool CheckPresent(HtmlNode node, StyleAttributes? inlineNodeStyle = null)
        {
            if (Attribute == null || Attribute.Count == 0) return true;

            var classAttribute = node.GetAttributeValue("class", null);
            if (string.IsNullOrEmpty(classAttribute))
                return false;

            // Split the class string into individual class names
            return classAttribute
                               .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(nodesClass => nodesClass.Trim())
                               .Where(nodesClass => Attribute.Contains(nodesClass))
                               .Count() != 0;
        }
    }

    internal sealed record class ColorCheck : AttributeCheck<string>
    {
        public ColorCheck(string? color) : base(color) { }

        protected override bool CheckPresent(HtmlNode node, StyleAttributes? inlineNodeStyle = null)
        {
            if (inlineNodeStyle == null) return false;
            foreach (Tuple<string, string> att in inlineNodeStyle)
                if (att.Item1 == "color" && att.Item2 == Attribute)
                    return true;
            return false;
        }
    }

    internal sealed record class FontCheck : AttributeCheck<string>
    {
        public FontCheck(string? fontName) : base(fontName) { }

        protected override bool CheckPresent(HtmlNode node, StyleAttributes? inlineNodeStyle = null)
        {
            if (inlineNodeStyle == null) return false;
            foreach (Tuple<string, string> att in inlineNodeStyle)
                if (att.Item1 == "font-family" && att.Item2.Split(',', StringSplitOptions.TrimEntries).Contains(Attribute))
                    return true;
            return false;
        }
    }
}
