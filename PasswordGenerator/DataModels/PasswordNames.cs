using System;
using System.Runtime.Serialization;

namespace PasswordGenerator
{
    [DataContract]
    public struct PasswordName : IEquatable<PasswordName>
    {
        [DataMember]
        public string Name { get; set; }

        [IgnoreDataMember]
        public string NormalizedName => Name?.ToUpper();

        public PasswordName(string name)
        {
            Name = name;
        }

        public bool Equals(PasswordName other)
        {
            return NormalizedName == other.NormalizedName;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}