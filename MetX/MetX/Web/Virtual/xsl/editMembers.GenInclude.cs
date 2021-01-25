namespace MetX.Web.Virtual.xsl {
   /// <summary>Provides access to static virtual file content for files</summary>
   public partial class EditMembers {
       /// <summary>The static contents of the file: "C:\data\code\wmr\MetX\Web\Virtual\xsl\editMembers.xsl" as it existed at compile time.</summary>
       public const string Xsl = "<?xml version=\"1.0\" ?>\r\n<xsl:stylesheet xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"\r\n               \r\n                xmlns:xlg=\"urn:xlg\"\r\n                version=\"1.0\">\r\n  <xsl:output method=\"html\" />\r\n  <xsl:template match=\"/xlgDoc\">\r\n    <HTML>\r\n      <HEAD>\r\n        <title>Security Group Membership Editor</title>\r\n        <LINK REL=\"StyleSheet\" TYPE=\"text/css\" HREF=\"/xlgSupport/css/Styles.css\" />\r\n        <script language=\"javascript\">\r\n            function HandleAdd()\r\n            {\r\n            }\r\n            \r\n			      function DeleteMember()\r\n			      {\r\n				      var UserName = '';\r\n				      document.forms[0].hDeleteUser.value = \"false\";\r\n				      if(document.forms[0].listGroupMembers.value.length &gt; 0)\r\n				      {\r\n					      UserName = document.forms[0].listGroupMembers.value;\r\n				      }\r\n				      else if(document.forms[0].listNonGroupMembers.value.length &gt; 0)\r\n				      {\r\n					      UserName = document.forms[0].listNonGroupMembers.value;\r\n				      }\r\n				      if(UserName.length &gt; 0)\r\n				      {\r\n					      var message = \"Permanently delete the '\" + UserName + \"' security profile ? (type 'yes' and press enter to continue)\";\r\n					      if(prompt(message, 'no') == 'yes')\r\n					      {\r\n						      document.forms[0].hDeleteUser.value = \"true\";\r\n						      document.forms[0].submit();\r\n					      }\r\n				      }\r\n			      }\r\n		      </script>\r\n      </HEAD>\r\n      <body>\r\n		    <H1>XLG Security Group Membership Editor</H1>\r\n		    <H2>Application: <font color=\"blue\"><xsl:value-of select=\"/xlgDoc/Application/@ApplicationName\"/></font></H2>\r\n		    <H2>Security Group: <font color=\"blue\"><xsl:value-of select=\"/xlgDoc/Group/@GroupName\"/></font></H2>\r\n        <H3><a class=\"buttonLink\"><xsl:attribute name=\"href\">editApplication.aspx?Application=<xsl:value-of select=\"/xlgDoc/Application/@ApplicationName\"/></xsl:attribute> Change Security Groups</a></H3>\r\n			<p>\r\n				<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\">\r\n					<tr CLASS=\"HeaderContent\">\r\n						<td style=\"width: 188px\">Security Group Members</td>\r\n            <td style=\"width: 20px; background-color: white;\"></td>\r\n						<td style=\"width: 188px\">Other Members</td>\r\n            <td style=\"width: 20px; background-color: white;\"></td>\r\n            <td style=\"width: 188px;\">Add a new user to this group</td>\r\n					</tr>\r\n					<tr>\r\n						<td valign=\"top\" style=\"width: 188px\">\r\n              <table border=\"1\" cellpadding=\"3\" cellspacing=\"1\">\r\n                 <tr class=\"subHeaderContent\">\r\n                  <td width=\"100\">&amp;nbsp;</td>\r\n                   <td>Remove from Security Group</td>\r\n                   <td>User Name</td>\r\n                   <td>Full Name</td>\r\n                   <td>Category</td>\r\n                 </tr>\r\n              <xsl:for-each select=\"/xlgDoc/Members/User[@GroupName=/xlgDoc/Group/@GroupName]\">\r\n                <tr>\r\n									<xsl:choose>\r\n										<xsl:when test=\"position() mod 2 = 0\">\r\n											<xsl:attribute name=\"class\">contentDataRow1</xsl:attribute>\r\n										</xsl:when>\r\n										<xsl:otherwise>\r\n											<xsl:attribute name=\"class\">contentDataRow2</xsl:attribute>\r\n										</xsl:otherwise>\r\n									</xsl:choose>\r\n                  <td></td>\r\n                  <td>\r\n                    <a class=\"buttonLink\"><xsl:attribute name=\"href\">editMembers.aspx?UserName=<xsl:value-of select=\"@UserName\" />&amp;Application=<xsl:value-of select=\"/xlgDoc/Application/@ApplicationName\" />&amp;GroupName=<xsl:value-of select=\"/xlgDoc/Group/@GroupName\" />&amp;&amp;PostAction=removemember</xsl:attribute>Remove</a>\r\n                  </td>\r\n                  <td nowrap=\"nowrap\"><xsl:value-of select=\"@UserName\" /></td>\r\n                  <td nowrap=\"nowrap\"><xsl:value-of select=\"@FullName\" /></td>\r\n                  <td nowrap=\"nowrap\"><xsl:value-of select=\"@Category\" /></td>\r\n                </tr>\r\n              </xsl:for-each>\r\n              </table>\r\n            </td>\r\n            <td style=\"width: 20px; background-color: white;\"></td>\r\n						<td valign=\"top\" style=\"width: 188px\">\r\n              <table border=\"1\" cellpadding=\"3\" cellspacing=\"1\">\r\n                 <tr class=\"subHeaderContent\">\r\n                  <td width=\"100\">&amp;nbsp;</td>\r\n                   <td>Add to Security Group</td>\r\n                   <td>User Name</td>\r\n                   <td>Full Name</td>\r\n                   <td>Security Group</td>\r\n                   <td>Category</td>\r\n                 </tr>\r\n              <xsl:for-each select=\"/xlgDoc/Members/User[@GroupName!=/xlgDoc/Group/@GroupName]\">\r\n                <tr>\r\n									<xsl:choose>\r\n										<xsl:when test=\"position() mod 2 = 0\">\r\n											<xsl:attribute name=\"class\">contentDataRow1</xsl:attribute>\r\n										</xsl:when>\r\n										<xsl:otherwise>\r\n											<xsl:attribute name=\"class\">contentDataRow2</xsl:attribute>\r\n										</xsl:otherwise>\r\n									</xsl:choose>\r\n                  <td></td>\r\n                  <td>\r\n                    <a class=\"buttonLink\"><xsl:attribute name=\"href\">editMembers.aspx?UserName=<xsl:value-of select=\"@UserName\" />&amp;Application=<xsl:value-of select=\"/xlgDoc/Application/@ApplicationName\" />&amp;GroupName=<xsl:value-of select=\"/xlgDoc/Group/@GroupName\" />&amp;PostAction=addmember</xsl:attribute>Add</a>\r\n                  </td>\r\n                  <td nowrap=\"nowrap\"><xsl:value-of select=\"@UserName\" /></td>\r\n                  <td nowrap=\"nowrap\"><xsl:value-of select=\"@FullName\" /></td>\r\n                  <td nowrap=\"nowrap\"><xsl:value-of select=\"@GroupName\" /></td>\r\n                  <td nowrap=\"nowrap\"><xsl:value-of select=\"@Category\" /></td>\r\n                </tr>\r\n              </xsl:for-each>\r\n              </table>\r\n            </td>\r\n            <td style=\"width: 20px; background-color: white;\"></td>\r\n            <td valign=\"top\" style=\"width: 188px\">\r\n	            <form method=\"post\" action=\"editMember.aspx\">\r\n                <input type=\"hidden\" name=\"PostAction\" value=\"addnewuser\" />\r\n			            <table border=\"1\" cellpadding=\"3\" cellspacing=\"1\">\r\n				            <tr bgcolor=\"#ffcc00\">\r\n					            <td colspan=\"2\" style=\"WIDTH: 349px\">\r\n                        <input type=\"text\" ID=\"txtUserName\" Width=\"365px\" />\r\n                      </td>\r\n					            <td colspan=\"2\" style=\"WIDTH: 349px\">\r\n                        <input type=\"button\" value=\"Add New User\" onclick=\"HandleAdd();\" />\r\n                      </td>\r\n				            </tr>\r\n			            </table>\r\n	            </form>\r\n            </td>\r\n					</tr>\r\n				</table>\r\n			</p>\r\n  \r\n      </body>\r\n    </HTML>\r\n  </xsl:template>\r\n</xsl:stylesheet>";
       /// <summary>Returns xsl inside a StringBuilder.</summary>
       /// <returns>A StringBuilder with the compile time file contents</returns>
       public static System.Text.StringBuilder XslStringBuilder => new(Xsl);
   }
}
