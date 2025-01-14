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
''' DROP TABLE IF EXISTS `supermatch`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `supermatch` (
'''   `protein_ac` varchar(6) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `entry_ac` varchar(9) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `pos_from` int(5) NOT NULL,
'''   `pos_to` int(5) NOT NULL,
'''   PRIMARY KEY (`protein_ac`,`entry_ac`,`pos_to`,`pos_from`),
'''   KEY `fkv_supermatch$entry_ac` (`entry_ac`),
'''   CONSTRAINT `fkv_supermatch$entry_ac` FOREIGN KEY (`entry_ac`) REFERENCES `entry` (`entry_ac`) ON DELETE CASCADE ON UPDATE NO ACTION,
'''   CONSTRAINT `fkv_supermatch$protein_ac` FOREIGN KEY (`protein_ac`) REFERENCES `protein` (`protein_ac`) ON DELETE CASCADE ON UPDATE NO ACTION
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("supermatch", Database:="interpro", SchemaSQL:="
CREATE TABLE `supermatch` (
  `protein_ac` varchar(6) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `entry_ac` varchar(9) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `pos_from` int(5) NOT NULL,
  `pos_to` int(5) NOT NULL,
  PRIMARY KEY (`protein_ac`,`entry_ac`,`pos_to`,`pos_from`),
  KEY `fkv_supermatch$entry_ac` (`entry_ac`),
  CONSTRAINT `fkv_supermatch$entry_ac` FOREIGN KEY (`entry_ac`) REFERENCES `entry` (`entry_ac`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `fkv_supermatch$protein_ac` FOREIGN KEY (`protein_ac`) REFERENCES `protein` (`protein_ac`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;")>
Public Class supermatch: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("protein_ac"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "6")> Public Property protein_ac As String
    <DatabaseField("entry_ac"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "9")> Public Property entry_ac As String
    <DatabaseField("pos_from"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "5")> Public Property pos_from As Long
    <DatabaseField("pos_to"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "5")> Public Property pos_to As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `supermatch` (`protein_ac`, `entry_ac`, `pos_from`, `pos_to`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `supermatch` (`protein_ac`, `entry_ac`, `pos_from`, `pos_to`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `supermatch` WHERE `protein_ac`='{0}' and `entry_ac`='{1}' and `pos_to`='{2}' and `pos_from`='{3}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `supermatch` SET `protein_ac`='{0}', `entry_ac`='{1}', `pos_from`='{2}', `pos_to`='{3}' WHERE `protein_ac`='{4}' and `entry_ac`='{5}' and `pos_to`='{6}' and `pos_from`='{7}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `supermatch` WHERE `protein_ac`='{0}' and `entry_ac`='{1}' and `pos_to`='{2}' and `pos_from`='{3}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, protein_ac, entry_ac, pos_to, pos_from)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `supermatch` (`protein_ac`, `entry_ac`, `pos_from`, `pos_to`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, protein_ac, entry_ac, pos_from, pos_to)
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{protein_ac}', '{entry_ac}', '{pos_from}', '{pos_to}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `supermatch` (`protein_ac`, `entry_ac`, `pos_from`, `pos_to`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, protein_ac, entry_ac, pos_from, pos_to)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `supermatch` SET `protein_ac`='{0}', `entry_ac`='{1}', `pos_from`='{2}', `pos_to`='{3}' WHERE `protein_ac`='{4}' and `entry_ac`='{5}' and `pos_to`='{6}' and `pos_from`='{7}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, protein_ac, entry_ac, pos_from, pos_to, protein_ac, entry_ac, pos_to, pos_from)
    End Function
#End Region
End Class


End Namespace
