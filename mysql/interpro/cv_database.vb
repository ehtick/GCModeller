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
''' DROP TABLE IF EXISTS `cv_database`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `cv_database` (
'''   `dbcode` char(1) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `dbname` varchar(20) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   `dborder` int(5) NOT NULL,
'''   `dbshort` varchar(10) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
'''   PRIMARY KEY (`dbcode`),
'''   UNIQUE KEY `uq_cv_database$database` (`dbname`),
'''   UNIQUE KEY `uq_cv_database$dborder` (`dborder`),
'''   UNIQUE KEY `uq_cv_database$dbshort` (`dbshort`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' 
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("cv_database", Database:="interpro", SchemaSQL:="
CREATE TABLE `cv_database` (
  `dbcode` char(1) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `dbname` varchar(20) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `dborder` int(5) NOT NULL,
  `dbshort` varchar(10) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  PRIMARY KEY (`dbcode`),
  UNIQUE KEY `uq_cv_database$database` (`dbname`),
  UNIQUE KEY `uq_cv_database$dborder` (`dborder`),
  UNIQUE KEY `uq_cv_database$dbshort` (`dbshort`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;")>
Public Class cv_database: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("dbcode"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "1")> Public Property dbcode As String
    <DatabaseField("dbname"), NotNull, DataType(MySqlDbType.VarChar, "20")> Public Property dbname As String
    <DatabaseField("dborder"), NotNull, DataType(MySqlDbType.Int64, "5")> Public Property dborder As Long
    <DatabaseField("dbshort"), NotNull, DataType(MySqlDbType.VarChar, "10")> Public Property dbshort As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `cv_database` (`dbcode`, `dbname`, `dborder`, `dbshort`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `cv_database` (`dbcode`, `dbname`, `dborder`, `dbshort`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `cv_database` WHERE `dbcode` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `cv_database` SET `dbcode`='{0}', `dbname`='{1}', `dborder`='{2}', `dbshort`='{3}' WHERE `dbcode` = '{4}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `cv_database` WHERE `dbcode` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, dbcode)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `cv_database` (`dbcode`, `dbname`, `dborder`, `dbshort`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, dbcode, dbname, dborder, dbshort)
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{dbcode}', '{dbname}', '{dborder}', '{dbshort}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `cv_database` (`dbcode`, `dbname`, `dborder`, `dbshort`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, dbcode, dbname, dborder, dbshort)
    End Function
''' <summary>
''' ```SQL
''' UPDATE `cv_database` SET `dbcode`='{0}', `dbname`='{1}', `dborder`='{2}', `dbshort`='{3}' WHERE `dbcode` = '{4}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, dbcode, dbname, dborder, dbshort, dbcode)
    End Function
#End Region
End Class


End Namespace
