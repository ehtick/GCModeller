﻿#Region "Microsoft.VisualBasic::f8b58d78574159fd85b552ec513c7ad8, Data_science\Graph\Analysis\MorganFingerprint\MorganFingerprint.vb"

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

    '   Total Lines: 158
    '    Code Lines: 53 (33.54%)
    ' Comment Lines: 87 (55.06%)
    '    - Xml Docs: 74.71%
    ' 
    '   Blank Lines: 18 (11.39%)
    '     File Size: 8.49 KB


    '     Class GraphMorganFingerprint
    ' 
    '         Properties: FingerprintLength
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: CalculateFingerprint, CalculateFingerprintCheckSum, HashLabelKey
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.GraphTheory.Network
Imports Microsoft.VisualBasic.Math.HashMaps

Namespace Analysis.MorganFingerprint

    ''' <summary>
    ''' Morgan fingerprints, also known as circular fingerprints, are a type of molecular fingerprint 
    ''' used in cheminformatics to represent the structure of chemical compounds. The algorithm steps 
    ''' for generating Morgan fingerprints are as follows:
    ''' 
    ''' 1. **Initialization**:
    '''  - Start with the initial set of atoms in the molecule.
    '''  - Assign a unique identifier (e.g., integer) to each atom.
    '''  
    ''' 2. **Atom Environment Encoding**:
    '''  - For each atom, encode its immediate environment, which includes the atom type and the types of its directly connected neighbors.
    '''  - This information can be represented as a string or a hash.
    '''  
    ''' 3. **Iterative Expansion**:
    '''  - Expand the environment encoding iteratively to include atoms further away from the starting atom.
    '''  - In each iteration, update the encoding to include the types of atoms that are two, three, etc., bonds away from the starting atom.
    '''  
    ''' 4. **Hashing**:
    '''  - Convert the environment encoding into a fixed-size integer using a hashing function. This integer represents the fingerprint of the atom's environment.
    '''  - Different atoms in the molecule will have different fingerprints based on their environments.
    '''  
    ''' 5. **Circular Fingerprint Generation**:
    '''  - For each atom, generate a series of fingerprints that represent its environment at different radii (number of bonds away).
    '''  - The final fingerprint for an atom is a combination of these series of fingerprints.
    '''  
    ''' 6. **Molecular Fingerprint**:
    '''  - Combine the fingerprints of all atoms in the molecule to create the final molecular fingerprint.
    '''  - This can be done by taking the bitwise OR of all atom fingerprints, resulting in a single fingerprint that represents the entire molecule.
    '''  
    ''' 7. **Optional Folding**:
    '''  - To reduce the size of the fingerprint, an optional folding step can be applied. This involves 
    '''    dividing the fingerprint into chunks and performing a bitwise XOR operation within each chunk.
    '''    
    ''' 8. **Result**:
    '''  - The final result is a binary vector (or a list of integers) that represents the Morgan fingerprint 
    '''    of the molecule. This fingerprint can be used for similarity searching, clustering, and other 
    '''    cheminformatics tasks.
    '''    
    ''' Morgan fingerprints are particularly useful because they capture the circular nature of molecular
    ''' environments, meaning that the path taken to reach an atom is not as important as the environment 
    ''' around it. This makes them effective for comparing the similarity of molecules based on their 
    ''' structural features.
    ''' </summary>
    Public MustInherit Class GraphMorganFingerprint(Of V As IMorganAtom, E As IndexEdge)

        ''' <summary>
        ''' the size of the fingerprint data
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property FingerprintLength As Integer = 4096

        Sub New(size As Integer)
            FingerprintLength = size
        End Sub

        Public Function CalculateFingerprintCheckSum(Of G As MorganGraph(Of V, E))(struct As G, Optional radius As Integer = 3) As Byte()
            Dim bits As BitArray = CalculateFingerprint(struct, radius)
            Dim bytes = New Byte(FingerprintLength / 8 - 1) {}
            bits.CopyTo(bytes, 0)
            Return bytes
        End Function

        Public Function CalculateFingerprint(Of G As MorganGraph(Of V, E))(struct As G, Optional radius As Integer = 3) As BitArray
            Dim atoms As V() = struct.Atoms

            ' Initialize atom codes based on atom type
            For i As Integer = 0 To struct.Atoms.Length - 1
                atoms(i).Code = CULng(HashAtom(struct.Atoms(i)))
                atoms(i).Index = i
            Next

            ' Perform iterations to expand the atom codes
            For r As Integer = 0 To radius - 1
                Dim newCodes As ULong() = New ULong(struct.Atoms.Length - 1) {}

                For Each bound As E In struct.Graph
                    newCodes(bound.U) = HashEdge(atoms, bound, flip:=False)
                    newCodes(bound.V) = HashEdge(atoms, bound, flip:=True)
                Next

                For i As Integer = 0 To struct.Atoms.Length - 1
                    atoms(i).Code = newCodes(i)
                Next
            Next

            ' Generate the final fingerprint
            Dim fingerprint As New BitArray(FingerprintLength)

            For Each atom As IMorganAtom In atoms
                Call fingerprint.Xor(position:=atom.Code Mod FingerprintLength)
            Next

            Return fingerprint
        End Function

        Protected MustOverride Function HashAtom(v As V) As ULong
        Protected MustOverride Function HashEdge(atoms As V(), e As E, flip As Boolean) As ULong

        ''' <summary>
        ''' A helper function for create hashcode of the string label in the graph
        ''' </summary>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 在 .NET 中，字符串的 `GetHashCode()` 方法返回的哈希值**并不能保证在不同时间、操作系统或硬件上保持一致**。以下是具体原因和背景分析：
        '''
        ''' ---
        ''' 
        ''' ### 1. **实现依赖性与版本差异**
        ''' - **不同 .NET 版本可能生成不同的哈希值**  
        '''   微软明确声明，`GetHashCode()` 的实现可能因公共语言运行时（CLR）的版本变化而调整，例如从 .NET Framework 4.x 到 .NET Core 或 .NET 5+ 的升级。这种调整可能是出于性能优化或哈希分布均匀性的考虑。
        '''   
        ''' - **哈希随机化（.NET Core 2.1+）**  
        '''   从 .NET Core 2.1 开始，默认启用了哈希随机化机制，即使同一程序在不同时间运行，同一字符串的哈希值也可能不同。此设计旨在防止哈希碰撞攻击，增强安全性。
        ''' 
        ''' ---
        ''' 
        ''' ### 2. **跨操作系统与硬件的差异**
        ''' - **操作系统的影响**  
        '''   .NET 的不同运行时（如 .NET Framework 仅支持 Windows，而 .NET Core 支持跨平台）在实现哈希算法时可能采用不同策略。例如，Windows 和 Linux 上的哈希计算结果可能不一致。
        ''' 
        ''' - **硬件架构的差异**  
        '''   32 位与 64 位系统的内存寻址方式不同，可能影响 `GetHashCode()` 的默认行为（如对象地址计算）。此外，CPU 架构（x86/x64/ARM）也可能导致哈希值差异。
        ''' 
        ''' ---
        ''' 
        ''' ### 3. **设计原则与使用场景限制**
        ''' - **仅保证同一进程内的唯一性**  
        '''   `GetHashCode()` 的主要设计目标是支持哈希表等数据结构的高效查找，其核心保证是：**在同一进程的同一执行周期内，相同内容的字符串返回相同的哈希值**。但跨进程、跨机器或持久化存储时，这一保证失效。
        ''' 
        ''' - **哈希冲突的可能性**  
        '''   即使在同一环境中，不同字符串可能生成相同的哈希值（哈希碰撞）。例如，字符串 `"FB"` 和 `"Ea"` 在某些情况下哈希值相同。
        ''' 
        ''' ---
        ''' </remarks>
        Public Shared Function HashLabelKey(key As String) As ULong
            Static hashcodes As New Dictionary(Of String, ULong)

            Return hashcodes.ComputeIfAbsent(
                key,
                lazyValue:=Function(k)
                               Dim hashcode As ULong = 0

                               For Each c As Char In k
                                   hashcode = HashMap.HashCodePair(hashcode, CULng(Asc(c)))
                               Next

                               Return hashcode
                           End Function)
        End Function
    End Class
End Namespace
