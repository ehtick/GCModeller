REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 2.1.0.2569

REM  Dump @2018/5/23 13:13:37


Imports System.Data.Linq.Mapping
Imports System.Xml.Serialization
Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes
Imports MySqlScript = Oracle.LinuxCompatibility.MySQL.Scripting.Extensions

Namespace LocalMySQL

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `data_modules`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `data_modules` (
'''   `uid` int(11) NOT NULL,
'''   `KEGG` varchar(45) DEFAULT NULL,
'''   `name` varchar(45) DEFAULT NULL,
'''   `definition` varchar(45) DEFAULT NULL,
'''   `map` varchar(45) DEFAULT NULL COMMENT 'image -> gzip -> base64 string',
'''   PRIMARY KEY (`uid`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("data_modules", Database:="jp_kegg2", SchemaSQL:="
CREATE TABLE `data_modules` (
  `uid` int(11) NOT NULL,
  `KEGG` varchar(45) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `definition` varchar(45) DEFAULT NULL,
  `map` varchar(45) DEFAULT NULL COMMENT 'image -> gzip -> base64 string',
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;")>
Public Class data_modules: Inherits Oracle.LinuxCompatibility.MySQL.MySQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("uid"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "11"), Column(Name:="uid"), XmlAttribute> Public Property uid As Long
    <DatabaseField("KEGG"), DataType(MySqlDbType.VarChar, "45"), Column(Name:="KEGG")> Public Property KEGG As String
    <DatabaseField("name"), DataType(MySqlDbType.VarChar, "45"), Column(Name:="name")> Public Property name As String
    <DatabaseField("definition"), DataType(MySqlDbType.VarChar, "45"), Column(Name:="definition")> Public Property definition As String
''' <summary>
''' image -> gzip -> base64 string
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
    <DatabaseField("map"), DataType(MySqlDbType.VarChar, "45"), Column(Name:="map")> Public Property map As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Friend Shared ReadOnly INSERT_SQL$ = 
        <SQL>INSERT INTO `data_modules` (`uid`, `KEGG`, `name`, `definition`, `map`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>

    Friend Shared ReadOnly INSERT_AI_SQL$ = 
        <SQL>INSERT INTO `data_modules` (`uid`, `KEGG`, `name`, `definition`, `map`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>

    Friend Shared ReadOnly REPLACE_SQL$ = 
        <SQL>REPLACE INTO `data_modules` (`uid`, `KEGG`, `name`, `definition`, `map`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>

    Friend Shared ReadOnly REPLACE_AI_SQL$ = 
        <SQL>REPLACE INTO `data_modules` (`uid`, `KEGG`, `name`, `definition`, `map`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>

    Friend Shared ReadOnly DELETE_SQL$ =
        <SQL>DELETE FROM `data_modules` WHERE `uid` = '{0}';</SQL>

    Friend Shared ReadOnly UPDATE_SQL$ = 
        <SQL>UPDATE `data_modules` SET `uid`='{0}', `KEGG`='{1}', `name`='{2}', `definition`='{3}', `map`='{4}' WHERE `uid` = '{5}';</SQL>

#End Region

''' <summary>
''' ```SQL
''' DELETE FROM `data_modules` WHERE `uid` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, uid)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `data_modules` (`uid`, `KEGG`, `name`, `definition`, `map`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, uid, KEGG, name, definition, map)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `data_modules` (`uid`, `KEGG`, `name`, `definition`, `map`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(INSERT_AI_SQL, uid, KEGG, name, definition, map)
        Else
        Return String.Format(INSERT_SQL, uid, KEGG, name, definition, map)
        End If
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue(AI As Boolean) As String
        If AI Then
            Return $"('{uid}', '{KEGG}', '{name}', '{definition}', '{map}')"
        Else
            Return $"('{uid}', '{KEGG}', '{name}', '{definition}', '{map}')"
        End If
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `data_modules` (`uid`, `KEGG`, `name`, `definition`, `map`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, uid, KEGG, name, definition, map)
    End Function

''' <summary>
''' ```SQL
''' REPLACE INTO `data_modules` (`uid`, `KEGG`, `name`, `definition`, `map`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(REPLACE_AI_SQL, uid, KEGG, name, definition, map)
        Else
        Return String.Format(REPLACE_SQL, uid, KEGG, name, definition, map)
        End If
    End Function

''' <summary>
''' ```SQL
''' UPDATE `data_modules` SET `uid`='{0}', `KEGG`='{1}', `name`='{2}', `definition`='{3}', `map`='{4}' WHERE `uid` = '{5}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, uid, KEGG, name, definition, map, uid)
    End Function
#End Region

''' <summary>
                     ''' Memberwise clone of current table Object.
                     ''' </summary>
                     Public Function Clone() As data_modules
                         Return DirectCast(MyClass.MemberwiseClone, data_modules)
                     End Function
End Class


End Namespace
