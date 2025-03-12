namespace ServerVariables.Services;

public interface IServerVariablesService
{
    /// <summary>
    ///     Sets a server variable
    /// </summary>
    /// <param name="key">The key</param>
    /// <param name="value">The value</param>
    /// <param name="sectionName">The section, for example a section "brand" would be imported from "vars/brand.js".</param>
    public void SetVariable(string key, dynamic value, string sectionName);

    /// <summary>
    ///     Sets a server variable
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetVariable(string key, dynamic value);

    /// <summary>
    ///     Gets a section of server variables
    /// </summary>
    /// <param name="section">The name of the section</param>
    /// <returns></returns>
    public Dictionary<string, dynamic> GetSection(string section);

    /// <summary>
    ///     Sets a section of server variables
    /// </summary>
    /// <param name="sectionName">The name of the section</param>
    /// <param name="values">The dictionary of the variables</param>
    public bool SetSection(string sectionName, Dictionary<string, dynamic> values);
}
