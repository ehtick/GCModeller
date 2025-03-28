﻿#Region "Microsoft.VisualBasic::1ecab8ac3cb9960f01088069d6b64bf9, data\RegulonDatabase\RegulonDB\MySQL\regulon_tmp.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xie (genetics@smrucc.org)
    '       xieguigang (xie.guigang@live.com)
    ' 
    ' Copyright (c) 2018 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.



    ' /********************************************************************************/

    ' Summaries:


    ' Code Statistics:

    '   Total Lines: 152
    '    Code Lines: 75 (49.34%)
    ' Comment Lines: 55 (36.18%)
    '    - Xml Docs: 94.55%
    ' 
    '   Blank Lines: 22 (14.47%)
    '     File Size: 5.83 KB


    ' Class regulon_tmp
    ' 
    '     Properties: key_id_org, regulon_id, regulon_name
    ' 
    '     Function: Clone, GetDeleteSQL, GetDumpInsertValue, (+2 Overloads) GetInsertSQL, (+2 Overloads) GetReplaceSQL
    '               GetUpdateSQL
    ' 
    ' 
    ' /********************************************************************************/

#End Region

REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 2.1.0.2569

REM  Dump @2018/5/23 13:13:36


Imports System.Data.Linq.Mapping
Imports System.Xml.Serialization
Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes
Imports MySqlScript = Oracle.LinuxCompatibility.MySQL.Scripting.Extensions

Namespace RegulonDB.Tables

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `regulon_tmp`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `regulon_tmp` (
'''   `regulon_id` char(12) NOT NULL,
'''   `regulon_name` varchar(500) NOT NULL,
'''   `key_id_org` varchar(5) NOT NULL
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("regulon_tmp", Database:="regulondb_93", SchemaSQL:="
CREATE TABLE `regulon_tmp` (
  `regulon_id` char(12) NOT NULL,
  `regulon_name` varchar(500) NOT NULL,
  `key_id_org` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;")>
Public Class regulon_tmp: Inherits Oracle.LinuxCompatibility.MySQL.MySQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("regulon_id"), NotNull, DataType(MySqlDbType.VarChar, "12"), Column(Name:="regulon_id")> Public Property regulon_id As String
    <DatabaseField("regulon_name"), NotNull, DataType(MySqlDbType.VarChar, "500"), Column(Name:="regulon_name")> Public Property regulon_name As String
    <DatabaseField("key_id_org"), NotNull, DataType(MySqlDbType.VarChar, "5"), Column(Name:="key_id_org")> Public Property key_id_org As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Friend Shared ReadOnly INSERT_SQL$ = 
        <SQL>INSERT INTO `regulon_tmp` (`regulon_id`, `regulon_name`, `key_id_org`) VALUES ('{0}', '{1}', '{2}');</SQL>

    Friend Shared ReadOnly INSERT_AI_SQL$ = 
        <SQL>INSERT INTO `regulon_tmp` (`regulon_id`, `regulon_name`, `key_id_org`) VALUES ('{0}', '{1}', '{2}');</SQL>

    Friend Shared ReadOnly REPLACE_SQL$ = 
        <SQL>REPLACE INTO `regulon_tmp` (`regulon_id`, `regulon_name`, `key_id_org`) VALUES ('{0}', '{1}', '{2}');</SQL>

    Friend Shared ReadOnly REPLACE_AI_SQL$ = 
        <SQL>REPLACE INTO `regulon_tmp` (`regulon_id`, `regulon_name`, `key_id_org`) VALUES ('{0}', '{1}', '{2}');</SQL>

    Friend Shared ReadOnly DELETE_SQL$ =
        <SQL>DELETE FROM `regulon_tmp` WHERE ;</SQL>

    Friend Shared ReadOnly UPDATE_SQL$ = 
        <SQL>UPDATE `regulon_tmp` SET `regulon_id`='{0}', `regulon_name`='{1}', `key_id_org`='{2}' WHERE ;</SQL>

#End Region

''' <summary>
''' ```SQL
''' DELETE FROM `regulon_tmp` WHERE ;
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Throw New NotImplementedException("Table key was Not found, unable To generate ___DELETE_SQL_Invoke automatically, please edit this Function manually!")
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `regulon_tmp` (`regulon_id`, `regulon_name`, `key_id_org`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, regulon_id, regulon_name, key_id_org)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `regulon_tmp` (`regulon_id`, `regulon_name`, `key_id_org`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(INSERT_AI_SQL, regulon_id, regulon_name, key_id_org)
        Else
        Return String.Format(INSERT_SQL, regulon_id, regulon_name, key_id_org)
        End If
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue(AI As Boolean) As String
        If AI Then
            Return $"('{regulon_id}', '{regulon_name}', '{key_id_org}')"
        Else
            Return $"('{regulon_id}', '{regulon_name}', '{key_id_org}')"
        End If
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `regulon_tmp` (`regulon_id`, `regulon_name`, `key_id_org`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, regulon_id, regulon_name, key_id_org)
    End Function

''' <summary>
''' ```SQL
''' REPLACE INTO `regulon_tmp` (`regulon_id`, `regulon_name`, `key_id_org`) VALUES ('{0}', '{1}', '{2}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(REPLACE_AI_SQL, regulon_id, regulon_name, key_id_org)
        Else
        Return String.Format(REPLACE_SQL, regulon_id, regulon_name, key_id_org)
        End If
    End Function

''' <summary>
''' ```SQL
''' UPDATE `regulon_tmp` SET `regulon_id`='{0}', `regulon_name`='{1}', `key_id_org`='{2}' WHERE ;
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Throw New NotImplementedException("Table key was Not found, unable To generate ___UPDATE_SQL_Invoke automatically, please edit this Function manually!")
    End Function
#End Region

''' <summary>
                     ''' Memberwise clone of current table Object.
                     ''' </summary>
                     Public Function Clone() As regulon_tmp
                         Return DirectCast(MyClass.MemberwiseClone, regulon_tmp)
                     End Function
End Class


End Namespace
