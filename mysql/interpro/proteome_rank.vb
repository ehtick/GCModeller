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
''' DROP TABLE IF EXISTS `proteome_rank`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `proteome_rank` (
'''   `entry_ac` varchar(9) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `oscode` varchar(5) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `rank` int(7) NOT NULL,
'''   PRIMARY KEY (`entry_ac`,`oscode`),
'''   KEY `fk_proteome_rank$oscode` (`oscode`),
'''   CONSTRAINT `fk_proteome_rank$entry` FOREIGN KEY (`entry_ac`) REFERENCES `entry` (`entry_ac`) ON DELETE CASCADE ON UPDATE NO ACTION,
'''   CONSTRAINT `fk_proteome_rank$oscode` FOREIGN KEY (`oscode`) REFERENCES `organism` (`oscode`) ON DELETE CASCADE ON UPDATE NO ACTION
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("proteome_rank", Database:="interpro", SchemaSQL:="
CREATE TABLE `proteome_rank` (
  `entry_ac` varchar(9) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `oscode` varchar(5) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `rank` int(7) NOT NULL,
  PRIMARY KEY (`entry_ac`,`oscode`),
  KEY `fk_proteome_rank$oscode` (`oscode`),
  CONSTRAINT `fk_proteome_rank$entry` FOREIGN KEY (`entry_ac`) REFERENCES `entry` (`entry_ac`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `fk_proteome_rank$oscode` FOREIGN KEY (`oscode`) REFERENCES `organism` (`oscode`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;")>
Public Class proteome_rank: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("entry_ac"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "9")> Public Property entry_ac As String
    <DatabaseField("oscode"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "5")> Public Property oscode As String
    <DatabaseField("rank"), NotNull, DataType(MySqlDbType.Int64, "7")> Public Property rank As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `proteome_rank` (`entry_ac`, `oscode`, `rank`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `proteome_rank` (`entry_ac`, `oscode`, `rank`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `proteome_rank` WHERE `entry_ac`='{0}' and `oscode`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `proteome_rank` SET `entry_ac`='{0}', `oscode`='{1}', `rank`='{2}' WHERE `entry_ac`='{3}' and `oscode`='{4}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `proteome_rank` WHERE `entry_ac`='{0}' and `oscode`='{1}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, entry_ac, oscode)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `proteome_rank` (`entry_ac`, `oscode`, `rank`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, entry_ac, oscode, rank)
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{entry_ac}', '{oscode}', '{rank}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `proteome_rank` (`entry_ac`, `oscode`, `rank`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, entry_ac, oscode, rank)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `proteome_rank` SET `entry_ac`='{0}', `oscode`='{1}', `rank`='{2}' WHERE `entry_ac`='{3}' and `oscode`='{4}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, entry_ac, oscode, rank, entry_ac, oscode)
    End Function
#End Region
End Class


End Namespace
