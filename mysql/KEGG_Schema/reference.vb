REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 1.0.0.0

REM  Dump @3/29/2017 10:06:32 PM


Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace LocalMySQL

''' <summary>
''' ```SQL
''' 参考文献数据表
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `reference`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `reference` (
'''   `authors` longtext,
'''   `title` longtext,
'''   `journal` longtext,
'''   `pmid` bigint(20) NOT NULL,
'''   PRIMARY KEY (`pmid`),
'''   UNIQUE KEY `pmid_UNIQUE` (`pmid`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='参考文献数据表';
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("reference", Database:="jp_kegg2", SchemaSQL:="
CREATE TABLE `reference` (
  `authors` longtext,
  `title` longtext,
  `journal` longtext,
  `pmid` bigint(20) NOT NULL,
  PRIMARY KEY (`pmid`),
  UNIQUE KEY `pmid_UNIQUE` (`pmid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='参考文献数据表';")>
Public Class reference: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("authors"), DataType(MySqlDbType.Text)> Public Property authors As String
    <DatabaseField("title"), DataType(MySqlDbType.Text)> Public Property title As String
    <DatabaseField("journal"), DataType(MySqlDbType.Text)> Public Property journal As String
    <DatabaseField("pmid"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "20")> Public Property pmid As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `reference` (`authors`, `title`, `journal`, `pmid`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `reference` (`authors`, `title`, `journal`, `pmid`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `reference` WHERE `pmid` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `reference` SET `authors`='{0}', `title`='{1}', `journal`='{2}', `pmid`='{3}' WHERE `pmid` = '{4}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `reference` WHERE `pmid` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, pmid)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `reference` (`authors`, `title`, `journal`, `pmid`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, authors, title, journal, pmid)
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{authors}', '{title}', '{journal}', '{pmid}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `reference` (`authors`, `title`, `journal`, `pmid`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, authors, title, journal, pmid)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `reference` SET `authors`='{0}', `title`='{1}', `journal`='{2}', `pmid`='{3}' WHERE `pmid` = '{4}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, authors, title, journal, pmid, pmid)
    End Function
#End Region
End Class


End Namespace
