REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 1.0.0.0

REM  Dump @3/29/2017 8:48:52 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace Interpro.Tables

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `organism`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `organism` (
'''   `oscode` varchar(5) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `name` varchar(100) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `italics_name` varchar(100) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `full_name` varchar(100) CHARACTER SET latin1 COLLATE latin1_bin DEFAULT NULL,
'''   `tax_code` decimal(38,0) DEFAULT NULL,
'''   PRIMARY KEY (`oscode`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("organism", Database:="interpro", SchemaSQL:="
CREATE TABLE `organism` (
  `oscode` varchar(5) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `name` varchar(100) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `italics_name` varchar(100) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `full_name` varchar(100) CHARACTER SET latin1 COLLATE latin1_bin DEFAULT NULL,
  `tax_code` decimal(38,0) DEFAULT NULL,
  PRIMARY KEY (`oscode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;")>
Public Class organism: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("oscode"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "5")> Public Property oscode As String
    <DatabaseField("name"), NotNull, DataType(MySqlDbType.VarChar, "100")> Public Property name As String
    <DatabaseField("italics_name"), NotNull, DataType(MySqlDbType.VarChar, "100")> Public Property italics_name As String
    <DatabaseField("full_name"), DataType(MySqlDbType.VarChar, "100")> Public Property full_name As String
    <DatabaseField("tax_code"), DataType(MySqlDbType.Decimal)> Public Property tax_code As Decimal
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `organism` (`oscode`, `name`, `italics_name`, `full_name`, `tax_code`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `organism` (`oscode`, `name`, `italics_name`, `full_name`, `tax_code`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `organism` WHERE `oscode` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `organism` SET `oscode`='{0}', `name`='{1}', `italics_name`='{2}', `full_name`='{3}', `tax_code`='{4}' WHERE `oscode` = '{5}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `organism` WHERE `oscode` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, oscode)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `organism` (`oscode`, `name`, `italics_name`, `full_name`, `tax_code`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, oscode, name, italics_name, full_name, tax_code)
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{oscode}', '{name}', '{italics_name}', '{full_name}', '{tax_code}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `organism` (`oscode`, `name`, `italics_name`, `full_name`, `tax_code`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, oscode, name, italics_name, full_name, tax_code)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `organism` SET `oscode`='{0}', `name`='{1}', `italics_name`='{2}', `full_name`='{3}', `tax_code`='{4}' WHERE `oscode` = '{5}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, oscode, name, italics_name, full_name, tax_code, oscode)
    End Function
#End Region
End Class


End Namespace
