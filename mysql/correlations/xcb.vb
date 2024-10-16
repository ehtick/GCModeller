REM  Oracle.LinuxCompatibility.MySQL.CodeGenerator
REM  Microsoft VisualBasic MYSQL




Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace MySQL.Tables

''' <summary>
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `xcb`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `xcb` (
'''   `uid` int(11) NOT NULL AUTO_INCREMENT,
'''   `g1_entity` varchar(45) NOT NULL,
'''   `g2_entity` varchar(45) NOT NULL,
'''   `pcc` double DEFAULT '0',
'''   `spcc` double DEFAULT '0',
'''   `wgcna_weight` double DEFAULT '0',
'''   PRIMARY KEY (`g1_entity`,`g2_entity`),
'''   UNIQUE KEY `uid_UNIQUE` (`uid`)
''' ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' /*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
''' 
''' /*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
''' /*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
''' /*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
''' /*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
''' /*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
''' /*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
''' /*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
''' 
''' -- Dump completed on 2015-11-06  3:46:31
''' 
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("xcb", Database:="correlations")>
Public Class xcb: Inherits Oracle.LinuxCompatibility.MySQL.SQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("uid"), AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11")> Public Property uid As Long
    <DatabaseField("g1_entity"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "45")> Public Property g1_entity As String
    <DatabaseField("g2_entity"), PrimaryKey, NotNull, DataType(MySqlDbType.VarChar, "45")> Public Property g2_entity As String
    <DatabaseField("pcc"), DataType(MySqlDbType.Double)> Public Property pcc As Double
    <DatabaseField("spcc"), DataType(MySqlDbType.Double)> Public Property spcc As Double
    <DatabaseField("wgcna_weight"), DataType(MySqlDbType.Double)> Public Property wgcna_weight As Double
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `xcb` (`g1_entity`, `g2_entity`, `pcc`, `spcc`, `wgcna_weight`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `xcb` WHERE `g1_entity`='{0}' and `g2_entity`='{1}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `xcb` SET `uid`='{0}', `g1_entity`='{1}', `g2_entity`='{2}', `pcc`='{3}', `spcc`='{4}', `wgcna_weight`='{5}' WHERE `g1_entity`='{6}' and `g2_entity`='{7}';</SQL>
#End Region
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, g1_entity, g2_entity)
    End Function
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, g1_entity, g2_entity, pcc, spcc, wgcna_weight)
    End Function
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, uid, g1_entity, g2_entity, pcc, spcc, wgcna_weight, g1_entity, g2_entity)
    End Function
#End Region
End Class


End Namespace
