﻿using System.Xml;

namespace MetX.Standard.Generation.CSharp.Project
{
    public interface IRefer
    {
        IGenerateCsProj Parent { get; set; }
        XmlElement Remove();
        XmlElement InsertOrUpdate();
    }
}