REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 1.0.0.0

REM  Dump @3/29/2017 10:06:32 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace LocalMySQL

''' <summary>
''' ```SQL
''' KEGG orthology database cross reference to COG database.
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `xref_ko2cog`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `xref_ko2cog` (
'''   `uid` int(11) NOT NULL AUTO_INCREMENT,
'''   `ko` varchar(45) NOT NULL,
'''   `COG` varchar(45) NOT NULL,
'''   `url` text,
'''   PRIMARY KEY (`ko`,`COG`),
'''   UNIQUE KEY `uid_UNIQUE` (`uid`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='KEGG orthology database cross reference to COG database.';
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("xref_ko2cog", Database:="jp_kegg2", SchemaSQL:="
CREATE TABLE `xref_ko2cog` (
  `uid` int(11) NOT NULL AUTO_INCREMENT,
  `ko` varchar(45) NOT NULL,
  `COG` varchar(45) NOT NULL,
  `url` text,
  PRIMARY KEY (`ko`,`COG`),
  UNIQUE KEY `uid_UNIQUE` (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='KEGG orthology database cross reference to COG database.';")>
Public Class xref_ko2cog: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("uid"), AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property uid As Long
    <DatabaseField("ko"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "45")> Public Property ko As String
    <DatabaseField("COG"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "45")> Public Property COG As String
    <DatabaseField("url"), DataType(MySqlDbType.Text)> Public Property url As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `xref_ko2cog` (`ko`, `COG`, `url`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `xref_ko2cog` (`ko`, `COG`, `url`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `xref_ko2cog` WHERE `ko`='{0}' and `COG`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `xref_ko2cog` SET `uid`='{0}', `ko`='{1}', `COG`='{2}', `url`='{3}' WHERE `ko`='{4}' and `COG`='{5}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `xref_ko2cog` WHERE `ko`='{0}' and `COG`='{1}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, ko, COG)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `xref_ko2cog` (`ko`, `COG`, `url`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, ko, COG, url)
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{ko}', '{COG}', '{url}', '{3}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `xref_ko2cog` (`ko`, `COG`, `url`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, ko, COG, url)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `xref_ko2cog` SET `uid`='{0}', `ko`='{1}', `COG`='{2}', `url`='{3}' WHERE `ko`='{4}' and `COG`='{5}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, uid, ko, COG, url, ko, COG)
    End Function
#End Region
End Class


End Namespace
