Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
<MetadataType(GetType(Meta_RaceEvent))>
Partial Public Class RaceEvent

End Class

Partial Public Class Meta_RaceEvent
    Public Property RaceID As Integer
    <DisplayName("Event Name")>
    Public Property RaceName As String
    <DisplayName("Description")>
    Public Property RaceDescription As String
    <DisplayName("Date")>
    <DataType(DataType.Date)>
    <DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)>
    Public Property RaceDate As Nullable(Of Date)
    <DisplayName("Type")>
    Public Property RaceType As String
    Public Property Coordinates As String
    Public Property Address As String
    Public Property City As String
    Public Property Province As String
    <DisplayName("Admin Charge")>
    Public Property AdminCharge As Nullable(Of Double)
    <DisplayName("Host Organization")>
    Public Property OrgID As Nullable(Of Integer)
    <DisplayName("Race Logo")>
    Public Property Image As String
    Public Property Background As String
    <DisplayName("Race Detail Image - First")>
    Public Property ImgDetail1 As String
    <DisplayName("Race Detail Image - Second")>
    Public Property ImgDetail2 As String
    <DisplayName("Race Detail Image - Third")>
    Public Property ImgDetail3 As String
    <DisplayName("Display Race Classes")>
    Public Property DispClasses As Boolean
    <DisplayName("Display Admin Charge Amount")>
    Public Property DispAdmCharge As Boolean
    <DisplayName("Display Race Address")>
    Public Property DispAddress As Boolean
    <DisplayName("Race Info Page")>
    <AllowHtml>
    <DataType(DataType.Html)>
    Public Property RaceHtmlPage As String
    Public Property Indemnity As String
    <DisplayName("Terms and Conditions")>
    Public Property TandC As String
    <DisplayName("Entry Closes")>
    <DataType(DataType.Date)>
    <DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)>
    Public Property EntriesCloseDate As Nullable(Of Date)

End Class
