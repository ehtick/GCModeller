﻿#Region "Microsoft.VisualBasic::c1a01203704949d6d9240932e3de9418, data\ExternalDBSource\MetaCyc\MySQL\gene.vb"

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

    ' Class gene
    ' 
    '     Properties: CodingRegionEnd, CodingRegionEndApproximate, CodingRegionStart, CodingRegionStartApproximate, DataSetWID
    '                 Direction, GenomeID, Interrupted, Name, NucleicAcidWID
    '                 SubsequenceWID, Type, WID
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
''' DROP TABLE IF EXISTS `gene`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `gene` (
'''   `WID` bigint(20) NOT NULL,
'''   `Name` varchar(255) DEFAULT NULL,
'''   `NucleicAcidWID` bigint(20) DEFAULT NULL,
'''   `SubsequenceWID` bigint(20) DEFAULT NULL,
'''   `Type` varchar(100) DEFAULT NULL,
'''   `GenomeID` varchar(35) DEFAULT NULL,
'''   `CodingRegionStart` int(11) DEFAULT NULL,
'''   `CodingRegionEnd` int(11) DEFAULT NULL,
'''   `CodingRegionStartApproximate` varchar(10) DEFAULT NULL,
'''   `CodingRegionEndApproximate` varchar(10) DEFAULT NULL,
'''   `Direction` varchar(25) DEFAULT NULL,
'''   `Interrupted` char(1) DEFAULT NULL,
'''   `DataSetWID` bigint(20) NOT NULL,
'''   PRIMARY KEY (`WID`),
'''   KEY `GENE_DATASETWID` (`DataSetWID`),
'''   KEY `FK_Gene1` (`NucleicAcidWID`),
'''   CONSTRAINT `FK_Gene1` FOREIGN KEY (`NucleicAcidWID`) REFERENCES `nucleicacid` (`WID`) ON DELETE CASCADE,
'''   CONSTRAINT `FK_Gene2` FOREIGN KEY (`DataSetWID`) REFERENCES `dataset` (`WID`) ON DELETE CASCADE
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("gene", Database:="warehouse", SchemaSQL:="
CREATE TABLE `gene` (
  `WID` bigint(20) NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `NucleicAcidWID` bigint(20) DEFAULT NULL,
  `SubsequenceWID` bigint(20) DEFAULT NULL,
  `Type` varchar(100) DEFAULT NULL,
  `GenomeID` varchar(35) DEFAULT NULL,
  `CodingRegionStart` int(11) DEFAULT NULL,
  `CodingRegionEnd` int(11) DEFAULT NULL,
  `CodingRegionStartApproximate` varchar(10) DEFAULT NULL,
  `CodingRegionEndApproximate` varchar(10) DEFAULT NULL,
  `Direction` varchar(25) DEFAULT NULL,
  `Interrupted` char(1) DEFAULT NULL,
  `DataSetWID` bigint(20) NOT NULL,
  PRIMARY KEY (`WID`),
  KEY `GENE_DATASETWID` (`DataSetWID`),
  KEY `FK_Gene1` (`NucleicAcidWID`),
  CONSTRAINT `FK_Gene1` FOREIGN KEY (`NucleicAcidWID`) REFERENCES `nucleicacid` (`WID`) ON DELETE CASCADE,
  CONSTRAINT `FK_Gene2` FOREIGN KEY (`DataSetWID`) REFERENCES `dataset` (`WID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;")>
Public Class gene: Inherits Oracle.LinuxCompatibility.MySQL.MySQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("WID"), PrimaryKey, NotNull, DataType(MySqlDbType.Int64, "20"), Column(Name:="WID"), XmlAttribute> Public Property WID As Long
    <DatabaseField("Name"), DataType(MySqlDbType.VarChar, "255"), Column(Name:="Name")> Public Property Name As String
    <DatabaseField("NucleicAcidWID"), DataType(MySqlDbType.Int64, "20"), Column(Name:="NucleicAcidWID")> Public Property NucleicAcidWID As Long
    <DatabaseField("SubsequenceWID"), DataType(MySqlDbType.Int64, "20"), Column(Name:="SubsequenceWID")> Public Property SubsequenceWID As Long
    <DatabaseField("Type"), DataType(MySqlDbType.VarChar, "100"), Column(Name:="Type")> Public Property Type As String
    <DatabaseField("GenomeID"), DataType(MySqlDbType.VarChar, "35"), Column(Name:="GenomeID")> Public Property GenomeID As String
    <DatabaseField("CodingRegionStart"), DataType(MySqlDbType.Int64, "11"), Column(Name:="CodingRegionStart")> Public Property CodingRegionStart As Long
    <DatabaseField("CodingRegionEnd"), DataType(MySqlDbType.Int64, "11"), Column(Name:="CodingRegionEnd")> Public Property CodingRegionEnd As Long
    <DatabaseField("CodingRegionStartApproximate"), DataType(MySqlDbType.VarChar, "10"), Column(Name:="CodingRegionStartApproximate")> Public Property CodingRegionStartApproximate As String
    <DatabaseField("CodingRegionEndApproximate"), DataType(MySqlDbType.VarChar, "10"), Column(Name:="CodingRegionEndApproximate")> Public Property CodingRegionEndApproximate As String
    <DatabaseField("Direction"), DataType(MySqlDbType.VarChar, "25"), Column(Name:="Direction")> Public Property Direction As String
    <DatabaseField("Interrupted"), DataType(MySqlDbType.VarChar, "1"), Column(Name:="Interrupted")> Public Property Interrupted As String
    <DatabaseField("DataSetWID"), NotNull, DataType(MySqlDbType.Int64, "20"), Column(Name:="DataSetWID")> Public Property DataSetWID As Long
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Friend Shared ReadOnly INSERT_SQL$ = 
        <SQL>INSERT INTO `gene` (`WID`, `Name`, `NucleicAcidWID`, `SubsequenceWID`, `Type`, `GenomeID`, `CodingRegionStart`, `CodingRegionEnd`, `CodingRegionStartApproximate`, `CodingRegionEndApproximate`, `Direction`, `Interrupted`, `DataSetWID`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');</SQL>

    Friend Shared ReadOnly INSERT_AI_SQL$ = 
        <SQL>INSERT INTO `gene` (`WID`, `Name`, `NucleicAcidWID`, `SubsequenceWID`, `Type`, `GenomeID`, `CodingRegionStart`, `CodingRegionEnd`, `CodingRegionStartApproximate`, `CodingRegionEndApproximate`, `Direction`, `Interrupted`, `DataSetWID`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');</SQL>

    Friend Shared ReadOnly REPLACE_SQL$ = 
        <SQL>REPLACE INTO `gene` (`WID`, `Name`, `NucleicAcidWID`, `SubsequenceWID`, `Type`, `GenomeID`, `CodingRegionStart`, `CodingRegionEnd`, `CodingRegionStartApproximate`, `CodingRegionEndApproximate`, `Direction`, `Interrupted`, `DataSetWID`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');</SQL>

    Friend Shared ReadOnly REPLACE_AI_SQL$ = 
        <SQL>REPLACE INTO `gene` (`WID`, `Name`, `NucleicAcidWID`, `SubsequenceWID`, `Type`, `GenomeID`, `CodingRegionStart`, `CodingRegionEnd`, `CodingRegionStartApproximate`, `CodingRegionEndApproximate`, `Direction`, `Interrupted`, `DataSetWID`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');</SQL>

    Friend Shared ReadOnly DELETE_SQL$ =
        <SQL>DELETE FROM `gene` WHERE `WID` = '{0}';</SQL>

    Friend Shared ReadOnly UPDATE_SQL$ = 
        <SQL>UPDATE `gene` SET `WID`='{0}', `Name`='{1}', `NucleicAcidWID`='{2}', `SubsequenceWID`='{3}', `Type`='{4}', `GenomeID`='{5}', `CodingRegionStart`='{6}', `CodingRegionEnd`='{7}', `CodingRegionStartApproximate`='{8}', `CodingRegionEndApproximate`='{9}', `Direction`='{10}', `Interrupted`='{11}', `DataSetWID`='{12}' WHERE `WID` = '{13}';</SQL>

#End Region

''' <summary>
''' ```SQL
''' DELETE FROM `gene` WHERE `WID` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, WID)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `gene` (`WID`, `Name`, `NucleicAcidWID`, `SubsequenceWID`, `Type`, `GenomeID`, `CodingRegionStart`, `CodingRegionEnd`, `CodingRegionStartApproximate`, `CodingRegionEndApproximate`, `Direction`, `Interrupted`, `DataSetWID`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, WID, Name, NucleicAcidWID, SubsequenceWID, Type, GenomeID, CodingRegionStart, CodingRegionEnd, CodingRegionStartApproximate, CodingRegionEndApproximate, Direction, Interrupted, DataSetWID)
    End Function

''' <summary>
''' ```SQL
''' INSERT INTO `gene` (`WID`, `Name`, `NucleicAcidWID`, `SubsequenceWID`, `Type`, `GenomeID`, `CodingRegionStart`, `CodingRegionEnd`, `CodingRegionStartApproximate`, `CodingRegionEndApproximate`, `Direction`, `Interrupted`, `DataSetWID`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(INSERT_AI_SQL, WID, Name, NucleicAcidWID, SubsequenceWID, Type, GenomeID, CodingRegionStart, CodingRegionEnd, CodingRegionStartApproximate, CodingRegionEndApproximate, Direction, Interrupted, DataSetWID)
        Else
        Return String.Format(INSERT_SQL, WID, Name, NucleicAcidWID, SubsequenceWID, Type, GenomeID, CodingRegionStart, CodingRegionEnd, CodingRegionStartApproximate, CodingRegionEndApproximate, Direction, Interrupted, DataSetWID)
        End If
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue(AI As Boolean) As String
        If AI Then
            Return $"('{WID}', '{Name}', '{NucleicAcidWID}', '{SubsequenceWID}', '{Type}', '{GenomeID}', '{CodingRegionStart}', '{CodingRegionEnd}', '{CodingRegionStartApproximate}', '{CodingRegionEndApproximate}', '{Direction}', '{Interrupted}', '{DataSetWID}')"
        Else
            Return $"('{WID}', '{Name}', '{NucleicAcidWID}', '{SubsequenceWID}', '{Type}', '{GenomeID}', '{CodingRegionStart}', '{CodingRegionEnd}', '{CodingRegionStartApproximate}', '{CodingRegionEndApproximate}', '{Direction}', '{Interrupted}', '{DataSetWID}')"
        End If
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `gene` (`WID`, `Name`, `NucleicAcidWID`, `SubsequenceWID`, `Type`, `GenomeID`, `CodingRegionStart`, `CodingRegionEnd`, `CodingRegionStartApproximate`, `CodingRegionEndApproximate`, `Direction`, `Interrupted`, `DataSetWID`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, WID, Name, NucleicAcidWID, SubsequenceWID, Type, GenomeID, CodingRegionStart, CodingRegionEnd, CodingRegionStartApproximate, CodingRegionEndApproximate, Direction, Interrupted, DataSetWID)
    End Function

''' <summary>
''' ```SQL
''' REPLACE INTO `gene` (`WID`, `Name`, `NucleicAcidWID`, `SubsequenceWID`, `Type`, `GenomeID`, `CodingRegionStart`, `CodingRegionEnd`, `CodingRegionStartApproximate`, `CodingRegionEndApproximate`, `Direction`, `Interrupted`, `DataSetWID`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL(AI As Boolean) As String
        If AI Then
        Return String.Format(REPLACE_AI_SQL, WID, Name, NucleicAcidWID, SubsequenceWID, Type, GenomeID, CodingRegionStart, CodingRegionEnd, CodingRegionStartApproximate, CodingRegionEndApproximate, Direction, Interrupted, DataSetWID)
        Else
        Return String.Format(REPLACE_SQL, WID, Name, NucleicAcidWID, SubsequenceWID, Type, GenomeID, CodingRegionStart, CodingRegionEnd, CodingRegionStartApproximate, CodingRegionEndApproximate, Direction, Interrupted, DataSetWID)
        End If
    End Function

''' <summary>
''' ```SQL
''' UPDATE `gene` SET `WID`='{0}', `Name`='{1}', `NucleicAcidWID`='{2}', `SubsequenceWID`='{3}', `Type`='{4}', `GenomeID`='{5}', `CodingRegionStart`='{6}', `CodingRegionEnd`='{7}', `CodingRegionStartApproximate`='{8}', `CodingRegionEndApproximate`='{9}', `Direction`='{10}', `Interrupted`='{11}', `DataSetWID`='{12}' WHERE `WID` = '{13}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, WID, Name, NucleicAcidWID, SubsequenceWID, Type, GenomeID, CodingRegionStart, CodingRegionEnd, CodingRegionStartApproximate, CodingRegionEndApproximate, Direction, Interrupted, DataSetWID, WID)
    End Function
#End Region

''' <summary>
                     ''' Memberwise clone of current table Object.
                     ''' </summary>
                     Public Function Clone() As gene
                         Return DirectCast(MyClass.MemberwiseClone, gene)
                     End Function
End Class


End Namespace
