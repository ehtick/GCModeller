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
''' DROP TABLE IF EXISTS `pfam_clan_data`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `pfam_clan_data` (
'''   `clan_id` varchar(15) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `name` varchar(50) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `description` varchar(75) CHARACTER SET latin1 COLLATE latin1_bin DEFAULT NULL,
'''   PRIMARY KEY (`clan_id`,`name`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("pfam_clan_data", Database:="interpro", SchemaSQL:="
CREATE TABLE `pfam_clan_data` (
  `clan_id` varchar(15) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `name` varchar(50) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `description` varchar(75) CHARACTER SET latin1 COLLATE latin1_bin DEFAULT NULL,
  PRIMARY KEY (`clan_id`,`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;")>
Public Class pfam_clan_data: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("clan_id"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "15")> Public Property clan_id As String
    <DatabaseField("name"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "50")> Public Property name As String
    <DatabaseField("description"), DataType(MySqlDbType.VarChar, "75")> Public Property description As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `pfam_clan_data` (`clan_id`, `name`, `description`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `pfam_clan_data` (`clan_id`, `name`, `description`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `pfam_clan_data` WHERE `clan_id`='{0}' and `name`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `pfam_clan_data` SET `clan_id`='{0}', `name`='{1}', `description`='{2}' WHERE `clan_id`='{3}' and `name`='{4}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `pfam_clan_data` WHERE `clan_id`='{0}' and `name`='{1}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, clan_id, name)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `pfam_clan_data` (`clan_id`, `name`, `description`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, clan_id, name, description)
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{clan_id}', '{name}', '{description}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `pfam_clan_data` (`clan_id`, `name`, `description`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, clan_id, name, description)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `pfam_clan_data` SET `clan_id`='{0}', `name`='{1}', `description`='{2}' WHERE `clan_id`='{3}' and `name`='{4}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, clan_id, name, description, clan_id, name)
    End Function
#End Region
End Class


End Namespace
