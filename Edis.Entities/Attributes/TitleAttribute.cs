using System;

namespace Edis.Entities.Attributes
{
    public class TitleAttribute : Attribute
    {
        public string Title;
        public TitleAttribute(string title) { Title = title; }
    }
}
