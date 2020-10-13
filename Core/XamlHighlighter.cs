/*
 * Switcheroo - The incremental-search task switcher for Windows.
 * http://www.switcheroo.io/
 * Copyright 2009, 2010 James Sulak
 * Copyright 2014 Regin Larsen
 * 
 * Switcheroo is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Switcheroo is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Switcheroo.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Switcheroo.Core.Matchers;

namespace Switcheroo.Core
{
    public class XamlHighlighter
    {
        public string Highlight(IEnumerable<StringPart> stringParts)
        {
            if (stringParts == null) return string.Empty;

            var xDocument = new XDocument(new XElement("Root"));
            foreach (var stringPart in stringParts)
            {
                if (stringPart.IsMatch)
                {
                    xDocument.Root.Add(new XElement("Bold", ReplaceLowOrderASCIICharacters(stringPart.Value)));
                }
                else
                {
                    xDocument.Root.Add(new XText(ReplaceLowOrderASCIICharacters(stringPart.Value)));
                }
            }
            return string.Join("", xDocument.Root.Nodes().Select(x => x.ToString()).ToArray());
        }

        public static string ReplaceLowOrderASCIICharacters(string tmp)
        {
            StringBuilder info = new StringBuilder();
            foreach (char cc in tmp)
            {
                int ss = (int)cc;
                if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12))
                    || ((ss >= 14) && (ss <= 32)))
                    info.AppendFormat(" ", ss);//&#x{0:X};
                else info.Append(cc);
            }
            return info.ToString();
        }
    }
}