﻿#Region "Microsoft.VisualBasic::12acd10b67d3058c8afe21427f296c6b, data\RegulonDatabase\Regtransbase\MySQL\dict_exp_technique_types_superclasses.vb"

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

    '   Total Lines: 151
    '    Code Lines: 74 (49.01%)
    ' Comment Lines: 55 (36.42%)
    '    - Xml Docs: 94.55%
    ' 
    '   Blank Lines: 22 (14.57%)
    '     File Size: 6.42 KB


    ' Class dict_exp_technique_types_superclasses
    ' 
    '     Properties: exp_technique_type_superclass_guid, name
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

REM  Dump @2018/5/23 13:13:38


Imports System.Data.Linq.Mapping
Imports System.Xml.Serialization
Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes
Imports MySqlScript = Oracle.LinuxCompatibility.MySQL.Scripting.Extensions

Namespace Regtransbase.MySQL

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `dict_exp_technique_types_superclasses`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `dict_exp_technique_types_superclasses` (
'''   `exp_technique_type_superclass_guid` int(10) unsigned NOT NULL DEFAULT '0',
'''   `name` varchar(255) NOT NULL DEFAULT '',
'''   PRIMARY KEY (`exp_technique_type_superclass_guid`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("dict_exp_technique_types_superclasses", Database:="dbregulation_update", SchemaSQL:="
CREATE TABLE `dict_exp_technique_types_superclasses` (
  `exp_technique_type_superclass_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `name` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`exp_technique_type_superclass_guid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;")>
Public Class dict_exp_technique_types_superclasses: Inherits Oracle.LinuxCompatibility.MySQL.MySQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("exp_technique_type_superclass_guid"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "10"), Column(Name:="exp_technique_type_superclass_guid"), XmlAttribute> Public Property exp_technique_type_superclass_guid As Long
    <DatabaseField("name"), NotNull, DataType(MySqlDbType.VarChar, "255"), Column(Name:="name")> Public Property name As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Friend Shared ReadOnly INSERT_SQL$ = 
        <SQL>INSERT INTO `dict_exp_technique_types_superclasses` (`exp_technique_type_superclass_guid`, `name`) VALUES ('{0}', '{1}');</SQL>

    Friend Shared ReadOnly INSERT_AI_SQL$ = 
        <SQL>INSERT INTO `dict_exp_technique_types_superclasses` (`exp_technique_type_superclass_guid`, `name`) VALUES ('{0}', '{1}');</SQL>

    Friend Shared ReadOnly REPLACE_SQL$ = 
        <SQL>REPLACE INTO `dict_exp_technique_types_superclasses` (`exp_technique_type_superclass_guid`, `name`) VALUES ('{0}', '{1}');</SQL>

    Friend Shared ReadOnly REPLACE_AI_SQL$ = 
        <SQL>REPLACE INTO `dict_exp_technique_types_superclasses` (`exp_technique_type_superclass_guid`, `name`) VALUES ('{0}', '{1}');</SQL>

    Friend Shared ReadOnly DELETE_SQL$ =
        <SQL>DELETE FROM `dict_exp_technique_types_superclasses` WHERE `exp_technique_type_superclass_guid` = '{0}';</SQL>

    Friend Shared ReadOnly UPDATE_SQL$ = 
        <SQL>UPDATE `dict_exp_technique_types_superclasses` SET `exp_technique_type_superclass_guid`='{0}', `name`='{1}' WHERE `exp_technique_type_superclass_guid` = '{2}';</SQL>

#End Region

''' <summary>
''' ```SQL
''' DELETE FROM `dict_exp_technique_types_superclasses` WHERE `exp_technique_type_superclass_guid` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, exp_technique_type_superclass_guid)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `dict_exp_technique_types_superclasses` (`exp_technique_type_superclass_guid`, `name`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, exp_technique_type_superclass_guid, name)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `dict_exp_technique_types_superclasses` (`exp_technique_type_superclass_guid`, `name`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(INSERT_AI_SQL, exp_technique_type_superclass_guid, name)
        Else
        Return String.Format(INSERT_SQL, exp_technique_type_superclass_guid, name)
        End If
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue(AI As Boolean) As String
        If AI Then
            Return $"('{exp_technique_type_superclass_guid}', '{name}')"
        Else
            Return $"('{exp_technique_type_superclass_guid}', '{name}')"
        End If
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `dict_exp_technique_types_superclasses` (`exp_technique_type_superclass_guid`, `name`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, exp_technique_type_superclass_guid, name)
    End Function

''' <summary>
''' ```SQL
''' REPLACE INTO `dict_exp_technique_types_superclasses` (`exp_technique_type_superclass_guid`, `name`) VALUES ('{0}', '{1}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(REPLACE_AI_SQL, exp_technique_type_superclass_guid, name)
        Else
        Return String.Format(REPLACE_SQL, exp_technique_type_superclass_guid, name)
        End If
    End Function

''' <summary>
''' ```SQL
''' UPDATE `dict_exp_technique_types_superclasses` SET `exp_technique_type_superclass_guid`='{0}', `name`='{1}' WHERE `exp_technique_type_superclass_guid` = '{2}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, exp_technique_type_superclass_guid, name, exp_technique_type_superclass_guid)
    End Function
#End Region

''' <summary>
                     ''' Memberwise clone of current table Object.
                     ''' </summary>
                     Public Function Clone() As dict_exp_technique_types_superclasses
                         Return DirectCast(MyClass.MemberwiseClone, dict_exp_technique_types_superclasses)
                     End Function
End Class


End Namespace
