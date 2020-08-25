Public Class modParser
    Protected mFunction As String
    Protected mX, mY, mZ As Decimal
    Protected mFunctionIsCorrect As Boolean
    Protected mErrorAt As Integer
    Protected mResultIsCorrect As Boolean
    Protected mErrorDescription As String

    Dim MulSet As String
    Dim AddSet As String
    Dim OprSet As String
    Dim NumSet As String
    Dim VarSet As String
    Dim ConSet As String
    Dim FunSet As String

    Dim CurrentPos As Integer
    Dim CurrentChar As Char

    Dim FnTree As Object

    Public Sub New()
        MulSet = "*/"
        AddSet = "+-"
        OprSet = MulSet & AddSet & "^"
        Dim Nfi As System.Globalization.NumberFormatInfo = System.Globalization.CultureInfo.InstalledUICulture.NumberFormat
        NumSet = Nfi.NumberDecimalSeparator
        Nfi = Nothing
        NumSet &= "0123456789"
        VarSet = "XYZ"
        ConSet = "P,E"  'Math.Pi, Math.E
        FunSet = "sin,cos,tan,atn,sqr,lne,log" 'lne=neperian,log=decimal
        mFunction = ""
        mErrorAt = -1
        mX = 0
        mY = 0
        mZ = 0
    End Sub

    'The function in terms of x,y,z
    WriteOnly Property [Function]() As String
        Set(ByVal Value As String)
            mFunction = Value
        End Set
    End Property

    'X variable
    Property X() As Decimal
        Get
            Return mX
        End Get
        Set(ByVal Value As Decimal)
            mX = Value
        End Set
    End Property

    'Y variable
    Property Y() As Decimal
        Get
            Return mY
        End Get
        Set(ByVal Value As Decimal)
            mY = Value
        End Set
    End Property

    'Z variable
    Property Z() As Decimal
        Get
            Return mZ
        End Get
        Set(ByVal Value As Decimal)
            mZ = Value
        End Set
    End Property

    'True if the function is correctly written
    ReadOnly Property FunctionIsCorrect() As Boolean
        Get
            Return mFunctionIsCorrect
        End Get
    End Property

    'True if it's possible to calculate the result
    ReadOnly Property ResultIsCorrect() As Boolean
        Get
            Return mResultIsCorrect
        End Get
    End Property

    'Position in the function text where an error has occourred
    ReadOnly Property ErrorAt() As Integer
        Get
            Return mErrorAt
        End Get
    End Property

    ReadOnly Property ErrorDescription() As String
        Get
            Return mErrorDescription
        End Get
    End Property

    'Builds the tree which represents the function
    Public Overridable Sub BuildFunctionTree()
        FnTree = Nothing
        mFunctionIsCorrect = False
        CurrentPos = -1
        CurrentChar = ""

        mFunction &= "#"
        CurrentPos = -1

        Try
            FnTree = Expression()
            If CurrentChar <> "#" Then
                mErrorAt = CurrentPos + 1
                mFunctionIsCorrect = False
            Else
                mFunctionIsCorrect = True
                mErrorAt = -1
            End If
        Catch ex As Exception
            mErrorAt = CurrentPos + 1
            mFunctionIsCorrect = False
            mErrorDescription = ex.Message
        End Try

        If FnTree Is Nothing Or CurrentChar <> "#" Then
            mErrorAt = CurrentPos + 1
            mFunctionIsCorrect = False
            mErrorDescription = "Syntax Error"
        End If

        mFunction = mFunction.Replace("#", "")
    End Sub

    'Returns the value of the function
    Public Overridable Function Result() As Decimal
        If FnTree Is Nothing Then BuildFunctionTree()

        If mFunctionIsCorrect Then
            Try
                mErrorDescription = ""
                mResultIsCorrect = True
                Return GetResult(FnTree)
            Catch ex As Exception
                mErrorDescription = ex.Message
                mResultIsCorrect = False
                Return 0
            End Try
        Else
            mResultIsCorrect = False
            Return 0
        End If
    End Function

    Private Sub GetChar()
        'gets the next character from the function to be parsed
        Do
            CurrentPos += 1
            If CurrentPos > mFunction.Length - 1 Then
                CurrentChar = "$"
            Else
                CurrentChar = mFunction.Chars(CurrentPos)
            End If
        Loop Until CurrentChar <> " "
    End Sub

    Private Function isFunction(ByVal s As String) As Boolean
        Return InStr(FunSet, s, CompareMethod.Text) > 0
    End Function

    Protected Overridable Function Expression() As Object
        'returns an object which represents a function "tree"
        'an expression is always "something left", an operator, "something right"
        'e.g. x + 5
        'if there's no operator then there'll be the left side only
        'e.g. x
        Dim Left As Object = Nothing, Right As Object = Nothing, Tree As Object = Nothing

        GetChar()

        If AddSet.IndexOf(CurrentChar) >= 0 Then
            If CurrentPos = 0 Then
                mFunction = "0" & mFunction
            Else
                mFunction = mFunction.Substring(0, CurrentPos) & "0" & mFunction.Substring(CurrentPos)
            End If
            CurrentChar = "0"
        End If

        'evaluate the hierarchy of the operations
        'from bottom to top so the tree will be builded in the
        'correct order (),sin|cos....,^,*/,+-

        'here the add|sub operators

        Left = Term()

        Do While AddSet.IndexOf(CurrentChar) >= 0
            Mkn_Operator(CurrentChar, Tree)
            GetChar()
            Right = Term()
            Left = CreateSubTree(Tree, Left, Right)
        Loop

        Return Left
    End Function

    Protected Overridable Function Term() As Object
        Dim Left As Object = Nothing, Right As Object = Nothing, Tree As Object = Nothing

        'each side of an addition|subtraction
        'could be a multiplication|division

        Left = Factor()

        Do While MulSet.IndexOf(CurrentChar) >= 0
            Mkn_Operator(CurrentChar, Tree)
            GetChar()
            Right = Factor()
            Left = CreateSubTree(Tree, Left, Right)
        Loop

        Return Left
    End Function

    Protected Overridable Function Factor() As Object
        Dim Left As Object = Nothing, Right As Object = Nothing, Tree As Object = Nothing

        'each side of a multiplication|division
        'could be a power elevation

        Left = Power()

        Do While CurrentChar = "^"
            Mkn_Operator(CurrentChar, Tree)
            GetChar()
            Right = Power()
            Left = CreateSubTree(Tree, Left, Right)
        Loop

        Return Left
    End Function

    Protected Overridable Function Power() As Object
        Dim Pow As Object = Nothing
        Dim s As String

        If CurrentChar = "(" Then
            'open bracket --> recursively build the expression 
            Power = Expression()
            If CurrentChar <> ")" Then
                'no closing bracket --> error
                CurrentChar = "$"
            Else
                GetChar()
            End If
            Exit Function
        End If

        'check if the next three chars are a supported function
        'such as sin,cos,...
        'then build a node which keeps the name and a "pointer"
        'to the argument which is an expression noless!!!
        If CurrentPos >= 0 AndAlso CurrentPos <= mFunction.Length - 4 AndAlso isFunction(mFunction.Substring(CurrentPos, 3).ToLower) Then
            Mkn_Function(mFunction.Substring(CurrentPos, 3), Pow)
            CurrentPos += 2
            GetChar()

            If CurrentChar <> "(" Then
                CurrentChar = "$"
                Return Nothing
            Else
                Pow.Argument = Expression()
                If CurrentChar <> ")" Then
                    CurrentChar = "$"
                    Return Nothing
                Else
                    GetChar()
                    Return Pow
                End If
            End If
        End If

        If VarSet.IndexOf(CurrentChar.ToString.ToUpper) >= 0 Then
            'any of the variables
            Mkn_Operand(CurrentChar, Pow)
        ElseIf ConSet.IndexOf(CurrentChar.ToString.ToUpper) >= 0 Then
            'any of the constants
            Mkn_Operand(CurrentChar, Pow)
        Else
            'finally... must be a number
            s = mFunction.Chars(CurrentPos)
            GetChar()
            Do While NumSet.IndexOf(CurrentChar) >= 0
                s &= mFunction.Chars(CurrentPos)
                GetChar()
            Loop
            CurrentPos -= 1
            Mkn_Operand(s, Pow)
        End If

        Power = Pow
        If CurrentChar <> "$" Then GetChar()
    End Function

    Private Function CreateSubTree(ByVal objNode As Object, ByVal LeftSide As Object, ByVal RightSide As Object) As Object
        objNode.left = LeftSide
        objNode.right = RightSide
        Return objNode
    End Function

    Private Sub Mkn_Function(ByVal Funct As String, ByRef objNode As clsFunction)
        objNode = New clsFunction
        objNode.Name = Funct
    End Sub

    Private Sub Mkn_Operator(ByVal xval As String, ByRef objNode As clsOperator)
        objNode = New clsOperator
        If OprSet.IndexOf(xval) >= 0 Then
            objNode.[Operator] = xval
            objNode.Left = Nothing
            objNode.Right = Nothing
        End If
    End Sub

    Private Sub Mkn_Operand(ByVal s As String, ByRef objNode As clsOperand)
        objNode = New clsOperand
        If VarSet.IndexOf(s.ToUpper) >= 0 Then
            'variable
            objNode.IsVariable = True
            objNode.Variable = s.ToUpper
        ElseIf ConSet.IndexOf(s.ToUpper) >= 0 Then
            'const
            objNode.IsVariable = False
            Select Case s.ToUpper
                Case "P"
                    objNode.Operand = System.Math.PI
                Case "E"
                    objNode.Operand = System.Math.E
            End Select
        Else
            'number
            objNode.IsVariable = False
            objNode.Operand = s
        End If
    End Sub

    Protected Overridable Function GetResult(ByVal objNode As Object) As Decimal
        'calculate the result of the function in terms of x,y,z
        'by recursively scanning the tree from left to right

        If objNode Is Nothing Then Return 0

        If TypeOf objNode Is clsFunction Then
            Select Case objNode.Name.ToLower
                Case "sin"
                    Return System.Math.Sin(GetResult(objNode.Argument))
                Case "cos"
                    Return System.Math.Cos(GetResult(objNode.Argument))
                Case "tan"
                    Return System.Math.Tan(GetResult(objNode.Argument))
                Case "atn"
                    Return System.Math.Atan(GetResult(objNode.Argument))
                Case "lne"
                    Return System.Math.Log(GetResult(objNode.Argument))
                Case "log"
                    Return System.Math.Log10(GetResult(objNode.Argument))
                Case "sqr"
                    Return System.Math.Sqrt(GetResult(objNode.Argument))
            End Select
        ElseIf TypeOf objNode Is clsOperator Then
            Select Case objNode.[operator]
                Case "+"
                    Return GetResult(objNode.Left) + GetResult(objNode.Right)
                Case "-"
                    Return GetResult(objNode.Left) - GetResult(objNode.Right)
                Case "*"
                    Return GetResult(objNode.Left) * GetResult(objNode.Right)
                Case "/"
                    Return GetResult(objNode.Left) / GetResult(objNode.Right)
                Case "^"
                    Return GetResult(objNode.Left) ^ GetResult(objNode.Right)
                Case "%"
                    Return GetResult(objNode.Left) * GetResult(objNode.Right)
            End Select
        ElseIf objNode.isvariable Then
            Select Case objNode.variable
                Case "X"
                    Return mX
                Case "Y"
                    Return mY
                Case "Z"
                    Return mZ
            End Select
        Else
            Return objNode.operand
        End If
    End Function

    Private Class clsFunction
        Protected mName As String
        Protected mArgument As Object
        Public Sub New()
            mName = ""
            mArgument = Nothing
        End Sub
        Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal Value As String)
                mName = Value
            End Set
        End Property
        Property Argument() As Object
            Get
                Return mArgument
            End Get
            Set(ByVal Value As Object)
                mArgument = Value
            End Set
        End Property
    End Class

    Private Class clsOperator
        Protected mOperator As String
        Protected mLeft As Object
        Protected mRight As Object
        Public Sub New()
            mLeft = Nothing
            mRight = Nothing
        End Sub
        Property [Operator]() As String
            Get
                Return mOperator
            End Get
            Set(ByVal Value As String)
                mOperator = Value
            End Set
        End Property
        Property Left() As Object
            Get
                Return mLeft
            End Get
            Set(ByVal Value As Object)
                mLeft = Value
            End Set
        End Property
        Property Right() As Object
            Get
                Return mRight
            End Get
            Set(ByVal Value As Object)
                mRight = Value
            End Set
        End Property
    End Class

    Private Class clsOperand
        Protected mIsVariable As Boolean
        Protected mVariable As String
        Protected mOperand As Decimal
        Property IsVariable() As Boolean
            Get
                Return mIsVariable
            End Get
            Set(ByVal Value As Boolean)
                mIsVariable = Value
            End Set
        End Property
        Property Variable() As String
            Get
                Return mVariable
            End Get
            Set(ByVal Value As String)
                mVariable = Value
            End Set
        End Property
        Property Operand() As Decimal
            Get
                Return mOperand
            End Get
            Set(ByVal Value As Decimal)
                mOperand = Value
            End Set
        End Property
    End Class
End Class
