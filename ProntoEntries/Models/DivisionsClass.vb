Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
<MetadataType(GetType(Meta_Division))>
Partial Public Class Division

End Class

Partial Public Class Meta_Division
    Public Property DivisionID As Integer
    Public Property Distance As Nullable(Of Decimal)
    Public Property Category As String
    Public Property Description As String
    <DisplayName("Start Time")>
    <DataType(DataType.Time)>
    Public Property StartTime As Nullable(Of Date)
    <DataType(DataType.Currency)>
    <DisplayFormat(DataFormatString:="{0:n2}", ApplyFormatInEditMode:=True)>
    Public Property Price As Nullable(Of Decimal)
    <DisplayName("Event")>
    Public Property RaceID As Nullable(Of Integer)
End Class