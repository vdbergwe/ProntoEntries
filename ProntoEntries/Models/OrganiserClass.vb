Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
<MetadataType(GetType(Meta_Organiser))>
Partial Public Class Organiser

End Class

Partial Public Class Meta_Organiser
    Public Property OrgID As Integer
    <DisplayName("Name")>
    Public Property OrgName As String
    <DisplayName("Email")>
    <DataType(DataType.EmailAddress)>
    Public Property OrgEmail As String
    <DisplayName("Phone")>
    <DataType(DataType.PhoneNumber)>
    Public Property OrgTel As String
    <DisplayName("Website")>
    <DataType(DataType.Url)>
    Public Property OrgWebsite As String
    <DisplayName("Vat Number")>
    Public Property OrgVatNumber As String
    Public Property AdminUserID As Nullable(Of Integer)
    <DisplayName("Logo")>
    Public Property Image As String

End Class