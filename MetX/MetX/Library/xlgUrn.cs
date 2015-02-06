using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml.XPath;

namespace MetX.Library
{

    /// <summary>This class is automatically made available as urn:xlg while rendering xsl pages from any of the MetX.Web xsl rendering classes. Each function provides some string, date, and totaling capability as well as some basic variable storage that can survive template calls.
    /// <para>NOTE: XSL functions must return a variable. Most functions in this library normally wouldn't return anything, but to accomodate XSL, they return a blank string.</para>
    /// </summary>
    public class XlgUrn
    {
        private int m_NextID;
        private int m_NextHash = 1;
        private int m_NextLayer = 10000;
        private string m_CurrRowClass = "contentDataRow2";
        private string m_NextRowClass = "contentDataRow1";
        private Dictionary<string, double> m_RunningTotals;
        private Dictionary<string, string> m_Ht;
        private Dictionary<string, string> m_Vars;
        private Dictionary<string, StringBuilder> m_SbVars;

        private string m_ThemePath = "~/theme/default/";
        private string m_DefaultThemePath = "~/theme/default/";
        private string m_SupportPath = "/xlgSupport/";

        HttpContext m_C;


        /// <summary>
        /// Returns if one or more bits are set in ToCheck
        /// </summary>
        /// <param name="toCheck">The Integer to check</param>
        /// <param name="mask">The bit mask to check</param>
        /// <returns>true if all bits set in Mask are set in ToCheck, else false</returns>
        public bool HasBit(int toCheck, int mask)
        {
            return (toCheck & mask) != 0;
        }

        /// <summary>Sets the internal variable 'ThemePath' used with FilePath, FileUrl, and FileContents</summary>
        /// <param name="themePath">The virtual path to the theme directory (such as '/~/theme/YourClientName/')</param>
        /// <param name="defaultThemePath">The virtual path to the defalt theme directory (such as '/~/theme/default/')</param>
        /// <returns>A blank string</returns>
        public string SetThemePath(string themePath, string defaultThemePath)
        {
            if (!themePath.EndsWith("/"))
                themePath += "/";
            if (!defaultThemePath.EndsWith("/"))
                defaultThemePath += "/";
            this.m_ThemePath = themePath;
            this.m_DefaultThemePath = defaultThemePath;
            return string.Empty;
        }

        /// <summary>
        /// Returns the URL encoded value of the string passed in
        /// </summary>
        /// <param name="toEncode">The string to URL encode</param>
        /// <returns>The URL encoded string</returns>
        public string UrlEncode(string toEncode)
        {
            if (toEncode == null)
                return string.Empty;
            return System.Web.HttpUtility.UrlEncode(toEncode);
        }

        /// <summary>Sets the internal variable 'Support' used with FilePath, FileUrl, and FileContents</summary>
        /// <param name="supportPath">The virtual path to the xlgSupport directory (defalts to '/xlgSupport/')</param>
        /// <returns>A blank string</returns>
        public string SetSupportPath(string supportPath)
        {
            if (!supportPath.EndsWith("/"))
                supportPath += "/";
            this.m_SupportPath = supportPath;
            return string.Empty;
        }

        /// <summary>Returns the value of the internal variable 'ThemePath'</summary>
        /// <returns>The value of ThemePath</returns>
        public string GetThemePath()
        {
            return m_ThemePath;
        }

        /// <summary>Returns the value of the internal variable 'SupportPath'</summary>
        /// <returns>The value of SupportPath</returns>
        public string GetSupportPath()
        {
            return m_SupportPath;
        }

        /// <summary>Returns the physical path and file name of RelativePathFile.
        /// <para>Checks if ThemePath + RelativePathFile exists first, if so, returns that physical path and filename.</para>
        /// <para>If RelativePathFile exists, returns that.</para>
        /// <para>If SupportPath + RelativePathFile exists, returns that.</para>
        /// <para>Otherwise returns the string "unknown.file"</para>
        /// </summary>
        /// <param name="relativePathFile">The relative path and file to retrieve (such as 'xsl/Appearence.xsl')</param>
        /// <returns>The physical path and file found</returns>
        public string FilePath(string relativePathFile)
        {
            if (m_C == null)
                m_C = HttpContext.Current;

            relativePathFile = relativePathFile.Replace(m_DefaultThemePath, string.Empty);
            relativePathFile = relativePathFile.Replace(m_DefaultThemePath.Replace("~/", string.Empty), string.Empty);

            string ret = m_C.Server.MapPath(m_ThemePath + relativePathFile);
            if (File.Exists(ret))
                return ret;
            ret = m_C.Server.MapPath(m_DefaultThemePath + relativePathFile);
            if (File.Exists(ret))
                return ret;
            ret = m_C.Server.MapPath(relativePathFile);
            if (File.Exists(ret))
                return ret;
            ret = m_C.Server.MapPath("~/" + relativePathFile);
            if (File.Exists(ret))
                return ret;
            ret = m_C.Server.MapPath(m_SupportPath + relativePathFile);
            if (File.Exists(ret))
                return ret;
            return "unknown.file";
        }


        /// <summary>Returns the virtual path and file name of RelativePathFile.
        /// <para>Checks if ThemePath + RelativePathFile exists first, if so, returns that virtual path and filename.</para>
        /// <para>If RelativePathFile exists, returns that.</para>
        /// <para>If SupportPath + RelativePathFile exists, returns that.</para>
        /// <para>Otherwise returns the string "unknown.file"</para>
        /// </summary>
        /// <param name="relativePathFile">The relative path and file to retrieve (such as 'xsl/Appearence.xsl')</param>
        /// <returns>The virtual path and file found</returns>
        public string FileUrl(string relativePathFile)
        {
            string ret = "unknown.file";
            if (m_C == null)
                m_C = HttpContext.Current;

            relativePathFile = relativePathFile.Replace(m_DefaultThemePath, string.Empty);
            relativePathFile = relativePathFile.Replace(m_DefaultThemePath.Replace("~/", string.Empty), string.Empty);

            string filePath = m_C.Server.MapPath(m_ThemePath + relativePathFile);
            string vDirPath;

            if (File.Exists(filePath))
                ret = m_ThemePath + relativePathFile;
            else
            {
                filePath = m_C.Server.MapPath(m_DefaultThemePath + relativePathFile);
                if (File.Exists(filePath))
                    ret = m_DefaultThemePath + relativePathFile;
                else
                {
                    filePath = m_C.Server.MapPath(relativePathFile);
                    if (File.Exists(filePath))
                    {
                        ret = StringExtensions.TokensAfter(m_C.Request.Url.AbsoluteUri, 4, "/");
                        ret = StringExtensions.FirstToken(StringExtensions.TokensBefore(ret, StringExtensions.TokenCount(ret, "/"), "/"), "?");
                        ret = ret + "/" + relativePathFile;
                    }
                    else
                    {
                        filePath = m_C.Server.MapPath("~/" + relativePathFile);
                        if (File.Exists(filePath))
                        {
                            ret = StringExtensions.TokensAfter(m_C.Request.Url.AbsoluteUri, 4, "/");
                            ret = StringExtensions.FirstToken(StringExtensions.TokensBefore(ret, StringExtensions.TokenCount(ret, "/"), "/"), "?");
                            ret = ret + "/" + relativePathFile;
                        }
                        else
                        {
                            filePath = m_C.Server.MapPath(m_SupportPath + relativePathFile);
                            if (File.Exists(filePath))
                            {
                                ret = m_SupportPath + relativePathFile;
                                vDirPath = StringExtensions.FirstToken(StringExtensions.TokensBefore(m_C.Request.Url.AbsoluteUri, 4, "/"), "?");
                                ret = ret.Replace("~/", string.Empty);
                                if (ret.StartsWith("/"))
                                    ret = vDirPath + ret;
                                else
                                    ret = vDirPath + "/" + ret;
                                return ret;
                            }
                        }
                    }
                }
            }
            vDirPath = StringExtensions.FirstToken(StringExtensions.TokensBefore(m_C.Request.Url.AbsoluteUri, 5, "/"), "?");
            ret = ret.Replace("~/", string.Empty);
            if (ret.StartsWith("/"))
                ret = vDirPath + ret;
            else
                ret = vDirPath + "/" + ret;
            return ret;
        }


        /// <summary>Uses FilePath to determine the path to a physical file and then retrieves the string contents.</summary>
        /// <param name="relativePathFile">The relative path and file to retrieve</param>
        /// <returns>The contents of the file</returns>
        public string FileContents(string relativePathFile)
        {
            relativePathFile = FilePath(relativePathFile);
            if (relativePathFile != "unknown.file")
                return MetX.IO.FileSystem.FileToString(relativePathFile);
            return string.Empty;
        }

        /// <summary>
        /// Same as calling System.IO.File.Exists
        /// </summary>
        /// <param name="pathAndFilename">Path and filename to the file</param>
        /// <returns>True if the file exists</returns>
        public bool FileExists(string pathAndFilename)
        {
            return File.Exists(pathAndFilename);
        }


        /// <summary>Same as calling System.IO.Directory.Exists</summary>
        /// <param name="path">The path to test existence for</param>
        /// <returns>True if the folder exists</returns>
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>Same as calling System.IO.Directory.Create</summary>
        /// <param name="path">The path to create</param>
        /// <returns>True if the folder exists</returns>
        public string CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return string.Empty;
        }

        /// <summary>Same as calling System.IO.Directory.Create</summary>
        /// <param name="path">The path to create</param>
        /// <returns>True if the folder is created, false if it already existed</returns>
        public bool InsureDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return false;
            }
            Directory.CreateDirectory(path);
            return true;
        }

        /// <summary>
        /// Appends a string to an internal string builder
        /// </summary>
        /// <param name="sbVarName">The name of the string builder to append to</param>
        /// <param name="toAppend">The string to append</param>
        /// <returns>An empty string</returns>
        public string SbAppend(string sbVarName, string toAppend)
        {
            if (m_SbVars == null)
                m_SbVars = new Dictionary<string, StringBuilder>();
            if (m_SbVars.ContainsKey(sbVarName))
                m_SbVars[sbVarName].Append(toAppend);
            else
                m_SbVars[sbVarName] = new StringBuilder(toAppend);
            return string.Empty;
        }


        /// <summary>
        /// Appends a string to an internal string builder with a new line character at the end
        /// </summary>
        /// <param name="sbVarName">The name of the string builder to append to</param>
        /// <param name="toAppend">The string to append</param>
        /// <returns>An empty string</returns>
        public string SbAppendLine(string sbVarName, string toAppend)
        {
            if (m_SbVars == null)
                m_SbVars = new Dictionary<string, StringBuilder>();
            if (!m_SbVars.ContainsKey(sbVarName))
                m_SbVars[sbVarName] = new StringBuilder();
            m_SbVars[sbVarName].AppendLine(toAppend);
            return string.Empty;
        }


        /// <summary>
        /// Retrieves the contents of an internal string builder
        /// </summary>
        /// <param name="sbVarName">The string builder to retrieve</param>
        /// <returns>The string builder contents</returns>
        public string SbGetVar(string sbVarName)
        {
            if (m_SbVars != null && m_SbVars.ContainsKey(sbVarName))
                return m_SbVars[sbVarName].ToString();
            return string.Empty;
        }


        /// <summary>
        /// Removes an internal string builder
        /// </summary>
        /// <param name="sbVarName">The string builder to remove</param>
        /// <returns>An empty string</returns>
        public string SbRemove(string sbVarName)
        {
            if (m_SbVars != null && m_SbVars.ContainsKey(sbVarName))
                m_SbVars.Remove(sbVarName);
            return string.Empty;
        }


        /// <summary>Removes a specific internal variable set by SetVar</summary>
        /// <param name="varName">The variable to remove</param>
        /// <returns>a blank string</returns>
        public string RemoveVar(string varName)
        {
            if (m_Vars == null)
                m_Vars = new Dictionary<string, string>();
            if (m_Vars.ContainsKey(varName))
                m_Vars.Remove(varName);
            return string.Empty;
        }


        /// <summary>Clears all internal (non-xsl) variables</summary>
        /// <returns>a blank string</returns>
        public string ClearVars()
        {
            if (m_Vars != null)
                m_Vars.Clear();
            return string.Empty;
        }


        /// <summary>Sets an internal variable to some value. This value will persist until changed or until the end of rendering</summary>
        /// <param name="varName">The variable name to set</param>
        /// <param name="varValue">The value of the variable</param>
        /// <returns>a blank string</returns>
        public string SetVar(string varName, string varValue)
        {
            if (m_Vars == null)
                m_Vars = new Dictionary<string, string>();
            m_Vars[varName] = varValue;
            return string.Empty;
        }


        /// <summary>Returns the value of an internal variable or a blank string if that variable wasn't set.</summary>
        /// <param name="varName">The variable to retrieve</param>
        /// <returns>The variables value or a blank string if not set</returns>
        public string GetVar(string varName)
        {
            if (m_Vars == null)
                m_Vars = new Dictionary<string, string>();
            if (m_Vars.ContainsKey(varName))
                return m_Vars[varName];
            return string.Empty;
        }


        /// <summary>Returns true if the variable has been set and if the value's length is greater than zero</summary>
        /// <param name="varName">The variable to test.</param>
        /// <returns>True if the variable has been set to a non empty string</returns>
        public bool IsVarSet(string varName)
        {
            if (m_Vars == null)
                m_Vars = new Dictionary<string, string>();
            if (m_Vars.ContainsKey(varName) && m_Vars[varName] != null && m_Vars[varName].Length > 0)
                return true;
            return false;
        }


        /// <summary>Creates an incrementing number for the ToHash value and stores it in a table. Each time afterward sHash is called with ToHash, the same value will be returned. Among other things, useful for generating a script block and a short unique div name.</summary>
        /// <param name="toHash">The item to hash</param>
        /// <returns>The hash value for the item</returns>
        public string SHash(string toHash)
        {
            if (m_Ht == null)
                m_Ht = new Dictionary<string, string>();
            // ReSharper disable once InvertIf
            if (!m_Ht.ContainsKey(toHash))
            {
                m_Ht.Add(toHash, m_NextHash.ToString());
                m_NextHash += 1;
            }
            return m_Ht[toHash];
        }


        /// <summary>Replaces one string with another. Equivalent to calling string.Replace</summary>
        /// <param name="toSearch">The text to do the replacement on</param>
        /// <param name="toFind">The text to find</param>
        /// <param name="toReplace">The text to replace</param>
        /// <returns>The string with text replaced.</returns>
        public string SReplace(string toSearch, string toFind, string toReplace)
        {
            return toSearch.Replace(toFind, toReplace);
        }


        /// <summary>Returns an xml string of the Inner text of one or more Nodes</summary>
        /// <param name="nodes">The nodes to return InnerXml for</param>
        /// <returns>The inner xml string of the nodes</returns>
        public string InnerXml(XPathNodeIterator nodes)
        {
            if (nodes == null)
            {
                return string.Empty;
            }
            XPathNavigator nameValueNodes = null;
            nodes.MoveNext();
            nameValueNodes = nodes.Current;
            return nameValueNodes.InnerXml;
        }


        /// <summary>Returns an xml string of the Outer text of one or more Nodes</summary>
        /// <param name="nodes">The nodes to return OuterXml for</param>
        /// <returns>The outer xml string of the nodes</returns>
        public string OuterXml(XPathNodeIterator nodes)
        {
            if (nodes == null)
            {
                return string.Empty;
            }
            XPathNavigator nameValueNodes = null;
            nodes.MoveNext();
            nameValueNodes = nodes.Current;
            return nameValueNodes.OuterXml;
        }


        /// <summary>Returns the OuterXml of Nodes in javascript format set to the javascript variable VarName. So this generates a single (usually very long) line of javascript.</summary>
        /// <param name="varName">The javascript variable name to place the outerxml into</param>
        /// <param name="nodes">The nodes to javascript code</param>
        /// <returns>One line of javascript of the form var VarName = "outerxml of Nodes";</returns>
        public string OuterXmlJson(string varName, XPathNodeIterator nodes)
        {
            StringBuilder ret = new StringBuilder(OuterXml(nodes));
            if (ret.Length > 0)
            {
                ret.Replace("\"", "\\\"");
                ret.Replace(System.Environment.NewLine, "\\n");
                ret.Replace("\r", "\\n");
                ret.Replace("\n", "\\n");
                ret.Insert(0, "var " + varName + " = \"<?xml version=\\\"1.0\\\" encoding=\\\"UTF-8\\\" ?>\\n");
                ret.Append("\";");
            }
            return ret.ToString();
        }


        /// <summary>Returns the maximum of two dates</summary>
        /// <param name="sDate1">The first date to compare</param>
        /// <param name="sDate2">The second date to compare</param>
        /// <returns>The greater date</returns>
        public string DateMax(string sDate1, string sDate2)
        {
            if (sDate1.Length > 0)
            {
                if (sDate2.Length > 0)
                {
                    System.DateTime date1 = System.DateTime.Parse(sDate1);
                    System.DateTime date2 = System.DateTime.Parse(sDate2);
                    if (date1 > date2)
                        return sDate1;
                    else
                        return sDate2;
                }
                else
                    return sDate1;
            }
            return sDate2;
        }


        /// <summary>Determins if one date is between or equal to two other dates</summary>
        /// <param name="sDateToTest">The date to test</param>
        /// <param name="sDateBegin">The lower boundary to test</param>
        /// <param name="sDateEnd">The upper boundary to test</param>
        /// <returns>true if sDateToTest is between sDateBegin and sDateEnd</returns>
        public bool DateIsInRange(string sDateToTest, string sDateBegin, string sDateEnd)
        {
            if (sDateToTest.Length > 0)
            {
                if (sDateBegin.Length > 0)
                {
                    if (sDateEnd.Length > 0)
                    {
                        System.DateTime dateToTest = System.DateTime.Parse(sDateToTest);
                        System.DateTime dateBegin = System.DateTime.Parse(sDateBegin);
                        System.DateTime dateEnd = System.DateTime.Parse(sDateEnd);
                        if (dateToTest >= dateBegin && dateToTest <= dateEnd)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        /// <summary>Retrieves the nth delimited token</summary>
        /// <param name="allTokens">The string to retrieve a token from</param>
        /// <param name="n">The token number to retrieve</param>
        /// <param name="delimiter">The delimiter separating each token</param>
        /// <returns>The requested token or a blank string</returns>
        public string GetToken(string allTokens, int n, string delimiter)
        {
            return StringExtensions.TokenAt(allTokens, n, delimiter);
        }


        /// <summary>Returns the index of a string inside another string. Equivalent to string.IndexOf</summary>
        /// <param name="toSearch">The string to search</param>
        /// <param name="toFind">The string to find</param>
        /// <returns>The index of ToFind in ToSearch. -1 if nothing is found</returns>
        public int IndexOf(string toSearch, string toFind)
        {
            return toSearch.IndexOf(toFind);
        }


        /// <summary>Determins if the date passed in is today regardless of the time of day</summary>
        /// <param name="dateStringToTest">The date to test</param>
        /// <returns>true if the date is between 00:00am and 11:59pm today</returns>
        public bool IsToday(string dateStringToTest)
        {
            if (Microsoft.VisualBasic.Information.IsDate(dateStringToTest))
            {
                return (System.Convert.ToDateTime(dateStringToTest) >= System.DateTime.Today) & (System.Convert.ToDateTime(dateStringToTest) < System.DateTime.Today.AddDays(1));
            }
            else
            {
                return false;
            }
        }

        /// <summary>Determines if a date is in the past</summary>
        /// <param name="dateStringToTest">The date to test</param>
        /// <returns>true if the date is in the past (even by a second)</returns>
        public bool IsInThePast(string dateStringToTest)
        {
            if (Microsoft.VisualBasic.Information.IsDate(dateStringToTest))
                return Convert.ToDateTime(dateStringToTest).CompareTo(DateTime.Now) < 0;
            return false;
        }

        /// <summary>Returns the proper case of a string (such as a name). So "this is a test" becomes "This Is A Test".</summary>
        /// <param name="text">The text to proper case</param>
        /// <returns>The proper case string</returns>
        public string ProperCase(string text)
        {
            string ret = string.Empty;
            try
            {
                ret = Microsoft.VisualBasic.Strings.StrConv(text, Microsoft.VisualBasic.VbStrConv.ProperCase, 0);
            }
            catch { }
            return ret;
        }


        /// <summary>Returns the current date and time</summary>
        /// <returns>The current date/time</returns>
        public string Today()
        {
            return System.DateTime.Now.ToString();
        }


        /// <summary>Returns the integer representation of a string</summary>
        /// <param name="sOriginalText">The string to convert</param>
        /// <returns>The integer representation of a string</returns>
        public int NzInteger(string sOriginalText)
        {
            return Worker.nzInteger(sOriginalText);
        }

        /// <summary>Returns the double representation of a string</summary>
        /// <param name="sOriginalText">The string to convert</param>
        /// <returns>The double representation of a string</returns>
        public double NzDouble(string sOriginalText)
        {
            return Worker.NzDouble(sOriginalText);
        }


        /// <summary>Extracts a name from an email address</summary>
        /// <param name="sOriginalText">The Email address</param>
        /// <returns>The extracted proper case name</returns>
        public string EmailToName(string sOriginalText)
        {
            return Worker.EmailToName(sOriginalText, string.Empty);
        }


        /// <summary>Coverts the passed string to lowercase</summary>
        /// <param name="sOriginalText">The text to convert</param>
        /// <returns>The lowercase version of the string</returns>
        public string Lower(string sOriginalText)
        {
            return Worker.nzString(sOriginalText).ToLower();
        }


        /// <summary>Coverts the passed string to uppercase</summary>
        /// <param name="sOriginalText">The text to convert</param>
        /// <returns>The uppercase version of the string</returns>
        public string Upper(string sOriginalText)
        {
            return Worker.nzString(sOriginalText).ToUpper();
        }

        public string Camel(string sOriginalText)
        {
            if (string.IsNullOrEmpty(sOriginalText))
                return string.Empty;
            string ret = Worker.nzString(sOriginalText);
            ret = ret == ret.ToUpper() 
                ? ret.ToLower() 
                : ret[0].ToString().ToLower() + ret.Substring(1);
            return ret;
        }

        public string Proper(string sOriginalText)
        {
            if (string.IsNullOrEmpty(sOriginalText))
                return string.Empty;
            string ret = Worker.nzString(sOriginalText);
            ret = ret[0].ToString().ToUpper() + ret.Substring(1);
            return ret;
        }

        /// <summary>Returns the number of days old the date passed in is or a blank string if it isn't a valid date</summary>
        /// <param name="xmlDate">The date to calculate</param>
        /// <returns>The number of days old the date is</returns>
        public string SXmlAgeOfDate(string xmlDate)
        {
            xmlDate = System.Convert.ToString(xmlDate + string.Empty).Trim();
            if (xmlDate.Length > 0)
            {
                System.DateTime dt = System.Convert.ToDateTime(xmlDate);
                dt = dt.AddHours(-dt.Hour).AddMinutes(-dt.Minute);
                TimeSpan ts = DateTime.Today.Subtract(dt);
                return ts.Days.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// Takes a "StringLikeThis" and adds spaces at each upper case letter, except consecutive upper letters, 
        /// to make a "String Like This"
        /// </summary>
        /// <param name="target">The string to expand</param>
        /// <returns>The space expanded string</returns>
        public string Expand(string target)
        {
            if (string.IsNullOrEmpty(target))
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < target.Length; i++)
            {
                char curr = target[i];
                if (i > 0 && ((curr >= 'A' && curr <= 'Z') || (curr >= '0' && curr <= '9')))
                {
                    char prev = target[i - 1];
                    //if (!((prev >= 'A' && prev <= 'Z') || (prev >= '0' && prev <= '9')))
                    if(curr >= 'A' && curr <= 'Z')
                    {
                        if (!(prev >= 'A' && prev <= 'Z'))
                            sb.Append(" ");
                    }
                    else // Is a number
                    {
                        if (!(prev >= '0' && prev <= '9'))
                            sb.Append(" ");
                    }
                }
                sb.Append(curr);
            }
            return sb.ToString();
        }
        /// <summary>Converts an xml date/time into a javascript compatible date</summary>
        /// <param name="xmlDate">The date/time to convert</param>
        /// <param name="defaultValue">The value to return if the date passed is blank or invalid</param>
        /// <returns>The javascript date in MMMM, dd YYYY format</returns>
        public string SXmlDateOnlyForJavaScript(string xmlDate, string defaultValue)
        {
            xmlDate = System.Convert.ToString(xmlDate + string.Empty).Trim();
            if (xmlDate.Length > 0 & Microsoft.VisualBasic.Information.IsDate(xmlDate))
            {
                return System.Convert.ToDateTime(xmlDate).ToString("MMMM, dd yyyy");
            }
            return defaultValue;
        }


        /// <summary>Converts an xml date/time into a displayable date (MM/dd/YYYY format)</summary>
        /// <param name="xmlDate">The date to convert</param>
        /// <returns>The displayable date</returns>
        public string SXmlDateOnly(string xmlDate)
        {
            xmlDate = System.Convert.ToString(xmlDate + string.Empty).Trim();
            if (xmlDate.Length > 0 & Microsoft.VisualBasic.Information.IsDate(xmlDate))
            {
                return System.Convert.ToDateTime(xmlDate).ToString("MM/dd/yyyy").ToLower().Replace("01/01/1900", string.Empty);
            }
            return string.Empty;
        }


        /// <summary>Converts an xml date/time into a displayable date/time value ("MM/dd/YYYY hh:mm tt" format)</summary>
        /// <param name="xmlDate">The date to convert</param>
        /// <returns>The displayable date</returns>
        public string sXmlDate(string xmlDate)
        {
            xmlDate = System.Convert.ToString(xmlDate + string.Empty).Trim();
            if (xmlDate.Length > 0)
            {
                return System.Convert.ToDateTime(xmlDate).ToString("MM/dd/yyyy hh:mm tt").ToLower().Replace(" 12:00 am", string.Empty);
            }
            return string.Empty;
        }


        /// <summary>Formats a number to a particular format (see the VB Format() function).</summary>
        /// <param name="value">The value to format</param>
        /// <param name="formatString">The VB.NET format string</param>
        /// <returns>The formated string</returns>
        public string Format(float value, string formatString)
        {
            return Microsoft.VisualBasic.Strings.Format(value, formatString);
        }


        public string Chars(int count, string toRepeat)
        {
            if (count < 0)
                return string.Empty;
            if (string.IsNullOrEmpty(toRepeat))
                toRepeat = " ";
            if (count > 200)
                count = 200;
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < count; i++) { ret.Append(toRepeat); }
            return ret.ToString();
        }


        /// <summary>Converts an xml date/time into a displayable date/time value ("MM/dd/YYYY hh:mm tt" format)</summary>
        /// <param name="xmlDate">The date to convert</param>
        /// <param name="sFormat">The VB.NET format string to format the date/time to</param>
        /// <returns>The formated date/time string</returns>
        public string sXmlDate(string xmlDate, string sFormat)
        {
            xmlDate = System.Convert.ToString(xmlDate + string.Empty).Trim();
            if (xmlDate.Length > 0)
            {
                return System.Convert.ToDateTime(xmlDate).ToString(sFormat).ToLower();
            }
            return string.Empty;
        }

        /// <summary>Returns the first name (word) from the given string</summary>
        /// <param name="name">The name to parse</param>
        /// <returns>The first name found</returns>
        public string FirstName(string name)
        {
            return StringExtensions.FirstToken(name, " ");
        }

        /// <summary>Returns the last name (word) from the given string</summary>
        /// <param name="name">The name to parse</param>
        /// <returns>The last name found</returns>
        public string LastName(string name)
        {
            return StringExtensions.LastToken(name, " ");
        }


        /// <summary>Increments an internal counter (NextID) and returns that value.</summary>
        /// <returns>The next higher ID</returns>
        public string GetNextID()
        {
            m_NextID += 1;
            return m_NextID.ToString();
        }


        /// <summary>Subtracts one from the internal layer count (starting at 10,000) and returns that value</summary>
        /// <returns>The next lower layer</returns>
        public string GetNextLayer()
        {
            m_NextLayer -= 1;
            return m_NextLayer.ToString();
        }


        /// <summary>Occilates between returning "contentDataRow1" and "contentDataRow2"</summary>
        /// <returns>The next row CSS class value</returns>
        public string GetNextRowClass()
        {
            string t = m_CurrRowClass;
            m_CurrRowClass = m_NextRowClass;
            m_NextRowClass = t;
            return t;
        }

        /// <summary>
        /// Clears the internal list of totals
        /// </summary>
        /// <returns></returns>
        public string ClearTotals()
        {
            m_RunningTotals = new Dictionary<string, double>();
            return string.Empty;
        }

        /// <summary>Clears the internal total to 0</summary>
        /// <param name="totalName">The name of the total to clear</param>
        /// <returns>The string "0"</returns>
        public string ClearTotal(string totalName)
        {
            return ClearTotal(totalName, "0");
        }

        /// <summary>Clears the internal total to some intial value</summary>
        /// <param name="totalName">The name of the total to clear</param>
        /// <param name="sInitialValue">The value to set internal total to</param>
        /// <returns>an empty string</returns>
        public string ClearTotal(string totalName, string sInitialValue)
        {
            if (m_RunningTotals == null)
                m_RunningTotals = new Dictionary<string, double>();
            if (m_RunningTotals.ContainsKey(totalName))
                m_RunningTotals[totalName] = Worker.NzDouble(sInitialValue);
            else
                m_RunningTotals.Add(totalName, Worker.NzDouble(sInitialValue));
            return string.Empty;
        }


        /// <summary>Adds a value to the internal total</summary>
        /// <param name="totalName">The name of the total</param>
        /// <param name="toAdd">The value to add to the internal total</param>
        /// <returns>An empty string</returns>
        public string AddToTotal(string totalName, string toAdd)
        {
            if (m_RunningTotals == null)
                m_RunningTotals = new Dictionary<string, double>();
            if (m_RunningTotals.ContainsKey(totalName))
                m_RunningTotals[totalName] += NzDouble(toAdd);
            else
                m_RunningTotals.Add(totalName, Worker.NzDouble(toAdd));
            return string.Empty;
        }


        /// <summary>Subtracts a value from the internal total</summary>
        /// <param name="totalName">The name of the total</param>
        /// <param name="toSubtract">The amount to subtract</param>
        /// <returns>a blank string</returns>
        public string SubtractFromTotal(string totalName, string toSubtract)
        {
            if (m_RunningTotals == null)
                m_RunningTotals = new Dictionary<string, double>();
            if (m_RunningTotals.ContainsKey(totalName))
                m_RunningTotals[totalName] -= NzDouble(toSubtract);
            else
                m_RunningTotals.Add(totalName, -Worker.NzDouble(toSubtract));
            return string.Empty;
        }


        /// <summary>Returns the current internal total value</summary>
        /// <param name="totalName">The name of the total</param>
        /// <param name="decimalPlaces">The number of decimal places to return the internal total (normally 2 or 0)</param>
        /// <returns>The internal total formatted to the number of decimal places</returns>
        public string GetTotal(string totalName, int decimalPlaces)
        {
            if (m_RunningTotals == null)
                m_RunningTotals = new Dictionary<string, double>();
            if (decimalPlaces > 0)
                if (m_RunningTotals.ContainsKey(totalName))
                    return m_RunningTotals[totalName].ToString("0." + new String('0', decimalPlaces));
                else
                    return "0." + new String('0', decimalPlaces);
            else if (m_RunningTotals.ContainsKey(totalName))
                return m_RunningTotals[totalName].ToString();
            return "0";
        }

        /// <summary>
        /// Returns a distinct set of nodes
        /// </summary>
        /// <param name="nodeset"></param>
        /// <returns></returns>
        public XPathNavigator[] Distinct(XPathNodeIterator nodeset)
        {
            if (nodeset.Count == 0)
                return new XPathNavigator[0];
            Dictionary<string, XPathNavigator> retNodes = new Dictionary<string, XPathNavigator>();
            while (nodeset.MoveNext())
                if (!retNodes.ContainsKey(nodeset.Current.Value))
                    retNodes.Add(nodeset.Current.Value, nodeset.Current);
            XPathNavigator[] ret = new XPathNavigator[retNodes.Count];
            retNodes.Values.CopyTo(ret, 0);
            return ret;
        }

        public bool IsIn(string attributeName, string toFind, XPathNodeIterator nodeset)
        {
            if (nodeset.Count == 0 || string.IsNullOrEmpty(attributeName) || string.IsNullOrEmpty(toFind))
                return false;

            while (nodeset.MoveNext())
            {
                string attributeValue = nodeset.Current.GetAttribute(attributeName, string.Empty);
                if (attributeValue == toFind)
                    return true;
            }
            return false;
        }

        public XPathNavigator[] In(string attributeName, string toFind, XPathNodeIterator nodeset)
        {
            if (nodeset.Count == 0 || string.IsNullOrEmpty(attributeName) || string.IsNullOrEmpty(toFind))
                return new XPathNavigator[0];

            Dictionary<string, XPathNavigator> retNodes = new Dictionary<string, XPathNavigator>();
            while (nodeset.MoveNext())
            {
                string attributeValue = nodeset.Current.GetAttribute(attributeName, string.Empty);
                if (attributeValue == toFind && !retNodes.ContainsKey(nodeset.Current.Value))
                    retNodes.Add(nodeset.Current.Value, nodeset.Current);
            }
            XPathNavigator[] ret = new XPathNavigator[retNodes.Count];
            retNodes.Values.CopyTo(ret, 0);
            return ret;
        }

        // xlg:AllNotIn('ColumnName',$NonPointerMasterColumns,$HistoryTable/Columns/Column)
        public XPathNavigator[] AllNotIn(string attributeName, XPathNodeIterator nodeSetToCompareAgainst, XPathNodeIterator nodesetToPossiblyKeep)
        {
            if (nodesetToPossiblyKeep.Count == 0 || string.IsNullOrEmpty(attributeName))
                return new XPathNavigator[0];

            // Handle special case of when there's nothing to compare against
            // Return all possible nodes 
            if (nodeSetToCompareAgainst.Count == 0)
            {
                return XPathNodeIteratorToNavigators(nodesetToPossiblyKeep);
            }

            // Build a list we can look over multiple times
            List<string> compareSet = new List<string>();
            while (nodeSetToCompareAgainst.MoveNext())
            {
                string attributeValue = nodeSetToCompareAgainst.Current.GetAttribute(attributeName, string.Empty);
                if (!string.IsNullOrEmpty(attributeValue) && !compareSet.Contains(attributeValue))
                    compareSet.Add(attributeValue);
            }

            // Compare and add 
            Dictionary<string, XPathNavigator> retNodes = new Dictionary<string, XPathNavigator>();
            while (nodesetToPossiblyKeep.MoveNext())
            {
                string attributeValue = nodesetToPossiblyKeep.Current.GetAttribute(attributeName, string.Empty);
                if (!compareSet.Contains(attributeValue))
                    retNodes.Add(attributeValue, nodesetToPossiblyKeep.Current);
            }
            XPathNavigator[] ret = new XPathNavigator[retNodes.Count];
            retNodes.Values.CopyTo(ret, 0);
            return ret;
        }

        private static XPathNavigator[] XPathNodeIteratorToNavigators(XPathNodeIterator nodesetToPossiblyKeep)
        {
            Dictionary<string, XPathNavigator> retNodes = new Dictionary<string, XPathNavigator>();
            while (nodesetToPossiblyKeep.MoveNext())
            {
                string key = nodesetToPossiblyKeep.Current.Value;
                if (string.IsNullOrEmpty(key)) key = Guid.NewGuid().ToString();
                retNodes.Add(key, nodesetToPossiblyKeep.Current);
            }
            XPathNavigator[] ret = new XPathNavigator[retNodes.Count];
            retNodes.Values.CopyTo(ret, 0);
            return ret;
        }
    }
}
