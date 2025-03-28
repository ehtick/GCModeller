﻿#Region "Microsoft.VisualBasic::35b047773b27ca3d5852f380848758dc, data\ExternalDBSource\MetaCyc\MySQL\pathwayreaction.vb"

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

    ' Class pathwayreaction
    ' 
    '     Properties: Hypothetical, PathwayWID, PriorReactionWID, ReactionWID
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

REM  Dump @2018/5/23 13:13:40


Imports System.Data.Linq.Mapping
Imports System.Xml.Serialization
Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes
Imports MySqlScript = Oracle.LinuxCompatibility.MySQL.Scripting.Extensions

Namespace MetaCyc.MySQL

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `pathwayreaction`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `pathwayreaction` (
'''   `PathwayWID` bigint(20) NOT NULL,
'''   `ReactionWID` bigint(20) NOT NULL,
'''   `PriorReactionWID` bigint(20) DEFAULT NULL,
'''   `Hypothetical` char(1) NOT NULL,
'''   KEY `PR_PATHWID_REACTIONWID` (`PathwayWID`,`ReactionWID`),
'''   KEY `FK_PathwayReaction3` (`PriorReactionWID`),
'''   CONSTRAINT `FK_PathwayReaction1` FOREIGN KEY (`PathwayWID`) REFERENCES `pathway` (`WID`) ON DELETE CASCADE,
'''   CONSTRAINT `FK_PathwayReaction3` FOREIGN KEY (`PriorReactionWID`) REFERENCES `reaction` (`WID`) ON DELETE CASCADE
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("pathwayreaction", Database:="warehouse", SchemaSQL:="
CREATE TABLE `pathwayreaction` (
  `PathwayWID` bigint(20) NOT NULL,
  `ReactionWID` bigint(20) NOT NULL,
  `PriorReactionWID` bigint(20) DEFAULT NULL,
  `Hypothetical` char(1) NOT NULL,
  KEY `PR_PATHWID_REACTIONWID` (`PathwayWID`,`ReactionWID`),
  KEY `FK_PathwayReaction3` (`PriorReactionWID`),
  CONSTRAINT `FK_PathwayReaction1` FOREIGN KEY (`PathwayWID`) REFERENCES `pathway` (`WID`) ON DELETE CASCADE,
  CONSTRAINT `FK_PathwayReaction3` FOREIGN KEY (`PriorReactionWID`) REFERENCES `reaction` (`WID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;")>
Public Class pathwayreaction: Inherits Oracle.LinuxCompatibility.MySQL.MySQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("PathwayWID"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "20"), Column(Name:="PathwayWID"), XmlAttribute> Public Property PathwayWID As Long
    <DatabaseField("ReactionWID"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "20"), Column(Name:="ReactionWID"), XmlAttribute> Public Property ReactionWID As Long
    <DatabaseField("PriorReactionWID"), DataType(MySqlDbType.Int64, "20"), Column(Name:="PriorReactionWID")> Public Property PriorReactionWID As Long
    <DatabaseField("Hypothetical"), NotNull, DataType(MySqlDbType.VarChar, "1"), Column(Name:="Hypothetical")> Public Property Hypothetical As String
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Friend Shared ReadOnly INSERT_SQL$ = 
        <SQL>INSERT INTO `pathwayreaction` (`PathwayWID`, `ReactionWID`, `PriorReactionWID`, `Hypothetical`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>

    Friend Shared ReadOnly INSERT_AI_SQL$ = 
        <SQL>INSERT INTO `pathwayreaction` (`PathwayWID`, `ReactionWID`, `PriorReactionWID`, `Hypothetical`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>

    Friend Shared ReadOnly REPLACE_SQL$ = 
        <SQL>REPLACE INTO `pathwayreaction` (`PathwayWID`, `ReactionWID`, `PriorReactionWID`, `Hypothetical`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>

    Friend Shared ReadOnly REPLACE_AI_SQL$ = 
        <SQL>REPLACE INTO `pathwayreaction` (`PathwayWID`, `ReactionWID`, `PriorReactionWID`, `Hypothetical`) VALUES ('{0}', '{1}', '{2}', '{3}');</SQL>

    Friend Shared ReadOnly DELETE_SQL$ =
        <SQL>DELETE FROM `pathwayreaction` WHERE `PathwayWID`='{0}' and `ReactionWID`='{1}';</SQL>

    Friend Shared ReadOnly UPDATE_SQL$ = 
        <SQL>UPDATE `pathwayreaction` SET `PathwayWID`='{0}', `ReactionWID`='{1}', `PriorReactionWID`='{2}', `Hypothetical`='{3}' WHERE `PathwayWID`='{4}' and `ReactionWID`='{5}';</SQL>

#End Region

''' <summary>
''' ```SQL
''' DELETE FROM `pathwayreaction` WHERE `PathwayWID`='{0}' and `ReactionWID`='{1}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, PathwayWID, ReactionWID)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `pathwayreaction` (`PathwayWID`, `ReactionWID`, `PriorReactionWID`, `Hypothetical`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, PathwayWID, ReactionWID, PriorReactionWID, Hypothetical)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `pathwayreaction` (`PathwayWID`, `ReactionWID`, `PriorReactionWID`, `Hypothetical`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(INSERT_AI_SQL, PathwayWID, ReactionWID, PriorReactionWID, Hypothetical)
        Else
        Return String.Format(INSERT_SQL, PathwayWID, ReactionWID, PriorReactionWID, Hypothetical)
        End If
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue(AI As Boolean) As String
        If AI Then
            Return $"('{PathwayWID}', '{ReactionWID}', '{PriorReactionWID}', '{Hypothetical}')"
        Else
            Return $"('{PathwayWID}', '{ReactionWID}', '{PriorReactionWID}', '{Hypothetical}')"
        End If
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `pathwayreaction` (`PathwayWID`, `ReactionWID`, `PriorReactionWID`, `Hypothetical`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, PathwayWID, ReactionWID, PriorReactionWID, Hypothetical)
    End Function

''' <summary>
''' ```SQL
''' REPLACE INTO `pathwayreaction` (`PathwayWID`, `ReactionWID`, `PriorReactionWID`, `Hypothetical`) VALUES ('{0}', '{1}', '{2}', '{3}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(REPLACE_AI_SQL, PathwayWID, ReactionWID, PriorReactionWID, Hypothetical)
        Else
        Return String.Format(REPLACE_SQL, PathwayWID, ReactionWID, PriorReactionWID, Hypothetical)
        End If
    End Function

''' <summary>
''' ```SQL
''' UPDATE `pathwayreaction` SET `PathwayWID`='{0}', `ReactionWID`='{1}', `PriorReactionWID`='{2}', `Hypothetical`='{3}' WHERE `PathwayWID`='{4}' and `ReactionWID`='{5}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, PathwayWID, ReactionWID, PriorReactionWID, Hypothetical, PathwayWID, ReactionWID)
    End Function
#End Region

''' <summary>
                     ''' Memberwise clone of current table Object.
                     ''' </summary>
                     Public Function Clone() As pathwayreaction
                         Return DirectCast(MyClass.MemberwiseClone, pathwayreaction)
                     End Function
End Class


End Namespace
