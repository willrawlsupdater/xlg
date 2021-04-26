using System.Collections.Generic;
using MetX.Standard.Library;
namespace MetX.Standard.Metadata
{
    //------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
    using System.Xml.Serialization;
// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Tables
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Table", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<TablesTable> Table;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Include")]
        public List<Include> Include;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TablesTable
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Column", typeof(ColumnsColumn),
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
         public List<List<ColumnsColumn>> Columns;
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Key", typeof(TablesTableKeysKey),
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
         public List<List<TablesTableKeysKey>> Keys;
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Index", typeof(TablesTableIndexesIndex),
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
         public List<List<TablesTableIndexesIndex>> Indexes;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TableName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClassName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PrimaryKeyColumnName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RowCount;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ColumnsColumn
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ColumnName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PropertyName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CSharpVariableType;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Location;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsDotNetObject;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CovertToPart;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VBVariableType;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AuditField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DbType;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AutoIncrement;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsForeignKey;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsNullable;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsIdentity;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsPrimaryKey;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsIndexed;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MaxLength;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SourceType;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DomainName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Precision;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Scale;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Column;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TablesTableKeysKey
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Column", typeof(ColumnsColumn),
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
         public List<List<ColumnsColumn>> Columns;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsPrimary;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Location;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TablesTableIndexesIndex
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("IndexColumn",
            typeof(TablesTableIndexesIndexIndexColumnsIndexColumn), Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
            IsNullable = false)]
         public List<List<TablesTableIndexesIndexIndexColumnsIndexColumn>> IndexColumns;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IndexName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsClustered;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SingleColumnIndex;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PropertyName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Location;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TablesTableIndexesIndexIndexColumnsIndexColumn
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IndexColumnName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Location;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PropertyName;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Include
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Columns
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Column", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<ColumnsColumn> Column;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class StoredProcedures
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StoredProcedure",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<StoredProceduresStoredProcedure> StoredProcedure;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Include")]
        public List<Include> Include;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Exclude")]
        public List<Exclude> Exclude;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClassName;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class StoredProceduresStoredProcedure
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Parameter",
            typeof(StoredProceduresStoredProcedureParametersParameter),
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
         public List<List<StoredProceduresStoredProcedureParametersParameter>> Parameters;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StoredProcedureName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MethodName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Location;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class StoredProceduresStoredProcedureParametersParameter
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DataType;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CSharpVariableType;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Location;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CovertToPart;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VBVariableType;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsDotNetObject;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ParameterName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VariableName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsInput;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsOutput;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Exclude
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class XslEndpoints
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("XslEndpoints")]
        public List<XslEndpoints> XslEndpoints1;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VirtualPath;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string xlgPath;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VirtualDir;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Path;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Folder;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class xlgDoc 
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Tables")]
        public List<Tables> Tables;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StoredProcedures")]
        public List<StoredProcedures> StoredProcedures;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("XslEndpoints")]
        public List<XslEndpoints> XslEndpoints;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Render", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<xlgDocRender> Render;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IncludeNamespace;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ConnectionStringName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Namespace;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VDirName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DatabaseProvider;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ProviderName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MetXObjectName;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MetXProviderAssemblyString;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ProviderAssemblyString;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OutputFolder;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Now;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string XlgInstanceID;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MetXAssemblyString;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class xlgDocRender
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Xsls", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<xlgDocRenderXsls> Xsls;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Tables")]
        public List<Tables> Tables;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StoredProcedures")]
        public List<StoredProcedures> StoredProcedures;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class xlgDocRenderXsls
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Include")]
        public List<Include> Include;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Exclude")]
        public List<Exclude> Exclude;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Path;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UrlExtension;
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class NewDataSet
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Columns", typeof(Columns))]
        [System.Xml.Serialization.XmlElementAttribute("Exclude", typeof(Exclude))]
        [System.Xml.Serialization.XmlElementAttribute("Include", typeof(Include))]
        [System.Xml.Serialization.XmlElementAttribute("StoredProcedures", typeof(StoredProcedures))]
        [System.Xml.Serialization.XmlElementAttribute("Tables", typeof(Tables))]
        [System.Xml.Serialization.XmlElementAttribute("XslEndpoints", typeof(XslEndpoints))]
        [System.Xml.Serialization.XmlElementAttribute("xlgDoc", typeof(xlgDoc))]
        public List<object> Items;
    }
}
