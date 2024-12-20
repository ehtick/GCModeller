﻿#Region "Microsoft.VisualBasic::90e9505f976210019e394a837f5a6c5c, core\Bio.Assembly\ComponentModel\Equations\EquationBuilder.vb"

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

    '   Total Lines: 185
    '    Code Lines: 135 (72.97%)
    ' Comment Lines: 20 (10.81%)
    '    - Xml Docs: 65.00%
    ' 
    '   Blank Lines: 30 (16.22%)
    '     File Size: 8.39 KB


    '     Module EquationBuilder
    ' 
    '         Function: __tryParse, (+2 Overloads) CreateObject, GetSides, MeasureDelimiter, (+4 Overloads) ToString
    ' 
    '         Sub: __appendSide, (+2 Overloads) AppendSides
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq

Namespace ComponentModel.EquaionModel

    Public Module EquationBuilder

        ''' <summary>
        ''' 可逆的代谢反应过程的箭头
        ''' </summary>
        Public Const EQUATION_DIRECTIONS_REVERSIBLE As String = "<=>"
        ''' <summary>
        ''' 不可逆的代谢反应过程的箭头
        ''' </summary>
        Public Const EQUATION_DIRECTIONS_INREVERSIBLE As String = " --> "
        Public Const EQUATION_DIRECTIONS_RIGHT_TO_LEFT As String = " <-- "
        Public Const EQUATION_SPECIES_CONNECTOR As String = " + "

        Private Function MeasureDelimiter(eq_str As String) As (direction As Integer, delimiter As String)
            If InStr(eq_str, EQUATION_DIRECTIONS_REVERSIBLE) > 0 Then
                Return (0, EQUATION_DIRECTIONS_REVERSIBLE)
            ElseIf InStr(eq_str, EQUATION_DIRECTIONS_INREVERSIBLE) > 0 Then
                Return (1, EQUATION_DIRECTIONS_INREVERSIBLE)
            ElseIf InStr(eq_str, " => ") > 0 Then
                Return (1, " => ")
            ElseIf InStr(eq_str, EQUATION_DIRECTIONS_RIGHT_TO_LEFT) > 0 Then
                Return (-1, EQUATION_DIRECTIONS_RIGHT_TO_LEFT)
            ElseIf InStr(eq_str, " <= ") > 0 Then
                Return (-1, " <= ")
            ElseIf InStr(eq_str, " = ") > 0 Then
                Return (0, " = ")
            Else
                Throw New NotImplementedException(eq_str)
            End If
        End Function

        ''' <summary>
        ''' 从代谢过程的表达式字符串值创建代谢过程的对象模型
        ''' </summary>
        ''' <typeparam name="TCompound"></typeparam>
        ''' <typeparam name="TEquation"></typeparam>
        ''' <param name="eqStr"></param>
        ''' <returns></returns>
        <Extension>
        Public Function CreateObject(Of TCompound As ICompoundSpecies, TEquation As IEquation(Of TCompound))(eqStr As String) As TEquation
            With Activator.CreateInstance(Of TEquation)()
                Dim deli = MeasureDelimiter(eq_str:=eqStr)
                Dim tokens As String() = Strings.Split(eqStr, deli.delimiter)

                If tokens.Length < 2 Then
                    Throw New FormatException($"Invalid format text: {eqStr}, it should be in syntax like: left <=> right.")
                End If

                Try
                    .Reversible = deli.direction = 0
                    .Reactants = tokens(left).GetSides(Of TCompound)()
                    .Products = tokens(right).GetSides(Of TCompound)()

                    If deli.direction = -1 Then
                        ' a <- b
                        ' swap list data
                        .Reactants.Swap(.Products)
                    End If
                Catch ex As Exception
                    ' 生成字典的时候可能会因为重复的代谢物而出错
                    Throw New Exception(String.Format(Duplicated, eqStr), ex)
                End Try

                Return .ByRef
            End With
        End Function

        Const left% = Scan0
        Const right = 1

        Const Duplicated As String = "Could not process ""{0}"", duplicated found!"

        Public Function CreateObject(Equation As String) As DefaultTypes.Equation
            Return CreateObject(Of DefaultTypes.CompoundSpecieReference, DefaultTypes.Equation)(Equation)
        End Function

        <Extension>
        Private Function GetSides(Of T As ICompoundSpecies)(expr As String) As T()
            If String.IsNullOrEmpty(expr) Then
                Return New T() {}
            End If

            Dim tokens As String() = Strings.Split(expr, EQUATION_SPECIES_CONNECTOR)
            Dim LQuery As T() = tokens.Select(AddressOf __tryParse(Of T)).ToArray
            Return LQuery
        End Function

        Private Function __tryParse(Of T As ICompoundSpecies)(token As String) As T
            Dim compound As T = Activator.CreateInstance(Of T)()
            Dim SC As String = Regex.Match(token, "^\s*\d+\s*", RegexICMul).Value

            If String.IsNullOrEmpty(SC) Then
                Dim tokens As String() = token.Trim.StringSplit("\s+")

                If tokens.Length > 1 AndAlso tokens(Scan0).IsPattern("\d+(\.\d+)?") Then
                    ' 2018-11-19
                    ' 如果不是ID编号的话，则代谢物名字中间可能会包含有空格
                    ' 所以在这里代谢物名称为tokens跳过第一个数字之后的
                    ' 所有token的链接结果字符串
                    compound.Stoichiometry = Scripting.CTypeDynamic(Of Double)(tokens(Scan0))
                    compound.Key = tokens.Skip(1).JoinBy(" ")
                Else
                    compound.Stoichiometry = 1
                    compound.Key = token.Trim
                End If
            Else
                compound.Stoichiometry = Val(SC.Trim)
                compound.Key = Mid(token, SC.Length + 1).Trim
            End If

            Return compound
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function ToString(GetLeftSide As Func(Of KeyValuePair(Of Double, String)()),
                                 GetRightSide As Func(Of KeyValuePair(Of Double, String)()),
                                 Reversible As Boolean) As String
            Return ToString(GetLeftSide(), GetRightSide(), Reversible)
        End Function

        Public Function ToString(LeftSide As KeyValuePair(Of Double, String)(), RightSide As KeyValuePair(Of Double, String)(), Reversible As Boolean) As String
            Dim sBuilder As New StringBuilder(1024)
            Dim DirectionFlag As String =
                If(Reversible,
                EQUATION_DIRECTIONS_REVERSIBLE,
                EQUATION_DIRECTIONS_INREVERSIBLE)

            Call EquationBuilder.AppendSides(sBuilder, Compounds:=LeftSide)
            Call sBuilder.Append(DirectionFlag)
            Call EquationBuilder.AppendSides(sBuilder, Compounds:=RightSide)

            Return sBuilder.ToString
        End Function

        Private Sub AppendSides(sb As StringBuilder, Compounds As KeyValuePair(Of Double, String)())
            Call Compounds.__appendSide(sb, Function(x) x.Key, Function(x) x.Value)
        End Sub

        Public Function ToString(Of TCompound As ICompoundSpecies)(Equation As IEquation(Of TCompound)) As String
            Dim sBuilder As StringBuilder = New StringBuilder(1024)
            Dim DirectionFlag As String =
                If(Equation.Reversible,
                EQUATION_DIRECTIONS_REVERSIBLE,
                EQUATION_DIRECTIONS_INREVERSIBLE)

            Call EquationBuilder.AppendSides(sBuilder, Compounds:=Equation.Reactants)
            Call sBuilder.Append(DirectionFlag)
            Call EquationBuilder.AppendSides(sBuilder, Compounds:=Equation.Products)

            Return sBuilder.ToString
        End Function

        Public Function ToString(Equation As DefaultTypes.Equation) As String
            Return ToString(Of DefaultTypes.CompoundSpecieReference)(Equation)
        End Function

        <Extension>
        Private Sub __appendSide(Of T)(compounds As IEnumerable(Of T), sb As StringBuilder, getSto As Func(Of T, Double), getId As Func(Of T, String))
            If Not compounds Is Nothing Then
                Dim array$() = LinqAPI.Exec(Of String) _
                                                       _
                    () <= From cp As T
                          In compounds
                          Let sto As Double = getSto(cp)
                          Let id As String = getId(cp)
                          Select If(sto > 1, $"{sto} {id}", id)

                Dim side As String = String.Join(EQUATION_SPECIES_CONNECTOR, array)
                Call sb.Append(side)
            End If
        End Sub

        Private Sub AppendSides(sBuilder As StringBuilder, Compounds As ICompoundSpecies())
            Call Compounds.__appendSide(sBuilder, Function(x) x.Stoichiometry, Function(x) x.Key)
        End Sub
    End Module
End Namespace
