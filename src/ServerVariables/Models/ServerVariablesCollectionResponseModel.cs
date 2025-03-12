using System.Runtime.Serialization;

namespace ServerVariables.Models;

public class ServerVariablesCollectionResponseModel
{
    [DataMember(Name = "key")]
    public required string Key { get; set; }

    [DataMember(Name = "value")]
    public required dynamic Value { get; set; }

    [DataMember(Name = "section")]
    public required string Section { get; set; }
}
