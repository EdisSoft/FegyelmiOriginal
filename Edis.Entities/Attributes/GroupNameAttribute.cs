using System;

namespace Edis.Entities.Attributes
{
    public class GroupNameAttribute : Attribute
    {
        public string Name;
        public GroupNameAttribute(string name) { Name = name; }
    }
}
