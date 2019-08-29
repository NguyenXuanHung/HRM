using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ManifestInfo
/// </summary>
public class ManifestInfo
{
	public ManifestInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// id of control in form
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// description about control
    /// </summary>
    public string desc { get; set; }
    /// <summary>
    /// allow visible
    /// </summary>
    public bool visible { get; set; }
}