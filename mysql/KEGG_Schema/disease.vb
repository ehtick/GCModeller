REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 1.0.0.0

REM  Dump @3/29/2017 10:06:32 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace LocalMySQL

''' <summary>
''' ```SQL
''' KEGG上面的疾病的定义的数据表
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `disease`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `disease` (
'''   `entry_id` varchar(45) NOT NULL,
'''   `definition` longtext,
'''   `guid` int(11) NOT NULL AUTO_INCREMENT,
'''   PRIMARY KEY (`entry_id`),
'''   UNIQUE KEY `guid_UNIQUE` (`guid`),
'''   UNIQUE KEY `entry_id_UNIQUE` (`entry_id`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='KEGG上面的疾病的定义的数据表';
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("disease", Database:="jp_kegg2", SchemaSQL:="
CREATE TABLE `disease` (
  `entry_id` varchar(45) NOT NULL,
  `definition` longtext,
  `guid` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`entry_id`),
  UNIQUE KEY `guid_UNIQUE` (`guid`),
  UNIQUE KEY `entry_id_UNIQUE` (`entry_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='KEGG上面的疾病的定义的数据表';")>
Public Class disease: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("entry_id"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "45")> Public Property entry_id As String
    <DatabaseField("definition"), DataType(MySqlDbType.Text)> Public Property definition As String
    <DatabaseField("guid"), AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property guid As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `disease` (`entry_id`, `definition`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `disease` (`entry_id`, `definition`) VALUES ('{0}', '{1}', '{2}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `disease` WHERE `entry_id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `disease` SET `entry_id`='{0}', `definition`='{1}', `guid`='{2}' WHERE `entry_id` = '{3}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `disease` WHERE `entry_id` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, entry_id)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `disease` (`entry_id`, `definition`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, entry_id, definition)
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{entry_id}', '{definition}', '{2}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `disease` (`entry_id`, `definition`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, entry_id, definition)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `disease` SET `entry_id`='{0}', `definition`='{1}', `guid`='{2}' WHERE `entry_id` = '{3}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, entry_id, definition, guid, entry_id)
    End Function
#End Region
End Class


End Namespace
