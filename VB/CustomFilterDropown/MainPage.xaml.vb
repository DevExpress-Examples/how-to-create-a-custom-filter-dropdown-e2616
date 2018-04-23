Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DevExpress.Data.Filtering

Namespace CustomFilterDropown
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
			grid.ItemsSource = GridData.GetData()
		End Sub
	End Class

	Public Class GridData
		Public Shared Function GetData() As List(Of DataObject)
			Dim data As New List(Of DataObject)()
			For i As Integer = 0 To 99
				data.Add(New DataObject() With {.Index = i})
			Next i
			Return data
		End Function
	End Class
	Public Class DataObject
		Private privateIndex As Integer
		Public Property Index() As Integer
			Get
				Return privateIndex
			End Get
			Set(ByVal value As Integer)
				privateIndex = value
			End Set
		End Property
	End Class
	Public Class IntToCriteriaOperatorConverter
		Inherits MarkupExtension
		Implements IValueConverter
		Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
			Return Me
		End Function

		#Region "IValueConverter Members"

		Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
			Dim op As BinaryOperator = TryCast(value, BinaryOperator)
			If Object.ReferenceEquals(op, Nothing) Then
				Return Nothing
			End If
			Dim operandValue As OperandValue = TryCast(op.RightOperand, OperandValue)
			Return operandValue.Value
		End Function
		Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
			Return New BinaryOperator("Index", Convert.ToInt32(value), BinaryOperatorType.Greater)
		End Function

		#End Region
	End Class
End Namespace
